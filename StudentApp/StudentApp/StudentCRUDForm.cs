using StudentApp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentApp
{
    public partial class StudentCRUDForm : Form
    {
        private static string conStr = ConfigurationManager.ConnectionStrings["ProjectDbContext"].ToString();
        private static SqlConnection con = new SqlConnection(conStr);
        private SqlCommand cmd = new SqlCommand("", con);
        public StudentCRUDForm()
        {
            InitializeComponent();
            LoadDistrictData();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student
                {
                    Name = nameTextBox.Text,
                    Address = addressRichTextBox.Text,
                    RegNo = regNoTextBox.Text,
                    SubDistrictId = Convert.ToInt32(subDistrictComboBox.SelectedValue)
                };

                bool isSaved = Save(student);
                if (isSaved)
                {
                    ReloadStudentGridView();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }



        public void LoadDistrictData()
        {
            districtBindingSource.DataSource = null;

            string query = @"SELECT * FROM Districts";
            cmd.CommandText = query;
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            districtBindingSource.DataSource = dt;
            con.Close();
        }


        public void LoadSubDistrictData(int? districtId = null)
        {
            subDistrictBindingSource.DataSource = null;
            string query = "";

            if (districtId > 0)
            {
                query = @"SELECT * FROM SubDistricts WHERE DistrictId=" + districtId + "";
            }
            else
            {
                query = @"SELECT * FROM SubDistricts";
            }


            cmd.CommandText = query;
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            subDistrictBindingSource.DataSource = dt;
            con.Close();
        }

        private void districtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int districtId = Convert.ToInt32(districtComboBox.SelectedValue);
            if (districtId > 0)
            {
                LoadSubDistrictData(districtId);
            }
        }


        public bool Save(Student student)
        {

            var isRegNoExist = IsRegNoExist(student.RegNo);
            if (!isRegNoExist)
            {
                string query = @"INSERT INTO Students (Name,Address,SubDistrictId) VALUES ('" + student.Name + "','" + student.Address + "', '" + student.SubDistrictId + "')";
                cmd.CommandText = query;
                con.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowAffected > 0)
                {
                    return true;
                }
            }
            else
            {
                throw new Exception("Sorry! This Reg No Is Already Exist");
            }

            return false;
        }

        public void ReloadStudentGridView()
        {
            studentBindingSource.DataSource = null;

            string query = @"SELECT * FROM Students";
            cmd.CommandText = query;
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            studentBindingSource.DataSource = dt;
            con.Close();
        }



        public bool IsRegNoExist(string regNo)
        {
            string query = @"SELECT * FROM Students WHERE RegNo='" + regNo + "'";
            cmd.CommandText = query;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() && dr.HasRows)
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string regNo = regNoTextBox.Text;
                if (string.IsNullOrEmpty(regNo))
                {
                   throw new Exception("Sorry! No Reg No Found");
                }

                bool isRegNoIsValid = IsRegNoExist(regNo);
                if (isRegNoIsValid==false)
                {
                    throw new Exception("Sorry! No Student Found Against This Reg No");
                }

                string query = @"DELETE FROM Students WHERE RegNo='" + regNo + "'";
                cmd.CommandText = query;
                con.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    MessageBox.Show("Deleted Successfully");
                }
                else
                {
                    MessageBox.Show("Deleted Failed");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            studentVmBindingSource.DataSource = null;

            string query = @"SELECT * FROM VW_GetStudentInfo";
            cmd.CommandText = query;
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            studentVmBindingSource.DataSource = dt;
            con.Close();
        }
    }
}
