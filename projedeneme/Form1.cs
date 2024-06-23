using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projedeneme
{
    public partial class Form1 : Form
    {
        private const string apiUrl = "http://localhost:8080/login";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userId", kullaniciAdi),
                new KeyValuePair<string, string>("password", sifre)
            });

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.PostAsync(apiUrl, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        string accountType = "";
                        if (apiResponse.Contains("AccountType: Admin"))
                        {
                            accountType = "Admin";
                        }
                        else if (apiResponse.Contains("AccountType: Mutfak"))
                        {
                            accountType = "Mutfak";
                        }
                        else if (apiResponse.Contains("AccountType: Kasa"))
                        {
                            accountType = "Kasa";
                        }

                        if (accountType == "Kasa")
                        {
                            Form4 form4 = new Form4();
                            this.Hide(); 
                            form4.ShowDialog(); 
                            this.Close();
                        }
                        
                        else if (accountType == "Admin")
                        {
                            Form3 form3 = new Form3();
                            this.Hide(); 
                            form3.ShowDialog(); 
                            this.Close();
                        }
                        else if (accountType == "Mutfak")
                        {
                            Form2 form2 = new Form2();
                            this.Hide(); 
                            form2.ShowDialog(); 
                            this.Close();
                        }
                        else
                        {
                            label1.Text = "Kullanıcı Adı ve/veya şifre yanlış.";
                        }
                    }
                    else
                    {
                        label1.Text = "Kullanıcı Adı ve/veya şifre yanlış.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API isteği sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text =="Kullanıcı Adı")
            {
                textBox1.Text = "";

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "Kullanıcı Adı";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Şifre")
            {
                textBox2.Text = "";

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Şifre";
            }
        }
    }
}
