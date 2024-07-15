using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace projedeneme
{
    public partial class IncomeExpense : UserControl
    {
        public IncomeExpense()
        {
            InitializeComponent();
            LoadDataAsync();
            RoundCorners(label1);
            RoundCorners(label2);
            RoundCorners(label15);
            RoundCorners(label6);
            RoundCorners(label7);
            RoundCorners(label8);
            RoundCorners(label9);
            RoundCorners(label10);
            RoundCorners(label11);
            RoundCorners(dataGridView1);
            RoundCorners(dataGridView2);
            RoundCorners(dataGridView3);

        }

        private async Task LoadDataAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080/");

                    HttpResponseMessage responseLatest = await client.GetAsync("daily-profit/latest");
                    responseLatest.EnsureSuccessStatusCode();
                    string responseBodyLatest = await responseLatest.Content.ReadAsStringAsync();
                    var dataLatest = JsonConvert.DeserializeObject<DailyProfit>(responseBodyLatest);

                    HttpResponseMessage responseLast30Days = await client.GetAsync("daily-profit/last-30-days");
                    responseLast30Days.EnsureSuccessStatusCode();
                    string responseBodyLast30Days = await responseLast30Days.Content.ReadAsStringAsync();
                    var dataLast30Days = JsonConvert.DeserializeObject<List<DailyProfit>>(responseBodyLast30Days);

                    HttpResponseMessage responseLast12Months = await client.GetAsync("monthly-profit/last-12-months");
                    responseLast12Months.EnsureSuccessStatusCode();
                    string responseBodyLast12Months = await responseLast12Months.Content.ReadAsStringAsync();
                    var dataLast12Months = JsonConvert.DeserializeObject<List<MonthlyProfit>>(responseBodyLast12Months);

                    DataTable dtLatest = new DataTable();
                    dtLatest.Columns.Add("Gelir", typeof(decimal));
                    dtLatest.Columns.Add("Maliyet", typeof(decimal));
                    dtLatest.Columns.Add("Kâr", typeof(decimal));
                    dtLatest.Columns.Add("Tarih", typeof(DateTime));

                    DataRow rowLatest = dtLatest.NewRow();
                    rowLatest["Gelir"] = dataLatest.Income;
                    rowLatest["Maliyet"] = dataLatest.Expense;
                    rowLatest["Kâr"] = dataLatest.Profit;
                    rowLatest["Tarih"] = dataLatest.Date;
                    dtLatest.Rows.Add(rowLatest);

                    dataGridView1.DataSource = dtLatest;

                    DataTable dtLast30Days = new DataTable();
                    dtLast30Days.Columns.Add("Gelir", typeof(decimal));
                    dtLast30Days.Columns.Add("Maliyet", typeof(decimal));
                    dtLast30Days.Columns.Add("Kâr", typeof(decimal));
                    dtLast30Days.Columns.Add("Tarih", typeof(DateTime));

                    decimal totalIncomeLast30Days = 0;
                    decimal totalExpenseLast30Days = 0;
                    decimal totalProfitLast30Days = 0;

                    foreach (var dailyProfit in dataLast30Days)
                    {
                        DataRow row = dtLast30Days.NewRow();
                        row["Gelir"] = dailyProfit.Income;
                        row["Maliyet"] = dailyProfit.Expense;
                        row["Kâr"] = dailyProfit.Profit;
                        row["Tarih"] = dailyProfit.Date;
                        dtLast30Days.Rows.Add(row);

                        totalIncomeLast30Days += dailyProfit.Income;
                        totalExpenseLast30Days += dailyProfit.Expense;
                        totalProfitLast30Days += dailyProfit.Profit;
                    }

                    dataGridView2.DataSource = dtLast30Days;

                    DataTable dtLast12Months = new DataTable();
                    dtLast12Months.Columns.Add("Gelir", typeof(decimal));
                    dtLast12Months.Columns.Add("Maliyet", typeof(decimal));
                    dtLast12Months.Columns.Add("Kâr", typeof(decimal));
                    dtLast12Months.Columns.Add("Tarih", typeof(string));

                    decimal totalIncomeLast12Months = 0;
                    decimal totalExpenseLast12Months = 0;
                    decimal totalProfitLast12Months = 0;

                    foreach (var monthlyProfit in dataLast12Months)
                    {
                        DataRow row = dtLast12Months.NewRow();
                        row["Gelir"] = monthlyProfit.Income;
                        row["Maliyet"] = monthlyProfit.Expense;
                        row["Kâr"] = monthlyProfit.Profit;
                        row["Tarih"] = monthlyProfit.Month;
                        dtLast12Months.Rows.Add(row);

                        totalIncomeLast12Months += monthlyProfit.Income;
                        totalExpenseLast12Months += monthlyProfit.Expense;
                        totalProfitLast12Months += monthlyProfit.Profit;
                    }

                    dataGridView3.DataSource = dtLast12Months;


                    label6.Text = totalIncomeLast30Days.ToString();
                    label7.Text = totalExpenseLast30Days.ToString();
                    label8.Text = totalProfitLast30Days.ToString();

                    label9.Text = totalIncomeLast12Months.ToString();
                    label10.Text = totalExpenseLast12Months.ToString();
                    label11.Text = totalProfitLast12Months.ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Veri çekme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void RoundCorners(Control control)
        {
            int cornerRadius = 5;
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }
    }

    public class DailyProfit
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit { get; set; }
        public DateTime Date { get; set; }
    }

    public class MonthlyProfit
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit { get; set; }
        public string Month { get; set; }
    }
}
