using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projedeneme
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form4_Load);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            CaseScreen caseScreen = new CaseScreen();

            caseScreen.Size = panel1.Size;

            panel1.Controls.Add(caseScreen);
        }

        private void oKapat_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
