using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
namespace SmartTution
{
    public partial class CourseEntryFrm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source =DESKTOP-0DAQ0NS\SOWROV;Initial Catalog = SmartTutionDB;Integrated Security = True;");
        public CourseEntryFrm()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        { 
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Courses Values('"+txtName.Text+"')",conn);
            cmd.ExecuteNonQuery();
            lblCourse.Text = "Subject Added Sucessfully";
            LoadGrid();
            txtName.Text = "";
            conn.Close();

        }

        private void CourseEntryFrm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        { 
            SqlDataAdapter Da = new SqlDataAdapter("Select * From Courses", conn);
            DataTable dt = new DataTable();
            Da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
 