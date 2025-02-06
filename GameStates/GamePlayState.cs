using System.Net.NetworkInformation;
using gamificationHonoursProject.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates
{
    /// <summary>
    /// Game Play Screen inherited state handles all logic for loading and updating the Title Screen
    /// </summary>
    public class GamePlayState : State
    {
        private KeyboardState _previousKeyboardState;
        private Level currentLevel;

        public GamePlayState(Game1 game, int levelNumber)
            : base(game)
        {
            switch (levelNumber)
            {
                case 1:
                    currentLevel = new LevelOne(game);
                    break;
                case 2:
                    currentLevel = new LevelTwo(game);
                    break;
                default:
                    currentLevel = new LevelOne(game); // Default to LevelOne
                    break;
            }
        }

        public override void LoadContent()
        {
            currentLevel.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var currentKeyboardState = Keyboard.GetState();

            // Check if Enter is pressed and not held down
            if (
                currentKeyboardState.IsKeyDown(Keys.D)
                && !_previousKeyboardState.IsKeyDown(Keys.Enter)
            )
            {
                game.ChangeState(new GameOverState(game));
            }

            _previousKeyboardState = currentKeyboardState;
            
            currentLevel.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            currentLevel.Draw(game.SpriteBatch);
        }
    }
}
