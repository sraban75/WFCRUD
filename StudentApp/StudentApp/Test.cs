using StudentApp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentApp
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void StudentSaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();

                student.Name = nameTextBox.Text;
                student.Address = addressRichTextBox.Text;
                student.SubDistrictId = Convert.ToInt32(subDistrictId.Text);
                

                //string conStr = @"Server=SRABAN-CST; Database=StudentDb; Integrated Secuirity=true";

                //Connection String
                string conString = ConfigurationManager.ConnectionStrings["ProjectDbContext"].ToString();

                //Sql Connection

                SqlConnection con = new SqlConnection(conString);
                //con.ConnectionString = conString;


                // Query
                string query = @"INSERT INTO Students (Name,Address,SubDistrictId) VALUES ('" + student.Name + "','" + student.Address + "', '" + student.SubDistrictId + "')";


                //SQL Command
                SqlCommand cmd = new SqlCommand(query, con);



                //Connection Open
                con.Open();

                // Execute
                int rowAffected = cmd.ExecuteNonQuery();

                //Connection Close
                con.Close();

                if (rowAffected > 0)
                {
                    MessageBox.Show("Saved");
                }
                else
                {
                    MessageBox.Show("Failed");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }
    }
}
