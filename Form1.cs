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

namespace RotController
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

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
            RtsEnable_checkBox.Checked = true;
        }

        static public short DefBR = 2;//DefaultBuadRate
        public void data_set()//各コンボボックスに値をセットする
        {
            cmbPortName.Items.Clear();
            cmbPortName.Text = "";

            //シリアルポート名をセット
            var check_reg = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");//正規表現
            ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
            ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

            //全てのPnPデバイスを探索しシリアル通信が行われるデバイスを随時追加する
            foreach (ManagementObject manageObj in manageObjCol)
            {
                //Nameプロパティを取得
                var namePropertyValue = manageObj.GetPropertyValue("Name");
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


        int default_mode = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            connectButton.Text = "接続";

            data_set();
            Auto_Connect();
            Monitor_timer.Start();
            FirstOpr_comboBox.Items.Add("Hardware");
            FirstOpr_comboBox.Items.Add("Software");
            FirstOpr_comboBox.SelectedIndex = default_mode;

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
            {/*
                MessageBox.Show("利用可能なポートがありません。",
                "エラー",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);*/

            }
            return null;
        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.RtsEnable = false;

                //! シリアルポートをクローズする.
                serialPort1.Close();

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                else if (Regex.IsMatch(data, @"Software"))
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
                        //SendKeys.SendWait("]");

                        doublekeySim(0x11, 0x6B, true);

                        //Volume.VolumeUP();

                        Right_label.ForeColor = Color.Red;
                    }
                    if (data == "L")
                    {
                        //SendKeys.SendWait("[");

                        doublekeySim(0x11, 0x6D, true);

                        //Volume.VolumeDown();

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

        //http://edutainment-fun.com/hidemaru/microsoft/%E3%82%AD%E3%83%BC%E3%82%A8%E3%83%9F%E3%83%A5%E3%83%AC%E3%83%BC%E3%83%88%E9%80%81%E4%BF%A1%E3%81%AE%E3%81%BE%E3%81%A8%E3%82%81%E3%80%90c%E3%80%91%E3%80%90%E8%A6%9A%E6%9B%B8%E3%83%A1%E3%83%A2%E3%80%91_2535.html
        private void doublekeySim(byte meta, byte key, bool upf)
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
            }
        }

        int Ctrl_MODDE = 0;//操作デバイスの動作モード 0;ハードウェア 1:ソフトウェア
        private void button1_Click(object sender, EventArgs e)
        {
            Mode_Change();
        }

        private void Mode_Change()
        {
            string data = "H";

            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false)
            {
                LinkStatus_label.Text = "切断";
                operational_mode.Text = "不明";
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

            string[] PortList;
            //! 利用可能なシリアルポート名の配列を取得する.
            PortList = SerialPort.GetPortNames();

            int num = PortList.Length;

            for (int i = 0; i < num; i++)
            {
                try
                {
                    if (serialPort1.IsOpen == true)
                        return;

                    LinkStatus_label.Text = "接続試行中";

                    Serial_Connect(PortList[i], 38400, Handshake.None);
                    await Task.Delay(100);
                    serialPort1.Write("R");
                    await Task.Delay(100);

                    if (Ctrl_MODDE == 1) Ctrl_MODDE = 0; else Ctrl_MODDE = 1;

                    if (Rcv_buf != "6")//ACK：0x06(6)を受信しなければ切断
                    {
                        Serial_Connect(PortList[i], 38400, Handshake.None);
                    }
                    await Task.Delay(100);
                    if (serialPort1.IsOpen == true)
                    {
                        LinkStatus_label.Text = "接続済み";

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
                operational_mode.Text = "不明";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LinkMonitor_time_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                LinkStatus_label.Text = "切断";
                operational_mode.Text = "不明";
            }
        }

        private async void PnpEvent(object sender, EventArgs e)
        {
            await Task.Delay(500);
            if (SerialPort.GetPortNames().Length != 0)
                Auto_Connect();
        }



        enum WINDOW_MESSAGES : uint
        {
            WM_DEVICECHANGE = 0x0219,
        }

        //USBデバイスが接続されたときにPnpEventを発生
        protected override void WndProc(ref Message m)
        {
            switch ((WINDOW_MESSAGES)m.Msg)
            {
                case WINDOW_MESSAGES.WM_DEVICECHANGE:
                    PnpEvent(this, EventArgs.Empty);
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
