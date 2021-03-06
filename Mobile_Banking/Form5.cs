using System;
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
    public partial class Form5 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public static string Personal_Number = null;
        public String x = Form1.n;
        Double Ppn = 0, Aan = 0, A = 0;
        float Pn = 0, An = 0;
        String Agent_Number;
        String Amount;

        public Form5()
        {
            InitializeComponent();
            Personal_Number = x;
        } 

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Please Enter valid number");
            }
            else { errorProvider1.Clear(); }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Please Enter Amount");
            }
            else { errorProvider2.Clear(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agent_Number = textBox1.Text;
            Amount = textBox2.Text;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);

                String qurey4 = " Select mobile_number from user_information  where mobile_number=@mobile_number and USER_TYPE=@USER_TYPE ";
                SqlCommand cmd4 = new SqlCommand(qurey4, con);
                cmd4.Parameters.AddWithValue("@mobile_number", textBox1.Text);
                cmd4.Parameters.AddWithValue("@USER_TYPE", "Agent");
                con.Open();
                bool m = Personal_Number != Agent_Number;
                if (cmd4.ExecuteScalar() != null&&m)
                {
                    SqlCommand cmd = new SqlCommand(" Select balance from user_information where mobile_number='" + Personal_Number + " ' ", con);

                    Ppn = Convert.ToDouble(cmd.ExecuteScalar().ToString());

                    SqlCommand cmd2 = new SqlCommand(" Select balance from user_information where mobile_number='" + Agent_Number + " ' ", con);
                    String A_Balance = cmd2.ExecuteScalar().ToString();

                    Aan = Convert.ToDouble(A_Balance);
                    A = Convert.ToDouble(Amount);
                    if (Ppn >= (A + A * .005))
                    {
                        Ppn = Ppn - (A+A*.005);
                        Aan = Aan + (A + A * .005);
                        Pn = (float)Ppn;
                        An = (float)Aan;

                        String qurey = " Update USER_INFORMATION set BALANCE=@Pn where mobile_number='" + Personal_Number + " ' ";
                        String qurey2 = " Update USER_INFORMATION set BALANCE=@An where mobile_number='" + Agent_Number + "'";
                        SqlCommand cmd3 = new SqlCommand(qurey, con);
                        SqlCommand cmd5 = new SqlCommand(qurey2, con);

                        cmd3.Parameters.AddWithValue("@Pn", Pn);
                        cmd5.Parameters.AddWithValue("@An", An);


                        int a = cmd3.ExecuteNonQuery();
                        int b = cmd5.ExecuteNonQuery();

                        if (a > 0 && b > 0)
                        {
                            String qurey5 = " Insert into HISTORY_TABLE values  (@DATE,@TYPE,@SENDER,@RECEIVER,@AMOUNT)";
                            SqlCommand cmd6 = new SqlCommand(qurey5, con);

                            cmd6.Parameters.AddWithValue("@DATE", dateTimePicker2.Value);
                            cmd6.Parameters.AddWithValue("@TYPE", " Cash Out ");
                            cmd6.Parameters.AddWithValue("@SENDER", Personal_Number);
                            cmd6.Parameters.AddWithValue("@RECEIVER", Agent_Number);
                            cmd6.Parameters.AddWithValue("@AMOUNT", A);

                            int HISTORY = cmd6.ExecuteNonQuery();
                            MessageBox.Show("Cash Out Successful");
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Cash Out UnSuccessful");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Insufficient Balance ");
                        textBox1.Clear();
                        textBox2.Clear();

                    }
                }
                else { MessageBox.Show("Invalaid Mobile Number "); }


                con.Close();

            }
            else
            {
                MessageBox.Show("Please fill up the information");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.Show();
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
            
        }
    }
}
