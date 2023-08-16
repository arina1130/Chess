using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess
{
    class File
    {
        public static FileInfo FileGame { get; private set; }
        static FileInfo fileSetting;
        public static string NameGame { get; private set; }
        public File(string name1, string name2)
        {
            FileCreation(name1, name2);
        }
        public static void SaveSetting(string timeW, string timeB, bool isMoveVisible)
        {
            fileSetting = new FileInfo(@"Games\" + NameGame + "_set");
            FileStream f = fileSetting.Create();
            f.Close();
            StreamWriter writer = new StreamWriter(fileSetting.FullName);
            writer.WriteLine(timeW);
            writer.WriteLine(timeB);
            if (isMoveVisible)
            { writer.WriteLine("+"); }
            else { writer.WriteLine("-"); }
            writer.Close();
        }
        public static void SaveHistoryInFile()
        {
            StreamWriter writer = new StreamWriter(FileGame.FullName,false);
            foreach (string h in HistoryOfGame.History)
            {
                writer.WriteLine(h);
            }
            writer.Close();
        }
        private void FileCreation(string name1, string name2)
        {
            try
            {
                NameGame = name1 + " - " + name2;
                FileGame = new FileInfo(@"Games\" + NameGame);
                FileStream f = FileGame.Create();
                f.Close();
            }
            catch(ArgumentException)
            {
                FileGame = null;
                MessageBox.Show("Неверный формат имен");
            }
        }
        public static Win OpeningFile(string name)
        {
            NameGame = name.Split('_')[0];
            bool isTimerOn;
            bool isVisMove;
            HistoryOfGame.History.Clear();
            FileGame = new FileInfo(@"Games\" + name.Split('_')[0]);
            StreamReader reader = new StreamReader(FileGame.FullName);
            while (reader.Peek() >= 0)
            {
                HistoryOfGame.History.Insert(0, reader.ReadLine());
            }
            reader.Close();
            fileSetting = new FileInfo(@"Games\" + name + "_set");
            try {
                StreamReader readerSet = new StreamReader(fileSetting.FullName);
                List<string> setting = new List<string>();
                while (readerSet.Peek() >= 0)
                {
                    setting.Add(readerSet.ReadLine());
                }
                readerSet.Close();
                if (setting[0] == "" && setting[1] == "")
                {
                    isTimerOn = false;
                }
                else
                {
                    isTimerOn = true;
                }
                if (setting[2] == "+")
                {
                    isVisMove = true;
                }
                else isVisMove = false;

                return new Win(isTimerOn, setting[0], setting[1], isVisMove);
            }
            catch { MessageBox.Show("Файл не найден"); }
            return null;
        }
    }
}
