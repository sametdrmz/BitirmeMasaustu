using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace projedeneme
{
    public partial class CaseScreen : UserControl
    {
        private const string tablesApiUrl = "http://localhost:8080/tables";
        private const string moveTablesApiUrl = "http://localhost:8080/move/all";

        private int? sourceTableId = null;
        private int? targetTableId = null;
        private bool isMovingMode = false;
        private List<TableInfo> currentTables = new List<TableInfo>();
        private Timer timer;

        public CaseScreen()
        {
            InitializeComponent();

            RoundCorners(label1);
            RoundCorners(tasi);
            RoundCorners(birlestir);

            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;

            timer.Start();

            LoadTables();
        }

        private async void LoadTables()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(tablesApiUrl);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();
                    List<TableInfo> newTables = JsonConvert.DeserializeObject<List<TableInfo>>(content);

                    if (!AreTablesEqual(currentTables, newTables))
                    {
                        currentTables = newTables;
                        UpdateTableButtons();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Tabloları yüklerken hata oluştu: " + ex.Message);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadTables();
        }

        private bool AreTablesEqual(List<TableInfo> tables1, List<TableInfo> tables2)
        {
            if (tables1.Count != tables2.Count)
                return false;

            for (int i = 0; i < tables1.Count; i++)
            {
                if (tables1[i].id != tables2[i].id || tables1[i].tableNumber != tables2[i].tableNumber || tables1[i].status != tables2[i].status)
                    return false;
            }

            return true;
        }

        private void UpdateTableButtons()
        {
            flowLayoutPanel1.Controls.Clear();

            int maxButtonsPerRow = 5;

            foreach (var table in currentTables)
            {
                Button button = new Button();
                button.Text = table.tableNumber.ToString();
                button.Size = new Size(100, 100);
                button.Font = new Font("Arial", 12, FontStyle.Bold);
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = table.status == "DOLU" ? Color.FromArgb(255, 0, 255, 4) : Color.FromArgb(255, 255, 255, 255);
                button.Margin = new Padding(35, 20, 35, 20);
                button.Click += Masa_Click;

                button.Tag = $"{table.id}|{table.status}";

                RoundControlCorners(button);

                flowLayoutPanel1.Controls.Add(button);

                if ((flowLayoutPanel1.Controls.Count) % maxButtonsPerRow == 0)
                {
                    flowLayoutPanel1.SetFlowBreak(button, true);
                }
            }

            flowLayoutPanel1.Margin = new Padding(100);
        }

        private void Masa_Click(object sender, EventArgs e)
        {
            if (isMovingMode)
            {
                Button button = sender as Button;
                string[] tagParts = button.Tag.ToString().Split('|');
                int tableId = int.Parse(tagParts[0]);
                string status = tagParts[1];

                if (sourceTableId == null)
                {
                    if (status != "DOLU")
                    {
                        MessageBox.Show("Lütfen dolu bir masa seçin.");
                        return;
                    }

                    sourceTableId = tableId;
                    MessageBox.Show($"Birinci masa seçildi. Masa ID: {sourceTableId}");
                }
                else if (targetTableId == null)
                {
                    if (sourceTableId == tableId)
                    {
                        MessageBox.Show("Lütfen farklı bir masa seçin.");
                        return;
                    }

                    targetTableId = tableId;
                    MessageBox.Show($"İkinci masa seçildi. Masa ID: {targetTableId}");

                    MoveAndRefreshTables();
                }
            }
            else
            {
                Button button = sender as Button;
                string[] tagParts = button.Tag.ToString().Split('|');
                int tableId = int.Parse(tagParts[0]);
                string status = tagParts[1];

                if (status == "DOLU")
                {
                    CloseTableScreen closeTable = new CloseTableScreen();

                    closeTable.tableId = tableId;

                    closeTable.Show();
                }
                else
                {
                    MessageBox.Show("Lütfen dolu bir masa seçin.");
                }
            }
        }

        private void tasi_Click(object sender, EventArgs e)
        {
            isMovingMode = true;
            MessageBox.Show("Lütfen birinci ve ikinci masaları seçin.");
        }

        private void birlestir_Click(object sender, EventArgs e)
        {
            isMovingMode = true;
            MessageBox.Show("Lütfen birinci ve ikinci masaları seçin.");
        }

        private void MoveAndRefreshTables()
        {
            if (sourceTableId == null || targetTableId == null)
            {
                MessageBox.Show("Lütfen iki masa seçin.");
                return;
            }

            MoveTables(sourceTableId.Value, targetTableId.Value);
            RefreshTables();
            isMovingMode = false;
        }

        private void MoveTables(int sourceTableId, int targetTableId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new Dictionary<string, string>
                    {
                        { "sourceTableId", sourceTableId.ToString() },
                        { "targetTableId", targetTableId.ToString() }
                    };

                    var content = new FormUrlEncodedContent(data);

                    HttpResponseMessage response = client.PutAsync(moveTablesApiUrl, content).Result;
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show("Masalar başarıyla taşındı.");

                    this.sourceTableId = null;
                    this.targetTableId = null;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Masa taşıma işleminde hata oluştu: " + ex.Message);
                }
            }
        }

        private void RefreshTables()
        {
            LoadTables();
        }

        private void RoundControlCorners(Control control)
        {
            int cornerRadius = 20;
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }

        private void RoundCorners(Control control)
        {
            int cornerRadius = 10;
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - 2 * cornerRadius, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }

        public class TableInfo
        {
            public int id { get; set; }
            public int tableNumber { get; set; }
            public string status { get; set; }
        }
    }
}
