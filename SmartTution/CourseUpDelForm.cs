using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTution
{
    public partial class CourseUpDelForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source =DESKTOP-0DAQ0NS\SOWROV;Initial Catalog = SmartTutionDB;Integrated Security = True;");
        public CourseUpDelForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text!= "")
            {
                

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Courses Set CourseName=@n where CourseId=@i";
                cmd.Parameters.AddWithValue("@i", txtId.Text);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Course Updated Sucessfully";
                conn.Close();
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Courses Set CourseName=@n where CourseId=@i";
                cmd.Parameters.AddWithValue("@i", txtId.Text);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Course Updated Sucessfully";
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from Teachers where TeacherId=@i", conn);
            cmd.Parameters.AddWithValue("@i", txtId.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Course Sucessfully Deleted";
            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select CourseName From Courses Where CourseId=" + txtId.Text + "", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtId.Text = dt.Rows[0][0].ToString();
                txtName.Text = dt.Rows[0][1].ToString();

            }
            else
            {
                lblMsg.ForeColor = Color.Navy;
                lblMsg.Text = "Course Name Not Found";

            }
            conn.Close();
        }
    }
}
