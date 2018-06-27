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
using System.Diagnostics;

namespace RotController
{
    public partial class Form1 : Form
    {
        static public int mode = 0;
        static public string[] key = { "0", "NONE", "0", "NONE" };

        private string aplTitle = null; // アプリ名
        private string exeFile = null;  // exeファイル名
        private string jsFile = null;   // スクリプトファイル名
        private string lnkFile = null;  // リンク名
        private bool frmLoadFlg = false; // Form1ロード済みか

        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        //キーボードフック
        RamGecTools.KeyboardHook keyhook = new RamGecTools.KeyboardHook();

        /****************************************************************************/
        /*!
		 *	@brief	ボーレート格納用のクラス定義.
		 */
        private class BuadRateItem : Object
        {
            private string m_name = "";
            private int m_value = 0;

            // 表示名称
            public string NAME
            {
                set { m_name = value; }
                get { return m_name; }
            }

            // ボーレート設定値.
            public int BAUDRATE
            {
                set { m_value = value; }
                get { return m_value; }
            }

            // コンボボックス表示用の文字列取得関数.
            public override string ToString()
            {
                return m_name;
            }
        }

        /****************************************************************************/
        /*!
		 *	@brief	制御プロトコル格納用のクラス定義.
		 */
        private class HandShakeItem : Object
        {
            private string m_name = "";
            private Handshake m_value = Handshake.None;

            // 表示名称
            public string NAME
            {
                set { m_name = value; }
                get { return m_name; }
            }

            // 制御プロトコル設定値.
            public Handshake HANDSHAKE
            {
                set { m_value = value; }
                get { return m_value; }
            }

            // コンボボックス表示用の文字列取得関数.
            public override string ToString()
            {
                return m_name;
            }
        }


        public Form1()
        {
            InitializeComponent();
            //設定読み込み
            Settings.Read();
            //設定適用
            set_config();
            Shortcut_Initialization();

            RtsEnable_checkBox.Checked = true;
            keyhook.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
        }

        public void set_config()
        {
            RmodIndex = int.Parse(key[0]);
            Rshortcut_textBox.Text = key[1];
            KeyComb3[0] = key[1];
            LmodIndex = int.Parse(key[2]);
            Lshortcut_textBox.Text = key[3];
            KeyComb3[1] = key[3];
            Set_modifie(RmodIndex, 0);
            Set_modifie(LmodIndex, 1);

        }

        static public short DefBR = 2;//DefaultBuadRate
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
            BuadRateItem baud;

            baud = new BuadRateItem();
            baud.NAME = "9600bps";
            baud.BAUDRATE = 9600;
            cmbBaudRate.Items.Add(baud);

            baud = new BuadRateItem();
            baud.NAME = "19200bps";
            baud.BAUDRATE = 19200;
            cmbBaudRate.Items.Add(baud);

            baud = new BuadRateItem();
            baud.NAME = "38400bps";
            baud.BAUDRATE = 38400;
            cmbBaudRate.Items.Add(baud);

            baud = new BuadRateItem();
            baud.NAME = "67600bps";
            baud.BAUDRATE = 57600;
            cmbBaudRate.Items.Add(baud);

            baud = new BuadRateItem();
            baud.NAME = "115200bps";
            baud.BAUDRATE = 115200;
            cmbBaudRate.Items.Add(baud);

            cmbBaudRate.SelectedIndex = DefBR;

            cmbHandShake.Items.Clear();

            // フロー制御選択コンボボックスに選択項目をセットする.
            HandShakeItem ctrl;
            ctrl = new HandShakeItem();
            ctrl.NAME = "NONE";
            ctrl.HANDSHAKE = Handshake.None;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new HandShakeItem();
            ctrl.NAME = "XON/XOFF";
            ctrl.HANDSHAKE = Handshake.XOnXOff;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new HandShakeItem();
            ctrl.NAME = "RTS/CTS";
            ctrl.HANDSHAKE = Handshake.RequestToSend;
            cmbHandShake.Items.Add(ctrl);

            ctrl = new HandShakeItem();
            ctrl.NAME = "XON/XOFF + RTS/CTS";
            ctrl.HANDSHAKE = Handshake.RequestToSendXOnXOff;
            cmbHandShake.Items.Add(ctrl);
            cmbHandShake.SelectedIndex = 0;


        }

        int RmodIndex = 0;
        int LmodIndex = 0;
        private void Set_ModifierKey()
        {
            string[] comb = { "None", "Shift", "Ctrl", "Alt", "Win", "Ctrl + Shift", "Ctrl + Alt", "Shift + Alt", "Alt + Space" };
            Rmodifier_keyCmb.Items.Clear();
            Lmodifier_keyCmb.Items.Clear();
            for (int i = 0; i < comb.Length; i++)
            {
                Rmodifier_keyCmb.Items.Add(comb[i]);
                Lmodifier_keyCmb.Items.Add(comb[i]);
            }
            Rmodifier_keyCmb.SelectedIndex = RmodIndex;
            Lmodifier_keyCmb.SelectedIndex = LmodIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectButton.Text = "接続";

            data_set();
            Set_ModifierKey();
            Auto_Connect();
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
                var check = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");
                System.Text.RegularExpressions.Match m = check.Match(cmbPortName.SelectedItem.ToString());
                return m;
            }
            else//エラー処理
            {
                MessageBox.Show("利用可能なポートがありません。",
                "エラー",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);

            }
            return null;
        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    serialPort1.RtsEnable = false;

                    //! シリアルポートをクローズする.
                    serialPort1.Close();
                    notice_textBox.AppendText("SerialPort Close\r\n");

                    //! ボタンの表示を[切断]から[接続]に変える.
                    connectButton.Text = "接続";
                }
                else
                {
                    //! オープンするシリアルポートをコンボボックスから取り出す.
                    if (com_match() == null)
                        return;
                    serialPort1.PortName = com_match().ToString();

                    //! ボーレートをコンボボックスから取り出す.
                    BuadRateItem baud = (BuadRateItem)cmbBaudRate.SelectedItem;
                    serialPort1.BaudRate = baud.BAUDRATE;

                    //! データビットをセットする. (データビット = 8ビット)
                    serialPort1.DataBits = 8;

                    //! パリティビットをセットする. (パリティビット = なし)
                    serialPort1.Parity = Parity.None;

                    //! ストップビットをセットする. (ストップビット = 1ビット)
                    serialPort1.StopBits = StopBits.One;

                    //! フロー制御をコンボボックスから取り出す.
                    HandShakeItem ctrl = (HandShakeItem)cmbHandShake.SelectedItem;
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

                        //! ボタンの表示を[接続]から[切断]に変える.
                        connectButton.Text = "切断";

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
                //! 受信データを読み込む.
                string data = serialPort1.ReadExisting();

                //フォームがアクティブ場合のみ
                if (Form.ActiveForm == this)
                {
                    //! 受信したデータをテキストボックスに書き込む.
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

        string Rcv_buf;
        private async void RcvData(string data)
        {
            Rcv_buf = data;
            if (data.Length >= 9)
            {
                if (Regex.IsMatch(data, @"Hardware"))
                {
                    operational_mode.Text = "Hardware";
                    Ctrl_MODDE = 0;
                }
                if (Regex.IsMatch(data, @"Software"))
                {
                    operational_mode.Text = "Software";
                    Ctrl_MODDE = 1;
                }

            }
            else
            {
                Task task = Task.Run(new Action(() =>
                {
                    if (data == "R")
                    {
                        BehiberOfRrotated();
                        Right_label.ForeColor = Color.Red;
                    }
                    if (data == "L")
                    {
                        BehiberOfLrotated();
                        Left_label.ForeColor = Color.Red;
                    }

                    if (data == "Right\r\n")
                    {
                        Right_label.ForeColor = Color.Red;
                    }

                    if (data == "Left\r\n")
                    {
                        Left_label.ForeColor = Color.Red;
                    }

                }));
                await task;
                await Task.Delay(200);
                Right_label.ForeColor = Color.Black;
                Left_label.ForeColor = Color.Black;
            }
        }

        //0→R 1→L
        private void KeyComb_Set()
        {
            Set_modifie(Rmodifier_keyCmb.SelectedIndex, 0);
            Set_modifie(Lmodifier_keyCmb.SelectedIndex, 1);
            KeyComb3[0] = Rshortcut_textBox.Text;
            KeyComb3[1] = Lshortcut_textBox.Text;
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
                default:
                    KeyComb1[m] = 0x00;
                    KeyComb2[m] = 0x00;
                    break;
            }
        }



        byte[] KeyComb1 = new byte[2];
        byte[] KeyComb2 = new byte[2];
        string[] KeyComb3 = new string[2];

        private void BehiberOfRrotated()
        {
            //SendKeys.SendWait("]");

            //DoublekeySim(0x11, 0x6B, true);
            //TriplekeySim(0xa4, 0x00, 0x09, true, 1000);
            TriplekeySim(KeyComb1[0], KeyComb2[0], InputKeyClass.PickUp_Key(KeyComb3[0]), true, 50);

            //Volume.VolumeUP();
        }

        private void BehiberOfLrotated()
        {
            //SendKeys.SendWait("[");

            //DoublekeySim(0x11, 0x6D, true);
            //TriplekeySim(0xa4, 0x10, 0x09, true, 1000);
            TriplekeySim(KeyComb1[1], KeyComb2[1], InputKeyClass.PickUp_Key(KeyComb3[1]), true, 50);
            //Volume.VolumeDown();

        }

        //http://edutainment-fun.com/hidemaru/microsoft/%E3%82%AD%E3%83%BC%E3%82%A8%E3%83%9F%E3%83%A5%E3%83%AC%E3%83%BC%E3%83%88%E9%80%81%E4%BF%A1%E3%81%AE%E3%81%BE%E3%81%A8%E3%82%81%E3%80%90c%E3%80%91%E3%80%90%E8%A6%9A%E6%9B%B8%E3%83%A1%E3%83%A2%E3%80%91_2535.html
        private void DoublekeySim(byte meta, byte key, bool upf)
        {
            // キーの押し下げをシミュレートする。
            keybd_event(meta, 0, 0, (UIntPtr)0);
            System.Threading.Thread.Sleep(50);
            keybd_event(key, 0, 0, (UIntPtr)0);

            System.Threading.Thread.Sleep(50);

            // キーの解放をシミュレートする。
            keybd_event(key, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
            if (upf)
                keybd_event(meta, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
        }

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
            // キーの押し下げをシミュレートする。
            keybd_event(meta1, 0, 0, (UIntPtr)0);
            keybd_event(meta2, 0, 0, (UIntPtr)0);
            await Task.Delay(1);
            keybd_event(key, 0, 0, (UIntPtr)0);

            // キーの解放をシミュレートする。
            keybd_event(key, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
            if (upf)
            {
                keybd_event(meta1, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
                keybd_event(meta2, 0, 2/*KEYEVENTF_KEYUP*/, (UIntPtr)0);
            }
        }

        int AnyKey_flag = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            Rshortcut_textBox.Focus();
            keyhook.Install();
            AnyKey_flag = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lshortcut_textBox.Focus();
            keyhook.Install();
            AnyKey_flag = 2;
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
            AnyKey_flag = 0;
            keyhook.Uninstall();
            KeyComb_Set();
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

        int Ctrl_MODDE = 0;//操作デバイスの動作モード 0;ハードウェア 1:ソフトウェア
        private void button1_Click(object sender, EventArgs e)
        {
            Mode_Change();
            panel2.Focus();
        }

        private void Mode_Change()
        {
            string data = "H";

            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                LinkStatus_label.ForeColor = Color.Red;
                LinkStatus_label.Text = "Disconnected";
                operational_mode.Text = "Unknown";
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
                serialPort1.Write(data);
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
            Auto_Connect();
        }

        private async void Auto_Connect()
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
                    if (serialPort1.IsOpen == true)
                        return;

                    LinkStatus_label.ForeColor = Color.OrangeRed;
                    LinkStatus_label.Text = "Connecting";

                    Serial_Connect(PortList[i], 38400, Handshake.None);
                    await Task.Delay(10);
                    serialPort1.Write("R");
                    await Task.Delay(10);

                    if (Ctrl_MODDE == 1) Ctrl_MODDE = 0; else Ctrl_MODDE = 1;

                    if (Rcv_buf != "6")//ACK：0x06(6)を受信しなければ切断
                    {
                        Serial_Connect(PortList[i], 38400, Handshake.None);
                        notice_textBox.AppendText(PortList[i] + ": No response\r\n");
                    }

                    if (serialPort1.IsOpen == true)
                    {
                        LinkStatus_label.ForeColor = Color.Green;
                        LinkStatus_label.Text = "Connected";
                        notice_textBox.AppendText(PortList[i] + ": Received an ACK\r\n");
                        if (FirstOpr_comboBox.SelectedIndex == 0)
                            serialPort1.Write("H");
                        else
                            serialPort1.Write("S");
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            if (serialPort1.IsOpen == false)
            {
                operational_mode.Text = "Unknown";
            }
        }

        /// <summary>
        /// シリアルポートへ接続
        /// </summary>
        /// <param name="COM">COM番号(COMXX)</param>
        /// <param name="BaudRate">ボーレート</param>
        /// <param name="HandShake">フロー制御</param>
        private void Serial_Connect(string COM, int BaudRate, Handshake HandShake)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.RtsEnable = false;

                //! シリアルポートをクローズする.
                serialPort1.Close();
                notice_textBox.AppendText("SerialPort Close\r\n");

                //! ボタンの表示を[切断]から[接続]に変える.
                connectButton.Text = "接続";
            }
            else
            {
                if (com_match() == null)
                    return;
                serialPort1.PortName = COM;
                serialPort1.BaudRate = BaudRate;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = HandShake;
                serialPort1.Encoding = System.Text.Encoding.GetEncoding(0);
                try
                {
                    if (RtsEnable_checkBox.Checked)
                        serialPort1.RtsEnable = true;
                    serialPort1.Open();
                    connectButton.Text = "切断";
                    notice_textBox.AppendText("SerialPort Open\r\n");
                    portName_label.Text = COM;
                }
                catch (Exception ex)
                {
                    notice_textBox.AppendText(ex.Message);
                }
            }
        }

        private void LinkMonitor_time_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                LinkStatus_label.ForeColor = Color.Red;
                LinkStatus_label.Text = "Disconnected";
                portName_label.Text = "NONE";
                operational_mode.Text = "Unknown";
            }
        }

        private async void PnpEvent(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            if (serialPort1.IsOpen == false)
            {
                Auto_Connect();

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

            /*
            if (checkBox1.Checked == true)
            {
                //Runキーを開く
                Microsoft.Win32.RegistryKey regkey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Run", true);
                //値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
                regkey.SetValue(Application.ProductName, Application.ExecutablePath);
                //閉じる
                regkey.Close();
                startup = true;
            }
            else
            {
                del_reg();
                startup = false;
            }
            */
        }

        /*
        private void del_reg()
        {
            //Runキーを開く
            Microsoft.Win32.RegistryKey regkey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            //キーの削除
            regkey.DeleteValue(Application.ProductName, false);
            //閉じる
            regkey.Close();
        }
        */

        private void FirstOpr_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = FirstOpr_comboBox.SelectedIndex;
        }

        private void Rmodifier_keyCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            key[0] = Rmodifier_keyCmb.SelectedIndex.ToString();
        }

        private void Rshortcut_textBox_TextChanged(object sender, EventArgs e)
        {
            key[1] = Rshortcut_textBox.Text;
        }

        private void Lmodifier_keyCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            key[2] = Lmodifier_keyCmb.SelectedIndex.ToString();
        }

        private void Lshortcut_textBox_TextChanged(object sender, EventArgs e)
        {
            key[3] = Lshortcut_textBox.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings.Write();
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                // フォームが最小化の状態であればフォームを非表示にする
                this.Hide();
                // トレイリストのアイコンを表示する
                notifyIcon1.Visible = true;
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
    }
}
