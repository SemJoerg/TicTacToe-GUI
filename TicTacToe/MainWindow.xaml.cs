using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridGames.Game<string> game;
        private Dictionary<Button, uint> buttonToFieldIndex = new Dictionary<Button, uint>();

        public MainWindow()
        {   
            GridGames.Player<string> defaultPlayer = new GridGames.Player<string>(0, "");
            GridGames.Player<string>[] players = new GridGames.Player<string>[2];
            players[0] = new GridGames.Player<string>("Player 1", 1, "X");
            players[1] = new GridGames.Player<string>("Player 2", 2, "O");
            game = new GridGames.Game<string>(3, 3, 3, players, defaultPlayer, DisplayGrid, GridIsFullHandler);
            game.DetectedWinner += Game_DetectedWinner;
            InitializeComponent();
            game.DoGridOutput();
            DisplayNextPlayer();

            buttonToFieldIndex.Add(cmd0, 0);
            buttonToFieldIndex.Add(cmd1, 1);
            buttonToFieldIndex.Add(cmd2, 2);
            buttonToFieldIndex.Add(cmd3, 3);
            buttonToFieldIndex.Add(cmd4, 4);
            buttonToFieldIndex.Add(cmd5, 5);
            buttonToFieldIndex.Add(cmd6, 6);
            buttonToFieldIndex.Add(cmd7, 7);
            buttonToFieldIndex.Add(cmd8, 8);
        }

        private void Game_DetectedWinner(GridGames.Game<string> sender, GridGames.Player<string> winner, uint[] winningFields)
        {
            MessageBox.Show($"{winner.Name} hat gewonnen");
            winner.Winns += 1;
            DisplayScore();
            game.ResetGame();
        }

        private void DisplayGrid(string[] markerGrid, uint gridWidth, uint gridHeight, string changedFieldMarker, uint? changedFieldIndex = null)
        {
            Button[] buttons = buttonToFieldIndex.Keys.ToArray<Button>();
            for (int i = 0; i < buttonToFieldIndex.Count; i++)
            {
                buttons[i].Content = markerGrid[i];
            }
        }

        private void DisplayNextPlayer()
        {
            lbl_spieler.Content = $"{game.NextPlayer.Name} [{game.NextPlayer.Marker}]";
        }

        private void DisplayScore()
        {
            lbl_spielstand.Content = $"{game.Players[1].Winns} : {game.Players[2].Winns}";
        }

        private void GridIsFullHandler(GridGames.Grid senderGrid)
        {
            MessageBox.Show("Unentschieden");
            game.ResetGame();
            DisplayNextPlayer();
        }

        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            game.SetNextField(buttonToFieldIndex[button]);
            DisplayNextPlayer();
        }

        private void cmd_reset_match_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
            DisplayNextPlayer();
        }

        private void cmd_new_game_Click(object sender, RoutedEventArgs e)
        {
            game.ResetGame();
            //i starts a 1 as 0 is the default player
            for(int i = 1; i < game.Players.Length; i++)
            {
                game.Players[i].Winns = 0;
            }
            DisplayScore();
        }
    }
}
