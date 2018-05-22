﻿namespace RotController
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmbHandShake = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rcvTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.RtsEnable_checkBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.operational_mode = new System.Windows.Forms.Label();
            this.Port_Update_Button = new System.Windows.Forms.Button();
            this.AutoConnect_Button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LinkStatus_label = new System.Windows.Forms.Label();
            this.Left_label = new System.Windows.Forms.Label();
            this.Right_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RotR = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Monitor_timer = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.FirstOpr_comboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(6, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baud Rate";
            // 
            // cmbPortName
            // 
            this.cmbPortName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(9, 34);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(231, 26);
            this.cmbPortName.TabIndex = 3;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(9, 84);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(231, 26);
            this.cmbBaudRate.TabIndex = 4;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived1);
            // 
            // cmbHandShake
            // 
            this.cmbHandShake.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbHandShake.FormattingEnabled = true;
            this.cmbHandShake.Location = new System.Drawing.Point(9, 134);
            this.cmbHandShake.Name = "cmbHandShake";
            this.cmbHandShake.Size = new System.Drawing.Size(231, 26);
            this.cmbHandShake.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(6, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "HandShake";
            // 
            // rcvTextBox
            // 
            this.rcvTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.rcvTextBox.ForeColor = System.Drawing.Color.Lime;
            this.rcvTextBox.Location = new System.Drawing.Point(246, 13);
            this.rcvTextBox.Multiline = true;
            this.rcvTextBox.Name = "rcvTextBox";
            this.rcvTextBox.Size = new System.Drawing.Size(300, 262);
            this.rcvTextBox.TabIndex = 7;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.connectButton.Location = new System.Drawing.Point(9, 205);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 26);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "button1";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // RtsEnable_checkBox
            // 
            this.RtsEnable_checkBox.AutoSize = true;
            this.RtsEnable_checkBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RtsEnable_checkBox.Location = new System.Drawing.Point(9, 167);
            this.RtsEnable_checkBox.Name = "RtsEnable_checkBox";
            this.RtsEnable_checkBox.Size = new System.Drawing.Size(84, 22);
            this.RtsEnable_checkBox.TabIndex = 9;
            this.RtsEnable_checkBox.Text = "RtsEnable";
            this.RtsEnable_checkBox.UseVisualStyleBackColor = true;
            this.RtsEnable_checkBox.CheckedChanged += new System.EventHandler(this.RtsEnable_checkBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(6, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 31);
            this.button1.TabIndex = 10;
            this.button1.Text = "動作モード変更";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // operational_mode
            // 
            this.operational_mode.AutoSize = true;
            this.operational_mode.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.operational_mode.Location = new System.Drawing.Point(130, 9);
            this.operational_mode.Name = "operational_mode";
            this.operational_mode.Size = new System.Drawing.Size(58, 24);
            this.operational_mode.TabIndex = 11;
            this.operational_mode.Text = "まーだ";
            // 
            // Port_Update_Button
            // 
            this.Port_Update_Button.Location = new System.Drawing.Point(9, 236);
            this.Port_Update_Button.Name = "Port_Update_Button";
            this.Port_Update_Button.Size = new System.Drawing.Size(75, 26);
            this.Port_Update_Button.TabIndex = 12;
            this.Port_Update_Button.Text = "更新";
            this.Port_Update_Button.UseVisualStyleBackColor = true;
            this.Port_Update_Button.Click += new System.EventHandler(this.Port_Update_Button_Click);
            // 
            // AutoConnect_Button
            // 
            this.AutoConnect_Button.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AutoConnect_Button.Location = new System.Drawing.Point(90, 205);
            this.AutoConnect_Button.Name = "AutoConnect_Button";
            this.AutoConnect_Button.Size = new System.Drawing.Size(75, 23);
            this.AutoConnect_Button.TabIndex = 13;
            this.AutoConnect_Button.Text = "自動接続";
            this.AutoConnect_Button.UseVisualStyleBackColor = true;
            this.AutoConnect_Button.Click += new System.EventHandler(this.AutoConnect_Button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 331);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.FirstOpr_comboBox);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.Left_label);
            this.tabPage1.Controls.Add(this.Right_label);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.RotR);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.operational_mode);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LinkStatus_label);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 67);
            this.panel1.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(18, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Mode        ：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(18, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 24);
            this.label5.TabIndex = 16;
            this.label5.Text = "LinkStatus：";
            // 
            // LinkStatus_label
            // 
            this.LinkStatus_label.AutoSize = true;
            this.LinkStatus_label.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LinkStatus_label.Location = new System.Drawing.Point(130, 33);
            this.LinkStatus_label.Name = "LinkStatus_label";
            this.LinkStatus_label.Size = new System.Drawing.Size(56, 24);
            this.LinkStatus_label.TabIndex = 17;
            this.LinkStatus_label.Text = "label6";
            // 
            // Left_label
            // 
            this.Left_label.AutoSize = true;
            this.Left_label.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Left_label.Location = new System.Drawing.Point(282, 75);
            this.Left_label.Name = "Left_label";
            this.Left_label.Size = new System.Drawing.Size(38, 23);
            this.Left_label.TabIndex = 22;
            this.Left_label.Text = "Left";
            // 
            // Right_label
            // 
            this.Right_label.AutoSize = true;
            this.Right_label.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Right_label.Location = new System.Drawing.Point(487, 75);
            this.Right_label.Name = "Right_label";
            this.Right_label.Size = new System.Drawing.Size(48, 23);
            this.Right_label.TabIndex = 21;
            this.Right_label.Text = "Right";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(249, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "左回転";
            // 
            // RotR
            // 
            this.RotR.AutoSize = true;
            this.RotR.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RotR.Location = new System.Drawing.Point(249, 227);
            this.RotR.Name = "RotR";
            this.RotR.Size = new System.Drawing.Size(58, 24);
            this.RotR.TabIndex = 19;
            this.RotR.Text = "右回転";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(253, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 160);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(127, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 31);
            this.button2.TabIndex = 14;
            this.button2.Text = "自動接続";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.AutoConnect_Button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AutoConnect_Button);
            this.tabPage2.Controls.Add(this.rcvTextBox);
            this.tabPage2.Controls.Add(this.connectButton);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.Port_Update_Button);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmbPortName);
            this.tabPage2.Controls.Add(this.RtsEnable_checkBox);
            this.tabPage2.Controls.Add(this.cmbBaudRate);
            this.tabPage2.Controls.Add(this.cmbHandShake);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(576, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Monitor_timer
            // 
            this.Monitor_timer.Tick += new System.EventHandler(this.LinkMonitor_time_Tick);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(6, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(219, 79);
            this.label9.TabIndex = 24;
            this.label9.Text = "※ハードウェアモードでは音量操作が規定です．\r\nソフトウェアモードにて動作を変更することが出来ます．";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(313, 226);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(192, 25);
            this.textBox1.TabIndex = 25;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox2.Location = new System.Drawing.Point(313, 260);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(192, 25);
            this.textBox2.TabIndex = 26;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(511, 226);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 25);
            this.button3.TabIndex = 27;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(511, 260);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 25);
            this.button4.TabIndex = 28;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(249, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 24);
            this.label7.TabIndex = 29;
            this.label7.Text = "-Key割り当て-";
            // 
            // FirstOpr_comboBox
            // 
            this.FirstOpr_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FirstOpr_comboBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FirstOpr_comboBox.FormattingEnabled = true;
            this.FirstOpr_comboBox.Location = new System.Drawing.Point(6, 173);
            this.FirstOpr_comboBox.Name = "FirstOpr_comboBox";
            this.FirstOpr_comboBox.Size = new System.Drawing.Size(147, 26);
            this.FirstOpr_comboBox.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(8, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 18);
            this.label8.TabIndex = 31;
            this.label8.Text = "規定の動作モード";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 331);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmbHandShake;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rcvTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.CheckBox RtsEnable_checkBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label operational_mode;
        private System.Windows.Forms.Button Port_Update_Button;
        private System.Windows.Forms.Button AutoConnect_Button;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LinkStatus_label;
        private System.Windows.Forms.Timer Monitor_timer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Left_label;
        private System.Windows.Forms.Label Right_label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label RotR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox FirstOpr_comboBox;
    }
}
