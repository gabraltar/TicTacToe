///////////////////////////////////////////////////////
// TINFO 200 B, Winter 2024
// UWTacoma SET, Charlton C. Chan, Yoonis A. Barre, Justin Cho
// 2024-02-05 - Cs4 - C# programming project - Tic Tac Toe
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2024-02-05 - cchan95 - Creation of file to represent the Tic Tac Toe game, began designing the GUI.
// 2024-02-05 - cchan95 - Creation of labels which keep track of scores, ? buttons to represent game play area, and restart button.
// 2024-02-05 - cchan95 - Used table layout and docking features to make UI elements within the GUI to resize properly as needed.
// 2024-02-06 - cchan95 - Added functionality to ? buttons by making them clickable by the player, added functionality to restart button.
// 2024-02-07 - cchan95 - Redone the code for restartGame function as it did not properly reset the squares in the game back to their original state.
// 2024-02-07 - cchan95 - Restart button now functions properly.
// 2024-02-08 - cchan95 - Implemented a in-GUI textbox which announces who has won the game.
// 2024-02-08 - ybarre - Added functionality to the Tie Games counter.
// 2024-02-08 - cchan95 - Implemented functionality to Player and CPU win counters.
// 2024-02-08 - cchan95 - Added and implemented functionality to Player and CPU loss counters.

//Assignment description
// This program emulates a Tic Tac Toe game which is played by the user against a computer opponent.
// to start the game, the infobox prompts the user to click a square
// 
//
//

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public enum Player
        {
            X, O
        }
        // initializes the player, pseudo-random number generator is declared,
        // counters for the number of player wins, cpu wins, tie games, player losses, cpu losses are declared.
        // the list of buttons in the gameplay area is declared.
        Player currentPlayer;
        Random random = new Random();
        int playerWins = 0;
        int CPUWins = 0;
        int tieGame = 0;
        int playerLoses = 0;
        int CPULoses = 0;
        string name = "Player";
        List<Button> buttons;

        public Form1()
        {
            InitializeComponent();
            //PromptPlayerName();
            restartGame();
        }

        private void CPUmove(object sender, EventArgs e)
        {
            // this allows the computer player to click on a button to place an O on it
            // whichever button it selects will turn its color to red and the ? text is replaced by an O
            // afterwards the button is disabled which prevents the player from clicking it
            // the button is also removed from the list of buttons so the CPU doesn't end up picking the same button it just picked
            // the checkGame function runs to determine if the player or CPU has won yet
            // if there is no winner yet, the player gets to move next
            if (buttons.Count > 0)
            {
                infoBox.Text = "CPU has placed a O! Player's turn!";
                int index = random.Next(buttons.Count);
                buttons[index].Enabled = false;
                currentPlayer = Player.O;
                buttons[index].Text = currentPlayer.ToString();
                buttons[index].BackColor = Color.Red;
                buttons.RemoveAt(index);
                checkGame();
                CPUmoves.Stop();
            }
        }

        private void playerClickBtn(object sender, EventArgs e)
        {
            // this allows the player to click on a button to place an X on it
            // the button that is clicked on changes its color to blue and the ? text is replaced by an X
            // afterwards the button is disabled which prevents the player from clicking it again
            // the button is also removed from the list of buttons so the CPU doesn't pick the same button the player has clicked
            // the checkGame function runs to determine if the player or CPU has won yet
            // if there is no winner yet, the CPU gets to move next
            infoBox.Text = name + " has placed a X! CPU's turn!";
            var button = (Button)sender;
            currentPlayer = Player.X;
            button.Text = currentPlayer.ToString();
            button.Enabled = false;
            button.BackColor = Color.Blue;
            buttons.Remove(button);
            checkGame();
            CPUmoves.Start();

        }

        private void restartGame(object sender, EventArgs e)
        {
            // this allows the Restart button to be able to carry out its function in restartGame();
            restartGame();
        }

        private void checkGame()
        {
            // this is an if statement which checks if the player has won the game
            // it does this by checking rows and columns for three X's in a row, column, or diagonal
            // if any of these conditions are met, the CPU stops moving, the infobox announces that the player has won,
            // increases the player wins counter by 1, increases the CPU lose counter by 1, and resets the game.
            if (btn1.Text == "X" && btn2.Text == "X" && btn3.Text == "X"
               || btn4.Text == "X" && btn5.Text == "X" && btn6.Text == "X"
               || btn7.Text == "X" && btn9.Text == "X" && btn8.Text == "X"
               || btn1.Text == "X" && btn4.Text == "X" && btn7.Text == "X"
               || btn2.Text == "X" && btn5.Text == "X" && btn8.Text == "X"
               || btn3.Text == "X" && btn6.Text == "X" && btn9.Text == "X"
               || btn1.Text == "X" && btn5.Text == "X" && btn9.Text == "X"
               || btn3.Text == "X" && btn5.Text == "X" && btn7.Text == "X")
            {
                CPUmoves.Stop();
                infoBox.Text = name + " has won! Starting new game...";
                playerWins++;
                playerWinLabel.Text = name + " Wins: " + playerWins;
                CPULoses++;
                CPULoseLabel.Text = "CPU Losses: " + CPULoses;
                restartGame();
            }
            // this is an if statement which checks if the computer player has won the game
            // it does this by checking rows and columns for three O's in a row, column, or diagonal
            // if any of these conditions are met, the CPU stops moving, the infobox announces that the computer player has won,
            // increases the CPU wins counter by 1, increases the player lose counter by 1, and prompts the user to click a square to start again.
            else if (btn1.Text == "O" && btn2.Text == "O" && btn3.Text == "O"
            || btn4.Text == "O" && btn5.Text == "O" && btn6.Text == "O"
            || btn7.Text == "O" && btn9.Text == "O" && btn8.Text == "O"
            || btn1.Text == "O" && btn4.Text == "O" && btn7.Text == "O"
            || btn2.Text == "O" && btn5.Text == "O" && btn8.Text == "O"
            || btn3.Text == "O" && btn6.Text == "O" && btn9.Text == "O"
            || btn1.Text == "O" && btn5.Text == "O" && btn9.Text == "O"
            || btn3.Text == "O" && btn5.Text == "O" && btn7.Text == "O")
            {
                CPUmoves.Stop();
                infoBox.Text = "CPU has won! Click a square to play again!";
                CPUWins++;
                CPUWinLabel.Text = "CPU Wins: " + CPUWins;
                playerLoses++;
                playerLoseLabel.Text = name + " Losses: " + playerLoses;
                restartGame();
            }

            else if (buttons.Count == 0)
            {
                // if all buttons are pressed and don't meet any win conditions in checkGame,
                // the info box will announce a tie game and the Tie Games counter will increase by 1.
                CPUmoves.Stop();
                infoBox.Text = "It's a tie! Click a square to play again!";
                tieGame++;
                tieGameLabel.Text = "Tie Games: " + tieGame;
                restartGame();
            }

        }

        private void restartGame()
        {
            // when the restart button is pressed, it resets all buttons back to their original, unpressed state.
            // it does this by checking every button to see if it has been pressed.
            // any pressed buttons would revert to their original color and ? icon.
            buttons = new List<Button> { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = "?";
                x.BackColor = DefaultBackColor;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }



       

        private void nameButton_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            playerWinLabel.Text = name + " Wins: ";
            playerLoseLabel.Text = name + " Losses: ";
        }

    }
    }
//}
