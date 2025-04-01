using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates;

/// <summary>
/// Game Over Screen inherited state handles all logic for loading and updating the Game Over state
/// </summary>
public class GameOverState : State
{
    private KeyboardState _previousKeyboardState;

    public GameOverState(Game1 game)
        : base(game)
    {
        // ensure that all music, health, knowledge, battery bars are all toggled off
        game.StopBackgroundMusic();
        game.StopFightMusic();
        game.ToggleHealthKnowledge(false);
        game.ToggleBattery(false);

        // resets the battery so that the level is restarted properly
        game.SetBattery(0);

        // loose a life when you die
        if (game.GetCurrentHealth() != 1)
            game.DecreaseHealth();
    }

    public override void LoadContent()
    {
        BakeFont();
    }

    public override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        // sets game back to the title screen upon pressing enter
        if (
            currentKeyboardState.IsKeyDown(Keys.Space)
            && !_previousKeyboardState.IsKeyDown(Keys.Enter)
        )
            game.ChangeState(new TitleScreenState(game));

        _previousKeyboardState = currentKeyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);

        game.SpriteBatch.Begin();

        // a different text appears if you run out of time vs loosing a quiz
        if (!game.GetRanOutOfTime())
        {
            WriteFontCentre("GAME OVER", 0, -20, Color.White);
            WriteFontCentre(">PRESS THE SPACEBAR TO TRY AGAIN<", 0, 20, Color.White);
        }
        else
        {
            WriteFontCentre("Uh Oh! YOU RAN OUT OF TIME", 0, -30, Color.White);
            WriteFontCentre("GAME OVER", 0, 0, Color.White);
            WriteFontCentre(">PRESS THE SPACEBAR TO TRY AGAIN<", 0, 30, Color.White);
        }

        game.SpriteBatch.End();
    }
}
