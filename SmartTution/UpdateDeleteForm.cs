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
    public partial class UpdateDeleteForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source =DESKTOP-0DAQ0NS\SOWROV;Initial Catalog = SmartTutionDB;Integrated Security = True;");
        public UpdateDeleteForm()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtImage.Text = openFileDialog1.FileName;
            }
        }
        private void LoadCombo()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Courses", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbCourse.DataSource = ds.Tables[0];
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseId";
        }

        private void UpdateDeleteForm_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select TeacherId,TeacherName,ContactNo,TeacherEmail,Picture,CourseId,JoiningDate From Teachers Where TeacherId=" + txtId.Text + "", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0][1].ToString();           
                txtContact.Text = dt.Rows[0][2].ToString();
                txtEmail.Text = dt.Rows[0][3].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][4]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
                cmbCourse.SelectedValue = dt.Rows[0][5].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0][6].ToString());
            }
            else
            {
                lblMsg.ForeColor = Color.Teal;
                lblMsg.Text = "Data Not Found";

            }
            conn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtImage.Text != "")
            {
                Image img = Image.FromFile(txtImage.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Teachers Set TeacherName=@n,ContactNo=@c,TeacherEmail=@e,Picture=@p,CourseId=@s,JoiningDate=@d where TeacherId=@i";
                cmd.Parameters.AddWithValue("@i", txtId.Text);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtContact.Text);
                cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
                cmd.Parameters.AddWithValue("@s", cmbCourse.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Sucessfully";
                conn.Close();
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Teachers Set TeacherName=@n,ContactNo=@c,TeacherEmail=@e,CourseId=@s,JoiningDate=@d where TeacherId=@i";
                cmd.Parameters.AddWithValue("@i", txtId.Text);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtContact.Text);
                cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                cmd.Parameters.AddWithValue("@s", cmbCourse.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated Sucessfully";
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from Teachers where TeacherId=@i", conn);
            cmd.Parameters.AddWithValue("@i", txtId.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Sucessfully Deleted";
            conn.Close();
        }
    }
}
