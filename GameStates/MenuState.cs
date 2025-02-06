using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates
{

    /// <summary>
    /// Menu Screen inherited state handles all logic for loading and updating the Title Screen
    /// </summary>
    public class MenuState : State
    {
        private SpriteFont _menuFont; // Font for displaying menu options
        private string[] _menuOptions = { "Start Game", "Options", "Exit" }; // Menu options
        private int _selectedIndex = 0; // Index of the currently selected menu option

        public MenuState(Game1 game) : base(game) { }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // Check for user input to navigate the menu
            var state = Keyboard.GetState();

            // Move up the menu
            if (state.IsKeyDown(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0) _selectedIndex = _menuOptions.Length - 1; // Wrap to the last option
            }
            // Move down the menu
            else if (state.IsKeyDown(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex >= _menuOptions.Length) _selectedIndex = 0; // Wrap to the first option
            }

            // Select the currently highlighted option
            if (state.IsKeyDown(Keys.Enter))
            {
                switch (_selectedIndex)
                {
                    case 0: // Start Game
                        game.ChangeState(new GamePlayState(game, game.GetCurrentLevel())); // Transition to GameplayState
                        break;
                    case 1: // Options
                        // Add functionality for options here
                        break;
                    case 2: // Exit
                        game.Exit(); // Exit the game
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Clear the screen (optional, can be set to a background color)
            game.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Begin drawing
            var spriteBatch = new SpriteBatch(game.GraphicsDevice);
            spriteBatch.Begin();

            // Draw each menu option
            for (var i = 0; i < _menuOptions.Length; i++)
            {
                var color = (i == _selectedIndex) ? Color.Yellow : Color.White; // Highlight selected option
                //spriteBatch.DrawString(menuFont, menuOptions[i], new Vector2(100, 100 + i * 30), color); // Adjust position as needed
            }

            spriteBatch.End();
        }
    

    }
}