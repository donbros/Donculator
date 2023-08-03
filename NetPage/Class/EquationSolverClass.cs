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
    public class EquationSolverClass : UnbracketClass
    {

        // **********************************************************************************
        // *********************** Lygties sprendimas ir polinomai **************************
        // **********************************************************************************

        /// <summary>
        /// Tikslas: išspręsti lygtį (randamos roots naudojant polinomų metodą)
        /// Principas 1: Unbracket pilnai atskliaudžia bet kokią lygtį - to neįvykdžius neinama į kitus metodus
        /// Principas <...>
        /// Rezultatas: gauname roots
        /// </summary>
        public string SolveEquation(string textBox1, string textBox2, bool equation, out string metaTextToPut, out string resultTextToPut, out double[] roots)
        {
            roots = new double[1]; // fake assigning

            metaTextToPut = "";
            resultTextToPut = "";

            string text = "";

            if (equation)
                text = EquationPreparerSimple(textBox1, textBox2); // viskas sukeliama iš dešinės į kairę, kad būtų lygu nuliui
            else
                text = textBox1;

            text = BracketsFixer(text);

            text = operationSignsFixer1(text); // ištaiso tam tikrus operacijų ženklus
            if (equation)
            {
                metaTextToPut += "Rearranged Equation" + "\r\n";
                metaTextToPut += text + " = 0 " + "\r\n";
            }

            // * reikia perdaryti, kad tiktų taip pat ir dalybai (kaip pažymėta sąsiuvinyje)
            text = Unbracket(text, out List<string> denominatorsList);

            List<double> denominatorsListRoots = FindAllDenominatorsRoots(denominatorsList);

            if (equation)
            {
                metaTextToPut += "Unbracketed" + "\r\n";
                metaTextToPut += text + " = 0 " + "\r\n";
            }

            // spėju (dar nežinau), jog nebereiks ieškoti negiamiausio lygio, nes viskas bus sutvarkyta Unbracket()
            // kadangi atliksime suprastinimą ir neliks dalybos apačioje, tačiau dar pasidomėti
            NeutraliseExcessXCountPL(ref text, out int negativePowerLevel);

            //if (equation)
            //{

            if (equation)
            {
                metaTextToPut += "Without excess x" + "\r\n";
                metaTextToPut += text + " = 0 " + "\r\n";
            }

                // jei po prastinimo likę negiamų x
                text = ConvertToPolynomial(text, negativePowerLevel);

            if (equation)
            {
                metaTextToPut += " Power Level (" + negativePowerLevel + ") " + "\r\n";
                metaTextToPut += "Before adding to polynomial List" + "\r\n";
                metaTextToPut += text + "\r\n";
            }

            List<double> PolynomialList = GetPolynomialMembers(text);

            Polynomial polynomial = new Polynomial(PolynomialList);

            // check if all polynomials equals zero
            bool allPolynomialsEqualsZero = true; // testavimui padarau false
            allPolynomialsEqualsZero = AllPolynomialsEqualsZero(ref polynomial, PolynomialList);

            if (allPolynomialsEqualsZero == false)
            {

                string text_polynomial = "";

                if (equation)
                    text_polynomial += "Simplified equation\r\n" + polynomial + " = 0 " + "\r\n";
                else
                    text_polynomial += "Simplified function\r\n" + " y = " + polynomial + "\r\n";

                // resultTextToPut += text_polynomial;

                metaTextToPut += text_polynomial;

                // panaudosime libraries ir išspresime

                if (equation)
                {

                    roots = polynomial.FindRoots();

                    int rootLength = roots.Length;

                    if (rootLength != 0)
                    {
                        for (int i = 0; i < roots.Length; i++)
                        {
                            bool normalRootIsNotDenominatorRoot = true;
                            for (int k = 0; k < denominatorsListRoots.Count; k++)
                            {
                                if (denominatorsListRoots[k] == roots[i])
                                {
                                    normalRootIsNotDenominatorRoot = false;
                                }
                            }
                            if (normalRootIsNotDenominatorRoot)
                            {
                                int j = i + 1;
                                resultTextToPut += "x" + j + "=" + roots[i] + " ";
                            }
                        }
                    }
                    else
                    {
                        resultTextToPut += "No roots were found for this equation";
                    }
                }
            }
            else
            {
                resultTextToPut += "True for all";
            }

            // negativePowerLevel = 0;

            //}

            return text;

        }

        /// <summary>
        /// Rekursinė klasė randanti bendravardiklių šaknis t.y. viskas kam neturėtų būti lygus atsakymas
        /// </summary>
        /// <param name="denominatorsList"></param>
        /// <returns></returns>
        private List<double> FindAllDenominatorsRoots(List<string> denominatorsList)
        {
            List<double> denominatorsListRoots = new List<double>();
            // reikia rasti denominators list roots
            for (int i = 0; i < denominatorsList.Count; i++)
            {
                SolveEquation(denominatorsList[i], "0", true, out string metaTextToPut2, out string resultTextToPut2, out double[] rootsNeeded);
                // metaTextToPut2 ir resultTextToPut2 abu useless, nes mums tereikia roots
                for (int j = 0; j < rootsNeeded.Length; j++)
                {
                    denominatorsListRoots.Add(rootsNeeded[j]);
                }
            }

            return denominatorsListRoots;
        }

        /// <summary>
        /// TINKA KLASEI
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        private string EquationPreparerSimple(string text1, string text2)
        {
            string textJoined = "";
            //if (double.TryParse(text2, out double number) && number == 0) // jei įvestas nulis kažkaip keistai veikia, todėl dedam apsaugą
            //{
            //    textJoined = text1;
            //}
            //else
            //{
            // veiksmai vykdomi perkeliant eilutę į kitą pusę
            textJoined = text1 + "-1*(" + text2 + ")";
            //}

            return textJoined;
        }

        /// <summary>
        /// Tikslas: panaikinti perteklinius x ir surasti negiaimiausią laipsnį
        /// </summary>
        /// <param name="text"></param>
        /// <param name=""></param>
        private void NeutraliseExcessXCountPL(ref string text, out int pLevel)
        {
            int iStart = 0;
            bool fiksuotiPradzia = true;

            pLevel = int.MaxValue;
            string subText = "";


            // i laukia
            if (fiksuotiPradzia)
            {
                iStart = 0;
                fiksuotiPradzia = false;
            }

            subText += text[0];

            for (int i = 1; i < text.Length; i++)
            {
                // i laukia
                if (fiksuotiPradzia)
                {
                    iStart = i;
                    fiksuotiPradzia = false;
                }

                // kaupiame iki skirtumo arba sumos ir tada tvarkome tą dalį (nekreipk dėmesio per daug)
                if ((text[i] == '+') || (text[i] == '-' && text[i - 1] != '*' && text[i - 1] != '/'))
                {
                    NeutraliseExcessXPart(ref text, ref pLevel, ref iStart, ref i, ref subText, ref fiksuotiPradzia);
                }
                // kadangi else pliusai ir minusai neįtraukiami (tačiau jų nereiktų ištrinti)
                else
                {
                    subText += text[i];
                }
                // paskutinį patikriname
                if (i == text.Length - 1)
                {
                    NeutraliseExcessXPart(ref text, ref pLevel, ref iStart, ref i, ref subText, ref fiksuotiPradzia);
                    i = text.Length;
                }
            }
        }

        /// <summary>
        /// NeutraliseExcessXCountNegativePowerLevel pratęsimas
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pLevel"></param>
        /// <param name="iStart"></param>
        /// <param name="i"></param>
        /// <param name="subText"></param>
        /// <param name="fiksuotiPradzia"></param>
        private void NeutraliseExcessXPart(ref string text, ref int pLevel, ref int iStart, ref int i, ref string subText, ref bool fiksuotiPradzia)
        {
            fiksuotiPradzia = true;

            subText = subText;
            int ilgis = subText.Length - 1;
            int multiplierXCount = 0;
            int dividerXCount = 0;
            // užtikrinti atsakymą iki j = 2;
            if (subText[0] == 'x')
            {
                multiplierXCount++;
            }
            // sutaisė "subText.Length >= 2" ši eilutė
            else if ((subText.Length >= 2) && (subText[1] == 'x' && subText[0] == '-')) // matyt negiamas skaičius prieš jį
            {
                multiplierXCount++;
            }
            for (int j = 2; j < subText.Length; j++)
            {
                if (subText[j] == 'x')
                {
                    // aiškinamės konkretaus atvejo pLevel
                    if ((subText[j - 1] == '/') || (subText[j - 1] == '-' && subText[j - 2] == '/'))
                    {
                        dividerXCount++;
                    }
                    if ((subText[j - 1] == '*') || (subText[j - 1] == '-' && subText[j - 2] == '*'))
                    {
                        multiplierXCount++;
                    }
                }
            }

            multiplierXCount = multiplierXCount;
            dividerXCount = dividerXCount;
            subText = subText;

            if (multiplierXCount - dividerXCount < pLevel)
            {
                subText = subText;
                pLevel = multiplierXCount - dividerXCount;
            }

            int removeDividerXCount = 0;
            int removeMultiplierXCount = 0;
            if (multiplierXCount != 0 && dividerXCount != 0)
                while (multiplierXCount != 0 && dividerXCount != 0)
                {
                    multiplierXCount--;
                    dividerXCount--;
                    removeDividerXCount++;
                    removeMultiplierXCount++;
                }

            if (removeDividerXCount != 0 && removeMultiplierXCount != 0) // perteklinis tikrinimas, bet taip aiškiau
            {
                for (int j = 0; j < subText.Length; j++)
                {
                    if (removeDividerXCount != 0 || removeMultiplierXCount != 0)
                    {
                        removeDividerXCount = removeDividerXCount;
                        removeMultiplierXCount = removeMultiplierXCount;
                        if (subText[0] == 'x')
                        {
                            if (removeMultiplierXCount != 0)
                            {
                                subText = subText.Remove(0, 1);
                                subText = subText.Insert(0, "1");
                                removeMultiplierXCount--;
                            }
                        }
                        else if (subText[1] == 'x' && subText[0] == '-') // matyt negiamas skaičius prieš jį
                        {
                            if (removeMultiplierXCount != 0)
                            {
                                subText = subText.Remove(1, 1);
                                subText = subText.Insert(1, "1");
                                removeMultiplierXCount--;
                            }
                        }
                        else if (subText[j] == 'x')
                        {
                            // aiškinamės konkretaus atvejo pLevel
                            if ((subText[j - 1] == '/') || (subText[j - 1] == '-' && subText[j - 2] == '/'))
                            {
                                if (removeDividerXCount != 0)
                                {
                                    subText = subText.Remove(j, 1);
                                    subText = subText.Insert(j, "1");
                                    removeDividerXCount--;
                                }
                            }
                            else if ((subText[j - 1] == '*') || (subText[j - 1] == '-' && subText[j - 2] == '*'))
                            {
                                if (removeMultiplierXCount != 0)
                                {
                                    subText = subText.Remove(j, 1);
                                    subText = subText.Insert(j, "1");
                                    removeMultiplierXCount--;
                                }
                            }
                        }
                    }
                }
            }

            i = i;
            iStart = iStart;
            text = text;
            int length = text.Length;
            // ištriname seną įdedame naują ir tesiame
            if (i + 1 == text.Length)
            {
                text = text.Remove(iStart, i + 1 - iStart).Insert(iStart, subText);
            }
            else
                text = text.Remove(iStart, i - iStart).Insert(iStart, subText);

            subText = "";
        }

        /// <summary>
        /// konvertuoja skaičius į polinomus
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string ConvertToPolynomial(string text, int pLevel)
        {
            if (pLevel < 0)
            {
                text = "(" + text + ")";
                for (int i = 0; i < Math.Abs(pLevel); i++)
                {
                    text += "*x";
                }
            }

            UnbracketClass unbrackedClass = new UnbracketClass();

            text = unbrackedClass.Unbracket(text, out List<string> denominatorsList);

            // lažkokia problemėlė
            NeutraliseExcessXCountPL(ref text, out int negativePowerLevel);

            return text;
        }

        /// <summary>
        /// Kompleksinis metodas. Surandami polinomai ir sudedami į sąrašą
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<double> GetPolynomialMembers(string text)
        {
            List<double> polynomials = new List<double>();

            string polynomialText = "";

            // kol ne viskas perdėta į polinomų list ...
            while (text != "")
            {
                bool ignore = false, fiksuotiPradzia = true;

                string subText = "";

                int iStart = 0;

                bool nunulinami = false; // nes ir taip nunulinta

                // viskas kas nėra x dedame į sąrašą polynomialText
                for (int i = 0; i < text.Length; i++)
                {
                    if (nunulinami)
                    {
                        i = 0;
                    }

                    if (fiksuotiPradzia)
                    {
                        iStart = i;
                        fiksuotiPradzia = false;
                    }

                    if (i != 0)
                    {
                        if ((text[i] == '+')
                            || (text[i] == '-' && text[i - 1] != '*' && text[i - 1] != '/'))
                        {
                            if (!ignore)
                            {
                                // gal nereikia cia
                                polynomialText += "+" + subText; // pridedam į naują (bet reiktų su ženklu)
                                                                 // kai perrašo nunulinam iškart ir ženklą pridedame
                                subText = "";
                                // subText += text[i]; // kaupia ženklą esantį prieš naują eilutę

                                // KAŽKOKIA DIDELĖ PROBLEMA ČIA - neveikia universaliai

                                if (iStart - 1 < 0) // jei pradžioje reikia pratrinti
                                {
                                    if (text[i] == '+')
                                    {
                                        text = text.Remove(iStart, i + 1 - iStart); // reikia išimti jį iš text, text.Length dinamiškai keisis atkreipti dėmesį
                                    }
                                    else if (text[i] == '-')
                                    {
                                        text = text.Remove(iStart, i - iStart);
                                    }
                                    i = iStart;

                                    nunulinami = true;
                                }
                                else
                                {
                                    if (text[i] == '+')
                                    {
                                        text = text.Remove(iStart - 1, i + 1 - (iStart));
                                    }
                                    else if (text[i] == '-')
                                    {
                                        text = text.Remove(iStart - 1, i + 1 - (iStart));
                                    }
                                    i = iStart - 1;
                                }
                                // kažkur pažymėti kad i-- padarytų ir būtų i = -1;

                                // reikia pašalinti ir prieš jį esantį ženklą
                                // skaičiuojame veiksmus, ištrinam tą dalį iš normalaus text ir įdedam į polynomialText
                            }
                            else
                            {
                                // kai paignoruoja toliau varom
                                ignore = false;
                                subText = "";
                            }
                            fiksuotiPradzia = true;
                        }

                        subText += text[i];

                        if ((text.Length > i) && (text[i] == 'x'))
                        {
                            ignore = true;
                        }

                        // paskutinį patikriname
                        if (i == text.Length - 1)
                        {
                            if (!ignore)
                            {
                                iStart = iStart;
                                polynomialText += "+" + subText; // pridedam į naują (bet reiktų su ženklu, dabartinis kažko ženklo neima)
                                if (iStart - 1 < 0)
                                {
                                    text = text.Remove(iStart, i + 1 - iStart);
                                }
                                else
                                {
                                    // kad ištrintų ženklą iStart - 1
                                    text = text.Remove(iStart - 1, i + 2 - iStart);
                                }

                                // i = text.Length;
                            }
                        }

                        polynomialText = polynomialText;
                    }
                    else if (i == 0)
                    {
                        if (!nunulinami)
                            subText += text[i];
                        else
                            nunulinami = false;

                        if ((text.Length > i) && (text[i] == 'x'))
                        {
                            ignore = true;
                        }
                    }
                }
                // kai praeiname vieną ciklą suskaičiuojame polinomo lygį

                polynomialText = polynomialText;
                polynomialText = operationSignsFixer1(polynomialText);

                // panaikina perteklinį pliusą
                if ((polynomialText != "") && (polynomialText[0] == '+'))
                {
                    polynomialText = polynomialText.Remove(0, 1);
                }

                if (polynomialText == "")
                    polynomials.Add(0);
                else
                    polynomials.Add(double.Parse(Calculate(polynomialText)));

                // jei dar vis text kažką turi (t.y. x), tuomet paruošiame naują tikrinimą
                if (text != "")
                {
                    text = "(" + text + ")/x";

                    text = Unbracket(text, out List<string> denominatorsList);

                    NeutraliseExcessXCountPL(ref text, out int negativePowerLevel);
                }
                else
                    break;

                // nunulinam
                polynomialText = "";
            }

            return polynomials;
        }

        /// <summary>
        /// Tikslas: perdėti polinomus į Polynomial klasės objektą ir sužinoti ar polinomas lygus 0
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="PolynomialList"></param>
        /// <returns></returns>
        private bool AllPolynomialsEqualsZero(ref Polynomial polynomial, List<double> PolynomialList)
        {
            bool allPolynomialsEqualsZero = false;
            for (int i = 0; i < PolynomialList.Count; i++)
            {
                polynomial[i] = PolynomialList[i];
                // jei visi polinomai nuliniai
                if (polynomial[i] == 0)
                {
                    allPolynomialsEqualsZero = true;
                }
                else
                {
                    allPolynomialsEqualsZero = false;
                    break;
                }
            }

            return allPolynomialsEqualsZero;
        }

        /// <summary>
        /// sutaiso skliaustus iš "-(" į "-1*(", nes "-(" nesupranta algoritmas jog daugyba iš -1
        /// </summary>
        /// <param name="text"></param>
        private string BracketsFixer(string text)
        {

            for (int i = 0; i < text.Length; i++)
            {
                if (i - 1 >= 0 && text[i - 1] == '-' && text[i] == '(')
                {
                    text = text.Insert(i, "1*");
                }
            }

            return text;
        }

        //protected override string RemoveBrackets(string text)
        //{
        //    return text;
        //}

    }
}