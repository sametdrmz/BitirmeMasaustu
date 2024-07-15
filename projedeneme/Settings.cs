using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projedeneme
{
    public partial class Settings : UserControl
    {
        private Dictionary<string, int> employeeIds = new Dictionary<string, int>();
        private Dictionary<string, int> tableIds = new Dictionary<string, int>();

        public Settings()
        {
            InitializeComponent();
            LoadEmployeeIds();
            LoadTableNumbers();
            Roles();
            RoundCorners(label7);
            RoundCorners(label8);
            RoundCorners(label9);
            RoundCorners(label10);
            RoundCorners(label11);
            RoundCorners(label13);
            RoundCorners(panel2);
            RoundCorners(panel3);
            RoundCorners(panel5);
            RoundCorners(panel6);
        }

        private void Roles()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Roller");
            comboBox3.Items.Add("Admin");
            comboBox3.Items.Add("Kasa");
            comboBox3.Items.Add("Mutfak");
            comboBox3.Items.Add("Garson");
            comboBox3.SelectedIndex = 0;
        }
        private async void LoadEmployeeIds()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Personeller");
            comboBox1.SelectedIndex = 0;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:8080/employees");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var employees = JsonConvert.DeserializeObject<List<EmployeeResponse>>(responseData);

                    foreach (var employee in employees)
                    {
                        comboBox1.Items.Add(employee.UserId);
                        employeeIds[employee.UserId] = employee.Id;
                    }
                }
                else
                {
                    //MessageBox.Show("Çalışanlar yüklenemedi.");
                }
            }
        }

        private async void LoadTableNumbers()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Masalar");
            comboBox2.SelectedIndex = 0;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:8080/tables");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var tables = JsonConvert.DeserializeObject<List<TableResponse>>(responseData);

                    foreach (var table in tables)
                    {
                        comboBox2.Items.Add(table.TableNumber);
                        tableIds[table.TableNumber] = table.Id;
                    }
                }
                else
                {
                    //MessageBox.Show("Tablolar yüklenemedi.");
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            var surname = textBox5.Text;
            var position = textBox2.Text;
            var userId = textBox3.Text;
            var password = textBox4.Text;
            var accountType = comboBox3.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(userId)  || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz giriniz!");
                return;
            }

            else
            {
                if (accountType == "Roller" || accountType == null)
                {
                    MessageBox.Show("Lütfen geçerli bir rol seçin!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(position) || string.IsNullOrWhiteSpace(userId))
                {
                    MessageBox.Show("Lütfen bilgileri eksiksiz giriniz!");
                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("surname", surname),
                new KeyValuePair<string, string>("position", position),
                new KeyValuePair<string, string>("userId", userId),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("accountType", accountType)
            });

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync("http://localhost:8080/createEmployee", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Çalışan başarıyla oluşturuldu!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        comboBox3.SelectedIndex = 0;
                        comboBox1.Items.Clear();
                        employeeIds.Clear();
                        LoadEmployeeIds();
                    }
                    else
                    {
                        //MessageBox.Show("Çalışan oluşturulamadı.");
                    }
                }
            }      
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox1.SelectedItem.ToString() == "Personeller")
            {
                MessageBox.Show("Lütfen silinecek bir çalışanı seçin.");
                return;
            }

            var selectedUserId = comboBox1.SelectedItem.ToString();
            if (!employeeIds.TryGetValue(selectedUserId, out var employeeId))
            {
                MessageBox.Show("Geçersiz çalışan seçildi.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"http://localhost:8080/deleteEmployee?empId={employeeId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Çalışan başarıyla silindi!");
                    comboBox1.Items.Remove(selectedUserId);
                    employeeIds.Remove(selectedUserId);
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    //MessageBox.Show("Çalışan silinemedi.");
                }
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var tableNumber = textBox7.Text;

            if (IsValidInput(tableNumber, out string errorMessage))
            {
                var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tableNumber", tableNumber)
            });

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync("http://localhost:8080/createTable", content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Masa başarıyla oluşturuldu!");
                        textBox7.Text = "";
                        comboBox2.Items.Clear();
                        tableIds.Clear();
                        LoadTableNumbers();
                    }
                    else
                    {
                        //MessageBox.Show("Tablo oluşturulamadı.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir masa sayısı giriniz!");
                return ;
            }    
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || comboBox2.SelectedItem.ToString() == "Masalar")
            {
                MessageBox.Show("Lütfen silinecek bir masa seçin.");
                return;
            }

            var selectedTableNumber = comboBox2.SelectedItem.ToString();
            if (!tableIds.TryGetValue(selectedTableNumber, out var tableId))
            {
                MessageBox.Show("Geçersiz masa seçildi.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"http://localhost:8080/deleteTable?tableId={tableId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Masa başarıyla silindi!");
                    comboBox2.Items.Remove(selectedTableNumber);
                    tableIds.Remove(selectedTableNumber);
                    comboBox2.SelectedIndex = 0;
                }
                else
                {
                    //MessageBox.Show("Tablo silinemedi.");
                }
            }
        }

        private bool IsValidInput(string input, out string errorMessage)
        {
            errorMessage = "";

            // Check if the input is a number
            if (!decimal.TryParse(input, out decimal number))
            {
                errorMessage = "Giriş bir sayı olmalıdır.";
                return false;
            }

            // Check if the number is less than 0
            if (number < 0)
            {
                errorMessage = "Giriş 0'dan küçük olamaz.";
                return false;
            }

            return true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                comboBox3.SelectedIndex = -1;
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

    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }

    public class TableResponse
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
    }
}
