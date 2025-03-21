using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// Level three inherited state handles all logic for loading and updating content for level three
/// </summary>
public class LevelThree : Level
{
    private double _elapsedTime = 0;
    private double _beatTheClock = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _screenTwo;
    private Texture2D _screenTwoPointFive;
    private Texture2D _electron;
    private Texture2D _calvinOne;
    private Texture2D _calvinTwo;
    private Texture2D _calvinThree;
    private Texture2D _calvinFour;
    private Texture2D _calvinFive;
    private Texture2D _textBox;
    private Texture2D _fightTextBox;
    private KeyboardState _previousKeyboardState;
    private Vector2 _electronPosition;
    private float _electronSpeed;
    private Vector2 _calvinPosition;
    
    public LevelThree(Game1 game)
        : base(game) { }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/leaf");
        _screenTwo = game.Content.Load<Texture2D>("Backgrounds/chloroplastInside");
        _screenTwoPointFive = game.Content.Load<Texture2D>("Backgrounds/levelThreeHelp");
        _electron = game.Content.Load<Texture2D>("Sprites/electron");
        _calvinOne = game.Content.Load<Texture2D>("Sprites/calvinOne");
        _calvinTwo = game.Content.Load<Texture2D>("Sprites/calvinTwo");
        _calvinThree = game.Content.Load<Texture2D>("Sprites/calvinThree");
        _calvinFour = game.Content.Load<Texture2D>("Sprites/calvinFour");
        _calvinFive = game.Content.Load<Texture2D>("Sprites/calvinFive");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
        _fightTextBox = game.Content.Load<Texture2D>("Other/fightTextBox");

        _electronPosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2,
            game._graphics.PreferredBackBufferHeight / 2
        );
        _electronSpeed = 250f;
        
        _calvinPosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2,
            game._graphics.PreferredBackBufferHeight / 2
        );
    }

    private void UpdateCurrentScreenSequence(int newScreenSequence)
    {
        if (newScreenSequence == _currentScreenSequence + 1)
            _currentScreenSequence = newScreenSequence;
    }

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        _beatTheClock += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime > 3)
            UpdateCurrentScreenSequence(1);

        var currentKeyboardState = Keyboard.GetState();

        // Check if Enter is pressed and not held down
        if (
            currentKeyboardState.IsKeyDown(Keys.Enter) && _previousKeyboardState.IsKeyUp(Keys.Enter)
        )
            switch (_currentScreenSequence)
            {
                case 1:
                    UpdateCurrentScreenSequence(2);
                    break;
                case 2:
                    UpdateCurrentScreenSequence(3);
                    break;
                case 3:
                    _beatTheClock = 0;
                    game.startCountDown8();
                    UpdateCurrentScreenSequence(4);
                    break;
                case 9:
                    UpdateCurrentScreenSequence(10);
                    break;
                
            }

        _previousKeyboardState = currentKeyboardState;

        var updatedElectronSpeed = _electronSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;

        if (_currentScreenSequence.Equals(4) || _currentScreenSequence.Equals(5) || _currentScreenSequence.Equals(6) || _currentScreenSequence.Equals(7) || _currentScreenSequence.Equals(8))
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))&& _electronPosition.Y > 15)
                _electronPosition.Y -= updatedElectronSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
                && _electronPosition.Y < screenHeight - 30
            )
                _electronPosition.Y += updatedElectronSpeed;

            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A)) && _electronPosition.X > 26)
                _electronPosition.X -= updatedElectronSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                && _electronPosition.X < screenWidth - 18
            )
                _electronPosition.X += updatedElectronSpeed;
        }

        if (_currentScreenSequence.Equals(4))
        {
            if (
                _electronPosition.X is > 733 and < 758
                && _electronPosition.Y is > 191 and < 212
            )
            {
                UpdateCurrentScreenSequence(5);
            }
        }
        
        if (_currentScreenSequence.Equals(5))
        {
            if ((_electronPosition.X is > 712 and < 745
                 && _electronPosition.Y is > 324 and < 349) || (_electronPosition.X is > 520 and < 558
                                                                && _electronPosition.Y is > 395 and < 424)
            )
            {
                UpdateCurrentScreenSequence(6);
            }
        }
        if (_currentScreenSequence.Equals(6))
        {
            if (
                _electronPosition.X is > 329 and < 362
                && _electronPosition.Y is > 412 and < 433
            )
            {
                UpdateCurrentScreenSequence(7);
            }
        }
        
        if (_currentScreenSequence.Equals(7))
        {
            if (
                (_electronPosition.X is > 24 and < 45
                 && _electronPosition.Y is > 254 and < 279) || (_electronPosition.X is > 275 and < 300
                                                                && _electronPosition.Y is > 133 and < 162)
            )
            {
                UpdateCurrentScreenSequence(8);
            }
        }
        
        if (_currentScreenSequence.Equals(8))
        {
            if (
                (_electronPosition.X is > 291 and < 325
                 && _electronPosition.Y is > 87 and < 116) || (_electronPosition.X is > 558 and < 583
                                                                && _electronPosition.Y is > 62 and < 83)
            )
            {
                UpdateCurrentScreenSequence(9);
                game.endCountDown();
            }
        }

        // make sure health, knowledge, and battery are displayed for relevant scenes
        if (
            _currentScreenSequence.Equals(4)
            || _currentScreenSequence.Equals(5)
            || _currentScreenSequence.Equals(6)
            || _currentScreenSequence.Equals(7)
            || _currentScreenSequence.Equals(8)
        )
            game.toggleHealthKnowledge(true);
        else
            game.toggleHealthKnowledge(false);
        
        if (_beatTheClock > 8 && (_currentScreenSequence.Equals(4) || _currentScreenSequence.Equals(5) || _currentScreenSequence.Equals(6) || _currentScreenSequence.Equals(7) || _currentScreenSequence.Equals(8)))
        {
            game.ranOutOfTime();
            game.ChangeState(new GameOverState(game));
        }

        if (_currentScreenSequence.Equals(11))
        {
            game.ChangeLevel();
        }
        
        if (_currentScreenSequence.Equals(10))
        {
            game.startFightMusic();
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                UpdateCurrentScreenSequence(11);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.B) || currentKeyboardState.IsKeyDown(Keys.C))
                game.ChangeState(new GameOverState(game));
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
                WriteFontCentre("LEVEL THREE", 0, 0, Color.White);
                break;
            case 1:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    new Color(50, 50, 50)
                );
                WriteFontCentre("THE INDEPENDENT LIGHT REACTION", 0, -20, Color.White);
                WriteFontCentreSmaller(
                    "Also known as the Calvin Cycle, this is the last reaction of photosynthesis",
                    0,
                    20,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "it occurs within the inside of the chloroplast, and uses the products of the",
                    0,
                    40,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "light dependent reaction to create glucose.",
                    0,
                    60,
                    Color.White
                );
                break;
            case 2:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input =
                    "We are now back inside the main chamber of the CHLOROPLAST "
                    + "this reaction happens in a cyclical manner and uses the products"
                    + " of the dependent light reaction to keep on going. Can you "
                    + "help out by giving some ATP to the products so that they can be "
                    + "properly synthesised.";

                WriteTextBox(spriteBatch, input, _textBox);

                break;
            case 3:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwoPointFive,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );
                
                WriteFontCentreSmaller("HOW TO PLAY", 0, -150, Color.White);

                WriteFontCentreSmaller("catch the substrates", -110, 0, Color.White);
                WriteFontCentreSmaller(" to help them ", -110, 20, Color.White);
                WriteFontCentreSmaller("through the reaction", -110, 40, Color.White);

                WriteFontCentreSmaller("Hit the golden ", 120, 0, Color.White);
                WriteFontCentreSmaller("molecules to ", 120, 20, Color.White);
                WriteFontCentreSmaller("synthesise the ", 120, 40, Color.White);
                WriteFontCentreSmaller("reaction", 120, 60, Color.White);

                break;
            case 4:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                spriteBatch.Draw(
                    _electron,
                    _electronPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_electron.Width / 2, _electron.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                
                spriteBatch.Draw(
                    _calvinOne,
                    _calvinPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_calvinOne.Width / 2, _calvinOne.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                break;
            case 5:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                spriteBatch.Draw(
                    _electron,
                    _electronPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_electron.Width / 2, _electron.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                
                spriteBatch.Draw(
                    _calvinTwo,
                    _calvinPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_calvinOne.Width / 2, _calvinOne.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                break;
            case 6:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                spriteBatch.Draw(
                    _electron,
                    _electronPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_electron.Width / 2, _electron.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                
                spriteBatch.Draw(
                    _calvinThree,
                    _calvinPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_calvinOne.Width / 2, _calvinOne.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                break;
            case 7:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                spriteBatch.Draw(
                    _electron,
                    _electronPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_electron.Width / 2, _electron.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                
                spriteBatch.Draw(
                    _calvinFour,
                    _calvinPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_calvinOne.Width / 2, _calvinOne.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                break;
            case 8:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                spriteBatch.Draw(
                    _electron,
                    _electronPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_electron.Width / 2, _electron.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                
                spriteBatch.Draw(
                    _calvinFive,
                    _calvinPosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_calvinOne.Width / 2, _calvinOne.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
                break;
            case 9:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input3 =
                    "Well done! You successfully managed to produce GLUCOSE! "
                    + "The Calvin cycle is quite complex, but don't worry you won't be "
                    + "quizzed on the actual substrates. On that note though...";

                WriteTextBox(spriteBatch, input3, _textBox);

                break;
            case 10:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input4 =
                    "What are the main substrates of the Calvin cycle?";

                var ansA = "A) ATP AND CO2";
                var ansB = "B) LIGHT";
                var ansC = "C) ENZYME A";

                WriteTextBox(spriteBatch, input4, ansA, ansB, ansC, _fightTextBox);
                break;
            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}
