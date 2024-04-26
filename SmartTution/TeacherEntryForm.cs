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
    public partial class TeacherEntryForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source =DESKTOP-0DAQ0NS\SOWROV;Initial Catalog = SmartTutionDB;Integrated Security = True;");
        public TeacherEntryForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtImage.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtImage.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Insert Into Teachers(TeacherId,TeacherName,ContactNo,TeacherEmail,Picture,CourseId,JoiningDate) Values(@i,@n,@c,@e,@p,@s,@d)";
            cmd.Parameters.AddWithValue("@i", txtId.Text);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@c", txtContact.Text);
            cmd.Parameters.AddWithValue("@e", txtEmail.Text);
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@s", cmbCourse.SelectedValue);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Insert successfully done";
            conn.Close();

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtImage.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            dateTimePicker2.Text = "";
            cmbCourse.SelectedIndex = -1;
        }

        private void TeacherEntryForm_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From Courses", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbCourse.DataSource = ds.Tables[0];
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseId";
            conn.Close();
        }


    }
}
