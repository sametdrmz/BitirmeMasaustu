using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace projedeneme
{
    public partial class CloseTableScreen : Form
    {
        public int tableId { get; set; }
        private double income = 0;
        private double expense = 0;

        public CloseTableScreen()
        {
            InitializeComponent();
            this.Load += CloseTableScreen_Load;
            RoundCorners(label1);
            RoundCorners(dataGridView1);
        }

        private async void CloseTableScreen_Load(object sender, EventArgs e)
        {
            await DisplayTableOrders();
        }

        public async Task DisplayTableOrders()
        {
            income = 0;
            expense = 0;
            double totalPriceSum = 0; 

            string apiUrl = $"http://localhost:8080/orders/{tableId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(responseBody);

                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Quantity", "Adet");
                        dataGridView1.Columns.Add("TotalPrice", "Toplam Fiyat");
                        dataGridView1.Columns.Add("UnitPrice", "Birim Fiyatı");
                        dataGridView1.Columns.Add("MenuName", "Menü Adı");

                        dataGridView1.Rows.Clear();

                        foreach (Order order in orders)
                        {
                            if (order.Status != "iptal" && order.Status != "gosterme")
                            {
                                foreach (OrderDetail detail in order.OrderDetails)
                                {
                                    dataGridView1.Rows.Add(detail.Quantity, detail.TotalPrice, detail.UnitPrice, detail.MenuName);
                                    income += (double)detail.TotalPrice;
                                    expense += (double)detail.Expense;
                                    
                                    totalPriceSum += (double)detail.TotalPrice;
                                }
                            }
                        }
                        
                        label1.Text = $"Toplam Fiyat: {totalPriceSum}";
                    }
                    else
                    {
                        //MessageBox.Show("API'den veri alınamadı.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private async Task SendIncomeAndExpense(double income, double expense)
        {
            try
            {
                
                string apiUrl = "http://localhost:8080/createIncomeAndExpense";

                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("income", income.ToString()),
                    new KeyValuePair<string, string>("expense", expense.ToString())
                });

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(apiUrl, formData);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private async void CloseTable()
        {
            try
            {
                string apiUrl = $"http://localhost:8080/closeTable?tableId={tableId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Masa başarıyla kapatıldı. Ödeme tamamlandı.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Masa kapatılamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           
            await DisplayTableOrders();
           
            await SendIncomeAndExpense(income, expense);

            CloseTable();
            this.Close();
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

    public class Order
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalExpense { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string OrderNote { get; set; }
        public decimal Expense { get; set; }
    }
}
