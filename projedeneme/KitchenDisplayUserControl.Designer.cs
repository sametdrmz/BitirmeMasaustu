namespace projedeneme
{
    partial class KitchenDisplayUserControl
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.not = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.siparisIptal = new projedeneme.YeniButton();
            this.siparisOnay = new projedeneme.YeniButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(304, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(4);
            this.label2.Size = new System.Drawing.Size(760, 30);
            this.label2.TabIndex = 14;
            this.label2.Text = "Masa Siparişleri";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.CausesValidation = false;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.MinimumSize = new System.Drawing.Size(70, 25);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4);
            this.label1.Size = new System.Drawing.Size(270, 30);
            this.label1.TabIndex = 13;
            this.label1.Text = "MASALAR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(22, 68);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(270, 540);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // not
            // 
            this.not.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.not.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.not.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.not.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.not.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.not.Location = new System.Drawing.Point(434, 398);
            this.not.Margin = new System.Windows.Forms.Padding(5);
            this.not.Multiline = true;
            this.not.Name = "not";
            this.not.Size = new System.Drawing.Size(500, 130);
            this.not.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(304, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Size = new System.Drawing.Size(760, 322);
            this.dataGridView1.TabIndex = 8;
            // 
            // siparisIptal
            // 
            this.siparisIptal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(17)))), ((int)(((byte)(0)))));
            this.siparisIptal.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(17)))), ((int)(((byte)(0)))));
            this.siparisIptal.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.siparisIptal.BorderColor1 = System.Drawing.Color.PaleVioletRed;
            this.siparisIptal.BorderRadius = 5;
            this.siparisIptal.BorderRadius1 = 5;
            this.siparisIptal.BorderSize = 0;
            this.siparisIptal.BorderSize1 = 0;
            this.siparisIptal.FlatAppearance.BorderSize = 0;
            this.siparisIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.siparisIptal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.siparisIptal.ForeColor = System.Drawing.Color.Black;
            this.siparisIptal.Location = new System.Drawing.Point(503, 553);
            this.siparisIptal.Name = "siparisIptal";
            this.siparisIptal.Size = new System.Drawing.Size(150, 40);
            this.siparisIptal.TabIndex = 15;
            this.siparisIptal.Text = "Sipariş İptal";
            this.siparisIptal.TextColor = System.Drawing.Color.Black;
            this.siparisIptal.UseVisualStyleBackColor = false;
            this.siparisIptal.Click += new System.EventHandler(this.siparisIptal_Click);
            // 
            // siparisOnay
            // 
            this.siparisOnay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(4)))));
            this.siparisOnay.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(4)))));
            this.siparisOnay.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.siparisOnay.BorderColor1 = System.Drawing.Color.PaleVioletRed;
            this.siparisOnay.BorderRadius = 5;
            this.siparisOnay.BorderRadius1 = 5;
            this.siparisOnay.BorderSize = 0;
            this.siparisOnay.BorderSize1 = 0;
            this.siparisOnay.FlatAppearance.BorderSize = 0;
            this.siparisOnay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.siparisOnay.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.siparisOnay.ForeColor = System.Drawing.Color.Black;
            this.siparisOnay.Location = new System.Drawing.Point(714, 553);
            this.siparisOnay.Name = "siparisOnay";
            this.siparisOnay.Size = new System.Drawing.Size(150, 40);
            this.siparisOnay.TabIndex = 16;
            this.siparisOnay.Text = "Sipariş Onay";
            this.siparisOnay.TextColor = System.Drawing.Color.Black;
            this.siparisOnay.UseVisualStyleBackColor = false;
            this.siparisOnay.Click += new System.EventHandler(this.siparisOnay_Click);
            // 
            // KitchenDisplayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.siparisOnay);
            this.Controls.Add(this.siparisIptal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.not);
            this.Controls.Add(this.dataGridView1);
            this.Name = "KitchenDisplayUserControl";
            this.Size = new System.Drawing.Size(1086, 640);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox not;
        private System.Windows.Forms.DataGridView dataGridView1;
        private YeniButton siparisIptal;
        private YeniButton siparisOnay;
    }
}
