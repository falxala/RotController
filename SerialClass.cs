using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace RotController
{
    class SerialClass
    {

        /****************************************************************************/
        /*!
		 *	@brief	ボーレート格納用のクラス定義.
		 */
        public class BuadRateItem : Object
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
        public class HandShakeItem : Object
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

        public void Serial_properties(SerialPort serialPort, int databits, Parity parity, StopBits stopBits)
        {            
            serialPort.DataBits = databits;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;
        }

        /// <summary>
        /// シリアルポートへ接続
        /// </summary>
        /// <param name="COM">COM番号(COMXX)</param>
        /// <param name="BaudRate">ボーレート</param>
        /// <param name="HandShake">フロー制御</param>
        public void Connect(SerialPort serialPort, string COM, int BaudRate, Handshake HandShake, bool Rts)
        {
            if (serialPort.IsOpen == true)
            {
                serialPort.RtsEnable = false;
                //! シリアルポートをクローズする.
                serialPort.Close();
            }
            else
            {
                serialPort.PortName = COM;
                serialPort.BaudRate = BaudRate;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = HandShake;
                serialPort.Encoding = System.Text.Encoding.GetEncoding(0);
                if (Rts)
                    serialPort.RtsEnable = true;
                serialPort.Open();

            }
        }

    }
}
