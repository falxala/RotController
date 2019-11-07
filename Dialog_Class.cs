using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RotController
{
    public class Dialog_Class
    {
        public string FilePath()
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(ofd.FileName);
                return ofd.FileName;
            }
            return ofd.FileName;
        }

        /// <summary>
        ///Copyright 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Copyright()
        {
            Form form2 = new Form();
            form2.Text = "Copyright";
            TextBox CR = new TextBox();
            CR.Name = "CR";
            CR.Text = Properties.Resources.Copyright;
            CR.Multiline = true;
            CR.Dock = DockStyle.Fill;
            CR.ScrollBars = ScrollBars.Vertical;
            CR.Font = new System.Drawing.Font("Meiryo", 12, System.Drawing.FontStyle.Regular |
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            form2.Size = new Size(640, 360);
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.Controls.Add(CR);
            form2.ShowDialog();
            form2.Dispose();
        }
    }
}
