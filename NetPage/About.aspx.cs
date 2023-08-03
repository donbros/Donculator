// Domantas Bronušas 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Calculator
{
    public partial class About : Page
    {
        SqlConnection con;

        // ****************************** FORM METHODS ********************************

        protected void Page_Load(object sender, EventArgs e)
        {
            GeneralClass generalClass = new GeneralClass();
            con = generalClass.con;

            ConnectionState();

            ReloadHistory();

            if (bool.Parse(HiddenField8.Value))
            {
                Label2.Text = "";
                HiddenField8.Value = false.ToString();
            }
        }

        // Gridview vaizdavimas
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

        // paieška pagal id
        protected void Button8_Click(object sender, EventArgs e)
        {
            int id;
            if ((int.TryParse(TextBox2.Text, out id)))
            {
                string idLine = "ID=" + id;
                string columnsToShow = "*";
                SearchBy(idLine, columnsToShow);
            }
            else
            {
                HiddenField8.Value = true.ToString();
                Label2.Text = "error";
            }
        }

        // trynimas pagal id
        protected void Button10_Click(object sender, EventArgs e)
        {
            int id;
            if ((int.TryParse(TextBox2.Text, out id)))
            {
                string idLine = "ID=" + id;
                RemoveWhere(idLine);
            }
            else
            {
                HiddenField8.Value = true.ToString();
                Label2.Text = "error";
            }
        }

        // paieška pagal rezultatą
        protected void Button9_Click(object sender, EventArgs e)
        {
            string result = TextBox3.Text;
            //double result;
            //if ((double.TryParse(TextBox3.Text, out result)))
            //{
            // result = result.ToString().Replace(',', '.'); // Formatavimas
            if (result != "")
            {
                string resultLine = "result='" + result + "'";
                string columnsToShow = "*";
                SearchBy(resultLine, columnsToShow);
            }
            else
            {
                HiddenField8.Value = true.ToString();
                Label2.Text = "error";
            }
        }

        // trynimas pagal rezultatą
        protected void Button11_Click(object sender, EventArgs e)
        {
            string result = TextBox3.Text;
            //double result;
            //if ((double.TryParse(TextBox3.Text, out result)))
            //{
            if(result != "")
            {
                // string result = result.ToString().Replace(',', '.'); // Formatavimas
                string resultLine = "result=" + result;
                RemoveWhere(resultLine);
            }
            else
            {
                HiddenField8.Value = true.ToString();
                Label2.Text = "error";
            }
        }

        /// <summary>
        /// Reset table to show all result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button12_Click(object sender, EventArgs e)
        {
            //GeneralClass generalClass = new GeneralClass();
            GridView1.DataSource = DispData();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            createAccordianUsingRepeater();
        }

        // ****************************** METHODS ********************************

        /// <summary>
        /// PRISIJUNGIMAS PRIE MS SQL db !PRISIJUNGIMAS NAUDOJIMO METU VISADA ATVIRAS!
        /// </summary>
        private void ConnectionState()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        void ReoloadHistory()
        {
            GeneralClass generalClass = new GeneralClass();
            GridView1.DataSource = DispData();
            GridView1.DataBind();
        }

        private DataTable DispData()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Calculations";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// ieškojimas eilučių db pagal nurodytą stulp.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="columns"></param>
        private void SearchBy(string line, string columns)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            // šiuo atveju visi column
            cmd.CommandText = "select " + columns + " from Calculations WHERE " + line;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        /// <summary>
        /// pašalinami nurodyti duomenys pagal pasirinktą stulpelį duomenų bazėje
        /// </summary>
        /// <param name="line"></param>
        private void RemoveWhere(string line)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            // šiuo atveju visi column
            cmd.CommandText = "delete from Calculations WHERE " + line;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            // būtų gerai kokį pranešimą atspausdinti
        }

        public void createAccordianUsingRepeater()
        {
            //GridView1.DataSource = createDataTable();
            GridView1.DataBind();
        }

        public void HistoryShow(bool show)
        {
            if (show)
            {
                HiddenField7.Value = show.ToString();
                GridView1.Visible = show;
                Button7.Text = "Hide History";
            }
            else
            {
                HiddenField7.Value = show.ToString();
                GridView1.Visible = show;
                Button7.Text = "Show History";
            }
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
    }
}