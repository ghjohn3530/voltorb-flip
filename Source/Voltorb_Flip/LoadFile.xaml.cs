using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Voltorb_Flip
{
    /// <summary>
    /// Interaction logic for LoadFile.xaml
    /// </summary>
    public partial class LoadFile : Window
    {
        public LoadFile()
        {
            InitializeComponent();
            Show();
        }

        private void Combo_LoadFiles_Loaded(object sender, RoutedEventArgs e)
        {
            CreateFileList();
        }
        private void Combo_LoadFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected_value = Combo_LoadFiles.SelectedItem as string;
            int index = int.Parse(selected_value[11].ToString()) - 1;
            Coins.TotalCoins = int.Parse(Text.loads[index]);
            Text.SaveNum = index + 1;
        }

        private void CreateFileList()
        {
            Text.CreateFileList();
            Combo_LoadFiles.ItemsSource = Text.file_list;
            Combo_LoadFiles.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game game_screen = new Game();
            Close();
        }
    }
    public class Text
    {
        private static readonly string source = @"Resources\SaveFiles.txt";

        public static string[] loads = File.ReadAllLines(source);
        public static List<string> file_list = new List<string>();

        public static bool NewSave;
        public static int SaveNum;

        public static void CreateFileList()
        {
            for (int loadfile_idx = 1; loadfile_idx <= loads.Length; loadfile_idx++)
            {
                file_list.Add("Save File #" + loadfile_idx + " - " + loads[loadfile_idx - 1]);
            }
        }

        public static void SaveGame(int total_coins)
        {
            if(NewSave)
            {
                List<string> textFile = File.ReadAllLines(source).ToList();
                textFile.Add(total_coins.ToString());
                File.WriteAllLines(source, textFile);
                NewSave = false;
                SaveNum = textFile.Count;
                return;
            }
            else
            {
                string[] textFile = File.ReadAllLines(source);
                textFile[SaveNum - 1] = total_coins.ToString();
                File.WriteAllLines(source, textFile);
            }
        } //Saves Game

        public static string ReadTxtLine(string file_name, int line_number) //RETURNS A LINE FROM A TXT FILE
        {
            string line = File.ReadLines(file_name).Skip(line_number - 1).Take(1).First();
            return line;
        }

        public static void WriteTxtLine(string file_name, int line_number, string text) //WRITES A LINE TO THE TXT FILE
        {
            string[] textFile = File.ReadAllLines(file_name);
            textFile[line_number - 1] = text;
            File.WriteAllLines(file_name, textFile);
        }
    }
}
