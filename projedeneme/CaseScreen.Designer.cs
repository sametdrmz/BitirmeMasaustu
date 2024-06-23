namespace projedeneme
{
    partial class CaseScreen
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tasi = new projedeneme.YeniButton();
            this.birlestir = new projedeneme.YeniButton();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(83, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(920, 500);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(483, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "KASA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tasi
            // 
            this.tasi.BackColor = System.Drawing.Color.White;
            this.tasi.BackgroundColor = System.Drawing.Color.White;
            this.tasi.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.tasi.BorderColor1 = System.Drawing.Color.PaleVioletRed;
            this.tasi.BorderRadius = 5;
            this.tasi.BorderRadius1 = 5;
            this.tasi.BorderSize = 0;
            this.tasi.BorderSize1 = 0;
            this.tasi.FlatAppearance.BorderSize = 0;
            this.tasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tasi.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.tasi.ForeColor = System.Drawing.Color.Black;
            this.tasi.Location = new System.Drawing.Point(299, 575);
            this.tasi.Name = "tasi";
            this.tasi.Size = new System.Drawing.Size(150, 40);
            this.tasi.TabIndex = 5;
            this.tasi.Text = "Masa Taşı";
            this.tasi.TextColor = System.Drawing.Color.Black;
            this.tasi.UseVisualStyleBackColor = false;
            this.tasi.Click += new System.EventHandler(this.tasi_Click);
            // 
            // birlestir
            // 
            this.birlestir.BackColor = System.Drawing.Color.White;
            this.birlestir.BackgroundColor = System.Drawing.Color.White;
            this.birlestir.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.birlestir.BorderColor1 = System.Drawing.Color.PaleVioletRed;
            this.birlestir.BorderRadius = 5;
            this.birlestir.BorderRadius1 = 5;
            this.birlestir.BorderSize = 0;
            this.birlestir.BorderSize1 = 0;
            this.birlestir.FlatAppearance.BorderSize = 0;
            this.birlestir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.birlestir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.birlestir.ForeColor = System.Drawing.Color.Black;
            this.birlestir.Location = new System.Drawing.Point(660, 575);
            this.birlestir.Name = "birlestir";
            this.birlestir.Size = new System.Drawing.Size(150, 40);
            this.birlestir.TabIndex = 6;
            this.birlestir.Text = "Masa Birleştir";
            this.birlestir.TextColor = System.Drawing.Color.Black;
            this.birlestir.UseVisualStyleBackColor = false;
            this.birlestir.Click += new System.EventHandler(this.birlestir_Click);
            // 
            // CaseScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.birlestir);
            this.Controls.Add(this.tasi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CaseScreen";
            this.Size = new System.Drawing.Size(1086, 640);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private YeniButton tasi;
        private YeniButton birlestir;
    }
}
