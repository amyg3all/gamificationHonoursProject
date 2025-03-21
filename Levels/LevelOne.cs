using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// Level one inherited state handles all logic for loading and updating content for level one
/// </summary>
public class LevelOne : Level
{
    private double _elapsedTime = 0;
    private double _elapsedTimetemp = 0;
    private double _beatTheClock = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _screenTwo;
    private Texture2D _screenThree;
    private Texture2D _waterMolecule;
    private Texture2D _textBox;
    private Texture2D _fightTextBox;
    private KeyboardState _previousKeyboardState;
    private Vector2 _waterMoleculePosition;
    private float _waterMoleculeSpeed;

    public LevelOne(Game1 game)
        : base(game)
    {
    }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/rootSystem");
        _screenTwo = game.Content.Load<Texture2D>("Backgrounds/capillaryNetwork");
        _screenThree = game.Content.Load<Texture2D>("Backgrounds/levelOneHelp");
        _waterMolecule = game.Content.Load<Texture2D>("Sprites/water");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
        _fightTextBox = game.Content.Load<Texture2D>("Other/fightTextBox");
        _waterMoleculePosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2 - 20,
            game._graphics.PreferredBackBufferHeight / 2 + 215
        );
        _waterMoleculeSpeed = 100f;
    }

    private void UpdateCurrentScreenSequence(int newScreenSequence)
    {
        if (newScreenSequence == _currentScreenSequence + 1)
        {
            _currentScreenSequence = newScreenSequence;
        }
    }

    // slightly dangerous use with caution
    private void ChangeCurrentScreenSequence(int newScreenSequence)
    {
        if (_currentScreenSequence == 7) game.decreaseHealth();
        _waterMoleculePosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2 - 20,
            game._graphics.PreferredBackBufferHeight / 2 + 215
        );
        _currentScreenSequence = newScreenSequence;
    }

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime > 2) UpdateCurrentScreenSequence(1);

        var currentKeyboardState = Keyboard.GetState();

        // Check if Enter is pressed and not held down
        _elapsedTimetemp += gameTime.ElapsedGameTime.TotalSeconds;
        _beatTheClock += gameTime.ElapsedGameTime.TotalSeconds;

        if (
            currentKeyboardState.IsKeyDown(Keys.Enter)
            && _previousKeyboardState.IsKeyUp(Keys.Enter)
            && _elapsedTimetemp > 1.5
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
                    UpdateCurrentScreenSequence(4);
                    break;
                case 4:
                    _beatTheClock = 0;
                    game.startCountDown();
                    UpdateCurrentScreenSequence(5);
                    break;
                case 7:
                    game.toggleHealthKnowledge(false);
                    game.ChangeLevel();
                    break;
            }

        if (
            currentKeyboardState.IsKeyDown(Keys.Enter)
            && _previousKeyboardState.IsKeyUp(Keys.Enter)
            && _currentScreenSequence == 8
        )
        {
            ChangeCurrentScreenSequence(5);
        }

        _previousKeyboardState = currentKeyboardState;

        var updatedWaterSpeed = _waterMoleculeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;

        if ((currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W)) && _waterMoleculePosition.Y > 5)
            _waterMoleculePosition.Y -= updatedWaterSpeed;

        if (
            (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            && _waterMoleculePosition.Y < screenHeight - 30
        )
            _waterMoleculePosition.Y += updatedWaterSpeed;

        if (_currentScreenSequence.Equals(5))
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A)) && _waterMoleculePosition.X > 320)
                _waterMoleculePosition.X -= updatedWaterSpeed;
        }
        else
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.D)) && _waterMoleculePosition.X > 26)
                _waterMoleculePosition.X -= updatedWaterSpeed;
        }

        if (_currentScreenSequence.Equals(5))
        {
            if (
                (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                && _waterMoleculePosition.X < screenWidth - 410
            )
                _waterMoleculePosition.X += updatedWaterSpeed;
        }
        else
        {
            if (
                (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.A))
                && _waterMoleculePosition.X < screenWidth - 18
            )
                _waterMoleculePosition.X += updatedWaterSpeed;
        }

        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 425 and < 450
            && _waterMoleculePosition.X < 355
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 403 and < 428
            && _waterMoleculePosition.X < 371
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 315 and < 340
            && _waterMoleculePosition.X > 351
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 268 and < 293
            && _waterMoleculePosition.X < 350
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 161 and < 186
            && _waterMoleculePosition.X > 359
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 140 and < 165
            && _waterMoleculePosition.X > 340
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 123 and < 148
            && _waterMoleculePosition.X > 330
        )
            ChangeCurrentScreenSequence(8);
        if (
            _currentScreenSequence == 5
            && _waterMoleculePosition.Y is > 12 and < 37
            && _waterMoleculePosition.X > 360
        )
            ChangeCurrentScreenSequence(8);

        // complete moving up the capillary
        if (_currentScreenSequence.Equals(5) && _waterMoleculePosition.Y < 10)
        {
            game.endCountDown();
            UpdateCurrentScreenSequence(6);
            game.startFightMusic();
        }

        // answer question
        if (_currentScreenSequence.Equals(6))
        {
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                UpdateCurrentScreenSequence(7);
                game.stopFightMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.B) || currentKeyboardState.IsKeyDown(Keys.C))
                game.ChangeState(new GameOverState(game));
        }

        if (_currentScreenSequence.Equals(8)) game.PlayBackgroundMusic();

        if (_currentScreenSequence == 5 || _currentScreenSequence == 6 || _currentScreenSequence == 7 ||
            _currentScreenSequence == 8) game.toggleHealthKnowledge(true);

        if (_beatTheClock > 10 && _currentScreenSequence.Equals(5))
        {
            game.ranOutOfTime();
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
                WriteFontCentre("LEVEL ONE", 0, 0, Color.White);
                break;
            case 1:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    new Color(50, 50, 50)
                );
                WriteFontCentre("THE ROOT SYSTEM", 0, -20, Color.White);
                WriteFontCentreSmaller(
                    "The root system is where water and essential mineral ions are drawn into the plant.",
                    0,
                    20,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "They are carried along through the xylem and the phloem, respectively, to where they",
                    0,
                    40,
                    Color.White
                );
                WriteFontCentreSmaller("are needed.", 0, 60, Color.White);
                break;
            case 2:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input =
                    "Hey there! Welcome to the Root System."
                    + " Down here, it's our job to ensure some of us reach "
                    + "the photosynthesis site—but there's a long journey ahead!"
                    + " We'll have to navigate these roots to make our way up the "
                    + "plant to the leaves, where we’ll join some other molecules to "
                    + "get the reaction started. To succeed, we’ll travel via OSMOSIS,"
                    + " where we must use CAPILLARY ACTION  and harness our";

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
                    "HYDROGEN BONDS in order to reach the top. However, "
                    + "this may take a long time, make sure not to bump into any "
                    + "other water molecules or you may get stuck and have to go at "
                    + "their pace. Are you ready?";

                WriteTextBox(spriteBatch, input2, _textBox);

                break;
            
            case 4:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                WriteFontCentreSmaller(
                    "HOW TO PLAY",
                    0,
                    -150,
                    Color.White
                );
                
                WriteFontCentreSmaller(
                    "Use the arrow keys to ",
                    -110,
                    60,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "move",
                    -110,
                    80,
                    Color.White
                );
                
                WriteFontCentreSmaller(
                    "Reach the top of ",
                    120,
                    60,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "the capillary as ",
                    120,
                    80,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "fast as you can, ",
                    120,
                    100,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "avoiding all other",
                    120,
                    120,
                    Color.White
                );
                
                WriteFontCentreSmaller(
                    "molecules",
                    120,
                    140,
                    Color.White
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
                    _waterMolecule,
                    _waterMoleculePosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(_waterMolecule.Width / 2, _waterMolecule.Height / 2),
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

                var input4 =
                    "Well done! "
                    + "Let's hope you didn't just skip through my explanations, "
                    + "as it's now quiz time! What features allow you (water) to "
                    + "easily flow up the capillary? press the corresponding letter to answer";
                var ansA = "A) HYDROGEN BONDING";
                var ansB = "B) GOOD SOLUBILITY";
                var ansC = "C) LATENT HEAT OF EVAPORATION";

                WriteTextBox(spriteBatch, input4, ansA, ansB, ansC, _fightTextBox);

                break;

            case 7:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input5 =
                    "Good Job! Clearly you were paying attention,"
                    + " are you ready to get some reactions started?"
                    + " ha ha sorry you don't really get a choice!";

                WriteTextBox(spriteBatch, input5, _textBox);

                break;
            case 8:
                WriteFontCentre("Oh no, you seem to be stuck...", 0, 0, Color.White);
                break;

            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}