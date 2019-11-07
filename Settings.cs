using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace RotController
{
    /// <summary>
    /// 汎用設定保存クラス
    /// </summary>
    public class Settings
    {
        static public string path = System.IO.Path.GetFileNameWithoutExtension(
            (Assembly.GetExecutingAssembly()).Location);//アプリケーション名取得
        //保存元のファイル名
        static string fileName = path + ".xml";

        static public void Write()
        {

            //保存するクラスのインスタンスを作成
            Config obj = new Config();
            obj.mode = Form1.mode;
            obj.key = Form1.key;
            obj.color = Form1.color;
            obj.prosess_name = Form1.tgt_processName;
            obj.runApp_path = Form1.runApp_path;


            //XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(typeof(Config));
            //書き込むファイルを開く（UTF-8 BOM無し）
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fileName, false, new System.Text.UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, obj);
            //ファイルを閉じる
            sw.Close();

        }



        //XMLファイルをClassオブジェクトに復元する
        public static void Read()
        {
            if (System.IO.File.Exists(fileName))
            {
                //MessageBox.Show("'" + fileName + "'は存在します。");
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Config));
                //読み込むファイルを開く
                System.IO.StreamReader sr = new System.IO.StreamReader(
                    fileName, new System.Text.UTF8Encoding(false));
                //XMLファイルから読み込み、逆シリアル化する
                Config obj = null;
                try
                {
                    obj = (Config)serializer.Deserialize(sr);


                    //ファイルを閉じる
                    sr.Close();

                    Form1.mode = obj.mode;
                    Form1.key = obj.key;
                    Form1.color = obj.color;
                    Form1.tgt_processName = obj.prosess_name;
                    Form1.runApp_path = obj.runApp_path;

                }
                catch (Exception)
                {
                    sr.Close();
                    MessageBox.Show("設定ファイルが破損しています\r\n再保存で修復されます", "ERROR");                    
                }

            }
            else
            {
                MessageBox.Show("\"" + fileName + "\"が見つかりませんでした\r\nファイルは自動的に作成されます", "ERROR");
                Settings.Write();//書き込み
            }

        }


        public class Config
        {
            [System.Xml.Serialization.XmlElement("動作モード")]
            public int mode;
            [System.Xml.Serialization.XmlElement("キープリセット1")]
            public string[] key;
            [System.Xml.Serialization.XmlElement("アプリケーション配色")]
            public int color;
            [System.Xml.Serialization.XmlElement("TARGET_PROCESS")]
            public string prosess_name;
            [System.Xml.Serialization.XmlElement("APPLICATION")]
            public string runApp_path;
        }
    }
}
