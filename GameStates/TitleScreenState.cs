using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates;

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
        game.ToggleHealthKnowledge(false);
        game.UnsetRanOutOfTime();
    }

    public override void LoadContent()
    {
        _titleScreenTexture = game.Content.Load<Texture2D>("Backgrounds/TitleScreen");

        BakeFont();
    }

    public override void Update(GameTime gameTime)
    {
        // switches to the current level start, default is one but if you die goes to current level
        var currentKeyboardState = Keyboard.GetState();

        if (
            currentKeyboardState.IsKeyDown(Keys.Enter)
            && !_previousKeyboardState.IsKeyDown(Keys.Enter)
        )
            game.ChangeState(new GamePlayState(game, game.GetCurrentLevel()));

        _previousKeyboardState = currentKeyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        // positions the title screen background and the title
        game.GraphicsDevice.Clear(Color.Black);

        var imageWidth = (int)(game.GraphicsDevice.Viewport.Width * 0.7);
        var imageHeight = (int)(game.GraphicsDevice.Viewport.Height * 0.7);
        var imagePositionX = (game.GraphicsDevice.Viewport.Width - imageWidth) / 2 + 150;
        var imagePositionY = (game.GraphicsDevice.Viewport.Height - imageHeight) / 2;

        game.SpriteBatch.Begin();

        game.SpriteBatch.Draw(
            _titleScreenTexture,
            new Rectangle(imagePositionX, imagePositionY, imageWidth, imageHeight),
            Color.White
        );

        WriteFont("OXYGEN ODYSSEY", 60, 200, Color.White);
        WriteFont(">PRESS ENTER<", 60, 250, Color.White);

        game.SpriteBatch.End();
    }
}
