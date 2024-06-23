using System;
using System.Windows.Forms;

namespace projedeneme
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void yeniButton1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            KitchenDisplayUserControl kitchenDisplayUserControl = new KitchenDisplayUserControl();

            kitchenDisplayUserControl.Size = panel1.Size;

            panel1.Controls.Add(kitchenDisplayUserControl);
        }

        private void yeniButton2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            CaseScreen caseScreen = new CaseScreen();

            caseScreen.Size = panel1.Size;

            panel1.Controls.Add(caseScreen);
        }

        private void yeniButton3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            MenuSetings menuSetings = new MenuSetings();

            menuSetings.Size = panel1.Size;

            panel1.Controls.Add(menuSetings);
        }

        private void yeniButton4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            IncomeExpense incomeExpense = new IncomeExpense();

            incomeExpense.Size = panel1.Size;

            panel1.Controls.Add(incomeExpense);
        }

        private void yeniButton5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Product product = new Product();

            product.Size = panel1.Size;

            panel1.Controls.Add(product);
        }

        private void yeniButton6_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Settings settings = new Settings();

            settings.Size = panel1.Size;

            panel1.Controls.Add(settings);
        }

        private void yeniButton7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
