﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Mobile_Banking
{
    public partial class Form17 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public static string Personal_Number = null;
        public String x = Form1.n;

        public Form17()
        {
            InitializeComponent(); 
            BindGridView();
            Personal_Number = x;
        }
        
        private void Form15_Load(object sender, EventArgs e)
        {

        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from HISTORY_TABLE where SENDER='" + Personal_Number + "' OR RECEIVER='" + Personal_Number + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form13 obj = new Form13();
            obj.Show();
            this.Hide();
            Personal_Number = null;
        }

        private void Form17_Load(object sender, EventArgs e)
        {

        }
    }
}
