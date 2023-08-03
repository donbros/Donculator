using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// Bendriausia klasė
    /// </summary>
    public class GeneralClass
    {

        public readonly SqlConnection con = new SqlConnection(@"Server=NET138\SQLEXPRESS;Database=CalculatorDatabase; Trusted_Connection=Yes; Max Pool Size=200");

        /// <summary>
        /// prisijungimas prie MS SQL db !PRISIJUNGIMAS NAUDOJIMO METU VISADA ATVIRAS!
        /// </summary>
        public void ConnectionState()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        /// <summary>
        /// ištaiso kai kuriuos perteklinius ženklus arba situacijas, kurios algoritmas nesupranta
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string operationSignsFixer1(string text)
        {

            for (int i = 0; i < text.Length; i++)
            {
                if (i - 1 >= 0)
                {
                    if (text[i] == '-' && (text[i - 1] == '-' || text[i - 1] == '+'))
                    {
                        // - ir - = + bei + ir - = -
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
                    // - perkėlimas
                    else if (i - 2 >= 0 && text[i] == '-' && text[i - 1] == '(' && text[i - 2] == '+') // buvo "i - 2 > 0" prieš tai
                    {
                        StringBuilder text1 = new StringBuilder(text);
                        text1[i - 2] = '-';
                        text = text1.ToString();
                        text = text.Remove(i, 1);
                    }
                    // iš "-(-" į su nariu "-1*(-"
                    else if (i - 2 >= 0 && text[i] == '-' && text[i - 1] == '(' && text[i - 2] == '-')
                    {
                        text = text.Insert(i - 1, "1*");
                        i += 2;
                    }
                    // "+" po daugybos ar dalybos trynimas
                    else if (text[i] == '+' && (text[i - 1] == '*' || text[i - 1] == '/'))
                    {
                        text = text.Remove(i, 1);
                    }
                }
            }

            return text;
        }
    }
}