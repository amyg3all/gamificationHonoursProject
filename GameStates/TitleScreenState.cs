using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;

namespace gamificationHonoursProject.GameStates
{

    /// <summary>
    /// Title Screen inherited state handles all logic for loading and updating the Title Screen
    /// </summary>
    public class TitleScreenState : State
    {
        private Texture2D _titleScreenTexture;
        private KeyboardState _previousKeyboardState;

        public TitleScreenState(Game1 game)
            : base(game)
        {
            game.PlayBackgroundMusic();
            game.toggleHealthKnowledge(false);
        }

        public override void LoadContent()
        {
            // load the image for the Title Screen
            _titleScreenTexture = game.Content.Load<Texture2D>("Backgrounds/TitleScreen");

            BakeFont();
        }

        public override void Update(GameTime gameTime)
        {
            var currentKeyboardState = Keyboard.GetState();

            // Check if Enter is pressed and not held down
            if (
                currentKeyboardState.IsKeyDown(Keys.Enter)
                && !_previousKeyboardState.IsKeyDown(Keys.Enter)
            )
            {
                // change to game play
                game.ChangeState(new GamePlayState(game, game.GetCurrentLevel()));
            }

            _previousKeyboardState = currentKeyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            // background colour = black
            game.GraphicsDevice.Clear(Color.Black);

            // calculate the position of the image
            var imageWidth = (int)(game.GraphicsDevice.Viewport.Width * 0.7);
            var imageHeight = (int)(game.GraphicsDevice.Viewport.Height * 0.7);
            var imagePositionX = (game.GraphicsDevice.Viewport.Width - imageWidth) / 2 + 150;
            var imagePositionY = (game.GraphicsDevice.Viewport.Height - imageHeight) / 2;

            // Draw the title screen texture
            game.SpriteBatch.Begin();
            // image
            game.SpriteBatch.Draw(
                _titleScreenTexture,
                new Rectangle(imagePositionX, imagePositionY, imageWidth, imageHeight),
                Color.White
            );
            // Text
            WriteFont("OXYGEN ODYSSEY", 60, 200, Color.White);
            WriteFont(">PRESS ENTER<", 60, 250, Color.White);

            game.SpriteBatch.End();
        }
    }
}
