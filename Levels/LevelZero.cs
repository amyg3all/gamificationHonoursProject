using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// Level zero inherited state handles all logic for loading and updating content for level zero
/// </summary>
public class LevelZero : Level
{
    private double _elapsedTime = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _textBox;
    private KeyboardState _previousKeyboardState;

    public LevelZero(Game1 game)
        : base(game)
    {
    }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/rootSystem");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
    }

    private void UpdateCurrentScreenSequence(int newScreenSequence)
    {
        if (newScreenSequence == _currentScreenSequence + 1)
        {
            _currentScreenSequence = newScreenSequence;
        }
    }
    
    public override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime > 2) UpdateCurrentScreenSequence(1);
        if(_elapsedTime > 4) UpdateCurrentScreenSequence(2);

        var currentKeyboardState = Keyboard.GetState();
        
        if (
            currentKeyboardState.IsKeyDown(Keys.Enter)
            && _previousKeyboardState.IsKeyUp(Keys.Enter)
            && _currentScreenSequence == 2
        )
        {
            UpdateCurrentScreenSequence(3);
        }
        
        if (
            currentKeyboardState.IsKeyDown(Keys.Enter)
            && _previousKeyboardState.IsKeyUp(Keys.Enter)
            && _currentScreenSequence == 3
        )
        {
            game.toggleHealthKnowledge(false);
            game.ChangeLevel();
        }



    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;

        game.GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin();
        switch (_currentScreenSequence)
        {
            case 0:
                WriteFontCentre("HELLO PLAYER", 0, 0, Color.White);
                break;
            case 1: WriteFontCentre("WELCOME TO OXYGEN ODYSSEY", 0, 0, Color.White);
                break;
            case 2:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input =
                    "Hello, adventurer! I have to task you with a very special mission"
                    + " you see that tree over there, business has been "
                    + "a bit slow and I'm worried all of our stocks are running low "
                    + " could you possibly go and make sure everything is working as normal? "
                    + "Make sure you are speedy though, I'm going to have to time you as I need to" 
                    + " make sure everything's in before the "
                    + "boss gets back";

                WriteTextBox(spriteBatch, input, _textBox);

                break;
            case 3:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input2 =
                    "are you up to the challenge? Oh I forgot to say, there is the service entrance down there by the roots"
                    + " might be best to start there and get all the reactions going, we need to complete" 
                    + " the photosynthesis before it gets dark, I'll make sure some of my buddies look out for you!";

                WriteTextBox(spriteBatch, input2, _textBox);

                break;
            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}
