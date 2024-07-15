using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;
using System.Xml.Linq;

namespace projedeneme
{
    public partial class Product : UserControl
    {
        private List<int> productIds;
        private readonly string apiUrl = "http://localhost:8080/getProductsWithUsageAmount";
        public Product()
        {
            InitializeComponent();
            productIds = new List<int>();
            LoadProductsAsync();
            InitializeDataGridView();
            LoadDataFromApi();
            RoundCorners(label1);
            RoundCorners(label2);
            RoundCorners(label4);
            RoundCorners(label6);
            RoundCorners(label8);
            RoundCorners(label10);
            RoundCorners(panel2);
            RoundCorners(panel3);
            RoundCorners(panel5);
            RoundCorners(panel6);
            RoundCorners(dataGridView1);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string productName = textBox2.Text;
            string quantity = textBox3.Text;

            if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(quantity))
            {
                MessageBox.Show("Lütfen geçerli bilgiler giriniz!");
            }

            else
            {
                if (IsValidInput(quantity, out string errorMessage))
                {
                    var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("productName", productName),
                    new KeyValuePair<string, string>("quantity", quantity)
                });

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("http://localhost:8080/createProduct", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Ürün başarıyla oluşturuldu!");
                            textBox2.Text = "";
                            textBox3.Text = "";
                            await LoadProductsAsync();
                            InitializeDataGridView();
                            LoadDataFromApi();
                        }
                        else
                        {
                            //MessageBox.Show("Hata oluştu: " + response.ReasonPhrase);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir miktar giriniz!");
                }               
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex > 0)
            {
                int selectedProductId = productIds[comboBox3.SelectedIndex - 1];

                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"http://localhost:8080/deleteProduct?productId={selectedProductId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ürün başarıyla silindi!");
                        await LoadProductsAsync();
                        InitializeDataGridView();
                        LoadDataFromApi();
                    }
                    else
                    {
                        //MessageBox.Show("Hata oluştu: " + response.ReasonPhrase);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için geçerli bir ürün seçin.");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                int selectedProductId = productIds[comboBox1.SelectedIndex - 1];
                string amount = textBox7.Text;

                if (IsValidInput(amount, out string errorMessage))
                {
                    var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("productId", selectedProductId.ToString()),
                    new KeyValuePair<string, string>("amount", amount)
                });

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("http://localhost:8080/increaseProductQuantity", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Ürün miktarı başarıyla artırıldı!");
                            textBox7.Text = "";
                            await LoadProductsAsync();
                            InitializeDataGridView();
                            LoadDataFromApi();
                        }
                        else
                        {
                            //MessageBox.Show("Hata oluştu: " + response.ReasonPhrase);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir miktar giriniz!");
                }               
            }
            else
            {
                MessageBox.Show("Lütfen miktarı artırmak için geçerli bir ürün seçin.");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > 0)
            {
                int selectedProductId = productIds[comboBox2.SelectedIndex - 1];
                string amount = textBox1.Text;

                if (IsValidInput(amount, out string errorMessage))
                {
                    var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("productId", selectedProductId.ToString()),
                    new KeyValuePair<string, string>("usageAmount", amount)
                });

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("http://localhost:8080/reduceProductQuantity", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Ürün miktarı başarıyla azaltıldı!");
                            textBox1.Text = "";
                            await LoadProductsAsync();
                            InitializeDataGridView();
                            LoadDataFromApi();
                        }
                        else
                        {
                            //MessageBox.Show("Hata oluştu: " + response.ReasonPhrase);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir miktar giriniz!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen miktarı azaltmak için geçerli bir ürün seçin.");
            }
        }

        private async Task LoadProductsAsync()
        {
            productIds.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:8080/getProducts");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<ProductData>>(jsonString);

                    foreach (var product in products)
                    {
                        comboBox1.Items.Add(product.Name);
                        comboBox2.Items.Add(product.Name);
                        comboBox3.Items.Add(product.Name);
                        productIds.Add(product.Id);
                    }
                }
                else
                {
                    //MessageBox.Show("Ürünler getirilirken hata oluştu: " + response.ReasonPhrase);
                }
            }

            comboBox1.Items.Insert(0, "Ürünler");
            comboBox2.Items.Insert(0, "Ürünler");
            comboBox3.Items.Insert(0, "Ürünler");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private async void LoadDataFromApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<ProductDataGrid> products = JsonConvert.DeserializeObject<List<ProductDataGrid>>(json);

                        dataGridView1.Rows.Clear();

                        foreach (var product in products)
                        {
                            dataGridView1.Rows.Add(
                                product.productName,
                                product.productQuantity,
                                product.dailyUsageAmount,
                                product.weeklyUsageAmount,
                                product.monthlyUsageAmount,
                                product.estEndDay + " Gün"
                            );
                        }
                    }
                    else
                    {
                        //MessageBox.Show("API'den veri alınamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private bool IsValidInput(string input, out string errorMessage)
        {
            errorMessage = "";

            if (!decimal.TryParse(input, out decimal number))
            {
                errorMessage = "Giriş bir sayı olmalıdır.";
                return false;
            }

            if (number < 0)
            {
                errorMessage = "Giriş 0'dan küçük olamaz.";
                return false;
            }

            return true;
        }
        private void InitializeDataGridView()
        {
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Ürün Adı";
            dataGridView1.Columns[1].Name = "Miktar";
            dataGridView1.Columns[2].Name = "Günlük O.K.M.";
            dataGridView1.Columns[3].Name = "Haftalık O.K.M.";
            dataGridView1.Columns[4].Name = "Aylık O.K.M.";
            dataGridView1.Columns[5].Name = "T. Kalan Gün";
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

        public class ProductDataGrid
        {
            public string productName { get; set; }
            public double productQuantity { get; set; }
            public double dailyUsageAmount { get; set; }
            public double weeklyUsageAmount { get; set; }
            public double monthlyUsageAmount { get; set; }
            public int estEndDay { get; set; }
        }

        public class ProductData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}