namespace RotController
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
            this.Port_Update_Button = new System.Windows.Forms.Button();
            this.AutoConnect_Button = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Left_label = new System.Windows.Forms.Label();
            this.Right_label = new System.Windows.Forms.Label();
            this.Minimize = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.operational_mode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Lmodifier_keyCmb = new System.Windows.Forms.ComboBox();
            this.Rmodifier_keyCmb = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Lshortcut_textBox = new System.Windows.Forms.TextBox();
            this.Rshortcut_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RotR = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.portName_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LinkStatus_label = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.notice_textBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.Comb_ThemeColor = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.FirstOpr_comboBox = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Monitor_timer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(8, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(8, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baud Rate";
            // 
            // cmbPortName
            // 
            this.cmbPortName.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(11, 118);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(276, 26);
            this.cmbPortName.TabIndex = 3;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(11, 168);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(276, 26);
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
            this.cmbHandShake.Location = new System.Drawing.Point(11, 218);
            this.cmbHandShake.Name = "cmbHandShake";
            this.cmbHandShake.Size = new System.Drawing.Size(276, 26);
            this.cmbHandShake.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(8, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "HandShake";
            // 
            // rcvTextBox
            // 
            this.rcvTextBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.rcvTextBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rcvTextBox.ForeColor = System.Drawing.Color.Lime;
            this.rcvTextBox.Location = new System.Drawing.Point(291, 34);
            this.rcvTextBox.Multiline = true;
            this.rcvTextBox.Name = "rcvTextBox";
            this.rcvTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rcvTextBox.Size = new System.Drawing.Size(377, 151);
            this.rcvTextBox.TabIndex = 7;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.connectButton.Location = new System.Drawing.Point(11, 289);
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
            this.RtsEnable_checkBox.Location = new System.Drawing.Point(11, 250);
            this.RtsEnable_checkBox.Name = "RtsEnable_checkBox";
            this.RtsEnable_checkBox.Size = new System.Drawing.Size(84, 22);
            this.RtsEnable_checkBox.TabIndex = 9;
            this.RtsEnable_checkBox.Text = "RtsEnable";
            this.RtsEnable_checkBox.UseVisualStyleBackColor = true;
            this.RtsEnable_checkBox.CheckedChanged += new System.EventHandler(this.RtsEnable_checkBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(11, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 124);
            this.button1.TabIndex = 10;
            this.button1.Text = "CHANGE MODE ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Port_Update_Button
            // 
            this.Port_Update_Button.Location = new System.Drawing.Point(173, 289);
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
            this.AutoConnect_Button.Location = new System.Drawing.Point(92, 289);
            this.AutoConnect_Button.Name = "AutoConnect_Button";
            this.AutoConnect_Button.Size = new System.Drawing.Size(75, 26);
            this.AutoConnect_Button.TabIndex = 13;
            this.AutoConnect_Button.Text = "自動接続";
            this.AutoConnect_Button.UseVisualStyleBackColor = true;
            this.AutoConnect_Button.Click += new System.EventHandler(this.AutoConnect_Button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-5, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 375);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.Left_label);
            this.tabPage1.Controls.Add(this.Right_label);
            this.tabPage1.Controls.Add(this.Minimize);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.Lmodifier_keyCmb);
            this.tabPage1.Controls.Add(this.Rmodifier_keyCmb);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.Lshortcut_textBox);
            this.tabPage1.Controls.Add(this.Rshortcut_textBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.RotR);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(690, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // Left_label
            // 
            this.Left_label.AutoSize = true;
            this.Left_label.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Left_label.Location = new System.Drawing.Point(303, 84);
            this.Left_label.Name = "Left_label";
            this.Left_label.Size = new System.Drawing.Size(84, 46);
            this.Left_label.TabIndex = 22;
            this.Left_label.Text = "Counter\r\nClockWise";
            // 
            // Right_label
            // 
            this.Right_label.AutoSize = true;
            this.Right_label.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Right_label.Location = new System.Drawing.Point(575, 84);
            this.Right_label.Name = "Right_label";
            this.Right_label.Size = new System.Drawing.Size(84, 23);
            this.Right_label.TabIndex = 21;
            this.Right_label.Text = "ClockWise";
            // 
            // Minimize
            // 
            this.Minimize.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Minimize.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Minimize.Location = new System.Drawing.Point(491, 282);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(177, 43);
            this.Minimize.TabIndex = 43;
            this.Minimize.Text = "Minimize";
            this.Minimize.UseVisualStyleBackColor = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.operational_mode);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(11, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 43);
            this.panel2.TabIndex = 42;
            // 
            // operational_mode
            // 
            this.operational_mode.AutoSize = true;
            this.operational_mode.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.operational_mode.Location = new System.Drawing.Point(122, 9);
            this.operational_mode.Name = "operational_mode";
            this.operational_mode.Size = new System.Drawing.Size(82, 24);
            this.operational_mode.TabIndex = 40;
            this.operational_mode.Text = "unknown";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 24);
            this.label4.TabIndex = 41;
            this.label4.Text = "Mode  :";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button5.Location = new System.Drawing.Point(409, 266);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 60);
            this.button5.TabIndex = 39;
            this.button5.Text = "Save";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Save_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(240, 239);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 24);
            this.label14.TabIndex = 37;
            this.label14.Text = "Any key";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(87, 239);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 24);
            this.label15.TabIndex = 36;
            this.label15.Text = "Modifier key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(215, 302);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 24);
            this.label12.TabIndex = 35;
            this.label12.Text = "+";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(215, 268);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 24);
            this.label13.TabIndex = 34;
            this.label13.Text = "+";
            // 
            // Lmodifier_keyCmb
            // 
            this.Lmodifier_keyCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Lmodifier_keyCmb.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Lmodifier_keyCmb.FormattingEnabled = true;
            this.Lmodifier_keyCmb.Location = new System.Drawing.Point(91, 300);
            this.Lmodifier_keyCmb.Name = "Lmodifier_keyCmb";
            this.Lmodifier_keyCmb.Size = new System.Drawing.Size(118, 26);
            this.Lmodifier_keyCmb.TabIndex = 33;
            this.Lmodifier_keyCmb.SelectedIndexChanged += new System.EventHandler(this.Lmodifier_keyCmb_SelectedIndexChanged);
            // 
            // Rmodifier_keyCmb
            // 
            this.Rmodifier_keyCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Rmodifier_keyCmb.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Rmodifier_keyCmb.FormattingEnabled = true;
            this.Rmodifier_keyCmb.Location = new System.Drawing.Point(91, 266);
            this.Rmodifier_keyCmb.Name = "Rmodifier_keyCmb";
            this.Rmodifier_keyCmb.Size = new System.Drawing.Size(118, 26);
            this.Rmodifier_keyCmb.TabIndex = 32;
            this.Rmodifier_keyCmb.SelectedIndexChanged += new System.EventHandler(this.Rmodifier_keyCmb_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(15, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 24);
            this.label7.TabIndex = 29;
            this.label7.Text = "Key Combination";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(351, 301);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 25);
            this.button4.TabIndex = 28;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.LeftKey_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button3.Location = new System.Drawing.Point(351, 266);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 25);
            this.button3.TabIndex = 27;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.RightKey_Click);
            // 
            // Lshortcut_textBox
            // 
            this.Lshortcut_textBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Lshortcut_textBox.Location = new System.Drawing.Point(244, 301);
            this.Lshortcut_textBox.Name = "Lshortcut_textBox";
            this.Lshortcut_textBox.ReadOnly = true;
            this.Lshortcut_textBox.Size = new System.Drawing.Size(101, 25);
            this.Lshortcut_textBox.TabIndex = 26;
            this.Lshortcut_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Lshortcut_textBox.TextChanged += new System.EventHandler(this.Lshortcut_textBox_TextChanged);
            // 
            // Rshortcut_textBox
            // 
            this.Rshortcut_textBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Rshortcut_textBox.Location = new System.Drawing.Point(244, 266);
            this.Rshortcut_textBox.Name = "Rshortcut_textBox";
            this.Rshortcut_textBox.ReadOnly = true;
            this.Rshortcut_textBox.Size = new System.Drawing.Size(101, 25);
            this.Rshortcut_textBox.TabIndex = 25;
            this.Rshortcut_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Rshortcut_textBox.TextChanged += new System.EventHandler(this.Rshortcut_textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(7, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "CCW[←]";
            // 
            // RotR
            // 
            this.RotR.AutoSize = true;
            this.RotR.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RotR.Location = new System.Drawing.Point(18, 268);
            this.RotR.Name = "RotR";
            this.RotR.Size = new System.Drawing.Size(67, 24);
            this.RotR.TabIndex = 19;
            this.RotR.Text = "CW[→]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::RotController.Properties.Resources.イラスト3;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(293, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(375, 180);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.notice_textBox);
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
            this.tabPage2.Size = new System.Drawing.Size(690, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Serial";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(9, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(272, 18);
            this.label17.TabIndex = 25;
            this.label17.Text = "※開発者以外は既定値から変更しないでください";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.portName_label);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LinkStatus_label);
            this.panel1.Location = new System.Drawing.Point(9, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 50);
            this.panel1.TabIndex = 24;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 24);
            this.label16.TabIndex = 19;
            this.label16.Text = "COMPort";
            // 
            // portName_label
            // 
            this.portName_label.AutoSize = true;
            this.portName_label.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.portName_label.Location = new System.Drawing.Point(112, 0);
            this.portName_label.Name = "portName_label";
            this.portName_label.Size = new System.Drawing.Size(43, 24);
            this.portName_label.TabIndex = 18;
            this.portName_label.Text = "port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 24);
            this.label5.TabIndex = 16;
            this.label5.Text = "LinkStatus";
            // 
            // LinkStatus_label
            // 
            this.LinkStatus_label.AutoSize = true;
            this.LinkStatus_label.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LinkStatus_label.Location = new System.Drawing.Point(112, 24);
            this.LinkStatus_label.Name = "LinkStatus_label";
            this.LinkStatus_label.Size = new System.Drawing.Size(56, 24);
            this.LinkStatus_label.TabIndex = 17;
            this.LinkStatus_label.Text = "label6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(292, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 18);
            this.label11.TabIndex = 16;
            this.label11.Text = "Notice";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(291, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 18);
            this.label10.TabIndex = 15;
            this.label10.Text = "Receive Data";
            // 
            // notice_textBox
            // 
            this.notice_textBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.notice_textBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.notice_textBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.notice_textBox.Location = new System.Drawing.Point(291, 217);
            this.notice_textBox.Multiline = true;
            this.notice_textBox.Name = "notice_textBox";
            this.notice_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notice_textBox.Size = new System.Drawing.Size(377, 110);
            this.notice_textBox.TabIndex = 14;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.Comb_ThemeColor);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.FirstOpr_comboBox);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(690, 349);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Setting";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(10, 155);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 18);
            this.label18.TabIndex = 46;
            this.label18.Text = "アプリの配色";
            // 
            // Comb_ThemeColor
            // 
            this.Comb_ThemeColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Comb_ThemeColor.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Comb_ThemeColor.FormattingEnabled = true;
            this.Comb_ThemeColor.Items.AddRange(new object[] {
            "WHITE",
            "BLACK"});
            this.Comb_ThemeColor.Location = new System.Drawing.Point(13, 176);
            this.Comb_ThemeColor.Name = "Comb_ThemeColor";
            this.Comb_ThemeColor.Size = new System.Drawing.Size(147, 26);
            this.Comb_ThemeColor.TabIndex = 45;
            this.Comb_ThemeColor.SelectedIndexChanged += new System.EventHandler(this.Comb_ThemeColor_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button6.Location = new System.Drawing.Point(560, 13);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(109, 38);
            this.button6.TabIndex = 44;
            this.button6.Text = "Copyright";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.Copyright);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(12, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 52);
            this.button2.TabIndex = 43;
            this.button2.Text = "Save and Change";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Save_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(10, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 18);
            this.label8.TabIndex = 42;
            this.label8.Text = "デバイス接続時の動作モード";
            // 
            // FirstOpr_comboBox
            // 
            this.FirstOpr_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FirstOpr_comboBox.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FirstOpr_comboBox.FormattingEnabled = true;
            this.FirstOpr_comboBox.Location = new System.Drawing.Point(13, 97);
            this.FirstOpr_comboBox.Name = "FirstOpr_comboBox";
            this.FirstOpr_comboBox.Size = new System.Drawing.Size(148, 26);
            this.FirstOpr_comboBox.TabIndex = 41;
            this.FirstOpr_comboBox.SelectedIndexChanged += new System.EventHandler(this.FirstOpr_comboBox_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox1.Location = new System.Drawing.Point(13, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(175, 22);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Text = "Windows起動時に実行する";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Monitor_timer
            // 
            this.Monitor_timer.Interval = 200;
            this.Monitor_timer.Tick += new System.EventHandler(this.LinkMonitor_timer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "RotCon";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.表示ToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 48);
            // 
            // 表示ToolStripMenuItem
            // 
            this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
            this.表示ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.表示ToolStripMenuItem.Text = "RotController";
            this.表示ToolStripMenuItem.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 352);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(685, 23);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(134, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 375);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(701, 414);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button Port_Update_Button;
        private System.Windows.Forms.Button AutoConnect_Button;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer Monitor_timer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Left_label;
        private System.Windows.Forms.Label Right_label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label RotR;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox Lshortcut_textBox;
        private System.Windows.Forms.TextBox Rshortcut_textBox;
        private System.Windows.Forms.TextBox notice_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox Lmodifier_keyCmb;
        private System.Windows.Forms.ComboBox Rmodifier_keyCmb;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label portName_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LinkStatus_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label operational_mode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox FirstOpr_comboBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button Minimize;
        private System.Windows.Forms.ComboBox Comb_ThemeColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
    }
}

