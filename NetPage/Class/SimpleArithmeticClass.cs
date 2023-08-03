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
    /// <summary>
    /// skaičiuoja paprastus aritmetinius veiksmus (naudojame decimal tikslumui)
    /// </summary>
    public class SimpleArithmeticClass : GeneralClass
    {

        /// <summary>
        /// atskliaudėjas
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string RemoveBrackets(string text)
        {
            // ištaiso error (bet ne visus)
            text = "0+" + text;

            while (text.Contains('(') && text.Contains(')'))
            {
                int openIndex = 0;
                int closeIndex = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    // eina per kiekviena char
                    if (text[i] == '(')
                    {
                        openIndex = i;
                    }
                    if (text[i] == ')')
                    {
                        closeIndex = i;

                        text = text.Remove(openIndex, closeIndex - openIndex + 1).Insert(openIndex, ResolveBrackets(openIndex, closeIndex, text));

                        break;
                    }
                }
            }

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == '-' && (text[i - 1] == '*' || text[i - 1] == '/'))
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (text[j] == '+')
                        {
                            StringBuilder text1 = new StringBuilder(text);
                            text1[j] = '-';
                            text = text1.ToString();
                            text = text.Remove(i, 1);
                            break;
                        }
                        else if (text[j] == '-')
                        {
                            StringBuilder text1 = new StringBuilder(text);
                            text1[j] = '+';
                            text = text1.ToString();
                            text = text.Remove(i, 1);
                            break;
                        }
                    }
                }
            }

            // ženklų taisymas
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == '-' && (text[i - 1] == '-' || text[i - 1] == '+'))
                {
                    if (text[i - 1] == '-')
                    {
                        StringBuilder text1 = new StringBuilder(text);
                        text1[i] = '+';
                        text = text1.ToString();
                        text = text.Remove(i - 1, 1);
                    }
                    else
                    {
                        StringBuilder text1 = new StringBuilder(text);
                        text1[i] = '-';
                        text = text1.ToString();
                        text = text.Remove(i - 1, 1);
                    }
                }
                else if (text[i] == '+' && (text[i - 1] == '-' || text[i - 1] == '+'))
                {
                    if (text[i - 1] == '-')
                    {
                        StringBuilder text1 = new StringBuilder(text);
                        text1[i] = '-';
                        text = text1.ToString();
                        text = text.Remove(i - 1, 1);
                    }
                    else
                    {
                        StringBuilder text1 = new StringBuilder(text);
                        text1[i] = '+';
                        text = text1.ToString();
                        text = text.Remove(i - 1, 1);
                    }
                }
            }

            if (text[0] == '-')
            {
                text = '0' + text;
            }

            return Calculate(text);
        } // kažko neleidžia protected daryti

        /// <summary>
        /// vidinė atskliaudėjo f-ja
        /// </summary>
        /// <param name="openIndex"></param>
        /// <param name="closeIndex"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private string ResolveBrackets(int openIndex, int closeIndex, string text)
        {
            string bracketAnswer = Calculate(text.Substring(openIndex + 1, closeIndex - openIndex - 1));

            return bracketAnswer;
        }

        /// <summary>
        /// vykdo aritmetinius veiksmus (jame randasi AddAndSubtract2)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string Calculate(string text)
        {
            double finalAnswer = AddAndSubstract2(text);

            return finalAnswer.ToString();

        }

        /// <summary>
        /// vykdo veiksmus su "+" ir "-"
        /// </summary>
        /// <param name="text1"></param>
        /// <returns></returns>
        private double AddAndSubstract2(string text1)
        {
            // neveikia su 5*(4*-7*7)
            // ir 5*(4*-7*7)
            string[] textTemp = text1.Split('-');
            for (int i = 0; i < textTemp.Length; i++)
            {
                textTemp[i] = "";
            }
            int count = 0;
            int senas = 0;
            bool happened = false;

            // prirasius 0 skliaustuose pradzioje gerai gana veikia (-1-3)
            if (text1[0] == '-')
            {
                text1 = "0" + text1;
            }

            // galima įmest kokį -- fixerį

            for (int i = 0; i < text1.Length; i++)
            {
                if (text1[i] == '-' && text1[i + 1] == '-')
                {
                    text1 = text1.Remove(i, 2);
                    text1 = text1.Insert(i, "+");
                }
                else if (text1[i] == '+' && text1[i + 1] == '-')
                {
                    text1 = text1.Remove(i, 2);
                    text1 = text1.Insert(i, "-");
                }
            }

            // 0+ netinka nes gaunasi 1+-...
            // text1 = "0+" + text1;
            for (int i = 0; i < text1.Length; i++)
            {
                if (text1[i] == '-')
                {
                    if ((i != 0) && (
                        (text1[i - 1] == '*') ||
                        (text1[i - 1] == '/')
                        ))
                    {
                        //buvo = false;
                    }
                    else
                    {
                        Console.WriteLine(senas + " " + i);
                        string tempTXT = text1.Substring(senas, i - senas);
                        char senolis = text1[senas];

                        // šita vieta dažnai lemia minuso pametimą
                        if ((text1[senas] == '-') && (tempTXT != ""))
                        {
                            textTemp[count++] = text1.Substring(senas + 1, i - senas - 1);
                            senas = i;
                            happened = true;
                        }
                        else if (tempTXT != "")
                        {
                            textTemp[count++] = text1.Substring(senas, i - senas);
                            senas = i;
                            happened = true;
                        }
                    }
                }
            }
            // if(!buvo)
            // jei net nebuvo splitintas
            if (happened)
            {
                string senolis = text1.Substring(senas + 1);
                textTemp[count++] = text1.Substring(senas + 1);
            }
            else
            {
                // tinkamas testui 1+4*(5+77*-4+1*-1)
                textTemp[count++] = text1;
            }

            string[] text = new string[count];

            for (int i = 0; i < count; i++)
            {
                text[i] = textTemp[i];
            }

            List<string> textList = new List<string>();

            for (int i = 0; i < text.Length; i++)
            {
                textList.Add(text[i]);
                if (i != text.Length - 1)
                {
                    textList.Add("-");
                }
            }
            // cia neturi buti neigiamo skaiciaus pries nes jo netikrina
            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].Contains('+') && textList[i].Length > 1)
                {
                    string[] textPart = textList[i].Split('+');

                    textList.RemoveAt(i);

                    for (int j = textPart.Length - 1; j >= 0; j--)
                    {
                        textList.Insert(i, textPart[j]);
                        if (j != 0)
                        {
                            textList.Insert(i, "+");
                        }
                    }

                }
            }
            // čia tas buvo 5 | + | 7*4

            double total;
            if (textList[0].Contains('*') || textList[0].Contains('/'))
            {

                total = DivideAndMultiply2(textList[0]);
            }
            else
            {
                double.TryParse(textList[0], out total);
            }

            for (int i = 2; i < textList.Count; i += 2)
            {
                if (textList[i - 1] == "-")
                {
                    total = total - DivideAndMultiply2(textList[i]);
                }
                else if (textList[i - 1] == "+")
                {
                    total = total + DivideAndMultiply2(textList[i]);
                }
            }

            return (double) total;
        }

        /// <summary>
        /// vykdo veiksmus su "*" ir "/"
        /// </summary>
        /// <param name="text1"></param>
        /// <returns></returns>
        private double DivideAndMultiply2(string text1)
        {
            string[] text = text1.Split('*');
            List<string> textList = new List<string>();

            for (int i = 0; i < text.Length; i++)
            {
                textList.Add(text[i]);
                if (i != text.Length - 1)
                {
                    textList.Add("*");
                }
            }

            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].Contains('/') && textList[i].Length > 1)
                {
                    string[] textPart = textList[i].Split('/');

                    textList.RemoveAt(i);

                    for (int j = textPart.Length - 1; j >= 0; j--)
                    {
                        textList.Insert(i, textPart[j]);
                        if (j != 0)
                        {
                            textList.Insert(i, "/");
                        }
                    }

                }
            }

            string convertTo = textList[0];

            textList = textList;

            double totalDouble = 0;
            bool totalDoubleNeeded = false;
            decimal total = Convert.ToDecimal(textList[0]);
            for (int i = 2; i < textList.Count; i += 2)
            {
                if (textList[i - 1] == "/")
                {
                    convertTo = textList[i];
                    if (!double.IsInfinity((double)total / (double)Convert.ToDecimal(textList[i])) && !(double.IsNaN((double)total / (double)Convert.ToDecimal(textList[i]))))
                    {
                        total = total / Convert.ToDecimal(textList[i]);
                    }
                    else if (double.IsNaN((double)total / (double)Convert.ToDecimal(textList[i])))
                    {
                        // total = -1000000000000000000;
                        totalDoubleNeeded = true;
                        totalDouble = (double) total / Convert.ToDouble(textList[i]);
                    }
                    else if (double.IsInfinity((double)total / (double)Convert.ToDecimal(textList[i])))
                    {
                        // pakankamai aukštas skaičius, jei vyksta dalyba iš 0;
                        // total = 1000000000000000000;
                        totalDoubleNeeded = true;
                        totalDouble = (double)total / Convert.ToDouble(textList[i]);
                    }
                }
                else if (textList[i - 1] == "*")
                {
                    total = total * Convert.ToDecimal(textList[i]);
                }
            }

            if (totalDoubleNeeded)
                return totalDouble;

            // decimal kokstotal = total;
            else
                return (double) total;
        }

    }
}