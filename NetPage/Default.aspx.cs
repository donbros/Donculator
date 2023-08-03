// Domantas Bronušas 2021

// vykdome imdami visą string'ą
// HiddenField5 - "(" counter
// HiddenField6 - ar trinti duomenis užkraunant puslapį ?
// HiddenField7 - history things
// HiddenField8 - database features
// HiddenField9 - apsauga, kad nevestų skaičiaus prie skliausto
// HiddenField10 - ženklams
// HiddenField11 - minusui
// HiddenField - "(" counter
// TextBox1 - pirmasis laukas duomenų įvedimui
// TextBox2 - operacijų eilutė
// TextBox3 - antrasis laukas duomenų įvedimui

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using Extreme.Mathematics.Curves;
using System.Text.RegularExpressions;

namespace Calculator
{

    public partial class _Default : Page
    {
        // susikuriam klasės instancus
        readonly public EquationSolverClass equationSolverInstance = new EquationSolverClass();
        readonly public EquationSolverClass generalClassInstance = new EquationSolverClass(); // perteklinis

        SqlConnection con;/* = new SqlConnection(@"Server=NET138\SQLEXPRESS;Database=CalculatorDatabase; Trusted_Connection=Yes; Max Pool Size=200");*/

        bool EqualsClear; // tik tarpiniems duomenims dėl aiškumo, pagrinde naudojame hiddenFields
        bool databaseOnline;

        // ****************************************************************************
        // ****************************** FORM METHODS ********************************
        // ****************************************************************************

        // ****************************************************************************
        // **************************** COMPLEX BUTTONS *******************************
        // ****************************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // išsireiškiame con
            con = generalClassInstance.con;

            databaseOnline = Convert.ToBoolean(HiddenField8.Value);
            if (databaseOnline)
            {
                generalClassInstance.ConnectionState();
                if (!Page.IsPostBack)
                {
                    CreateAccordianUsingRepeater();
                }
                ReloadHistory();
            }

            // tikriname ar laikas "pravalyti"
            EqualsClear = Convert.ToBoolean(HiddenField6.Value);
            if (EqualsClear)
                ClearAfterEquals(out EqualsClear);
        }

        // "="
        protected void Button2_Click(object sender, EventArgs e)
        {
            bool foundX = VariableExist(TextBox1.Text) || VariableExist(TextBox3.Text);

            if (!TextBox1.Text.Equals("")
                && int.Parse(BracketsControlField.Value) == 0
                && int.Parse(BracketsControlField2.Value) == 0
                )
            {
                if (!foundX
                    && (TextBox3.Text == "")
                    && (bool.Parse(EqualsLegitField.Value))
                    )
                {
                    TextBox4.Text = "veiksmai su paprastais skaičias";

                    //var parent = (UnbracketClass)equationSolverInstance;
                    //var parent2 = (SimpleArithmeticClass)parent;

                    TextBox2.Text = equationSolverInstance.RemoveBrackets(TextBox1.Text);

                    databaseOnline = Convert.ToBoolean(HiddenField8.Value); // tikriname ar įjungtas duomenų įkėlimo į duomenų bazę punktas

                    if (databaseOnline)
                    {
                        InsertValuesToDatabase(FormatValue(TextBox1.Text), TextBox2.Text); // pakeičiau textbox2 i textbox1 kad ikeltų
                        ReloadHistory();
                    }

                    // trynimo funkcijai, kad trinant ir visos validacijos paeitų vienu punktu atgal
                    // ChangeLastHiddenValue();
                    // ClearReady(); // ištrina duomenis
                }
                else if ((foundX)
                        && (TextBox3.Text != "")
                        && (bool.Parse(EqualsLegitField.Value))
                        && (bool.Parse(EqualsLegitField2.Value))
                        )
                {
                    TextBox4.Text = "veiksmai su x - lygtis";

                    TextBox2.Text = "";
                    TextBox4.Text = "";

                    equationSolverInstance.SolveEquation(TextBox1.Text, TextBox3.Text, true, out string metaTextToPut, out string resultTextToPut, out double[] roots);

                    TextBox2.Text = resultTextToPut;
                    TextBox4.Text = metaTextToPut;

                    if (databaseOnline)
                    {
                        InsertValuesToDatabase(TextBox1.Text + "=" + TextBox3.Text, TextBox2.Text); // pakeičiau textbox2 i textbox1 kad ikeltų
                        ReloadHistory();
                    }
                }
                else if (foundX
                        && (TextBox3.Text == "")
                        && (bool.Parse(EqualsLegitField.Value))
                        && (bool.Parse(EqualsLegitField2.Value))
                        )
                {
                    TextBox4.Text = "veiksmai su x - grafikas";
                    // standartiniai
                    double start = -20;
                    double end = 20;
                    double step = 1;

                    if (start < end)
                    {
                        if (double.TryParse(TextBoxIntervalStart.Text, out double start_))
                        {
                            start = start_;
                        }
                        if (double.TryParse(TextBoxIntervalEnd.Text, out double end_))
                        {
                            end = end_;
                        }
                    }
                    else
                    {
                        TextBox4.Text = "intervalo pradžia turi būti mažesnė už pabaigą.";
                    }
                    if (double.TryParse(TextBoxIntervalStep.Text, out double step_))
                    {
                        step = step_;
                    }
                    //else
                    //{
                    //    step = Math.Abs(end - start);
                    //}
                    DrawGraph(start, end, step);
                }
                else
                {
                    TextBox2.Text = "";
                    TextBox4.Text = "Pasitikrinkite lygybes";
                }
            }
            else // jei nepradėti veiksmai (idėjau, nes be šito leidžia spaminti "0", kas bereikalingai apkrauna duomenų bazę)
            {
                TextBox4.Text = "Netikėta klaida";
            }
        }

        // Sudetis "+" paprastoje daugyboje / dalyboje be skliaustu + ir - visuomet cancelina "*" ar "/"
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Addition.Value)
                    )
                {
                    DataFilling("+");
                    ValidationTableAddition();
                }
            }
            else
            {
                if (bool.Parse(Addition2.Value)
                    )
                {
                    DataFilling("+");
                    ValidationTableAddition();
                }
            }
        }

        // Atimtis "-"
        // tas pats kas sudėtis, tačiau leidžiama rašyti po "*" ir "/"
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Subtraction.Value)
                )
                {
                    DataFilling("-");
                    ValidationTableSubtraction();
                }
            }
            else
            {
                if (bool.Parse(Subtraction2.Value)
                    )
                {
                    DataFilling("-");
                    ValidationTableSubtraction();
                }
            }
        }

        // Daugyba "*"
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(MultiplicationDivision.Value)
                )
                {
                    DataFilling("*");
                    ValidationTableMultiplicationDivision();
                }
            }
            else
            {
                if (bool.Parse(MultiplicationDivision2.Value)
                )
                {
                    DataFilling("*");
                    ValidationTableMultiplicationDivision();
                }
            }
        }

        // Dalyba "/"
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(MultiplicationDivision.Value)
                )
                {
                    DataFilling("/");
                    ValidationTableMultiplicationDivision();
                }
            }
            else
            {
                if (bool.Parse(MultiplicationDivision2.Value)
                )
                {
                    DataFilling("/");
                    ValidationTableMultiplicationDivision();
                }
            }
        }

        // ****************************************************************************
        // ***************************** SIMPLE BUTTONS *******************************
        // ****************************************************************************

        // "1"
        protected void Button11_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("1");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("1");
                    ValidationTableNumbers();
                }
            }
        }
        //"2"
        protected void Button12_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("2");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("2");
                    ValidationTableNumbers();
                }
            }
        }
        // "3"
        protected void Button13_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("3");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("3");
                    ValidationTableNumbers();
                }
            }
        }
        // "4"
        protected void Button14_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("4");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("4");
                    ValidationTableNumbers();
                }
            }
        }
        // "5"
        protected void Button15_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("5");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("5");
                    ValidationTableNumbers();
                }
            }
        }
        // "6"
        protected void Button16_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
            )
                {
                    DataFilling("6");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
            )
                {
                    DataFilling("6");
                    ValidationTableNumbers();
                }
            }
        }
        // "7"
        protected void Button17_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("7");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("7");
                    ValidationTableNumbers();
                }
            }
        }
        // "8"
        protected void Button18_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
            )
                {
                    DataFilling("8");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
            )
                {
                    DataFilling("8");
                    ValidationTableNumbers();
                }
            }
        }
        // "9"
        protected void Button19_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
            )
                {
                    DataFilling("9");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
            )
                {
                    DataFilling("9");
                    ValidationTableNumbers();
                }
            }
        }
        // "0"
        protected void Button20_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
            )
                {
                    DataFilling("0");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
            )
                {
                    DataFilling("0");
                    ValidationTableNumbers();
                }
            }
        }
        // "00"
        protected void Button24_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Number.Value)
                )
                {
                    DataFilling("00");
                    ValidationTableNumbers();
                }
            }
            else
            {
                if (bool.Parse(Number2.Value)
                )
                {
                    DataFilling("00");
                    ValidationTableNumbers();
                }
            }
        }
        // "."/","
        protected void Button21_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(Comma.Value)
                && bool.Parse(CommaLegitField.Value)
                )
                {
                    DataFilling(",");
                    ValidationTableDot();
                }
            }
            else
            {
                if (bool.Parse(Comma2.Value)
                && bool.Parse(CommaLegitField2.Value)
                )
                {
                    DataFilling(",");
                    ValidationTableDot();
                }
            }
        }
        // "("
        protected void ParenthesisOpen_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(OpenParenthesis.Value)
                )
                {
                    DataFilling("(");
                    ValidationTableParenthesisOpen();
                }
            }
            else
            {
                if (bool.Parse(OpenParenthesis2.Value)
                )
                {
                    DataFilling("(");
                    ValidationTableParenthesisOpen();
                }
            }
        }
        // ")"
        protected void ParenthesisClose_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(CloseParenthesis.Value)
                && (int.Parse(BracketsControlField.Value) > 0)
                )
                {
                    DataFilling(")");
                    ValidationTableParenthesisClose();
                }
            }
            else
            {
                if (bool.Parse(CloseParenthesis2.Value)
                && (int.Parse(BracketsControlField2.Value) > 0)
                )
                {
                    DataFilling(")");
                    ValidationTableParenthesisClose();
                }
            }
        }
        // "x"
        protected void UnknownVariable_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(SwitchToOther.Value))
            {
                if (bool.Parse(UnknownVar.Value))
                {
                    DataFilling("x");
                    ValidationUnknownValue();
                }
            }
            else
            {
                if (bool.Parse(UnknownVar2.Value))
                {
                    DataFilling("x");
                    ValidationUnknownValue();
                }
            }
        }
        // 'C' - Išvalymo mygtukas
        protected void Button1_Click(object sender, EventArgs e)
        {
            ClearResults();
            ResetHiddenValues();
        }
        // "HIS" - rodoma visa skaičiavimų istorija
        protected void Button7_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(HiddenField7.Value))
            {
                HistoryShow(true);
            }
            else
            {
                HistoryShow(false);
            }
        }
        // "<-" masyvo paskutinio duomens trynėjas (nerealizuotas)
        protected void Button23_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1, 1);
            }
        }
        // Duomenų bazių prisijngimo įjungimas/išjungimas (reikalingas kai skaičiuotuvas naudojame "offline" režime t.y. šiuo atveju ne ant to kompiuterio, kuriame yra duombazė
        protected void ButtonDatabase_Click(object sender, EventArgs e)
        {
            if (!bool.Parse(HiddenField8.Value))
            {
                OnlineFeatures(true);
            }
            else
            {
                OnlineFeatures(false);
            }
        }
        // pakeičia eilutę į kurią rašomas tekstas
        // (reikia padaryti jog ir į skritingus validation table įrašinėtų)
        protected void SwitchTextBoxButton_Click(object sender, EventArgs e)
        {
            SwitchToOther.Value = (!Convert.ToBoolean(SwitchToOther.Value)).ToString();
            if (Convert.ToBoolean(SwitchToOther.Value))
            {
                SwitchTextBoxButton.Text = "Line 2";
                // turn on validation table nr 1
            }
            else
            {
                SwitchTextBoxButton.Text = "Line 1";
                // turn on validation table nr 2
            }
        }

        // **********************************************************************************
        // ************************** Grafiko Braižymo Metodai ******************************
        // **********************************************************************************

        /// <summary>
        /// nupaišo grafiką (tiesiog replacina reikšmes ir naudoja paprastą algoritmą)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step">tikslumas</param>
        private void DrawGraph(double start, double end, double stepDouble)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("X", typeof(float)), new DataColumn("Y", typeof(float)) });
            
            // užtikrina, jog grafikas atrodytų begalinis
            Chart1.ChartAreas[0].AxisX.Minimum = start;
            Chart1.ChartAreas[0].AxisX.Maximum = end;

            Chart1.ChartAreas[0].AxisY.Minimum = start;
            Chart1.ChartAreas[0].AxisY.Maximum = end;

            //Chart1.ChartAreas[0].AxisX.Interval = Math.Abs(end - start) * 0.1;
            //Chart1.ChartAreas[0].AxisY.Interval = Math.Abs(end-start)*0.1;

            // galėtume kiekvienam step duoti vieną intervalą bet kuomet intervalas didelis atsiranda per daug linijų ir grafikas tampa
            // sunkiai įskaitomu
            double chartXLength = Math.Abs(end - start);
            // užtikrina, jog visada bus tik 10 linijų
            int numberOfLines = 10;
            double intervalDouble = (double) chartXLength / numberOfLines;
            Chart1.ChartAreas[0].AxisX.Interval = intervalDouble;
            Chart1.ChartAreas[0].AxisY.Interval = intervalDouble;

            TextBox2.Text = "";
            TextBox4.Text = "";

            string text = TextBox1.Text;

            // text = equationSolverInstance.SolveEquation(TextBox1.Text,"0", false, out string metaTextToPut, out string resultTextToPut, out double[] roots);

            // TextBox2.Text = resultTextToPut;
            // TextBox4.Text = metaTextToPut;

            bool allDataInfinity = true;

            bool NaN = false;

            decimal step = Convert.ToDecimal(stepDouble);
            decimal interval= Convert.ToDecimal(intervalDouble);

            // step grindys ir lubos
            // grindys
            if (step > interval)
            {
                step = step * interval / 20;
            }
            // lubos
            else if (100 <= interval)
            {
                step = step * interval / 20;
            }

            // step lubos (jei jas viršija grafikas nematomas pasidaro)
            if ((decimal) chartXLength/10 < step)
            {
                decimal timesWhole = step / (decimal)(chartXLength / 10);
                decimal timesFraction = step % (decimal) (chartXLength / 10);
                decimal times = timesWhole - timesFraction;
                step = step / times;
            }

            // reik step grindų (tikslumui mažinti)
            if (10000 < (double) chartXLength/ (double) step)
            {
                step = 0.01m;
            }
            for (decimal i = (decimal) start; i < (decimal) end; i += step)
            {
                // skaiciai be x daro pakelima y asimi
                    string textWithValue = InsertValue(i, text);
                    string values = equationSolverInstance.RemoveBrackets(textWithValue); // šituos realiai ir reiktų kišt į grafiko y ašį
                                                                                          // šitas veikia tik tuo atveju, jeigu naudojami double
                    if (double.IsInfinity(double.Parse(values))) // nevaizduotų begalybės
                    {
                        values = "1000000000000000000";
                    }
                    else if (double.IsNaN(double.Parse(values)))
                    {
                        NaN = true;
                        break;
                    }
                    else
                    {
                        allDataInfinity = false;
                    }
                    dt.Rows.Add(i, values);
                    TextBox2.Text += values + " ";
            }
            if(NaN)
            {
                TextBox2.Text = "NaN";
            }
            else if (allDataInfinity)
            {
                TextBox2.Text = "Graph is all infinity";
            }
            else
            {
                Chart1.DataSource = dt;
                Chart1.DataBind();
            }
        }

        // **********************************************************************************
        // ***************************** Validacijos Metodai ********************************
        // **********************************************************************************

        /// <summary>
        /// backspace skirtas
        /// dar nerealizuota
        /// </summary>
        void HiddenFieldArraysUpdate()
        {
            // pridėjimas į fieldą
            // išsireiškiame iš fieldo
            string FIELDAS = "true|||false";
            String[] myValues = FIELDAS.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> myValuesList = new List<string>(myValues);
            myValuesList.Add("true");
            myValuesList.RemoveAt(myValues.Length);
            string str = String.Join("|||", myValuesList);
            // įrašome į fieldą
            // TextBox2.Text = str.ToString();
        }

        void ValidationTableParenthesisOpen()
        {
            
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                double bracketsControl = Convert.ToDouble(BracketsControlField.Value);
                bracketsControl += 1;
                BracketsControlField.Value = bracketsControl.ToString();

                Number.Value = "True";
                Addition.Value = "False";
                Subtraction.Value = "True";
                MultiplicationDivision.Value = "False";
                Comma.Value = "False";
                OpenParenthesis.Value = "True";
                CloseParenthesis.Value = "False";

                UnknownVar.Value = "True";
                EqualsLegitField.Value = "False";
            }
            else
            {
                double bracketsControl = Convert.ToDouble(BracketsControlField2.Value);
                bracketsControl += 1;
                BracketsControlField2.Value = bracketsControl.ToString();

                Number2.Value = "True";
                Addition2.Value = "False";
                Subtraction2.Value = "True";
                MultiplicationDivision2.Value = "False";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "True";
                CloseParenthesis2.Value = "False";

                UnknownVar2.Value = "True";
                EqualsLegitField2.Value = "False";
            }

            HiddenFieldArraysUpdate();

            // įrašyti į stringą
        }
        void ValidationTableParenthesisClose()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                double bracketsControl = Convert.ToDouble(BracketsControlField.Value);
                bracketsControl -= 1;
                BracketsControlField.Value = bracketsControl.ToString();

                Number.Value = "False";
                Addition.Value = "True";
                Subtraction.Value = "True";
                MultiplicationDivision.Value = "True";
                Comma.Value = "False";
                OpenParenthesis.Value = "False";
                CloseParenthesis.Value = "True";
                UnknownVar.Value = "False";

                EqualsLegitField.Value = "True";
            }
            else
            {
                double bracketsControl = Convert.ToDouble(BracketsControlField2.Value);
                bracketsControl -= 1;
                BracketsControlField2.Value = bracketsControl.ToString();

                Number2.Value = "False";
                Addition2.Value = "True";
                Subtraction2.Value = "True";
                MultiplicationDivision2.Value = "True";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "False";
                CloseParenthesis2.Value = "True";
                UnknownVar2.Value = "False";

                EqualsLegitField2.Value = "True";
            }

        }
        void ValidationTableNumbers()
        {

            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "True";
                Addition.Value = "True";
                Subtraction.Value = "True";
                MultiplicationDivision.Value = "True";
                Comma.Value = "True";
                OpenParenthesis.Value = "False";
                CloseParenthesis.Value = "True";

                UnknownVar.Value = "False";
                EqualsLegitField.Value = "True";
            }
            else
            {
                Number2.Value = "True";
                Addition2.Value = "True";
                Subtraction2.Value = "True";
                MultiplicationDivision2.Value = "True";
                Comma2.Value = "True";
                OpenParenthesis2.Value = "False";
                CloseParenthesis2.Value = "True";

                UnknownVar2.Value = "False";
                EqualsLegitField2.Value = "True";
            }

        }
        void ValidationUnknownValue()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "False";
                Addition.Value = "True";
                Subtraction.Value = "True";
                MultiplicationDivision.Value = "True";
                Comma.Value = "False";
                OpenParenthesis.Value = "False";
                CloseParenthesis.Value = "True";

                UnknownVar.Value = "False";
                EqualsLegitField.Value = "True";
            }
            else
            {
                Number2.Value = "False";
                Addition2.Value = "True";
                Subtraction2.Value = "True";
                MultiplicationDivision2.Value = "True";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "False";
                CloseParenthesis2.Value = "True";

                UnknownVar2.Value = "False";
                EqualsLegitField2.Value = "True";
            }

        }
        void ValidationTableAddition()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "True";
                Addition.Value = "False";
                Subtraction.Value = "False";
                MultiplicationDivision.Value = "False";
                Comma.Value = "False";
                OpenParenthesis.Value = "True";
                CloseParenthesis.Value = "False";
                UnknownVar.Value = "True";

                CommaLegitField.Value = "True";
                EqualsLegitField.Value = "False";
            }
            else
            {
                Number2.Value = "True";
                Addition2.Value = "False";
                Subtraction2.Value = "False";
                MultiplicationDivision2.Value = "False";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "True";
                CloseParenthesis2.Value = "False";
                UnknownVar2.Value = "True";

                CommaLegitField2.Value = "True";
                EqualsLegitField2.Value = "False";
            }

        }
        void ValidationTableSubtraction()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "True";
                Addition.Value = "False";
                Subtraction.Value = "False";
                MultiplicationDivision.Value = "False";
                Comma.Value = "False";
                OpenParenthesis.Value = "True";
                CloseParenthesis.Value = "False";
                UnknownVar.Value = "True";

                CommaLegitField.Value = "True";
                EqualsLegitField.Value = "False";
            }
            else
            {
                Number2.Value = "True";
                Addition2.Value = "False";
                Subtraction2.Value = "False";
                MultiplicationDivision2.Value = "False";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "True";
                CloseParenthesis2.Value = "False";
                UnknownVar2.Value = "True";

                CommaLegitField2.Value = "True";
                EqualsLegitField2.Value = "False";
            }

        }
        void ValidationTableMultiplicationDivision()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "True";
                Addition.Value = "False";
                Subtraction.Value = "True";
                MultiplicationDivision.Value = "False";
                Comma.Value = "False";
                OpenParenthesis.Value = "True";
                CloseParenthesis.Value = "False";
                UnknownVar.Value = "True";

                CommaLegitField.Value = "True";
                EqualsLegitField.Value = "False";
            }
            else
            {
                Number2.Value = "True";
                Addition2.Value = "False";
                Subtraction2.Value = "True";
                MultiplicationDivision2.Value = "False";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "True";
                CloseParenthesis2.Value = "False";
                UnknownVar2.Value = "True";

                CommaLegitField2.Value = "True";
                EqualsLegitField2.Value = "False";
            }

        }
        void ValidationTableDot()
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                Number.Value = "True";
                Addition.Value = "False";
                Subtraction.Value = "False";
                MultiplicationDivision.Value = "False";
                Comma.Value = "False";
                OpenParenthesis.Value = "False";
                CloseParenthesis.Value = "False";
                UnknownVar.Value = "False";

                CommaLegitField.Value = "False"; // nereik jo visur žymėt neigiamo, čia ne table
                EqualsLegitField.Value = "False";
            }
            else
            {
                Number2.Value = "True";
                Addition2.Value = "False";
                Subtraction2.Value = "False";
                MultiplicationDivision2.Value = "False";
                Comma2.Value = "False";
                OpenParenthesis2.Value = "False";
                CloseParenthesis2.Value = "False";
                UnknownVar2.Value = "False";

                CommaLegitField2.Value = "False"; // nereik jo visur žymėt neigiamo, čia ne table
                EqualsLegitField2.Value = "False";
            }

        }

        // ****************************************************************************
        // **************************** Lentelė Istorijos ******************************
        // ****************************************************************************

        // GridView duomenų bazėms duomenims atvaizduoti
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CreateAccordianUsingRepeater();
        }

        /// <summary>
        /// duomenų prisaikdinimas GridView1
        /// </summary>
        public void CreateAccordianUsingRepeater()
        {
            GridView1.DataBind();
        }

        /// <summary>
        /// Iš naujo įkeliant atnaujinama istorija
        /// </summary>
        private void ReloadHistory()
        {
            // GeneralClass generalClass = new GeneralClass();
            GridView1.DataSource = DispData();
            GridView1.DataBind();
        }

        /// <summary>
        /// atspausdina lentelę
        /// </summary>
        /// <returns></returns>
        private DataTable DispData()
        {
            // čia reik prisijungti prie duombazės
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Calculations";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // **********************************************************************************
        // ****************************** DATABASE METHODS **********************************
        // **********************************************************************************

        /// <summary>
        /// duomenens įkėlimas Į db
        /// </summary>
        private void InsertValuesToDatabase(string text1, string text2)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //Guid guid = Guid.NewGuid(); // jei nenorėtume naudoti int increment
            cmd.CommandText = "insert into Calculations values('" + text1 + "','" + text2 + "')";
            //TextBox1.Text = GenerateAutoID().ToString(); // jei nenorėtume naudoti increment
            cmd.ExecuteNonQuery();
        }

        // **********************************************************************************
        // **************** Metodai (stipriau pririšti prie šios klasės) ********************
        // **********************************************************************************

        /// <summary>
        /// HiddenFields fake atstatymas (po lygybės), jis skiriasi tuo, jog šiuo atveju leidžiama pratęsti tai, kas buvo įvesta
        /// iš esmės tai nėra tikra atstatymas (kai bus masyvas čia reikš paskutinio masyvo duomens pakeitimą, o ne perrašymą)
        /// </summary>
        private void ChangeLastHiddenValue()
        {
            //    HiddenField3.Value = '0'.ToString();
            //    HiddenField12.Value = "False".ToString();
            //    HiddenField13.Value = '0'.ToString();
            //    HiddenField9.Value = "False".ToString();
            //    HiddenField10.Value = "True".ToString();
            //    HiddenField11.Value = "True".ToString();
        }

        /// <summary>
        /// visų duomenų įvedimo laukelių išvalymas
        /// </summary>
        private void ClearResults()
        {
            TextBox1.Text = "".ToString();
            TextBox3.Text = "".ToString();
            TextBox2.Text = "".ToString();
            TextBox4.Text = "".ToString();
        }

        /// <summary>
        /// skaičiaus eilutės pildymas vedant naudojantis GUI pateiktais skaičias (prie galo vis pridedama, su pelyte neleidžiu vedinėti kaip nori)
        /// </summary>
        /// <param name="number"></param>
        private void DataFilling(string number)
        {
            if (!Convert.ToBoolean(SwitchToOther.Value))
            {
                TextBox1.Text += number;
            }
            else
            {
                TextBox3.Text += number;
            }
        }

        /// <summary>
        /// HiddenFields tikras atstatymas
        /// (arba masyvo duomenų pilnas perrašymas)
        /// </summary>
        private void ResetHiddenValues()
        {
            // HiddenField3.Value = "".ToString();
            BracketsControlField.Value = "0".ToString();
            Number.Value = "True".ToString();
            Addition.Value = "False".ToString();
            Subtraction.Value = "True".ToString();
            MultiplicationDivision.Value = "False".ToString();
            Comma.Value = "False".ToString();
            OpenParenthesis.Value = "True".ToString();
            CloseParenthesis.Value = "False".ToString();
            UnknownVar.Value = "True".ToString();

            CommaLegitField.Value = "True".ToString();
            EqualsLegitField.Value = "False".ToString();

            BracketsControlField2.Value = "0".ToString();
            Number2.Value = "True".ToString();
            Addition2.Value = "False".ToString();
            Subtraction2.Value = "True".ToString();
            MultiplicationDivision2.Value = "False".ToString();
            Comma2.Value = "False".ToString();
            OpenParenthesis2.Value = "True".ToString();
            CloseParenthesis2.Value = "False".ToString();
            UnknownVar2.Value = "True".ToString();

            CommaLegitField2.Value = "True".ToString();
            EqualsLegitField2.Value = "True".ToString();
        }

        /// <summary>
        /// išvalomi laukeliai po lygybės ir EqualsClear vėl tampa false iki kitos lygybės
        /// </summary>
        /// <param name="EqualsClear"></param>
        private void ClearAfterEquals(out bool EqualsClear)
        {
            ClearResults();
            EqualsClear = false;
            HiddenField6.Value = EqualsClear.ToString();
        }

        /// <summary>
        /// paruošimas ištrinti laukelius kitą kartą užkraunant puslapį
        /// </summary>
        private void ClearReady()
        {
            EqualsClear = true; // išvalo counting ir rezultatų label
            HiddenField6.Value = EqualsClear.ToString();
        }

        /// <summary>
        /// veiksmų istorijos rodymas ir slėpimas
        /// </summary>
        /// <param name="show"></param>
        public void HistoryShow(bool show)
        {
            if (show)
            {
                HiddenField7.Value = show.ToString();
                GridView1.Visible = show;
                Button7.Text = "HHIS";
            }
            else
            {
                HiddenField7.Value = show.ToString();
                GridView1.Visible = show;
                Button7.Text = "HIS";
            }
        }
        
        public void OnlineFeatures(bool show)
        {
            if (show)
            {
                HiddenField8.Value = show.ToString();
                ButtonDatabase.Text = "Net On";
            }
            else
            {
                HiddenField8.Value = show.ToString();
                ButtonDatabase.Text = "Net Off";
            }
        }

        // **********************************************************************************
        // ***************************** Kiti Metodai **********************************
        // **********************************************************************************

        private string InsertValue(decimal i, string text)
        {
            text = text.Replace("x", i.ToString()); // Formatavimas
            return text;
        }

        private bool VariableExist(string text)
        {
            bool foundX = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'x')
                {
                    foundX = true;
                }
            }

            return foundX;
        }

        private string FormatValue(string text)
        {
            // double resultParsed = double.Parse(text);
            string resultFormatted = String.Format("{0:0.000000}", text); // čia tiksliau nei vaizduojama
            string ResultFinal = resultFormatted.Replace(',', '.'); // Formatavimas

            return ResultFinal;
        }

    }
}