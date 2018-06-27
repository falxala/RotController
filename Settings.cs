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
        static string path = System.IO.Path.GetFileNameWithoutExtension((Assembly.GetExecutingAssembly()).Location);//アプリケーション名取得
        //保存元のファイル名
        static string fileName = path + ".xml";

        static public void Write()
        {

            //保存するクラスのインスタンスを作成
            temp obj = new temp();
            obj.mode = Form1.mode;
            obj.key = Form1.key;


            //XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(typeof(temp));
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
                    new System.Xml.Serialization.XmlSerializer(typeof(temp));
                //読み込むファイルを開く
                System.IO.StreamReader sr = new System.IO.StreamReader(
                    fileName, new System.Text.UTF8Encoding(false));
                //XMLファイルから読み込み、逆シリアル化する
                temp obj = null;
                try
                {
                    obj = (temp)serializer.Deserialize(sr);


                    //ファイルを閉じる
                    sr.Close();

                    Form1.mode = obj.mode;
                    Form1.key = obj.key;

                }
                catch (Exception)
                {
                    MessageBox.Show("設定ファイルが破損しています", "ERROR");
                    Application.Exit();
                    Environment.Exit(0);
                }

            }
            else
            {
                MessageBox.Show("\"" + fileName + "\"が見つかりませんでした\r\nファイルは自動的に作成されます", "ERROR");
                Settings.Write();//書き込み
            }

        }


        public class temp
        {
            public int mode;
            public string[] key;
        }
    }
}
