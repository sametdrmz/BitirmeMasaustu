using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projedeneme
{
    public partial class MenuSetings : UserControl
    {
        private readonly string apiUrl = "http://localhost:8080/categories";
        private readonly string createCategoryUrl = "http://localhost:8080/createCategory";
        private readonly string deleteCategoryUrlBase = "http://localhost:8080/deleteCategory";
        private readonly string updateCategoryUrl = "http://localhost:8080/updateCategory";
        private readonly string deleteMenuUrlBase = "http://localhost:8080/deleteMenu";
        private readonly string changeMenuStatusUrl = "http://localhost:8080/changeMenuStatus";

        private List<Category> categories = new List<Category>();
        private Dictionary<string, int> categoryDictionary = new Dictionary<string, int>();
        private Dictionary<string, int> menuDictionary = new Dictionary<string, int>();

        private int selectedCategoryId;
        private int selectedMenuId;

        public MenuSetings()
        {
            InitializeComponent();
            LoadCategories();
            LoadCategoriesToComboBox();
            LoadMenusToComboBox();
            RoundCorners(label1);
            RoundCorners(label5);
            RoundCorners(label6);
            RoundCorners(label7);
            RoundCorners(label11);
            RoundCorners(label15);
            RoundCorners(label16);
            RoundCorners(panel3);
            RoundCorners(panel4);
            RoundCorners(panel5);
            RoundCorners(panel6);
            RoundCorners(panel7);
            RoundCorners(panel8);
            RoundCorners(panel9);

            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged;
            comboBox5.SelectedIndexChanged += ComboBox5_SelectedIndexChanged;
        }

        private async void LoadCategories()
        {
            panel1.Controls.Clear();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<Category>>(responseBody);

                        int topMargin = 10;
                        foreach (var category in categories)
                        {
                            Label lblCategory = new Label
                            {
                                Text = category.Name,
                                AutoSize = false,
                                TextAlign = ContentAlignment.MiddleCenter,
                                ForeColor = Color.White,
                                Width = 130,
                                Height = 25,
                                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point),
                                Location = new System.Drawing.Point((panel1.Width - 150) / 2, topMargin)
                            };
                            RoundCorners(lblCategory);
                            panel1.Controls.Add(lblCategory);

                            DataGridView dgvMenu = new DataGridView
                            {
                                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                                BackgroundColor = Color.FromArgb(30, 30, 30),
                                BorderStyle = BorderStyle.None,
                                AllowDrop = false,
                                AllowUserToAddRows = false,
                                AllowUserToDeleteRows = false,
                                AllowUserToOrderColumns = false,
                                AllowUserToResizeColumns = false,
                                AllowUserToResizeRows = false,
                                RowHeadersVisible = false,
                                Location = new System.Drawing.Point(10, lblCategory.Location.Y + lblCategory.Height + 10),
                                Height = 150,
                                Width = panel1.Width - 40
                            };
                            RoundCorners(dgvMenu);
                            dgvMenu.Columns.Add("Name", "Menü Adı");
                            dgvMenu.Columns.Add("Price", "Fiyat");

                            foreach (var menu in category.Menus)
                            {
                                dgvMenu.Rows.Add(menu.Name, menu.Price);
                            }

                            panel1.Controls.Add(dgvMenu);

                            topMargin = dgvMenu.Location.Y + dgvMenu.Height + 10;
                        }
                    }
                    else
                    {
                        MessageBox.Show("API'den veri alınamadı.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private async void LoadCategoriesToComboBox()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox6.Items.Clear();
            categoryDictionary.Clear();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(responseBody);

                        if (categories.Count == 0)
                        {
                            comboBox1.Items.Add("Kategoriler");
                            comboBox2.Items.Add("Kategoriler");
                            comboBox3.Items.Add("Kategoriler");
                            comboBox6.Items.Add("Kategoriler");
                        }
                        else
                        {
                            comboBox1.Items.Add("Kategoriler");
                            comboBox2.Items.Add("Kategoriler");
                            comboBox3.Items.Add("Kategoriler");
                            comboBox6.Items.Add("Kategoriler");

                            foreach (var category in categories)
                            {
                                comboBox1.Items.Add(category.Name);
                                comboBox2.Items.Add(category.Name);
                                comboBox3.Items.Add(category.Name);
                                comboBox6.Items.Add(category.Name);
                                categoryDictionary.Add(category.Name, category.Id);
                            }
                        }

                        comboBox1.SelectedIndex = 0;
                        comboBox2.SelectedIndex = 0;
                        comboBox3.SelectedIndex = 0;
                        comboBox6.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("API'den veri alınamadı.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private async void LoadMenusToComboBox()
        {
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox7.Items.Clear();
            menuDictionary.Clear();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(responseBody);

                        comboBox4.Items.Add("Menüler");
                        comboBox5.Items.Add("Menüler");
                        comboBox7.Items.Add("Menüler");

                        foreach (var category in categories)
                        {
                            foreach (var menu in category.Menus)
                            {
                                comboBox4.Items.Add(menu.Name);
                                comboBox5.Items.Add(menu.Name);
                                comboBox7.Items.Add(menu.Name);
                                menuDictionary.Add(menu.Name, menu.Id);
                            }
                        }

                        comboBox4.SelectedIndex = 0;
                        comboBox5.SelectedIndex = 0;
                        comboBox7.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("API'den veri alınamadı.");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string categoryName = textBox1.Text;

                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    MessageBox.Show("Kategori adı boş geçilemez.");
                    return;
                }

                var apiUrl = "http://localhost:8080/createCategory";

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name", categoryName)
                });

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Kategori Oluşturuldu.");
                    LoadCategories();
                    LoadCategoriesToComboBox();
                    LoadMenusToComboBox();
                    textBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("API'ye veri gönderilirken bir hata oluştu: " + ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedCategoryName = comboBox1.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedCategoryName) || selectedCategoryName == "Kategoriler")
                {
                    MessageBox.Show("Lütfen silmek için bir kategori seçiniz.");
                    return;
                }

                if (!categoryDictionary.TryGetValue(selectedCategoryName, out int categoryId))
                {
                    MessageBox.Show("Kategori ID bulunamadı.");
                    return;
                }

                string deleteCategoryUrl = deleteCategoryUrlBase + "?categoryId=" + categoryId;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(deleteCategoryUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Kategori başarıyla silindi.");

                        LoadCategories();
                        LoadCategoriesToComboBox();
                        LoadMenusToComboBox();
                    }
                    else
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("Kategori silinemedi. Hata: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Kategori silinirken hata oluştu: " + ex.Message);
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategoryName = comboBox2.SelectedItem?.ToString();

            if (selectedCategoryName != null && selectedCategoryName != "Kategoriler")
            {
                textBox2.Text = selectedCategoryName;

                if (categoryDictionary.TryGetValue(selectedCategoryName, out int categoryId))
                {
                    selectedCategoryId = categoryId;
                }
            }
            else
            {
                textBox2.Text = "";
                selectedCategoryId = 0;
            }
        }


        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMenuName = comboBox4.SelectedItem?.ToString();

            if (selectedMenuName != null && selectedMenuName != "Menüler")
            {
                if (menuDictionary.TryGetValue(selectedMenuName, out int menuId))
                {
                    selectedMenuId = menuId;
                }
            }
            else
            {
                selectedMenuId = 0;
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMenuName = comboBox5.SelectedItem?.ToString();

            if (selectedMenuName != null && selectedMenuName != "Menüler")
            {
                if (menuDictionary.TryGetValue(selectedMenuName, out int menuId))
                {
                    Menu selectedMenu = null;
                    foreach (var category in categories)
                    {
                        selectedMenu = category.Menus.FirstOrDefault(m => m.Id == menuId);
                        if (selectedMenu != null)
                        {
                            break;
                        }
                    }

                    if (selectedMenu != null)
                    {
                        textBox6.Text = selectedMenu.Name;
                        textBox7.Text = selectedMenu.Price.ToString();
                        textBox8.Text = selectedMenu.Expense.ToString();
                    }
                }
            }
            else
            {
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string categoryName = textBox2.Text.Trim();
                int categoryId = selectedCategoryId;


                if (string.IsNullOrEmpty(categoryName) || categoryId == 0)
                {
                    MessageBox.Show("Lütfen güncellemek için bir kategori seçiniz.");
                    return;
                }

                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("Lütfen geçerli bilgiler giriniz!");
                    return;
                }

                else
                {
                    var updateCategoryUrl = $"http://localhost:8080/updateCategory?id={categoryId}";

                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("name", categoryName)
                });

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PutAsync(updateCategoryUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Kategori başarıyla güncellendi.");

                            LoadCategories();
                            LoadCategoriesToComboBox();
                            LoadMenusToComboBox();
                            textBox2.Text = "";
                        }
                        else
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            //MessageBox.Show("Kategori güncellenemedi. Hata: " + result);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Kategori güncellenirken hata oluştu: " + ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Lütfen geçerli bilgiler giriniz!");
            }

            else 
            {
                try
                {
                    string name = textBox3.Text;
                    decimal price = decimal.Parse(textBox4.Text);
                    decimal expense = decimal.Parse(textBox5.Text);
                    int categoryId = categoryDictionary[comboBox3.SelectedItem.ToString()];

                    if (string.IsNullOrEmpty(name) || price <= 0 || expense <= 0 || categoryId == 0)
                    {
                        MessageBox.Show("Lütfen geçerli bilgiler giriniz!");
                        return;
                    }

                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("name", name),
                    new KeyValuePair<string, string>("price", price.ToString()),
                    new KeyValuePair<string, string>("expense", expense.ToString()),
                    new KeyValuePair<string, string>("categoryId", categoryId.ToString())
                });

                    var createMenuUrl = "http://localhost:8080/createMenu";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsync(createMenuUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Menü başarıyla oluşturuldu.");

                            LoadCategories();
                            LoadCategoriesToComboBox();
                            LoadMenusToComboBox();
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                        }
                        else
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            //MessageBox.Show("Menü oluşturulamadı. Hata: " + result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Menü oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedMenuName = comboBox4.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedMenuName) || selectedMenuName == "Menüler")
                {
                    MessageBox.Show("Lütfen silmek için bir menü seçiniz.");
                    return;
                }

                if (!menuDictionary.TryGetValue(selectedMenuName, out int menuId))
                {
                    MessageBox.Show("Menü ID bulunamadı.");
                    return;
                }

                string deleteMenuUrl = deleteMenuUrlBase + "?menuId=" + menuId;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(deleteMenuUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Menü başarıyla silindi.");

                        LoadCategories();
                        LoadCategoriesToComboBox();
                        LoadMenusToComboBox();
                    }
                    else
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("Menü silinemedi. Hata: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Menü silinirken hata oluştu: " + ex.Message);
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("Lütfen geçerli bilgiler giriniz!");
                return;
            }

            else
            {
                try
                {
                    string name = textBox6.Text;

                    if (!double.TryParse(textBox7.Text, out double price))
                    {
                        MessageBox.Show("Lütfen geçerli bir fiyat giriniz.");
                        return;
                    }

                    if (!double.TryParse(textBox8.Text, out double expense))
                    {
                        MessageBox.Show("Lütfen geçerli bir gider giriniz.");
                        return;
                    }

                    int selectedMenuId = menuDictionary[comboBox5.SelectedItem.ToString()];

                    int categoryId = categoryDictionary[comboBox6.SelectedItem.ToString()];

                    if (string.IsNullOrEmpty(name) || price <= 0 || expense <= 0 || categoryId == 0)
                    {
                        MessageBox.Show("Lütfen geçerli bilgiler giriniz.");
                        return;
                    }

                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("id", selectedMenuId.ToString()),
                    new KeyValuePair<string, string>("name", name),
                    new KeyValuePair<string, string>("price", price.ToString()),
                    new KeyValuePair<string, string>("expense", expense.ToString()),
                    new KeyValuePair<string, string>("categoryId", categoryId.ToString())
                });

                    var updateMenuUrl = "http://localhost:8080/updateMenu";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PutAsync(updateMenuUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Menü başarıyla güncellendi.");

                            LoadCategories();
                            LoadCategoriesToComboBox();
                            LoadMenusToComboBox();
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                        }
                        else
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            //MessageBox.Show("Menü güncellenemedi. Hata: " + result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lütfen geçerli bilgiler giriniz. ");
                }
            }
            
        }


        private async void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedMenuName = comboBox7.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedMenuName) || selectedMenuName == "Menüler")
                {
                    MessageBox.Show("Lütfen menü seçiniz.");
                    return;
                }

                if (!menuDictionary.TryGetValue(selectedMenuName, out int menuId))
                {
                    MessageBox.Show("Menü ID bulunamadı.");
                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("menuId", menuId.ToString())
                });

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(changeMenuStatusUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Menü durumu başarıyla güncellendi.");

                        LoadCategories();
                        LoadCategoriesToComboBox();
                        LoadMenusToComboBox();
                    }
                    else
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        //MessageBox.Show("Menü durumu güncellenemedi. Hata: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Menü durumu güncellenirken bir hata oluştu: " + ex.Message);
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

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Menu> Menus { get; set; }
    }

    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Expense { get; set; }
        public object Status { get; set; }
    }
}