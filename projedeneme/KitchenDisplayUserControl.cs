using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace projedeneme
{
    public partial class KitchenDisplayUserControl : UserControl
    {
        private const string getByTableNumberOrderUrl = "http://localhost:8080/getByTableNumberOrder";
        private const string ordersBaseUrl = "http://localhost:8080/orders/{tableId}";

        private List<TableOrderInfo> currentTables = new List<TableOrderInfo>(); 
        private Button lastClickedButton = null;
        private System.Windows.Forms.Timer timer;

        public KitchenDisplayUserControl()
        {
            InitializeComponent();
            LoadTables();

            dataGridView1.Columns.Add("QuantityColumn", "Adet");
            dataGridView1.Columns.Add("MenuNameColumn", "Menu Adı");

            dataGridView1.AllowUserToAddRows = false; 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.ReadOnly = true;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();

            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Padding = new Padding(24);
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await LoadTables();

            if (lastClickedButton != null)
            {
                lastClickedButton.PerformClick();
            }
        }

        private async Task LoadTables()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(getByTableNumberOrderUrl);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();
                    List<TableOrderInfo> newTables = JsonConvert.DeserializeObject<List<TableOrderInfo>>(content);

                    newTables = newTables.Where(table => table.status == "hazirlaniyor").ToList();

                    if (!AreTablesEqual(currentTables, newTables))
                    {
                        currentTables = newTables;

                        flowLayoutPanel1.Controls.Clear();

                        foreach (TableOrderInfo table in currentTables.GroupBy(t => t.tableId).Select(t => t.First()).ToList())
                        {
                            YeniButton button = new YeniButton();
                            button.Text = "Masa: " + table.tableNumber;

                            button.TextColor = Color.Black;
                            button.BorderRadius = 5;
                            button.BorderSize = 0;
                            button.Width = 210;
                            button.Height = 40;
                            button.Font = new Font("Arial", 14, FontStyle.Bold);

                            int orderId = table.orderId;

                            TableButtonInfo buttonInfo = new TableButtonInfo()
                            {
                                TableId = table.tableId,
                                OrderId = orderId
                            };

                            button.Tag = buttonInfo;
                            button.Click += Button_Click;

                            button.BackColor = Color.White;

                            flowLayoutPanel1.Controls.Add(button);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Masalar yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            TableButtonInfo buttonInfo = (TableButtonInfo)clickedButton.Tag;
            int tableId = buttonInfo.TableId;

            string ordersByTableIdUrl = ordersBaseUrl.Replace("{tableId}", tableId.ToString());

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(ordersByTableIdUrl);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();
                    List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);

                    HashSet<string> uniqueOrderNotes = new HashSet<string>();
                    foreach (var order in orders)
                    {
                        if (order.status == "hazirlaniyor")
                        {
                            foreach (var orderDetail in order.orderDetails)
                            {
                                if (!string.IsNullOrEmpty(orderDetail.orderNote) && orderDetail.orderNote != "string")
                                {
                                    uniqueOrderNotes.Add(orderDetail.orderNote.Trim());
                                }
                            }
                        }
                    }

                    string orderNotes = string.Join(" ", uniqueOrderNotes);

                    dataGridView1.Rows.Clear();

                    foreach (var order in orders)
                    {
                        if (order.status == "hazirlaniyor")
                        {
                            foreach (var orderDetail in order.orderDetails)
                            {
                                dataGridView1.Rows.Add(orderDetail.quantity, orderDetail.menuName);
                            }
                        }
                    }

                    lastClickedButton = clickedButton;

                    not.Text = orderNotes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Siparişler yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }


        private async void siparisOnay_Click(object sender, EventArgs e)
        {
            if (lastClickedButton != null)
            {
                TableButtonInfo buttonInfo = (TableButtonInfo)lastClickedButton.Tag;
                int tableId = buttonInfo.TableId;

                string ordersByTableIdUrl = ordersBaseUrl.Replace("{tableId}", tableId.ToString());

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(ordersByTableIdUrl);
                        response.EnsureSuccessStatusCode();

                        string content = await response.Content.ReadAsStringAsync();
                        List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);

                        List<int> activeOrderIds = orders.Where(order => order.status == "hazirlaniyor").Select(order => order.orderId).ToList();
                        await UpdateOrderStatus("confirmOrder", activeOrderIds);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Siparişler yüklenirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Önce masa seçiniz!");
            }
        }

        private async void siparisIptal_Click(object sender, EventArgs e)
        {
            if (lastClickedButton != null)
            {
                TableButtonInfo buttonInfo = (TableButtonInfo)lastClickedButton.Tag;
                int tableId = buttonInfo.TableId;

                string ordersByTableIdUrl = ordersBaseUrl.Replace("{tableId}", tableId.ToString());

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(ordersByTableIdUrl);
                        response.EnsureSuccessStatusCode();

                        string content = await response.Content.ReadAsStringAsync();
                        List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);

                        List<int> activeOrderIds = orders.Where(order => order.status == "hazirlaniyor").Select(order => order.orderId).ToList();
                        await UpdateOrderStatus("cancelOrder", activeOrderIds);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Siparişler yüklenirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Önce masa seçiniz!");
            }
        }

        private async Task UpdateOrderStatus(string action, List<int> orderIds)
        {
            if (orderIds != null && orderIds.Count > 0)
            {
                foreach (int orderId in orderIds)
                {
                    string updateOrderStatusUrl = "";
                    if (action == "confirmOrder")
                        updateOrderStatusUrl = "http://localhost:8080/confrimOrder";
                    else if (action == "cancelOrder")
                        updateOrderStatusUrl = "http://localhost:8080/cancelOrder";

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            var content = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("orderId", orderId.ToString())
                            });

                            HttpResponseMessage response = await client.PostAsync(updateOrderStatusUrl, content);
                            response.EnsureSuccessStatusCode();

                            MessageBox.Show("Sipariş başarıyla " + (action == "confirmOrder" ? "onaylandı!" : "iptal edildi!"));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Sipariş " + (action == "confirmOrder" ? "onaylanırken" : "iptal edilirken") + " bir hata oluştu: " + ex.Message);
                        }
                    }
                }

                dataGridView1.Rows.Clear();

                await LoadTables();
            }
            else
            {
                MessageBox.Show("Bu masa için " + (action == "confirmOrder" ? "onaylanacak" : "iptal edilecek") + " sipariş yok!");
            }
        }

        private bool AreTablesEqual(List<TableOrderInfo> tables1, List<TableOrderInfo> tables2)
        {
            if (ReferenceEquals(tables1, tables2))
                return true;

            if (tables1 == null || tables2 == null)
                return false;

            if (tables1.Count != tables2.Count)
                return false;

            for (int i = 0; i < tables1.Count; i++)
            {
                if (tables1[i].tableId != tables2[i].tableId || tables1[i].tableNumber != tables2[i].tableNumber || tables1[i].orderId != tables2[i].orderId)
                    return false;
            }

            return true;
        }

        public class TableButtonInfo
        {
            public int TableId { get; set; }
            public int OrderId { get; set; }
        }

        public class TableOrderInfo
        {
            public int tableNumber { get; set; }
            public int tableId { get; set; }
            public int orderId { get; set; }
            public string status { get; set; }
        }

        public class Order
        {
            public int orderId { get; set; }
            public int tableId { get; set; }
            public string status { get; set; }
            public List<OrderDetail> orderDetails { get; set; }
        }

        public class OrderDetail
        {
            public int orderDetailId { get; set; }
            public int quantity { get; set; }
            public double totalPrice { get; set; }
            public double unitPrice { get; set; }
            public int orderId { get; set; }
            public int menuId { get; set; }
            public string menuName { get; set; }
            public string orderNote { get; set; }
            public double expense { get; set; }
        }
    }
}
