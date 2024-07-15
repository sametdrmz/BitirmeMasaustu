namespace projedeneme
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.oKapat = new projedeneme.YeniButton();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1086, 640);
            this.panel1.TabIndex = 9;
            // 
            // oKapat
            // 
            this.oKapat.BackColor = System.Drawing.Color.White;
            this.oKapat.BackgroundColor = System.Drawing.Color.White;
            this.oKapat.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.oKapat.BorderColor1 = System.Drawing.Color.PaleVioletRed;
            this.oKapat.BorderRadius = 5;
            this.oKapat.BorderRadius1 = 5;
            this.oKapat.BorderSize = 0;
            this.oKapat.BorderSize1 = 0;
            this.oKapat.FlatAppearance.BorderSize = 0;
            this.oKapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oKapat.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.oKapat.ForeColor = System.Drawing.Color.Black;
            this.oKapat.Location = new System.Drawing.Point(620, 650);
            this.oKapat.Name = "oKapat";
            this.oKapat.Size = new System.Drawing.Size(151, 40);
            this.oKapat.TabIndex = 10;
            this.oKapat.Text = "Oturumu Kapat";
            this.oKapat.TextColor = System.Drawing.Color.Black;
            this.oKapat.UseVisualStyleBackColor = false;
            this.oKapat.Click += new System.EventHandler(this.oKapat_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1110, 717);
            this.Controls.Add(this.oKapat);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1126, 756);
            this.MinimumSize = new System.Drawing.Size(1126, 756);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restoran Otomasyon Sistemi";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private YeniButton oKapat;
    }
}