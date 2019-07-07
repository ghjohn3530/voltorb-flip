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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Voltorb_Flip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /*
     * TODO: Make Side Indicators
       TODO: Make TXT save files
     */ 
    public class Tile
    {
        public static Random rand = new Random();
        static int randomizer = 10;

        public byte content = 0; // 0 - null, 4 - voltorb
        public bool flipped = false;
        public bool selected;
        public bool[] markings = { false, false, false, false }; // V, 1, 2, 3

        public static int[] selection = { 0, 0 };

        public static List<List<Tile>> tiles = new List<List<Tile>>
        {
            new List<Tile>
            {
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile()
            }, //Row 0
            new List<Tile>
            {
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile()
            }, //Row 1
            new List<Tile>
            {
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile()
            }, //Row 2
            new List<Tile>
            {
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile()
            }, //Row 3
            new List<Tile>
            {
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile(),
                new Tile()
            }  //Row 4
        }; // tiles grid
        public static List<List<int>> voltorb_numbers = new List<List<int>>()
        {
            new List<int>() {0,0,0,0,0}, //columns
            new List<int>() {0,0,0,0,0}  //rows
        };
        public static List<List<int>> total_numbers = new List<List<int>>()
        {
            new List<int>() {0,0,0,0,0}, //columns
            new List<int>() {0,0,0,0,0}  //rows
        };


        public static void SetGrid(int num_voltorbs = 5, int num_2s = 4, int num_3s = 1)
        {
            ResetGrid();

            while (num_voltorbs > 0)
            {
                foreach(List<Tile> row in tiles)
                {
                    foreach(Tile tile in row)
                    {
                        if (num_voltorbs > 0 && rand.Next(0,randomizer) == 0 && tile.content == 0)
                        {
                            tile.content = 4;
                            num_voltorbs--;
                        }
                    }
                }
            }

            while (num_2s > 0)
            {
                foreach (List<Tile> row in tiles)
                {
                    foreach (Tile tile in row)
                    {
                        if (num_2s > 0 && rand.Next(0, randomizer) == 0 && tile.content == 0)
                        {
                            tile.content = 2;
                            num_2s--;
                        }
                    }
                }
            }

            while (num_3s > 0)
            {
                foreach (List<Tile> row in tiles)
                {
                    foreach (Tile tile in row)
                    {
                        if (num_3s > 0 && rand.Next(0, randomizer) == 0 && tile.content == 0)
                        {
                            tile.content = 3;
                            num_3s--;
                        }
                    }
                }
            }
            
            foreach (List<Tile> row in tiles)
            {
                foreach (Tile tile in row)
                {
                    if (tile.content == 0)
                    {
                        tile.content = 1;
                    }
                }
            }
        }
        static void ResetGrid()
        {
            foreach(List<Tile> row in tiles)
            {
                foreach(Tile tile in row)
                {
                    tile.content = 0;
                    tile.selected = false;
                    tile.flipped = false;
                    tile.markings[0] = false;
                    tile.markings[1] = false;
                    tile.markings[2] = false;
                    tile.markings[3] = false;
                }
            }
        } //Sets all to 0

        public static void AddMarkings(int marking)
        {
            foreach (var row in tiles)
            {
                foreach (var tile in row)
                {
                    if (tile.selected)
                    {
                        if (tile.markings[marking])
                        {
                            tile.markings[marking] = false;
                        }
                        else
                        {
                            tile.markings[marking] = true;
                        }
                        return;
                    }
                }
            }
        } //Change tile markings

        public static void SetNumbers()
        {
            ResetNumbers();

            for (int column = 0; column < 5; column++)
            {
                for(int row = 0; row < 5; row++)
                {
                    switch (tiles[row][column].content)
                    {
                        case 0:
                        case 1:
                            total_numbers[0][column] += 1;
                            break;
                        case 2:
                            total_numbers[0][column] += 2;
                            break;
                        case 3:
                            total_numbers[0][column] += 3;
                            break;
                        case 4:
                            voltorb_numbers[0][column] += 1;
                            break;
                    }
                }
            }

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    switch (tiles[row][column].content)
                    {
                        case 0:
                        case 1:
                            total_numbers[1][row] += 1;
                            break;
                        case 2:
                            total_numbers[1][row] += 2;
                            break;
                        case 3:
                            total_numbers[1][row] += 3;
                            break;
                        case 4:
                            voltorb_numbers[1][row] += 1;
                            break;
                    }
                }
            }
        } //Sets total_numbers and voltorb_numbers
        static void ResetNumbers()
        {
            for(int i = 0; i < 2; i++)
            {
                for(int number_idx = 0; number_idx < 5; number_idx++)
                {
                    total_numbers[i][number_idx] = 0;
                    voltorb_numbers[i][number_idx] = 0;
                }
            }
        }

        public static void DeselectAll()
        {
            foreach(List<Tile> row in tiles)
            {
                foreach(Tile tile in row)
                {
                    tile.selected = false;
                }
            }
        } //Deselects all tiles

        public static bool WinCheck()
        {
            foreach(var row in tiles)
            {
                foreach(var column in row)
                {
                    if (!column.flipped)
                    {
                        switch(column.content)
                        {
                            case 2:
                            case 3:
                                return false;
                            default:
                                continue;
                        }
                    }
                }
            }
            return true;
        } //Checks if all 2s + 3s are flipped, returns bool
    }

    public class Visuals
    {
        public static string[] tiles_directories = Directory.GetFiles(@".\Resources\tiles\");
        public static string[] selected_directories = Directory.GetFiles(@".\Resources\selected\");

        public static List<ImageBrush> tiles = new List<ImageBrush>();
        public static List<ImageBrush> selected = new List<ImageBrush>();

        public static List<List<ImageBrush>> Board = new List<List<ImageBrush>>(); //Grid of ImageBrushes

        public static void MakeImages()
        {
            ImageBrush current = new ImageBrush();
            for(int pic_idx = 0; pic_idx < tiles_directories.Length; pic_idx++)
            {
                current.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, tiles_directories[pic_idx])));
                tiles.Add(current);
                current = new ImageBrush();
            }
            foreach(string directory in selected_directories)
            {
                current.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, directory)));
                selected.Add(current);
                current = new ImageBrush();
            }

            Board.Add(new List<ImageBrush>()
            {
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0]
            }); //Row 1
            Board.Add(new List<ImageBrush>()
            {
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0]
            }); //Row 2
            Board.Add(new List<ImageBrush>()
            {
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0]
            }); //Row 3
            Board.Add(new List<ImageBrush>()
            {
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0]
            }); //Row 4
            Board.Add(new List<ImageBrush>()
            {
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0],
                tiles[0]
            }); //Row 5
        }
        
        public static void UpdateBoard()
        {
            Tile current = null;

            for (int row = 0; row < Board.Count; row++)
            {
                for (int tile_idx = 0; tile_idx < Board[0].Count; tile_idx++)
                {
                    current = Tile.tiles[row][tile_idx];

                    if (!current.selected) //tiles
                    {
                        if (!current.flipped) //notflipped
                        {
                            if (!(current.markings[0] || current.markings[1] || current.markings[2] || current.markings[3]))
                            {
                                Board[row][tile_idx] = tiles[0];
                            }
                            else if (current.markings[0])
                            {
                                if (!(current.markings[1] || current.markings[2] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = tiles[1];
                                }
                                else if (!(current.markings[2] || current.markings [3]))
                                {
                                    Board[row][tile_idx] = tiles[2];
                                }
                                else if (!(current.markings[1] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = tiles[3];
                                }
                                else if (!(current.markings[1] || current.markings[2]))
                                {
                                    Board[row][tile_idx] = tiles[4];
                                }
                                else if (!current.markings[3])
                                {
                                    Board[row][tile_idx] = tiles[5];
                                }
                                else if (!current.markings[2])
                                {
                                    Board[row][tile_idx] = tiles[6];
                                }
                                else if (!current.markings[1])
                                {
                                    Board[row][tile_idx] = tiles[7];
                                }
                                else
                                {
                                    Board[row][tile_idx] = tiles[8];
                                }
                            }
                            else if (current.markings[1])
                            {
                                if (!(current.markings[2] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = tiles[9];
                                }
                                else if (!(current.markings[3]))
                                {
                                    Board[row][tile_idx] = tiles[10];
                                }
                                else if (!(current.markings[2]))
                                {
                                    Board[row][tile_idx] = tiles[11];
                                }
                                else
                                {
                                    Board[row][tile_idx] = tiles[12];
                                }
                            }
                            else if (current.markings[2])
                            {
                                if (!current.markings[3])
                                {
                                    Board[row][tile_idx] = tiles[13];
                                }
                                else
                                {
                                    Board[row][tile_idx] = tiles[14];
                                }
                            }
                            else if (current.markings[3])
                            {
                                Board[row][tile_idx] = tiles[15];
                            }
                        }
                        else //flipped
                        {
                            switch (current.content)
                            {
                                case 0:
                                case 1:
                                    Board[row][tile_idx] = tiles[16];
                                    break;
                                case 2:
                                    Board[row][tile_idx] = tiles[17];
                                    break;
                                case 3:
                                    Board[row][tile_idx] = tiles[18];
                                    break;
                                case 4:
                                    Board[row][tile_idx] = tiles[19];
                                    break;
                            }
                        }
                    }
                    else //selected
                    {
                        if (!current.flipped) //notflipped
                        {
                            if (!(current.markings[0] || current.markings[1] || current.markings[2] || current.markings[3]))
                            {
                                Board[row][tile_idx] = selected[0];
                            }
                            else if (current.markings[0])
                            {
                                if (!(current.markings[1] || current.markings[2] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = selected[1];
                                }
                                else if (!(current.markings[2] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = selected[2];
                                }
                                else if (!(current.markings[1] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = selected[3];
                                }
                                else if (!(current.markings[1] || current.markings[2]))
                                {
                                    Board[row][tile_idx] = selected[4];
                                }
                                else if (!current.markings[3])
                                {
                                    Board[row][tile_idx] = selected[5];
                                }
                                else if (!current.markings[2])
                                {
                                    Board[row][tile_idx] = selected[6];
                                }
                                else if (!current.markings[1])
                                {
                                    Board[row][tile_idx] = selected[7];
                                }
                                else
                                {
                                    Board[row][tile_idx] = selected[8];
                                }
                            }
                            else if (current.markings[1])
                            {
                                if (!(current.markings[2] || current.markings[3]))
                                {
                                    Board[row][tile_idx] = selected[9];
                                }
                                else if (!(current.markings[3]))
                                {
                                    Board[row][tile_idx] = selected[10];
                                }
                                else if (!(current.markings[2]))
                                {
                                    Board[row][tile_idx] = selected[11];
                                }
                                else
                                {
                                    Board[row][tile_idx] = selected[12];
                                }
                            }
                            else if (current.markings[2])
                            {
                                if (!current.markings[3])
                                {
                                    Board[row][tile_idx] = selected[13];
                                }
                                else
                                {
                                    Board[row][tile_idx] = selected[14];
                                }
                            }
                            else if (current.markings[3])
                            {
                                Board[row][tile_idx] = selected[15];
                            }
                        }
                        else //flipped
                        {
                            switch (current.content)
                            {
                                case 0:
                                case 1:
                                    Board[row][tile_idx] = selected[16];
                                    break;
                                case 2:
                                    Board[row][tile_idx] = selected[17];
                                    break;
                                case 3:
                                    Board[row][tile_idx] = selected[18];
                                    break;
                                case 4:
                                    Board[row][tile_idx] = selected[19];
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    public class Coins
    {
        public static int TotalCoins = 0;
        public static int CurrentCoins = 0;
    }

    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
            this.Show();
            Tile.SetGrid(Tile.rand.Next(6,9), Tile.rand.Next(2,6), Tile.rand.Next(0,4));
            Tile.SetNumbers();
            Visuals.MakeImages();
            UpdateBoard();
            UpdateNumbers();
            UpdateCoinCounters();
        }

        void UpdateBoard()
        {
            Visuals.UpdateBoard();

            B_A0.Background = Visuals.Board[0][0];
            B_B0.Background = Visuals.Board[0][1];
            B_C0.Background = Visuals.Board[0][2];
            B_D0.Background = Visuals.Board[0][3];
            B_E0.Background = Visuals.Board[0][4];

            B_A1.Background = Visuals.Board[1][0];
            B_B1.Background = Visuals.Board[1][1];
            B_C1.Background = Visuals.Board[1][2];
            B_D1.Background = Visuals.Board[1][3];
            B_E1.Background = Visuals.Board[1][4];

            B_A2.Background = Visuals.Board[2][0];
            B_B2.Background = Visuals.Board[2][1];
            B_C2.Background = Visuals.Board[2][2];
            B_D2.Background = Visuals.Board[2][3];
            B_E2.Background = Visuals.Board[2][4];

            B_A3.Background = Visuals.Board[3][0];
            B_B3.Background = Visuals.Board[3][1];
            B_C3.Background = Visuals.Board[3][2];
            B_D3.Background = Visuals.Board[3][3];
            B_E3.Background = Visuals.Board[3][4];

            B_A4.Background = Visuals.Board[4][0];
            B_B4.Background = Visuals.Board[4][1];
            B_C4.Background = Visuals.Board[4][2];
            B_D4.Background = Visuals.Board[4][3];
            B_E4.Background = Visuals.Board[4][4];
        }

        void UpdateCoinCounters()
        {
            int current_coins = Coins.CurrentCoins;
            int total_coins = Coins.TotalCoins;

            string s_current_coins = current_coins.ToString();
            string sf_current_coins = "";
            for (int i = 0; i < 5 - s_current_coins.Length; i++)
            {
                sf_current_coins += "0";
            }
            sf_current_coins += s_current_coins;

            string s_total_coins = total_coins.ToString();
            string sf_total_coins = "";
            for (int i = 0; i < 5 - s_total_coins.Length; i++)
            {
                sf_total_coins += "0";
            }
            sf_total_coins += s_total_coins;

            Text.SaveGame(total_coins);
            L_CoinCurrent.Content = "CURRENT COINS:   " + sf_current_coins;
            L_CoinTotal.Content = "TOTAL COINS:     " + sf_total_coins;
        }

        void UpdateNumbers()
        {
            L_N00.Content = Tile.total_numbers[0][0];
            L_N01.Content = Tile.total_numbers[0][1];
            L_N02.Content = Tile.total_numbers[0][2];
            L_N03.Content = Tile.total_numbers[0][3];
            L_N04.Content = Tile.total_numbers[0][4];
            L_N10.Content = Tile.total_numbers[1][0];
            L_N11.Content = Tile.total_numbers[1][1];
            L_N12.Content = Tile.total_numbers[1][2];
            L_N13.Content = Tile.total_numbers[1][3];
            L_N14.Content = Tile.total_numbers[1][4];

            L_V00.Content = Tile.voltorb_numbers[0][0];
            L_V01.Content = Tile.voltorb_numbers[0][1];
            L_V02.Content = Tile.voltorb_numbers[0][2];
            L_V03.Content = Tile.voltorb_numbers[0][3];
            L_V04.Content = Tile.voltorb_numbers[0][4];
            L_V10.Content = Tile.voltorb_numbers[1][0];
            L_V11.Content = Tile.voltorb_numbers[1][1];
            L_V12.Content = Tile.voltorb_numbers[1][2];
            L_V13.Content = Tile.voltorb_numbers[1][3];
            L_V14.Content = Tile.voltorb_numbers[1][4];
        }

        void VoltorbExplode()
        {
            B_0Marking.IsEnabled = false;
            B_1Marking.IsEnabled = false;
            B_2Marking.IsEnabled = false;
            B_3Marking.IsEnabled = false;
            B_Flip.IsEnabled = false;
            B_CashCoins.IsEnabled = false;

            foreach(var row in Tile.tiles)
            {
                foreach(var tile in row)
                {
                    tile.flipped = true;
                }
            }
            UpdateBoard();
        }

        //==========================================================================//

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Null Button Method");
        } //default

        private void B_Flip_Click(object sender, RoutedEventArgs e)
        {
            foreach (var row in Tile.tiles)
            {
                foreach (var tile in row)
                {
                    if (tile.selected && !tile.flipped)
                    {
                        tile.flipped = true;
                        switch(tile.content)
                        {
                            case 0:
                            case 1:
                                if (Coins.CurrentCoins < 1)
                                {
                                    Coins.CurrentCoins = 1;
                                }
                                UpdateBoard();
                                UpdateCoinCounters();
                                break;
                            case 2:
                                if (Coins.CurrentCoins < 1)
                                {
                                    Coins.CurrentCoins = 2;
                                }
                                else
                                {
                                    Coins.CurrentCoins *= 2;
                                }
                                UpdateBoard();
                                UpdateCoinCounters();
                                break;
                            case 3:
                                if (Coins.CurrentCoins < 1)
                                {
                                    Coins.CurrentCoins = 3;
                                }
                                else
                                {
                                    Coins.CurrentCoins *= 3;
                                }
                                UpdateBoard();
                                UpdateCoinCounters();
                                break;
                            case 4:
                                Coins.CurrentCoins = 0;
                                UpdateBoard();
                                UpdateCoinCounters();
                                VoltorbExplode();
                                break;
                        }
                        if (Tile.WinCheck())
                        {
                            if (Coins.CurrentCoins + Coins.TotalCoins <= 99999)
                            {
                                Coins.TotalCoins += Coins.CurrentCoins;
                            }
                            else
                            {
                                Coins.TotalCoins = 99999;
                            }
                            Coins.CurrentCoins = 0;
                            UpdateCoinCounters();
                            VoltorbExplode();
                        }
                        return;
                    }
                }
            }
        }
        private void B_CashCoins_Click(object sender, RoutedEventArgs e)
        {
            if (Coins.CurrentCoins + Coins.TotalCoins <= 99999)
            {
                Coins.TotalCoins += Coins.CurrentCoins;
            }
            else
            {
                Coins.TotalCoins = 99999;
            }
            Coins.CurrentCoins = 0;
            UpdateCoinCounters();
            VoltorbExplode();
        }
        
        private void B_0Marking_Click(object sender, RoutedEventArgs e)
        {
            Tile.AddMarkings(0);
            UpdateBoard();
        }
        private void B_1Marking_Click(object sender, RoutedEventArgs e)
        {
            Tile.AddMarkings(1);
            UpdateBoard();
        }
        private void B_2Marking_Click(object sender, RoutedEventArgs e)
        {
            Tile.AddMarkings(2);
            UpdateBoard();
        }
        private void B_3Marking_Click(object sender, RoutedEventArgs e)
        {
            Tile.AddMarkings(3);
            UpdateBoard();
        }

        private void B_NewGame_Click(object sender, RoutedEventArgs e)
        {
            Tile.SetGrid(Tile.rand.Next(6, 9), Tile.rand.Next(2, 6), Tile.rand.Next(0, 4));
            Tile.SetNumbers();
            UpdateBoard();
            UpdateNumbers();
            B_0Marking.IsEnabled = true;
            B_1Marking.IsEnabled = true;
            B_2Marking.IsEnabled = true;
            B_3Marking.IsEnabled = true;
            B_Flip.IsEnabled = true;
            B_CashCoins.IsEnabled = true;
        }
        //=========================================================================//

        private void B_A0_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[0][0].selected = true;
            UpdateBoard();
        }
        private void B_B0_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[0][1].selected = true;
            UpdateBoard();
        }
        private void B_C0_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[0][2].selected = true;
            UpdateBoard();
        }
        private void B_D0_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[0][3].selected = true;
            UpdateBoard();
        }
        private void B_E0_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[0][4].selected = true;
            UpdateBoard();
        }
        private void B_A1_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[1][0].selected = true;
            UpdateBoard();
        }
        private void B_B1_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[1][1].selected = true;
            UpdateBoard();
        }
        private void B_C1_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[1][2].selected = true;
            UpdateBoard();
        }
        private void B_D1_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[1][3].selected = true;
            UpdateBoard();
        }
        private void B_E1_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[1][4].selected = true;
            UpdateBoard();
        }
        private void B_A2_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[2][0].selected = true;
            UpdateBoard();
        }
        private void B_B2_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[2][1].selected = true;
            UpdateBoard();
        }
        private void B_C2_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[2][2].selected = true;
            UpdateBoard();
        }
        private void B_D2_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[2][3].selected = true;
            UpdateBoard();
        }
        private void B_E2_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[2][4].selected = true;
            UpdateBoard();
        }
        private void B_A3_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[3][0].selected = true;
            UpdateBoard();
        }
        private void B_B3_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[3][1].selected = true;
            UpdateBoard();
        }
        private void B_C3_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[3][2].selected = true;
            UpdateBoard();
        }
        private void B_D3_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[3][3].selected = true;
            UpdateBoard();
        }
        private void B_E3_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[3][4].selected = true;
            UpdateBoard();
        }
        private void B_A4_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[4][0].selected = true;
            UpdateBoard();
        }
        private void B_B4_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[4][1].selected = true;
            UpdateBoard();
        }
        private void B_C4_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[4][2].selected = true;
            UpdateBoard();
        }
        private void B_D4_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[4][3].selected = true;
            UpdateBoard();
        }
        private void B_E4_Click(object sender, RoutedEventArgs e)
        {
            Tile.DeselectAll();
            Tile.tiles[4][4].selected = true;
            UpdateBoard();
        }
        
    }
}
