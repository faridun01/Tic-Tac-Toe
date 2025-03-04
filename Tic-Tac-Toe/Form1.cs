namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        // 3x3 button grid for Tic-Tac-Toe game
        private Button[,] buttons = new Button[3, 3];

        //
        //Track the current player's turn ('X' or 'O')
        private char currentPlayer = 'X';

        // Constructor: Initializes the form and game board
        public Form1()
        {
            InitializeComponent();
            InitializeBoard();
        }

        /// <summary>
        /// Sets up the game board, including buttons for the Tic-Tac-Toe grid and a reset button.
        /// </summary>
        private void InitializeBoard()
        {
            this.Text = "Tic-Tac-Toe"; // Window title
            this.BackColor = Color.FromArgb(44, 62, 80); // Dark background for better contrast
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            int buttonSize = 100; // Size of each Tic-Tac-Toe cell

            // Create a 3x3 button grid for the game
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    buttons[row, col] = new Button
                    {
                        Width = buttonSize,
                        Height = buttonSize,
                        Location = new Point(col * buttonSize + 10, row * buttonSize + 10), // Positioning
                        Font = new Font("Comic Sans MS", 28, FontStyle.Bold), // Font styling
                        BackColor = Color.WhiteSmoke, // Default button color
                        FlatStyle = FlatStyle.Flat, // Flat style for modern look
                        ForeColor = Color.Black, // Text color
                        Tag = new Tuple<int, int>(row, col) // Store position in tag
                    };

                    // Style the button borders
                    buttons[row, col].FlatAppearance.BorderSize = 2;
                    buttons[row, col].FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219); // Blue borders

                    // Event handlers for button actions
                    buttons[row, col].Click += Button_Click!;
                    buttons[row, col].MouseEnter += Button_MouseEnter!;
                    buttons[row, col].MouseLeave += Button_MouseLeave!;

                    // Add button to the form
                    Controls.Add(buttons[row, col]);
                }
            }

            // Create the Reset button
            Button resetButton = new Button
            {
                Text = "Restart Game", // Button label
                Width = 300,
                Height = 50,
                Location = new Point(10, 320), // Position below grid
                Font = new Font("Comic Sans MS", 14, FontStyle.Bold), // Font styling
                BackColor = Color.FromArgb(231, 76, 60), // Red background
                ForeColor = Color.White, // White text for contrast
                FlatStyle = FlatStyle.Flat // Flat style for better UI
            };

            resetButton.FlatAppearance.BorderSize = 0;
            resetButton.Click += ResetGame!; // Attach reset button event
            Controls.Add(resetButton);
        }

        /// <summary>
        /// Handles the click event on a Tic-Tac-Toe cell.
        /// Updates the button text with the current player's symbol.
        /// Checks for a win condition.
        /// </summary>
        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Text == "")
                {
                    // Set button text to current player's symbol
                    btn.Text = currentPlayer.ToString();

                    // Set text color based on player
                    btn.ForeColor = (currentPlayer == 'X') ? Color.FromArgb(41, 128, 185) : Color.FromArgb(192, 57, 43);

                    // Check for win condition
                    if (CheckWin())
                    {
                        MessageBox.Show($"🎉 Player {currentPlayer} Wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetBoard();
                        return;
                    }

                    // Switch to the next player
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        /// <summary>
        /// Checks if a player has won the game by matching three symbols in a row, column, or diagonal.
        /// </summary>
        /// <returns>True if a player wins, otherwise false.</returns>
        private bool CheckWin()
        {
            // Check rows and columns for a win
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text)
                    return true;
                if (buttons[0, i].Text != "" && buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text)
                    return true;
            }

            // Check diagonals for a win
            if (buttons[0, 0].Text != "" && buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text)
                return true;
            if (buttons[0, 2].Text != "" && buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text)
                return true;

            return false;
        }

        /// <summary>
        /// Resets the game board when the reset button is clicked.
        /// </summary>
        private void ResetGame(object? sender, EventArgs e)
        {
            ResetBoard();
        }

        /// <summary>
        /// Clears the board and resets the player to 'X'.
        /// </summary>
        private void ResetBoard()
        {
            foreach (var btn in buttons)
            {
                btn.Text = ""; // Clear text
                btn.BackColor = Color.WhiteSmoke; // Reset color
            }
            currentPlayer = 'X'; // Set default player
        }

        /// <summary>
        /// Changes the button color when hovered over, for a better user experience.
        /// </summary>
        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Text == "")
                {
                    btn.BackColor = Color.FromArgb(236, 240, 241); // Light gray hover effect
                }
            }
        }

        /// <summary>
        /// Restores the button color when the mouse leaves.
        /// </summary>
        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Text == "")
                {
                    btn.BackColor = Color.WhiteSmoke; // Restore original color
                }
            }
        }
    }
}
