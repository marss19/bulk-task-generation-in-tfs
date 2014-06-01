using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marss.TasksGenerator
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public static void Show(Form parent)
        {
            var frm = new AboutForm();
            frm.lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version;
            frm.ShowDialog(parent);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:mykola.tarasyuk@live.com");
        }
        
    }
}
