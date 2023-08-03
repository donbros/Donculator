using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator
{
    public class NotUsedCode
    {


        ///// <summary>
        ///// PRISIJUNGIMAS PRIE MS SQL db !PRISIJUNGIMAS NAUDOJIMO METU VISADA ATVIRAS!
        ///// </summary>
        //private void ConnectionState()
        //{
        //    if (con.State == System.Data.ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    con.Open();
        //}

        ///// <summary>
        ///// gridView stiliaus lentelės vaizdavimas
        ///// </summary>
        //private DataTable DispData()
        //{
        //    ConnectionState();
        //    SqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select * from Calculations";
        //    cmd.ExecuteNonQuery();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);
        //    return dt;
        //}

        // seni nebenaudojami metodai

        /// <summary>
        /// spausdina rastus polinomus
        /// </summary>
        /// <param name="PolynomialList"></param>
        //private void PrintPolynomialList(List<double> PolynomialList)
        //{
        //    TextBox2.Text += " Polinomiškai pateikta: ";

        //    string text_ = "";

        //    for (int i = 0; i < PolynomialList.Count; i++)
        //    {
        //        text_ += PolynomialList[i];
        //        for (int j = 0; j < i; j++)
        //            text_ += "*x";
        //        if (i != PolynomialList.Count - 1)
        //            text_ += "+";
        //    }

        //    TextBox2.Text += operationSignsFixer1(text_);
        //}


        //private List<string> AdditionSubtractionDividerForTextBox3(string text_)
        //{
        //    string[] textTemp1 = text_.Split('-');
        //    for (int j = 0; j < textTemp1.Length; j++)
        //    {
        //        textTemp1[j] = "";
        //    }
        //    int count = 0;
        //    int senas = 0;
        //    bool happened = false;

        //    // prirasius 0 skliaustuose pradzioje gerai gana veikia (-1-3)
        //    // negali eit į [-1]
        //    //if (text1[0] == '-')
        //    //{
        //    //    text1 = "0" + text1;
        //    //}

        //    // !!!!!!!!!!!!!!!!!!!!!!dažnai čia padaro j = 2 ir išsprendžiama minuso problema
        //    for (int j = 0; j < text_.Length; j++)
        //    {
        //        if (text_[j] == '-')
        //        {
        //            if ((j != 0) && (
        //                (text_[j - 1] == '*') ||
        //                (text_[j - 1] == '/') ||
        //                (text_[j - 1] == '(') || // pataisymas
        //                (text_[j - 1] == ')') // pataisymas
        //                ))
        //            { }
        //            else
        //            {
        //                Console.WriteLine(senas + " " + j);
        //                string tempTXT = text_.Substring(senas, j - senas);
        //                char senolis = text_[senas];

        //                // šita vieta dažnai lemia minuso pametimą
        //                if ((text_[senas] == '-') && (tempTXT != ""))
        //                {
        //                    textTemp1[count++] = text_.Substring(senas + 1, j - senas - 1);
        //                    senas = j;
        //                    happened = true;
        //                }
        //                else if (tempTXT != "")
        //                {
        //                    textTemp1[count++] = text_.Substring(senas, j - senas);
        //                    senas = j;
        //                    happened = true;
        //                }
        //            }
        //        }
        //    }
        //    // if(!buvo)
        //    // jei net nebuvo splitintas
        //    if (happened)
        //    {
        //        string senolis = text_.Substring(senas + 1);
        //        textTemp1[count++] = text_.Substring(senas + 1);
        //    }
        //    else
        //    {
        //        // tinkamas testui 1+4*(5+77*-4+1*-1)
        //        textTemp1[count++] = text_;
        //    }
        //    // dar reik splitinti "+"

        //    string[] TEXT = new string[count];

        //    for (int j = 0; j < count; j++)
        //    {
        //        TEXT[j] = textTemp1[j];
        //    }

        //    List<string> textList = new List<string>();

        //    for (int j = 0; j < TEXT.Length; j++)
        //    {
        //        textList.Add(TEXT[j]);
        //        if (j != TEXT.Length - 1)
        //        {
        //            textList.Add("-");
        //        }
        //    }

        //    // dividinimas teigiamu skaičių reik pataisyti, nes dabar blogai veikia (dalija skliaustų viduje)

        //    // cia neturi buti neigiamo skaiciaus pries nes jo netikrina
        //    //for (int j = 0; j < textList.Count; j++)
        //    //{
        //    //    if (textList[j].Contains('+') && textList[j].Length > 1)
        //    //    {
        //    //        string[] textPart = textList[j].Split('+');

        //    //        textList.RemoveAt(j);

        //    //        for (int b = textPart.Length - 1; b >= 0; b--)
        //    //        {
        //    //            textList.Insert(j, textPart[b]);
        //    //            if (b != 0)
        //    //            {
        //    //                textList.Insert(j, "+");
        //    //            }
        //    //        }

        //    //    }
        //    //}

        //    return textList;
        //}

        //private double DivideAndMultiplyX(string text1)
        //{
        //    double total = 0;
        //    // string totalString;
        //    // !!!!!!!!!!!!!!!!!!!!!!!!!
        //    // reikia čia nurodyti, jog neskaidyti, jeigu yra x, tačiau galima skaidyti viską kas nėra x

        //    // if there is x any number near it should be not touched


        //    string[] text = text1.Split('*');
        //    List<string> textList = new List<string>();

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        textList.Add(text[i]);
        //        if (i != text.Length - 1)
        //        {
        //            textList.Add("*");
        //        }
        //    }

        //    for (int i = 0; i < textList.Count; i++)
        //    {
        //        if (textList[i].Contains('/') && textList[i].Length > 1)
        //        {
        //            string[] textPart = textList[i].Split('/');

        //            textList.RemoveAt(i);

        //            for (int j = textPart.Length - 1; j >= 0; j--)
        //            {
        //                textList.Insert(i, textPart[j]);
        //                if (j != 0)
        //                {
        //                    textList.Insert(i, "/");
        //                }
        //            }

        //        }
        //    }

        //    total = Convert.ToDouble(textList[0]);
        //    for (int i = 2; i < textList.Count; i += 2)
        //    {
        //        if (textList[i - 1] == "/")
        //        {
        //            total = total / Convert.ToDouble(textList[i]);
        //        }
        //        // žiūri sekantį
        //        else if (textList[i - 1] == "*")
        //        {
        //            total = total * Convert.ToDouble(textList[i]);
        //        }
        //    }

        //    //    double kokstotal = total;

        //    //    totalString = total.ToString();
        //    //}
        //    //else
        //    //{
        //    //    totalString = text1;
        //    //}

        //    return total;
        //}

        // SAVOTIŠKAI VEIKIANČIOS

        ///// <summary>
        ///// atskliaudėjas (kol kas neveikia su x, nelogiškai atskliaudžia)
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public string RemoveBracketsX(string text)
        //{
        //    // ištaiso error (bet ne visus)
        //    // text = "0+" + text;

        //    while (text.Contains('(') && text.Contains(')'))
        //    {
        //        int openIndex = 0;
        //        int closeIndex = 0;

        //        for (int i = 0; i < text.Length; i++)
        //        {
        //            // eina per kiekviena char
        //            if (text[i] == '(')
        //            {
        //                openIndex = i;
        //            }
        //            if (text[i] == ')')
        //            {
        //                // dėl šių indeksų nueina iki giliausio pirmiausia
        //                closeIndex = i;

        //                // šita vieta yra problematiška, ji durnai įdeda atsakymą
        //                // ištrina seną dalį ir įdeda naują
        //                string textTemp = text.Substring(openIndex, closeIndex - openIndex + 1);

        //                // čia ta vieta kur bandžiau pataisyti atskliaudimą, bet biški nepavyko

        //                //if (textTemp.Contains('x'))
        //                //// užsiciklino (padaryti kažkaip kad tik vieną kart būtent šitą tikrintų
        //                //// gal užsiciklino nes nieko nepakeičiam
        //                //{
        //                //    // manipuliacija su string'o eilute kurioje yra x
        //                //    // principas tas, kad Resolvint bracketus reik (atskiriam?)
        //                //    // taciau remove ir insert daromi neteisingai (jau buvo toks neatitikimas anksciau)
        //                //    // * kažką su textTemp bandyti
        //                //    // ištrynus viskas ok, tačiau inesrtinant reiktų pasižiūrėti koks ženklas buvo prieš ir su juo sudauginti

        //                //    // Užduotis: šito ženklus pakeisiti teisingai, seną deletinti ir naują insertinti
        //                //    string resolvedPartWithX = ResolveBracketsX(openIndex, closeIndex, text);

        //                //    resolvedPartWithX = operationSignsFixer1(resolvedPartWithX);

        //                //    // prieš skaidant išsiaiškinam ar minusas pradžioje
        //                //    bool minusInTheBeggining = false;
        //                //    if (resolvedPartWithX[0] == '-')
        //                //    {
        //                //        minusInTheBeggining = true;
        //                //    }

        //                //    List<string> textList = AdditionSubtractionDivider(resolvedPartWithX);

        //                //    resolvedPartWithX = "";

        //                //    // čia nesamonė, reik pataisyt
        //                //    for (int j = 2; j < textList.Count; j++)
        //                //    {
        //                //        if (textList[j - 1] == "-")
        //                //        {
        //                //            resolvedPartWithX += textList;
        //                //        }
        //                //    }

        //                //    // dabar jau išskaidžius reiktų

        //                //    // realiai
        //                //    // text = text.Remove(openIndex, closeIndex - openIndex + 1).Insert(openIndex, changedSign);

        //                //    //

        //                //    // text = text.Remove(openIndex, closeIndex - openIndex + 1);
        //                //    // text = text.Insert(openIndex, ResolveBracketsX(openIndex, closeIndex, text));
        //                //}
        //                //else
        //                text = text.Remove(openIndex, closeIndex - openIndex + 1).Insert(openIndex, ResolveBracketsX(openIndex, closeIndex, text)); ;



        //                break;
        //            }
        //        }
        //    }

        //    // ^ kadangi algoritmas nebesumuoja vidinių duomenų, jis sudaugina priešingą ženklą tik su pirmutiniu
        //    // dėmeniu. Algoritmas buvo kurtas nesigiliannt į šią situaciją, tad reikia pataisyti atskliaudimo vietą

        //    for (int i = 1; i < text.Length; i++)
        //    {
        //        if (text[i] == '-' && (text[i - 1] == '*' || text[i - 1] == '/'))
        //        {
        //            for (int j = i - 1; j >= 0; j--)
        //            {
        //                if (text[j] == '+')
        //                {
        //                    StringBuilder text1 = new StringBuilder(text);
        //                    text1[j] = '-';
        //                    text = text1.ToString();
        //                    text = text.Remove(i, 1);
        //                    break;
        //                }
        //                else if (text[j] == '-')
        //                {
        //                    StringBuilder text1 = new StringBuilder(text);
        //                    text1[j] = '+';
        //                    text = text1.ToString();
        //                    text = text.Remove(i, 1);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    // ženklų taisymas
        //    text = operationSignsFixer1(text);

        //    // prideda nulį, jei pradžia -, kad nepanikuotų programa
        //    if (text[0] == '-')
        //    {
        //        text = '0' + text;
        //    }

        //    return CalculateX(text);
        //}

        //private List<string> AdditionSubtractionDivider(string resolvedPartWithX)
        //{
        //    string[] textTemp1 = resolvedPartWithX.Split('-');
        //    for (int j = 0; j < textTemp1.Length; j++)
        //    {
        //        textTemp1[j] = "";
        //    }
        //    int count = 0;
        //    int senas = 0;
        //    bool happened = false;

        //    // prirasius 0 skliaustuose pradzioje gerai gana veikia (-1-3)
        //    //if (text1[0] == '-')
        //    //{
        //    //    text1 = "0" + text1;
        //    //}

        //    // AR PRAEIS ŠITA
        //    for (int j = 0; j < resolvedPartWithX.Length; j++)
        //    {
        //        if (resolvedPartWithX[j] == '-')
        //        {
        //            if ((j != 0) && (
        //                (resolvedPartWithX[j - 1] == '*') ||
        //                (resolvedPartWithX[j - 1] == '/')
        //                ))
        //            {
        //                //buvo = false;
        //            }
        //            else
        //            {
        //                Console.WriteLine(senas + " " + j);
        //                string tempTXT = resolvedPartWithX.Substring(senas, j - senas);
        //                char senolis = resolvedPartWithX[senas];

        //                // šita vieta dažnai lemia minuso pametimą
        //                if ((resolvedPartWithX[senas] == '-') && (tempTXT != ""))
        //                {
        //                    textTemp1[count++] = resolvedPartWithX.Substring(senas + 1, j - senas - 1);
        //                    senas = j;
        //                    happened = true;
        //                }
        //                else if (tempTXT != "")
        //                {
        //                    textTemp1[count++] = resolvedPartWithX.Substring(senas, j - senas);
        //                    senas = j;
        //                    happened = true;
        //                }
        //            }
        //        }
        //    }
        //    // if(!buvo)
        //    // jei net nebuvo splitintas
        //    if (happened)
        //    {
        //        string senolis = resolvedPartWithX.Substring(senas + 1);
        //        textTemp1[count++] = resolvedPartWithX.Substring(senas + 1);
        //    }
        //    else
        //    {
        //        // tinkamas testui 1+4*(5+77*-4+1*-1)
        //        textTemp1[count++] = resolvedPartWithX;
        //    }
        //    // dar reik splitinti "+"

        //    string[] TEXT = new string[count];

        //    for (int j = 0; j < count; j++)
        //    {
        //        TEXT[j] = textTemp1[j];
        //    }

        //    List<string> textList = new List<string>();

        //    for (int j = 0; j < TEXT.Length; j++)
        //    {
        //        textList.Add(TEXT[j]);
        //        if (j != TEXT.Length - 1)
        //        {
        //            textList.Add("-");
        //        }
        //    }

        //    // cia neturi buti neigiamo skaiciaus pries nes jo netikrina
        //    for (int j = 0; j < textList.Count; j++)
        //    {
        //        if (textList[j].Contains('+') && textList[j].Length > 1)
        //        {
        //            string[] textPart = textList[j].Split('+');

        //            textList.RemoveAt(j);

        //            for (int b = textPart.Length - 1; b >= 0; b--)
        //            {
        //                textList.Insert(j, textPart[b]);
        //                if (b != 0)
        //                {
        //                    textList.Insert(j, "+");
        //                }
        //            }

        //        }
        //    }

        //    return textList;
        //}


        ///// <summary>
        ///// vidinė atskliaudėjo f-ja
        ///// </summary>
        ///// <param name="openIndex"></param>
        ///// <param name="closeIndex"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public string ResolveBracketsX(int openIndex, int closeIndex, string text)
        //{
        //    string bracketAnswer = CalculateX(text.Substring(openIndex + 1, closeIndex - openIndex - 1));

        //    return bracketAnswer;
        //}



        // ------------------------******************************************















































        //private static NumericSeries GetNumericSeries1()
        //{
        //    NumericSeries series1 = new NumericSeries();
        //    // This code populates the series using unbound data
        //    series1.Points.Add(new NumericDataPoint(7.0, "Point A", false));
        //    series1.Points.Add(new NumericDataPoint(-2.0, "Point B", false));
        //    series1.Points.Add(new NumericDataPoint(8.0, "Point C", false));
        //    series1.Points.Add(new NumericDataPoint(9.0, "Point D", false));
        //    series1.Points.Add(new NumericDataPoint(10, "Point E", false));
        //    return series1;
        //}

        //void LineChart()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[3] {  new DataColumn("Employee", typeof(string))
        //                                    ,new DataColumn("Days", typeof(string))
        //                                    ,new DataColumn("CameOnTime", typeof(decimal)) });
        //    dt.Rows.Add("Jon", "Mon", "10");
        //    dt.Rows.Add("Jon", "Tue", "20");
        //    dt.Rows.Add("Jon", "Wed", "30");
        //    dt.Rows.Add("Jon", "Thu", "20");
        //    dt.Rows.Add("Jon", "Fri", "10");
        //    dt.Rows.Add("Jon", "Sat", "20");

        //    dt.Rows.Add("Mak", "Mon", "20");
        //    dt.Rows.Add("Mak", "Tue", "10");
        //    dt.Rows.Add("Mak", "Wed", "20");
        //    dt.Rows.Add("Mak", "Thu", "30");
        //    dt.Rows.Add("Mak", "Fri", "10");
        //    dt.Rows.Add("Mak", "Sat", "30");

        //    List<string> x = (from p in dt.AsEnumerable()
        //                      select p.Field<string>("Days")).Distinct().ToList();


        //    var Employee = (from p in dt.AsEnumerable()
        //                    select p.Field<string>("Employee")).Distinct().ToList();
        //    foreach (var emp in Employee)
        //    {
        //        List<decimal> totals = (from p in dt.AsEnumerable()
        //                                where p.Field<string>("Employee") == emp
        //                                select p.Field<decimal>("CameOnTime")).Cast<decimal>().ToList();
        //        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = emp, Data = totals.ToArray() });
        //    }

        //    LineChart1.CategoriesAxis = string.Join(",", x.ToArray());
        //    LineChart1.ChartTitle = string.Format("Employee Come time Details");
        //    Label lbl = new Label();
        //    lbl.Text = "<b>On Time: 10 <br/> Grace Time 20  <br/> Late Time: 30</b>";
        //    LineChart1.Controls.Add(lbl);
        //    LineChart1.Visible = true;
        //}

        /// <summary>
        /// Vienas iš chart'ų
        /// </summary>

        //    private void GetChartData()
        //    {
        //        //string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        //        //using (SqlConnection con = new SqlConnection(cs))
        //        //{
        //        //    // Query to retrieve the 2 columns (1 column for X-AXIS and
        //        //    // the other for Y-AXIS) of data required for the chart control
        //        //    SqlCommand cmd = new SqlCommand
        //        //        ("Select StudentName, TotalMarks from Students", con);
        //        //    con.Open();
        //        //    SqlDataReader rdr = cmd.ExecuteReader();
        //        //    // Pass the datareader object that contains the chart data and
        //        //    // specify the column to be used for X-AXIS values. The other
        //        //    // column values will be automatically used for Y-AXIS
        //        //    Chart1.DataBindTable(rdr, "StudentName");
        //        //}
        //    }

        //    private void GetChartTypes()
        //    {
        //        foreach (int chartType in Enum.GetValues(typeof(SeriesChartType)))
        //        {
        //            ListItem li = new ListItem(Enum.GetName(
        //                typeof(SeriesChartType), chartType), chartType.ToString());
        //            //DropDownList1.Items.Add(li);
        //        }
        //    }

        //    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        //GetChartData();
        //        //// Retrieve the series by index and then
        //        //// set the ChartType property value
        //        //this.Chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(
        //        //    typeof(SeriesChartType), DropDownList1.SelectedValue);
        //        //// Alternatively, FirstOrDefault() linq method can also be used
        //        //// this.Chart1.Series.FirstOrDefault().ChartType = (SeriesChartType)
        //        //// Enum.Parse(typeof(SeriesChartType), DropDownList1.SelectedValue);
        //    }






        //private SqlConnection con_;
        //private SqlCommand com;
        //private string constr, query;
        //private void connection()
        //{
        //    constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
        //    con_ = new SqlConnection(constr);
        //    con_.Open();

        //}
        //private void Bindchart()
        //{
        //    connection();
        //    query = "select *from Orderdet";//not recommended this i have written just for example,write stored procedure for security
        //    com = new SqlCommand(query, con_);
        //    SqlDataAdapter da = new SqlDataAdapter(query, con_);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //    DataTable ChartData = ds.Tables[0];

        //    //storing total rows count to loop on each Record
        //    string[] XPointMember = new string[ChartData.Rows.Count];
        //    int[] YPointMember = new int[ChartData.Rows.Count];

        //    for (int count = 0; count < ChartData.Rows.Count; count++)
        //    {
        //        //storing Values for X axis
        //        XPointMember[count] = ChartData.Rows[count]["Month"].ToString();
        //        //storing values for Y Axis
        //        YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Orders"]);


        //    }
        //    //binding chart control
        //    Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //    //Setting width of line
        //    Chart1.Series[0].BorderWidth = 1;
        //    //setting Chart type 
        //    //  Chart1.Series[0].ChartType = SeriesChartType.Line ;
        //    // Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        //    // Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        //    Chart1.Series[0].ChartType = SeriesChartType.Spline;
        //    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;


        //    con.Close();

        //}

        //protected void CalcDataGraphs()
        //{

        //    this.SuspendLayout();

        //    display.DataSources.Clear();
        //    display.SetDisplayRangeX(0, 400);

        //    for (int j = 0; j < NumGraphs; j++)
        //    {
        //        display.DataSources.Add(new DataSource());
        //        display.DataSources[j].Name = "Graph " + (j + 1);
        //        display.DataSources[j].OnRenderXAxisLabel += RenderXLabel;

        //        switch (CurExample)
        //        {
        //            case "NORMAL":
        //                this.Text = "Normal Graph";
        //                display.DataSources[j].Length = 5800;
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].SetDisplayRangeY(-300, 300);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                display.DataSources[j].OnRenderYAxisLabel = RenderYLabel;
        //                CalcSinusFunction_0(display.DataSources[j], j);
        //                break;

        //            case "NORMAL_AUTO":
        //                this.Text = "Normal Graph Autoscaled";
        //                display.DataSources[j].Length = 5800;
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
        //                display.DataSources[j].AutoScaleY = true;
        //                display.DataSources[j].SetDisplayRangeY(-300, 300);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                display.DataSources[j].OnRenderYAxisLabel = RenderYLabel;
        //                CalcSinusFunction_0(display.DataSources[j], j);
        //                break;

        //            case "STACKED":
        //                this.Text = "Stacked Graph";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.STACKED;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].SetDisplayRangeY(-250, 250);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_1(display.DataSources[j], j);
        //                break;

        //            case "VERTICAL_ALIGNED":
        //                this.Text = "Vertical aligned Graph";
        //                display.PanelLayout =
        //                    PlotterGraphPaneEx.LayoutMode.VERTICAL_ARRANGED;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].SetDisplayRangeY(-300, 300);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "VERTICAL_ALIGNED_AUTO":
        //                this.Text = "Vertical aligned Graph autoscaled";
        //                display.PanelLayout =
        //                    PlotterGraphPaneEx.LayoutMode.VERTICAL_ARRANGED;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = true;
        //                display.DataSources[j].SetDisplayRangeY(-300, 300);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "TILED_VERTICAL":
        //                this.Text = "Tiled Graphs (vertical prefered)";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_VER;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].SetDisplayRangeY(-300, 600);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "TILED_VERTICAL_AUTO":
        //                this.Text = "Tiled Graphs (vertical prefered) autoscaled";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_VER;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = true;
        //                display.DataSources[j].SetDisplayRangeY(-300, 600);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "TILED_HORIZONTAL":
        //                this.Text = "Tiled Graphs (horizontal prefered)";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_HOR;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].SetDisplayRangeY(-300, 600);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "TILED_HORIZONTAL_AUTO":
        //                this.Text = "Tiled Graphs (horizontal prefered) autoscaled";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_HOR;
        //                display.DataSources[j].Length = 5800;
        //                display.DataSources[j].AutoScaleY = true;
        //                display.DataSources[j].SetDisplayRangeY(-300, 600);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                CalcSinusFunction_2(display.DataSources[j], j);
        //                break;

        //            case "ANIMATED_AUTO":

        //                this.Text = "Animated graphs fixed x range";
        //                display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_HOR;
        //                display.DataSources[j].Length = 402;
        //                display.DataSources[j].AutoScaleY = false;
        //                display.DataSources[j].AutoScaleX = true;
        //                display.DataSources[j].SetDisplayRangeY(-300, 500);
        //                display.DataSources[j].SetGridDistanceY(100);
        //                display.DataSources[j].XAutoScaleOffset = 50;
        //                CalcSinusFunction_3(display.DataSources[j], j, 0);
        //                display.DataSources[j].OnRenderYAxisLabel = RenderYLabel;
        //                break;
        //        }
        //    }

        //    ApplyColorSchema();

        //    this.ResumeLayout();
        //    display.Refresh();

        //}

        //protected void CalcSinusFunction_0(DataSource src, int idx)
        //{
        //    for (int i = 0; i < src.Length; i++)
        //    {
        //        src.Samples[i].x = i;
        //        src.Samples[i].y = (float)(((float)200 * Math.Sin((idx + 1) * (
        //            i + 1.0) * 48 / src.Length)));
        //    }
        //}

        //private String RenderXLabel(DataSource s, int idx)
        //{
        //    if (s.AutoScaleX)
        //    {
        //        int Value = (int)(s.Samples[idx].x);
        //        return "" + Value;
        //    }
        //    else
        //    {
        //        int Value = (int)(s.Samples[idx].x / 200);
        //        String Label = "" + Value + "\"";
        //        return Label;
        //    }
        //}

        //private String RenderYLabel(DataSource s, float value)
        //{
        //    return String.Format("{0:0.0}", value);
        //}


        ///// <summary>
        ///// vidinė atskliaudėjo f-ja
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public string CalculateX(string substringedText)
        //{
        //    // teksto gabalas patenka čia
        //    string text1 = AddAndSubstractX(substringedText);

        //    //double finalAnswer = int.Parse(text);

        //    //return finalAnswer.ToString();

        //    return text1;

        //}


        ///// <summary>
        ///// vykdo veiksmus su "+" ir "-"
        ///// </summary>
        ///// <param name="text1"></param>
        ///// <returns></returns>
        //public string AddAndSubstractX(string text1)
        //{
        //    // neveikia su 5*(4*-7*7)
        //    // ir 5*(4*-7*7)
        //    string[] textTemp = text1.Split('-');
        //    for (int i = 0; i < textTemp.Length; i++)
        //    {
        //        textTemp[i] = "";
        //    }
        //    int count = 0;
        //    int senas = 0;
        //    bool happened = false;

        //    // prirasius 0 skliaustuose pradzioje gerai gana veikia (-1-3)
        //    //if (text1[0] == '-')
        //    //{
        //    //    text1 = "0" + text1;
        //    //}

        //    // galima įmest kokį -- fixerį

        //    for (int i = 0; i < text1.Length; i++)
        //    {
        //        if (text1[i] == '-' && text1[i + 1] == '-')
        //        {
        //            text1 = text1.Remove(i, 2);
        //            text1 = text1.Insert(i, "+");
        //        }
        //        else if (text1[i] == '+' && text1[i + 1] == '-')
        //        {
        //            text1 = text1.Remove(i, 2);
        //            text1 = text1.Insert(i, "-");
        //        }
        //    }

        //    // 0+ netinka nes gaunasi 1+-...
        //    // text1 = "0+" + text1;
        //    for (int i = 0; i < text1.Length; i++)
        //    {
        //        if (text1[i] == '-')
        //        {
        //            if ((i != 0) && (
        //                (text1[i - 1] == '*') ||
        //                (text1[i - 1] == '/')
        //                ))
        //            {
        //                //buvo = false;
        //            }
        //            else
        //            {
        //                Console.WriteLine(senas + " " + i);
        //                string tempTXT = text1.Substring(senas, i - senas);
        //                char senolis = text1[senas];

        //                // šita vieta dažnai lemia minuso pametimą
        //                if ((text1[senas] == '-') && (tempTXT != ""))
        //                {
        //                    textTemp[count++] = text1.Substring(senas + 1, i - senas - 1);
        //                    senas = i;
        //                    happened = true;
        //                }
        //                else if (tempTXT != "")
        //                {
        //                    textTemp[count++] = text1.Substring(senas, i - senas);
        //                    senas = i;
        //                    happened = true;
        //                }
        //            }
        //        }
        //    }
        //    // if(!buvo)
        //    // jei net nebuvo splitintas
        //    if (happened)
        //    {
        //        string senolis = text1.Substring(senas + 1);
        //        textTemp[count++] = text1.Substring(senas + 1);
        //    }
        //    else
        //    {
        //        // tinkamas testui 1+4*(5+77*-4+1*-1)
        //        textTemp[count++] = text1;
        //    }

        //    string[] text = new string[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        text[i] = textTemp[i];
        //    }

        //    List<string> textList = new List<string>();

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        textList.Add(text[i]);
        //        if (i != text.Length - 1)
        //        {
        //            textList.Add("-");
        //        }
        //    }

        //    // cia neturi buti neigiamo skaiciaus pries nes jo netikrina
        //    for (int i = 0; i < textList.Count; i++)
        //    {
        //        if (textList[i].Contains('+') && textList[i].Length > 1)
        //        {
        //            string[] textPart = textList[i].Split('+');

        //            textList.RemoveAt(i);

        //            for (int j = textPart.Length - 1; j >= 0; j--)
        //            {
        //                textList.Insert(i, textPart[j]);
        //                if (j != 0)
        //                {
        //                    textList.Insert(i, "+");
        //                }
        //            }

        //        }
        //    }
        //    // čia tas buvo 5 | + | 7*4

        //    double total = 0;
        //    string TOTAL;
        //    if (textList[0].Contains('x'))
        //    {
        //        total = 0;
        //        TOTAL = textList[0];
        //    }
        //    else if (textList[0].Contains('*') || textList[0].Contains('/'))
        //    {
        //        total = DivideAndMultiply2(textList[0]);
        //        TOTAL = total.ToString();
        //    }
        //    else
        //    {
        //        double.TryParse(textList[0], out total);
        //        TOTAL = total.ToString();
        //        // double.TryParse(textList[0], out total);
        //    }

        //    for (int i = 2; i < textList.Count; i += 2)
        //    {
        //        //if (textList[i - 1].Contains('x'))
        //        //{
        //        //    TOTAL += textList[i - 1].ToString();
        //        //}
        //        if (textList[i - 1] == "-")
        //        {
        //            if (textList[i].Contains('x'))
        //            {
        //                TOTAL += "-" + textList[i];
        //            }
        //            else
        //            {
        //                total = DivideAndMultiply2(textList[i]);
        //                TOTAL += "-" + total.ToString();
        //            }
        //        }
        //        else if (textList[i - 1] == "+")
        //        {
        //            // jeigu ši daugybos dalis turi pliusą tiesiog ją sudedame
        //            if (textList[i].Contains('x'))
        //            {
        //                TOTAL += "+" + textList[i];
        //            }
        //            // jei neturi x tiesiog suskaičiuojame
        //            else
        //            {
        //                total = DivideAndMultiply2(textList[i]);
        //                TOTAL += "+" + total.ToString();
        //            }
        //        }
        //    }

        //    // !!!!!!!!!!!!!!!!!!
        //    // galima padaryti teisingą pridėjimą t.y. sudedam ne x (dabar be x treatinam lygiai kaip x, kas iš dalies teisinga, nes šiaip reiktų grupuoti tuomet ir x)
        //    // grupavima galime ant galo padaryti, svarbiausia dabar padaryti atskliaudimą

        //    return TOTAL;
        //}


        ///// <summary>
        ///// ID skirto PK (pirminiam raktui) generavimas
        ///// </summary>
        ///// <returns></returns>
        //private int GenerateAutoID()
        //{
        //    SqlCommand cmd = new SqlCommand("Select Count(id) from Calculations", con);
        //    int i = Convert.ToInt32(cmd.ExecuteScalar());
        //    i++;
        //    return i;
        //    //iblempid.Text = "ID" + i.ToString(); // galima uzsideti ne vien int
        //}




    }
}