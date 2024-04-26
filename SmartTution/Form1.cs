using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void courseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CourseEntryFrm Cef = new CourseEntryFrm();
            Cef.Show();
            

        }

        private void teacherEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherEntryForm Tef= new TeacherEntryForm();
            Tef.Show();
            
        }

        private void updateDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateDeleteForm udf = new UpdateDeleteForm();
            udf.Show();
        }

        private void updateDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseUpDelForm cuf= new CourseUpDelForm();
            cuf.Show();
        }

        private void tutionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TutionReportForm Trf = new TutionReportForm();
            Trf.Show();
        }
    }
}
