using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices; // DLL Import
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;

namespace RotController
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        static public int mode = 0;     //動作モード
        static public string[] key = { "0", "NONE", "0", "NONE", "0", "NONE" };  //初期Key設定
        static public int color = 0;    //配色　0:白 1:黒
        static public string runApp_path = "";
        private int AnyKey_flag = 0;    //
        private int RmodIndex = 0;      //
        private int LmodIndex = 0;
        private int MmodIndex = 0;//
        private int Ctrl_MODDE = 0;     //操作デバイスの動作モード 0;ハードウェア 1:ソフトウェア
        private byte[] KeyComb1 = new byte[3];
        private byte[] KeyComb2 = new byte[3];
        private string[] KeyComb3 = new string[3];
        private string Rcv_buf;         //受信バッファ
        private string aplTitle = null; // アプリ名
        private string exeFile = null;  // exeファイル名
        private string jsFile = null;   // スクリプトファイル名
        private string lnkFile = null;  // リンク名

        //キーボードフック
        RamGecTools.KeyboardHook keyhook = new RamGecTools.KeyboardHook();

        private SerialClass SerialClass = new SerialClass();


        public Form1()
        {
            InitializeComponent();
            //設定読み込み
            Settings.Read();
            //設定適用
            Set_config();
            Shortcut_Initialization();
            Microsoft.Win32.SystemEvents.PowerModeChanged +=
              new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        RtsEnable_checkBox.Checked = true;

            keyhook.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            Status_view(Properties.Resources.Status1);
        }

        /// <summary>
        /// スリープ前に切断　復帰後再接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_PowerModeChanged(object sender,Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case Microsoft.Win32.PowerModes.Suspend:
                    if (serialPort1.IsOpen == true)
                        connect(serialPort1);
                    break;
                case Microsoft.Win32.PowerModes.Resume:
                    Reconnect_button_Click(null,null);
                    break;
            }
        }

        public void Set_config()
        {
            try
            {
                RmodIndex = int.Parse(key[0]);
                Rshortcut_textBox.Text = key[1];
                KeyComb3[0] = key[1];

                LmodIndex = int.Parse(key[2]);
                Lshortcut_textBox.Text = key[3];
                KeyComb3[1] = key[3];

                MmodIndex = int.Parse(key[4]);
                Mshortcut_textBox.Text = key[5];
                KeyComb3[2] = key[5];

                Set_modifie(RmodIndex, 0);
                Set_modifie(LmodIndex, 1);
                Set_modifie(MmodIndex, 2);
                Comb_ThemeColor.SelectedIndex = color;
                Change_Theme();
                Process_comboBox.Items.Add(tgt_processName);
                Process_comboBox.SelectedIndex = 0;
                textBox2.Text = runApp_path;
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show(Properties.Resources.Exception1,"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        static public short DefBR = 5;//DefaultBuadRate
        public void data_set()//各コンボボックスに値をセットする
        {
            cmbPortName.Items.Clear();
            cmbPortName.Text = "";

            notice_textBox.AppendText("SerialPort Update\r\n");

            //シリアルポート名をセット
            var check_reg = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");//正規表現
            ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
            ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

            //全てのPnPデバイスを探索しシリアル通信が行われるデバイスを随時追加する
            foreach (ManagementObject manageObj in manageObjCol)
            {
                //Nameプロパティを取得
                var namePropertyValue = manageObj.GetPropertyValue("Name");
                var CaptionPropertyValue = manageObj.GetPropertyValue("PNPDeviceID");
                /*
                 * PowerShell
                 * Get-WmiObject -Class Win32_SerialPort
                 */

                if (namePropertyValue == null)
                {
                    continue;
                }

                //Nameプロパティ文字列の一部が"(COM1)～(COM999)"と一致するときリストに追加"
                string name = namePropertyValue.ToString();
                if (check_reg.IsMatch(name))
                {
                    cmbPortName.Items.Add(name);
                    if (cmbPortName.Items.Count > 0)
                    {
                        cmbPortName.SelectedIndex = 0;
                    }
                }

            }


            cmbBaudRate.Items.Clear();

            // ボーレート選択コンボボックスに選択項目をセットする.
            SerialClass.BuadRateItem baud;

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "1200bps(リセット用)";
            baud.BAUDRATE = 1200;
            cmbBaudRate.Items.Add(baud);

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "9600bps";
            baud.BAUDRATE = 9600;
            cmbBaudRate.Items.Add(baud);

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "19200bps";
            baud.BAUDRATE = 19200;
            cmbBaudRate.Items.Add(baud);

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "38400bps";
            baud.BAUDRATE = 38400;
            cmbBaudRate.Items.Add(baud);

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "67600bps";
            baud.BAUDRATE = 57600;
            cmbBaudRate.Items.Add(baud);

            baud = new SerialClass.BuadRateItem();
            baud.NAME = "115200bps";
            baud.BAUDRATE = 115200;
            cmbBaudRate.Items.Add(baud);

            cmbBaudRate.SelectedIndex = DefBR;

            cmbHandShake.Items.Clear();

            // フロー制御選択コンボボックスに選択項目をセットする.
            SerialClass.HandShakeItem ctrl;
            ctrl = new SerialClass.HandShakeItem();
            ctrl.NAME = "NONE";
            ctrl.HANDSHAKE = Handshake.None;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new SerialClass.HandShakeItem();
            ctrl.NAME = "XON/XOFF";
            ctrl.HANDSHAKE = Handshake.XOnXOff;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new SerialClass.HandShakeItem();
            ctrl.NAME = "RTS/CTS";
            ctrl.HANDSHAKE = Handshake.RequestToSend;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new SerialClass.HandShakeItem();
            ctrl.NAME = "XON/XOFF + RTS/CTS";
            ctrl.HANDSHAKE = Handshake.RequestToSendXOnXOff;
            cmbHandShake.Items.Add(ctrl);
            cmbHandShake.SelectedIndex = 0;


        }

        private void Set_ModifierKey()
        {
            string[] comb = { "None", "Shift", "Ctrl", "Alt", "Win", "Ctrl + Shift",
                "Ctrl + Alt", "Alt + Shift", "Alt + Space", "Alt + Tab","Switch","Run Program" };
            Rmodifier_keyCmb.Items.Clear();
            Lmodifier_keyCmb.Items.Clear();
            Mmodifier_keyCmb.Items.Clear();
            for (int i = 0; i < comb.Length; i++)
            {
                if (i < 10)
                {
                    Rmodifier_keyCmb.Items.Add(comb[i]);
                    Lmodifier_keyCmb.Items.Add(comb[i]);
                }
                    Mmodifier_keyCmb.Items.Add(comb[i]);
            }
            Rmodifier_keyCmb.SelectedIndex = RmodIndex;
            Lmodifier_keyCmb.SelectedIndex = LmodIndex;
            Mmodifier_keyCmb.SelectedIndex = MmodIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectButton.Text = "接続";

            data_set();
            Set_ModifierKey();
            Auto_Connect(serialPort1);
            Monitor_timer.Start();
            FirstOpr_comboBox.Items.Add("Hardware");
            FirstOpr_comboBox.Items.Add("Software");
            FirstOpr_comboBox.SelectedIndex = mode;

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            this.Hide();
            notifyIcon1.Visible = true;
        }

        /// <summary>
        /// コンボボックスからCOM番号を抜き出します
        /// </summary>
        /// <returns></returns>
        Match com_match()
        {

            if (cmbPortName.SelectedIndex != -1)
            {
                //正規表現を用いてコンボボックスからCOM"XXX"を抜き出す
                var check = new Regex("(COM[1-9][0-9]?[0-9]?)");
                Match m = check.Match(cmbPortName.SelectedItem.ToString());
                return m;
            }
            else//エラー処理
            {
                MessageBox.Show(Properties.Resources.SerialError1,
                "Error",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);

            }
            return null;
        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            connect(serialPort1);
        }

        /// <summary>
        /// シリアルポート接続(接続時切断)
        /// </summary>
        /// <param name="serialPort"></param>
        private void connect(SerialPort serialPort)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    serialPort1.RtsEnable = false;

                    //! シリアルポートをクローズする.
                    serialPort1.Close();
                    notice_textBox.AppendText("SerialPort Close\r\n");
                }
                else
                {
                    //! オープンするシリアルポートをコンボボックスから取り出す.
                    if (com_match() == null)
                        return;
                    serialPort1.PortName = com_match().ToString();

                    //! ボーレートをコンボボックスから取り出す.
                    SerialClass.BuadRateItem baud = (SerialClass.BuadRateItem)cmbBaudRate.SelectedItem;
                    serialPort1.BaudRate = baud.BAUDRATE;

                    //! データビットをセットする. (データビット = 8ビット)
                    //! パリティビットをセットする. (パリティビット = なし)
                    //! ストップビットをセットする. (ストップビット = 1ビット)
                    SerialClass.Serial_properties(serialPort, 8, Parity.None, StopBits.One);

                    //! フロー制御をコンボボックスから取り出す.
                    SerialClass.HandShakeItem ctrl = (SerialClass.HandShakeItem)cmbHandShake.SelectedItem;
                    serialPort1.Handshake = ctrl.HANDSHAKE;

                    //! 文字コードをセットする.
                    //serialPort1.Encoding = Encoding.Unicode;
                    serialPort1.Encoding = System.Text.Encoding.GetEncoding(0);

                    try
                    {
                        if (RtsEnable_checkBox.Checked)
                            serialPort1.RtsEnable = true;

                        //! シリアルポートをオープンする.
                        serialPort1.Open();


                        notice_textBox.AppendText("SerialPort Open\r\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (serialPort.IsOpen)
                {
                    connectButton.Text = "切断";
                }
                else
                {
                    connectButton.Text = "接続";
                }
            }
        }

        private delegate void Delegate_RcvDataToTextBox(string data);
        private delegate void Delegate_RcvData(string data);

        private void serialPort1_DataReceived1(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                return;
            }

            try
            {
                //受信データを読み込む.
                string data = serialPort1.ReadExisting();

                //フォームがアクティブの場合のみ
                if (Form.ActiveForm == this)
                {
                    //!受信したデータをテキストボックスに書き込む.
                    Invoke(new Delegate_RcvDataToTextBox(RcvDataToTextBox), new Object[] { data });
                }
                Invoke(new Delegate_RcvDataToTextBox(RcvData), new Object[] { data });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RcvDataToTextBox(string data)
        {
            //! 受信データをテキストボックスの最後に追記する.
            if (data != null)
            {
                rcvTextBox.AppendText(data);
            }
        }

        private async void RcvData(string data)
        {
            Rcv_buf = data;
            if (data.Length >= 9)
            {
                if (Regex.IsMatch(data, @"Hardware"))
                {
                    operational_mode.Text = "Hardware";
                    Ctrl_MODDE = 0;
                    Status_view(Properties.Resources.Status7);
                }
                if (Regex.IsMatch(data, @"Software"))
                {
                    operational_mode.Text = "Software";
                    Ctrl_MODDE = 1;
                    Status_view(Properties.Resources.Status8);
                }

            }
            else
            {
                Task task = Task.Run(new Action(() =>
                {
                    if (data == "R")
                    {
                        BehiberOfRrotated();
                        if (WindowState != FormWindowState.Minimized)
                            Right_label.ForeColor = Color.Red;
                    }
                    if (data == "L")
                    {
                        BehiberOfLrotated();
                        if (WindowState != FormWindowState.Minimized)
                            Left_label.ForeColor = Color.Red;
                    }

                    if (data == "SW\r\n")
                    {
                        BehiberOfMrotated();
                        if (WindowState != FormWindowState.Minimized)
                            Push_label.ForeColor = Color.Red;
                    }

                    if (data == "Right\r\n")
                    {
                        if (WindowState != FormWindowState.Minimized)
                            Right_label.ForeColor = Color.Red;
                    }

                    if (data == "Left\r\n")
                    {
                        if (WindowState != FormWindowState.Minimized)
                            Left_label.ForeColor = Color.Red;
                    }

                    if (data == "Switch\r\n")
                    {
                        if (WindowState != FormWindowState.Minimized)
                            Push_label.ForeColor = Color.Red;
                    }

                }));
                await task;
                await Task.Delay(500);
                Right_label.ForeColor = Left_label.ForeColor= Push_label.ForeColor = 
                    Color.FromArgb(color*255, color * 255, color * 255);
            }
        }

        //0→R 1→L
        private void KeyComb_Set()
        {
            Set_modifie(Rmodifier_keyCmb.SelectedIndex, 0);
            Set_modifie(Lmodifier_keyCmb.SelectedIndex, 1);
            Set_modifie(Mmodifier_keyCmb.SelectedIndex, 2);
            KeyComb3[0] = Rshortcut_textBox.Text;
            KeyComb3[1] = Lshortcut_textBox.Text;
            KeyComb3[2] = Mshortcut_textBox.Text;
        }

        private void Set_modifie(int Combination, int m)
        {
            switch (Combination)
            {
                case 0:
                    KeyComb1[m] = 0x00;
                    KeyComb2[m] = 0x00;
                    break;
                case 1:
                    KeyComb1[m] = 0x10;
                    KeyComb2[m] = 0x00;
                    break;
                case 2:
                    KeyComb1[m] = 0x11;
                    KeyComb2[m] = 0x00;
                    break;
                case 3:
                    KeyComb1[m] = 0x12;
                    KeyComb2[m] = 0x00;
                    break;
                case 4:
                    KeyComb1[m] = 0x5b;
                    KeyComb2[m] = 0x00;
                    break;
                case 5:
                    KeyComb1[m] = 0x11;
                    KeyComb2[m] = 0x10;
                    break;
                case 6:
                    KeyComb1[m] = 0x11;
                    KeyComb2[m] = 0x12;
                    break;
                case 7:
                    KeyComb1[m] = 0x10;
                    KeyComb2[m] = 0x12;
                    break;
                case 8:
                    KeyComb1[m] = 0x12;
                    KeyComb2[m] = 0x20;
                    break;
                case 9:
                    KeyComb1[m] = 0x12;
                    KeyComb2[m] = 0x09;
                    break;
                case 10:
                    KeyComb1[m] = 0xff;
                    KeyComb2[m] = 0xff;
                    break;
                case 11:
                    KeyComb1[m] = 0xff;
                    KeyComb2[m] = 0xff;
                    break;
                default:
                    KeyComb1[m] = 0x00;
                    KeyComb2[m] = 0x00;
                    break;
            }
        }


        private void BehiberOfRrotated()
        {
            Active_Window(null,null);
            TriplekeySim(KeyComb1[0], KeyComb2[0], InputKeyClass.PickUp_Key(KeyComb3[0]), true, 50);
        }

        private void BehiberOfLrotated()
        {
            Active_Window(null, null);
            TriplekeySim(KeyComb1[1], KeyComb2[1], InputKeyClass.PickUp_Key(KeyComb3[1]), true, 50);
        }

        private void BehiberOfMrotated()
        {
            Active_Window(null, null);
            if (key[4] == "10")
            {
                Mode_Change(serialPort1);
            }
            if (key[4] == "11")
            {
                try
                {
                    Process.Start(runApp_path);
                }
                catch(Exception ex)
                {
                    Invoke(new Delegate_Notice(notice_Write), new Object[] { ex.Message });
                }
            }
            else
            {
                Active_Window(null, null);
                TriplekeySim(KeyComb1[2], KeyComb2[2], InputKeyClass.PickUp_Key(KeyComb3[2]), true, 50);
            }

        }

        private void notice_Write(string txt)
        {
            notice_textBox.AppendText(txt);
        }

        private delegate void Delegate_Notice(string txt);

        //http://edutainment-fun.com/hidemaru/microsoft/%E3%82%AD%E3%83%BC%E3%82%A8%E3%83%9F%E3%83%A5%E3%83%AC%E3%83%BC%E3%83%88%E9%80%81%E4%BF%A1%E3%81%AE%E3%81%BE%E3%81%A8%E3%82%81%E3%80%90c%E3%80%91%E3%80%90%E8%A6%9A%E6%9B%B8%E3%83%A1%E3%83%A2%E3%80%91_2535.html
        /// <summary>
        /// 3キーのコンビネーションキーを押下
        /// </summary>
        /// <param name="meta1">修飾キー1</param>
        /// <param name="meta2">修飾キー2</param>
        /// <param name="key">任意のキー</param>
        /// <param name="upf">開放を実行するか</param>
        /// <param name="delay">押し込み状態維持時間</param>
        private async void TriplekeySim(byte meta1, byte meta2, byte key, bool upf, int delay)
        {
            
            try
            {
                // キーの押し下げをシミュレートする。
                keybd_event(meta1, 0, 0, (UIntPtr)0);
                keybd_event(meta2, 0, 0, (UIntPtr)0);
                await Task.Delay(5);
                keybd_event(key, 0, 0, (UIntPtr)0);
                await Task.Delay(5);

                // キーの解放をシミュレートする。
                keybd_event(key, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
                if (upf)
                {
                    keybd_event(meta1, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
                    keybd_event(meta2, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
                }
            }
            catch (Exception ex)
            {
                Invoke(new Delegate_Notice(notice_Write), new Object[] { ex.Message });
                Console.WriteLine(ex.Message);
            }
        }

        private void RightKey_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            keyhook.Install();
            AnyKey_flag = 1;
            Status_view(Properties.Resources.Status3);
            this.Enabled = false;
        }

        private void LeftKey_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            keyhook.Install();
            AnyKey_flag = 2;
            Status_view(Properties.Resources.Status4);
            this.Enabled = false;
        }

        private void MiddleKey_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            keyhook.Install();
            AnyKey_flag = 3;
            Status_view(Properties.Resources.Status5);
            this.Enabled = false;
        }

        void keyboardHook_KeyDown(RamGecTools.KeyboardHook.VKeys key)
        {
            if (AnyKey_flag == 1)
            {
                Rshortcut_textBox.Text = key.ToString();
            }
            if (AnyKey_flag == 2)
            {
                Lshortcut_textBox.Text = key.ToString();
            }
            if (AnyKey_flag == 3)
            {
                Mshortcut_textBox.Text = key.ToString();
            }
            AnyKey_flag = 0;
            keyhook.Uninstall();
            KeyComb_Set();
            this.Enabled = true;
        }

        private void RtsEnable_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RtsEnable_checkBox.Checked == true)
            {
                serialPort1.RtsEnable = true;
            }
            else
            {
                serialPort1.RtsEnable = false;
            }
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen && serialPort1.RtsEnable)
            {
                e.Cancel = true;
                var tryCloseThread = new Thread(() =>
                {
                    try
                    {
                        serialPort1.RtsEnable = false;
                        serialPort1.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    this.Invoke((MethodInvoker)(() => this.Close()));
                });
                tryCloseThread.Start();

                // トレイリストのアイコンを非表示にする
                notifyIcon1.Visible = false;
            }

            // トレイリストのアイコンを非表示にする
            notifyIcon1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mode_Change(serialPort1);
            panel2.Focus();
        }

        private void Mode_Change(SerialPort serialPort)
        {
            string data = "H";

            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort.IsOpen == false)
            {
                LinkStatus_label.ForeColor = Color.Red;
                LinkStatus_label.Text = "Disconnected";
                operational_mode.Text = "unconnected";
                return;
            }

            if (Ctrl_MODDE == 0)
            {
                data = "S";
                Ctrl_MODDE = 1;

            }
            else if (Ctrl_MODDE == 1)
            {
                data = "H";
                Ctrl_MODDE = 0;
            }

            try
            {
                //! シリアルポートからテキストを送信する.
                serialPort.Write(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Port_Update_Button_Click(object sender, EventArgs e)
        {
            data_set();
        }

        private void AutoConnect_Button_Click(object sender, EventArgs e)
        {
            Auto_Connect(serialPort1);
        }

        /// <summary>
        /// シリアルポート自動接続
        /// </summary>
        /// <param name="serialPort"></param>
        private async void Auto_Connect(SerialPort serialPort)
        {

            data_set();
            string[] PortList = null;
            //! 利用可能なシリアルポート名の配列を取得する.
            PortList = SerialPort.GetPortNames();

            int num = PortList.Length;

            for (int i = 0; i < num; i++)
            {
                try
                {
                    if (serialPort.IsOpen == true)
                        return;

                    LinkStatus_label.ForeColor = Color.OrangeRed;
                    LinkStatus_label.Text = "Connecting";

                    SerialClass.Connect(serialPort, PortList[i], 115200, Handshake.None, RtsEnable_checkBox.Checked);
                    await Task.Delay(10);
                    serialPort.Write("R");
                    await Task.Delay(10);

                    if (Ctrl_MODDE == 1) Ctrl_MODDE = 0; else Ctrl_MODDE = 1;

                    if (!Regex.IsMatch(Rcv_buf, "[0-9]"))//ACK：0x06(0-9)を受信しなければ切断//
                    {
                        SerialClass.Connect(serialPort, PortList[i], 115200, Handshake.None, RtsEnable_checkBox.Checked);
                        notice_textBox.AppendText(PortList[i] + ": No response\r\n");
                    }

                    if (serialPort.IsOpen == true)
                    {
                        DeviceNum_label.Text = Regex.Match(Rcv_buf, "[0-9]").Value;
                        notice_textBox.AppendText(PortList[i] + ": Received an ACK\r\n");
                        if (FirstOpr_comboBox.SelectedIndex == 0)
                            serialPort.Write("H");
                        else
                            serialPort.Write("S");
                    }
                }
                catch (Exception ex)
                {
                    Invoke(new Delegate_Notice(notice_Write), new Object[] { ex.Message + "\r\n" });
                    continue;
                }
                finally
                {

                }
            }
        }


        private void LinkMonitor_timer_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                portName_label.Text = com_match().ToString();
                LinkStatus_label.ForeColor = Color.Green;
                LinkStatus_label.Text = "Connected";
                connectButton.Text = "切断";
            }
            else
            {
                LinkStatus_label.ForeColor = Color.Red;
                LinkStatus_label.Text = "Disconnected";
                portName_label.Text = "NONE";
                operational_mode.Text = "unconnected";
                DeviceNum_label.Text = "---";
                connectButton.Text = "接続";
            }
        }

        private async void PnpEvent(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            if (serialPort1.IsOpen == false)
            {
                Auto_Connect(serialPort1);
            }
        }



        enum WINDOW_MESSAGES : uint
        {
            WM_DEVICECHANGE = 0x0219,
        }

        private enum DBT
        {
            DBT_DEVICEARRIVAL = 0x8000,
            DBT_DEVICEQUERYREMOVE = 0x8001,
            DBT_DEVICEQUERYREMOVEFAILED = 0x8002,
            DBT_DEVICEREMOVEPENDING = 0x8003,
            DBT_DEVICEREMOVECOMPLETE = 0x8004,
        }


        //USBデバイスが接続されたときにPnpEventを発生
        protected override void WndProc(ref Message m)
        {
            switch ((WINDOW_MESSAGES)m.Msg)
            {
                case WINDOW_MESSAGES.WM_DEVICECHANGE:

                    switch ((DBT)m.WParam.ToInt32())
                    {
                        case DBT.DBT_DEVICEARRIVAL:
                            //ドライブが装着された時の処理を書く
                            PnpEvent(this, EventArgs.Empty);
                            break;
                        case DBT.DBT_DEVICEREMOVECOMPLETE:
                            //ドライブが取り外されたされた時の処理を書く
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void Shortcut_Initialization()
        {
            // プロジェクト＞プロパティ＞アセンブリ情報　で指定した「タイトル」を取得
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = Attribute.GetCustomAttribute(
              assembly,
              typeof(AssemblyTitleAttribute)
            ) as AssemblyTitleAttribute;
            aplTitle = attribute.Title;
            this.Text = aplTitle;

            // 自身のexeファイル名を取得
            exeFile = Path.GetFileName(Application.ExecutablePath);

            // WSHスクリプト名
            jsFile = Directory.GetParent(Application.ExecutablePath) + "\\addStartup.js";

            // ショートカットのリンク名
            String sMnu = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            lnkFile = sMnu + "\\" + aplTitle + ".lnk";

            // [スタートアップ]フォルダのパスをコンソールに表示
            Console.Write(Environment.GetFolderPath(Environment.SpecialFolder.Startup));

            // スタートアップ有無
            checkBox1.Checked = File.Exists(lnkFile);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                // スタートアップフォルダにショートカット作成
                //https://dobon.net/vb/dotnet/file/createshortcut.html
                try
                {
                    // スタートアップにショートカット作成
                    if (!File.Exists(lnkFile))
                    {
                        //作成するショートカットのパス
                        string shortcutPath = lnkFile;
                        //ショートカットのリンク先
                        string targetPath = Application.ExecutablePath;

                        //WshShellを作成
                        Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
                        dynamic shell = Activator.CreateInstance(t);

                        //WshShortcutを作成
                        var shortcut = shell.CreateShortcut(shortcutPath);

                        //リンク先
                        shortcut.TargetPath = targetPath;

                        shortcut.WorkingDirectory = Application.StartupPath;

                        //アイコンのパス
                        shortcut.IconLocation = Application.ExecutablePath + ",0";
                        //その他のプロパティも同様に設定できるため、省略

                        //ショートカットを作成
                        shortcut.Save();

                        //後始末
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);

                        // スタートアップフォルダに登録されたか確認
                        if (File.Exists(lnkFile))
                        {
                            Status_view(Properties.Resources.Status6);
                            MessageBox.Show(
                                "スタートアップに登録しました。\n\n" + lnkFile,
                                aplTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                
                        }
                        else
                        {
                            MessageBox.Show(
                                "スタートアップへの登録に失敗しました。\n\n" + lnkFile,
                                aplTitle,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "スタートアップへの登録に失敗しました。\n\n" + ex.Message,
                        aplTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                // スタートアップフォルダに登録済みか確認
                if (File.Exists(lnkFile))
                {
                    try
                    {
                        File.Delete(lnkFile);
                        MessageBox.Show(
                            "スタートアップから削除しました。",
                            aplTitle,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(
                            "スタートアップからの削除に失敗しました。\n\n" + ex.Message,
                            aplTitle,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }

            // スタートアップ有無
            checkBox1.Checked = File.Exists(lnkFile);

        }


        private void FirstOpr_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = FirstOpr_comboBox.SelectedIndex;
        }

        private void Rmodifier_keyCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            key[0] = Rmodifier_keyCmb.SelectedIndex.ToString();
            if (key[0] == "10")
            {
                Rshortcut_textBox.Clear();
            }
        }

        private void Rshortcut_textBox_TextChanged(object sender, EventArgs e)
        {
            key[1] = Rshortcut_textBox.Text;
        }

        private void Lmodifier_keyCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            key[2] = Lmodifier_keyCmb.SelectedIndex.ToString();
            if (key[2] == "10")
            {
                Lshortcut_textBox.Clear();
            }
        }

        private void Lshortcut_textBox_TextChanged(object sender, EventArgs e)
        {
            key[3] = Lshortcut_textBox.Text;
        }

        private void Mmodifier_keyCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mshortcut_textBox.Enabled = true;
            button8.Enabled = true;
            textBox2.Enabled = false;
            button9.Enabled = false;
            label15.Enabled = false;

            key[4] = Mmodifier_keyCmb.SelectedIndex.ToString();
            if(key[4] == "10")
            {
                Mshortcut_textBox.Enabled = false;
                button8.Enabled = false;
            }
            if (key[4] == "11")
            {
                Mshortcut_textBox.Enabled = false;
                button8.Enabled = false;
                textBox2.Enabled = true;
                button9.Enabled = true;
                label15.Enabled = true;
            }
        }

        private void Mshortcut_textBox_TextChanged(object sender, EventArgs e)
        {
            key[5] = Mshortcut_textBox.Text;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            KeyComb_Set();
            Settings.Write();
            Status_view(Properties.Resources.Status2);
            Change_Theme();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            if(Monitor_timer.Enabled == false)
                Monitor_timer.Start();
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                // フォームが最小化の状態であればフォームを非表示にする
                this.Hide();
                // トレイリストのアイコンを表示する
                notifyIcon1.Visible = true;
                //最小化時はタイマーストップ
                Monitor_timer.Stop();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // フォームを表示する
            this.Visible = true;
            // 現在の状態が最小化の状態であれば通常の状態に戻す
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            ShowInTaskbar = true;
            // フォームをアクティブにする
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// ステータスを5秒間表示します
        /// </summary>
        /// <param name="str"></param>
        async private void Status_view(string str)
        {
            toolStripStatusLabel1.Text = str;
            await Task.Delay(5000);
            toolStripStatusLabel1.Text = "";
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Change_Theme()
        {
            if (color == 0)
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
                foreach (Control c in this.tabControl1.Controls)
                {
                    c.BackColor = Color.White;
                    c.ForeColor = Color.Black;
                    foreach (Control c2 in c.Controls)
                    {
                        c2.BackColor = Color.White;
                        c2.ForeColor = Color.Black;
                    }

                }

                statusStrip1.BackColor = Color.White;
                statusStrip1.ForeColor = Color.Black;
                label17.ForeColor = Color.Red;
            }
            else if(color == 1)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                foreach (Control c in this.tabControl1.Controls)
                {
                        c.BackColor = Color.Black;
                        c.ForeColor = Color.White;
                    foreach (Control c2 in c.Controls)
                    {
                        c2.BackColor = Color.Black;
                        c2.ForeColor = Color.White;
                    }
                }
                statusStrip1.BackColor = Color.Black;
                statusStrip1.ForeColor = Color.White;
                label17.ForeColor = Color.Red;
            }
        }

        private void Comb_ThemeColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            color = Comb_ThemeColor.SelectedIndex;
        }

        private void OpenXML_Button_Click(object sender, EventArgs e)
        {
            var proc = new System.Diagnostics.Process();

            proc.StartInfo.FileName = "notepad.exe";
            proc.StartInfo.Arguments = Properties.Resources.ConfigFileName;
            proc.Start();
            this.Enabled = false;
            proc.WaitForExit();

            this.Enabled = true;
            Settings.Read();
            Set_config();
            //フォームを前面へ
            this.Activate();
        }

        private void Send_button_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(textBox1.Text + "\n");
                textBox1.Clear();
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Send_button_Click(null,null);
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //EnterやEscapeキーでビープ音が鳴らないようにする
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
            }
        }
        static public string tgt_processName = "NONE";
        private void Process_comboBox_Click(object sender, EventArgs e)
        {
            Process_comboBox.Items.Clear();
            Process_comboBox.Items.Add(tgt_processName);
            Process_comboBox.Items.Add("NONE");
            Process_comboBox.SelectedIndex = 0;
            //全てのプロセスを列挙する
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                //メインウィンドウのタイトルがある時だけ列挙する
                if (p.MainWindowTitle.Length != 0)
                {
                    //Console.WriteLine("プロセス名:" + p.ProcessName);
                    //Console.WriteLine("タイトル名:" + p.MainWindowTitle);
                    Process_comboBox.Items.Add(p.ProcessName);
                }
            }
        }

        private void Active_Window(object sender, EventArgs e)
        {
            try
            {
                if (tgt_processName != "NONE")
                    Microsoft.VisualBasic.Interaction.AppActivate(tgt_processName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Process_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            tgt_processName = Process_comboBox.SelectedItem.ToString();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                System.IO.Path.GetDirectoryName((Assembly.GetExecutingAssembly()).Location));
        }

        private void Reconnect_button_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                Auto_Connect(serialPort1);
            }
            else
            {
                Auto_Connect(serialPort1);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Dialog_Class d = new Dialog_Class();
            textBox2.Text = runApp_path = d.FilePath();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Dialog_Class d = new Dialog_Class();
            d.Copyright();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            runApp_path = textBox2.Text;
        }

        private void Button_CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void R_checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void L_checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
