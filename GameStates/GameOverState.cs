using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates;

/// <summary>
/// Game Over Screen inherited state handles all logic for loading and updating the Title Screen
/// </summary>
public class GameOverState : State
{
    private KeyboardState _previousKeyboardState;

    public GameOverState(Game1 game)
        : base(game)
    {
        game.StopBackgroundMusic();
        game.stopFightMusic();
        game.toggleHealthKnowledge(false);
        game.toggleBattery(false);
        game.setBattery(0);
        if (game.getCurrentHealth() != 1) game.decreaseHealth();

        game.toggleHealthKnowledge(false);
    }

    public override void LoadContent()
    {
        BakeFont();
    }

    public override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        // Check if Enter is pressed and not held down
        if (
                currentKeyboardState.IsKeyDown(Keys.Space)
                && !_previousKeyboardState.IsKeyDown(Keys.Enter)
            )
            // update game back to the Title Screen
            game.ChangeState(new TitleScreenState(game));

        _previousKeyboardState = currentKeyboardState;
    }

    public override void Draw(GameTime gameTime)
    {
        // background colour
        game.GraphicsDevice.Clear(Color.Black);

        game.SpriteBatch.Begin();
        
        if (!game.getRanOutOfTime())
        {
            // Draw the text at the calculated position
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