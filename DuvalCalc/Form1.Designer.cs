namespace DuvalCalc
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.teH2 = new System.Windows.Forms.TextBox();
            this.teCH4 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.teC2H2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.teC2H4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.teC2H6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.teRes = new System.Windows.Forms.TextBox();
            this.pbNormogram = new System.Windows.Forms.PictureBox();
            this.cbInteractivGraphic = new System.Windows.Forms.CheckBox();
            this.btnSaveDuval = new System.Windows.Forms.Button();
            this.btnSaveNormogram = new System.Windows.Forms.Button();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNormogram)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "H2:";
            // 
            // teH2
            // 
            this.teH2.Location = new System.Drawing.Point(52, 17);
            this.teH2.MaxLength = 10;
            this.teH2.Name = "teH2";
            this.teH2.Size = new System.Drawing.Size(71, 20);
            this.teH2.TabIndex = 1;
            this.teH2.TextChanged += new System.EventHandler(this.teH_TextChanged);
            this.teH2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teH2_KeyDown);
            this.teH2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teH2_KeyPress);
            this.teH2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.teH2_PreviewKeyDown);
            // 
            // teCH4
            // 
            this.teCH4.Location = new System.Drawing.Point(52, 43);
            this.teCH4.MaxLength = 10;
            this.teCH4.Name = "teCH4";
            this.teCH4.Size = new System.Drawing.Size(71, 20);
            this.teCH4.TabIndex = 2;
            this.teCH4.TextChanged += new System.EventHandler(this.teCH4_TextChanged);
            this.teCH4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teCH4_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CH4:";
            // 
            // teC2H2
            // 
            this.teC2H2.Location = new System.Drawing.Point(52, 120);
            this.teC2H2.MaxLength = 10;
            this.teC2H2.Name = "teC2H2";
            this.teC2H2.Size = new System.Drawing.Size(71, 20);
            this.teC2H2.TabIndex = 5;
            this.teC2H2.TextChanged += new System.EventHandler(this.teC2H2_TextChanged);
            this.teC2H2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teC2H2_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "C2H2:";
            // 
            // teC2H4
            // 
            this.teC2H4.Location = new System.Drawing.Point(52, 69);
            this.teC2H4.MaxLength = 10;
            this.teC2H4.Name = "teC2H4";
            this.teC2H4.Size = new System.Drawing.Size(71, 20);
            this.teC2H4.TabIndex = 3;
            this.teC2H4.TextChanged += new System.EventHandler(this.teC2H4_TextChanged);
            this.teC2H4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teC2H4_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "C2H4:";
            // 
            // teC2H6
            // 
            this.teC2H6.Location = new System.Drawing.Point(52, 95);
            this.teC2H6.MaxLength = 10;
            this.teC2H6.Name = "teC2H6";
            this.teC2H6.Size = new System.Drawing.Size(71, 20);
            this.teC2H6.TabIndex = 4;
            this.teC2H6.TextChanged += new System.EventHandler(this.teC2H6_TextChanged);
            this.teC2H6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teC2H6_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "C2H6:";
            // 
            // pb
            // 
            this.pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb.Location = new System.Drawing.Point(15, 159);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(420, 420);
            this.pb.TabIndex = 10;
            this.pb.TabStop = false;
            this.pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDown);
            this.pb.MouseHover += new System.EventHandler(this.pb_MouseHover);
            this.pb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_MouseMove);
            this.pb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_MouseUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(147, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Duval\'s method:";
            // 
            // cbMethod
            // 
            this.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Items.AddRange(new object[] {
            "Triangle 1 Mineral Oils",
            "Triangle 2 LTC",
            "Triangle 3 BioTemp",
            "Triangle 3 FR3",
            "Triangle 3 Midel",
            "Triangle 3 Silicone",
            "Triangle 4 LTF Mineral Oils",
            "Triangle 5 LTF Mineral Oils",
            "Triangle 6 LTF FR3 Oils",
            "Triangle 7 LTF FR3 Oils",
            "Pentagon 1",
            "Pentagon 2"});
            this.cbMethod.Location = new System.Drawing.Point(239, 17);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(220, 21);
            this.cbMethod.TabIndex = 6;
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(147, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Result:";
            // 
            // teRes
            // 
            this.teRes.Location = new System.Drawing.Point(239, 44);
            this.teRes.Name = "teRes";
            this.teRes.ReadOnly = true;
            this.teRes.Size = new System.Drawing.Size(824, 20);
            this.teRes.TabIndex = 7;
            // 
            // pbNormogram
            // 
            this.pbNormogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbNormogram.Location = new System.Drawing.Point(450, 159);
            this.pbNormogram.Name = "pbNormogram";
            this.pbNormogram.Size = new System.Drawing.Size(613, 420);
            this.pbNormogram.TabIndex = 15;
            this.pbNormogram.TabStop = false;
            this.pbNormogram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbNormogram_MouseDown);
            this.pbNormogram.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbNormogram_MouseMove);
            this.pbNormogram.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbNormogram_MouseUp);
            // 
            // cbInteractivGraphic
            // 
            this.cbInteractivGraphic.AutoSize = true;
            this.cbInteractivGraphic.Location = new System.Drawing.Point(150, 122);
            this.cbInteractivGraphic.Name = "cbInteractivGraphic";
            this.cbInteractivGraphic.Size = new System.Drawing.Size(102, 17);
            this.cbInteractivGraphic.TabIndex = 8;
            this.cbInteractivGraphic.Text = "interactive chart";
            this.cbInteractivGraphic.UseVisualStyleBackColor = true;
            this.cbInteractivGraphic.CheckedChanged += new System.EventHandler(this.cbInteractivGraphic_CheckedChanged);
            // 
            // btnSaveDuval
            // 
            this.btnSaveDuval.Location = new System.Drawing.Point(273, 585);
            this.btnSaveDuval.Name = "btnSaveDuval";
            this.btnSaveDuval.Size = new System.Drawing.Size(162, 31);
            this.btnSaveDuval.TabIndex = 16;
            this.btnSaveDuval.Tag = " && e.Button != System.Windows.Forms.MouseButtons.Left";
            this.btnSaveDuval.Text = "Сохранить изображение";
            this.btnSaveDuval.UseVisualStyleBackColor = true;
            this.btnSaveDuval.Click += new System.EventHandler(this.btnSaveDuval_Click);
            // 
            // btnSaveNormogram
            // 
            this.btnSaveNormogram.Location = new System.Drawing.Point(901, 585);
            this.btnSaveNormogram.Name = "btnSaveNormogram";
            this.btnSaveNormogram.Size = new System.Drawing.Size(162, 31);
            this.btnSaveNormogram.TabIndex = 17;
            this.btnSaveNormogram.Tag = " && e.Button != System.Windows.Forms.MouseButtons.Left";
            this.btnSaveNormogram.Text = "Сохранить изображение";
            this.btnSaveNormogram.UseVisualStyleBackColor = true;
            this.btnSaveNormogram.Click += new System.EventHandler(this.btnSaveNormogram_Click);
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.DefaultExt = "jpg";
            this.saveFileDlg.Filter = "JPG files|*.jpg";
            this.saveFileDlg.Title = "Сохранить изображение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 620);
            this.Controls.Add(this.btnSaveNormogram);
            this.Controls.Add(this.btnSaveDuval);
            this.Controls.Add(this.cbInteractivGraphic);
            this.Controls.Add(this.pbNormogram);
            this.Controls.Add(this.teRes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbMethod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.teC2H6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.teC2H4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.teC2H2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.teCH4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.teH2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duval";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNormogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox teH2;
        private System.Windows.Forms.TextBox teCH4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox teC2H2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox teC2H4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox teC2H6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox teRes;
        private System.Windows.Forms.PictureBox pbNormogram;
        private System.Windows.Forms.CheckBox cbInteractivGraphic;
        private System.Windows.Forms.Button btnSaveDuval;
        private System.Windows.Forms.Button btnSaveNormogram;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
    }
}

