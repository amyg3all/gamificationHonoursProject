using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// Level two inherited state handles all logic for loading and updating content for level two
/// </summary>
public class LevelTwo : Level
{
    private double _elapsedTime = 0;
    private double _sequenceTimer = 0;
    private double _beatTheClock = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _screenTwo;
    private Texture2D _screenTwoPointFive;
    private Texture2D _screenThree;
    private Texture2D _cutScene;
    private Texture2D _cutSceneZero;
    private Texture2D _cutSceneOne;
    private Texture2D _cutSceneTwo;
    private Texture2D _cutSceneThree;
    private Texture2D _cutSceneFour;
    private Texture2D _cutSceneFive;
    private Texture2D _cutSceneSix;
    private Texture2D _waterMolecule;
    private Texture2D _electron;
    private Texture2D _textBox;
    private Texture2D _fightTextBox;
    private KeyboardState _previousKeyboardState;
    private Vector2 _waterMoleculePosition;
    private float _waterMoleculeSpeed;
    private float _electronSpeed;

    private Texture2D _cellOne;
    private Texture2D _cellTwo;
    private Texture2D _cellThree;
    private Texture2D _cellFour;
    private Texture2D _cellFive;
    private Texture2D _cellSix;
    private Texture2D _cellSeven;
    private Texture2D _cellEight;
    private Texture2D _cellNine;
    private Texture2D _cellTen;

    private bool _hitCellOne = false;
    private bool _hitCellTwo = false;
    private bool _hitCellThree = false;
    private bool _hitCellFour = false;
    private bool _hitCellFive = false;
    private bool _hitCellSix = false;
    private bool _hitCellSeven = false;
    private bool _hitCellEight = false;
    private bool _hitCellNine = false;
    private bool _hitCellTen = false;

    private bool increaseCellBatteryFirst = true;
    private bool increaseCellBatterySecond = true;

    public LevelTwo(Game1 game)
        : base(game)
    {
        _hitCellOne = false;
        _hitCellTwo = false;
        _hitCellThree = false;
        _hitCellFour = false;
        _hitCellFive = false;
        _hitCellSix = false;
        _hitCellSeven = false;
        _hitCellEight = false;
        _hitCellNine = false;
        _hitCellTen = false;
    }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/leaf");
        _screenTwo = game.Content.Load<Texture2D>("Backgrounds/PSII");
        _screenTwoPointFive = game.Content.Load<Texture2D>("Backgrounds/levelTwoHelp");
        _screenThree = game.Content.Load<Texture2D>("Backgrounds/etc");
        _cutScene = game.Content.Load<Texture2D>("Backgrounds/cutscene");
        _cutSceneZero = game.Content.Load<Texture2D>("Backgrounds/cutscene0");
        _cutSceneOne = game.Content.Load<Texture2D>("Backgrounds/cutscene1");
        _cutSceneTwo = game.Content.Load<Texture2D>("Backgrounds/cutscene2");
        _cutSceneThree = game.Content.Load<Texture2D>("Backgrounds/cutscene3");
        _cutSceneFour = game.Content.Load<Texture2D>("Backgrounds/cutscene4");
        _cutSceneFive = game.Content.Load<Texture2D>("Backgrounds/cutscene5");
        _cutSceneSix = game.Content.Load<Texture2D>("Backgrounds/cutscene6");
        _waterMolecule = game.Content.Load<Texture2D>("Sprites/water");
        _electron = game.Content.Load<Texture2D>("Sprites/electron");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
        _fightTextBox = game.Content.Load<Texture2D>("Other/fightTextBox");

        _cellOne = game.Content.Load<Texture2D>("Objects/cellOne");
        _cellTwo = game.Content.Load<Texture2D>("Objects/cellTwo");
        _cellThree = game.Content.Load<Texture2D>("Objects/cellThree");
        _cellFour = game.Content.Load<Texture2D>("Objects/cellFour");
        _cellFive = game.Content.Load<Texture2D>("Objects/cellFive");
        _cellSix = game.Content.Load<Texture2D>("Objects/cellSix");
        _cellSeven = game.Content.Load<Texture2D>("Objects/cellSeven");
        _cellEight = game.Content.Load<Texture2D>("Objects/cellEight");
        _cellNine = game.Content.Load<Texture2D>("Objects/cellNine");
        _cellTen = game.Content.Load<Texture2D>("Objects/cellTen");

        _waterMoleculePosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2 - 350,
            game._graphics.PreferredBackBufferHeight / 2 + 210
        );
        _waterMoleculeSpeed = 100f;
        _electronSpeed = 250f;
    }

    private void UpdateCurrentScreenSequence(int newScreenSequence)
    {
        if (newScreenSequence == _currentScreenSequence + 1)
            _currentScreenSequence = newScreenSequence;
    }

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        _sequenceTimer += gameTime.ElapsedGameTime.TotalSeconds;
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
                    UpdateCurrentScreenSequence(4);
                    break;
                case 4:
                    UpdateCurrentScreenSequence(5);
                    break;
                case 9:
                    UpdateCurrentScreenSequence(10);
                    break;
                case 10:
                    _beatTheClock = 0;
                    game.startCountDown();
                    UpdateCurrentScreenSequence(11);
                    break;
                case 19:
                    _sequenceTimer = 0;
                    game.StopBackgroundMusic();
                    game.startFightMusic();
                    UpdateCurrentScreenSequence(20);
                    break;
                case 21:
                    game.ChangeLevel();
                    break;
            }

        _previousKeyboardState = currentKeyboardState;

        var updatedWaterSpeed = _waterMoleculeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        var updatedElectronSpeed = _electronSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;

        if (_currentScreenSequence.Equals(5))
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W)) && _waterMoleculePosition.Y > 256)
                _waterMoleculePosition.Y -= updatedWaterSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
                && _waterMoleculePosition.Y < screenHeight - 30
            )
                _waterMoleculePosition.Y += updatedWaterSpeed;

            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A)) && _waterMoleculePosition.X > 26)
                _waterMoleculePosition.X -= updatedWaterSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                && _waterMoleculePosition.X < screenWidth - 18
            )
                _waterMoleculePosition.X += updatedWaterSpeed;
        }

        if (_currentScreenSequence.Equals(11))
        {
            if ((currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W)) && _waterMoleculePosition.Y > 15)
                _waterMoleculePosition.Y -= updatedElectronSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
                && _waterMoleculePosition.Y < screenHeight - 30
            )
                _waterMoleculePosition.Y += updatedElectronSpeed;

            if ((currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A)) && _waterMoleculePosition.X > 26)
                _waterMoleculePosition.X -= updatedElectronSpeed;

            if (
                (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
                && _waterMoleculePosition.X < screenWidth - 18
            )
                _waterMoleculePosition.X += updatedElectronSpeed;
        }

        if (
            _currentScreenSequence.Equals(5)
            && _waterMoleculePosition.Y < 291
            && _waterMoleculePosition.X is > 60 and < 149
        )
        {
            game.StopBackgroundMusic();
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(6);
        }

        if (_currentScreenSequence.Equals(6) && _sequenceTimer > 0.7)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(7);
        }
        if (_currentScreenSequence.Equals(7) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(8);
        }

        if (_currentScreenSequence.Equals(8) && _sequenceTimer > 0.7)
        {
            game.PlayBackgroundMusic();
            UpdateCurrentScreenSequence(9);
        }

        if (_currentScreenSequence.Equals(12) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(13);
        }
        if (_currentScreenSequence.Equals(13) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(14);
        }
        if (_currentScreenSequence.Equals(14) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            game.setBattery(1);
            UpdateCurrentScreenSequence(15);
        }
        if (_currentScreenSequence.Equals(15) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(16);
        }
        if (_currentScreenSequence.Equals(16) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(17);
        }
        if (_currentScreenSequence.Equals(17) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            game.setBattery(0);
            UpdateCurrentScreenSequence(18);
        }
        if (_currentScreenSequence.Equals(18) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(19);
        }

        if (
            _currentScreenSequence.Equals(11)
            && _hitCellOne
            && _hitCellTwo
            && _hitCellThree
            && _hitCellFour
            && _hitCellFive
            && increaseCellBatteryFirst
        )
        {
            increaseCellBatteryFirst = false;
            game.increaseBattery();
        }
        if (
            _currentScreenSequence.Equals(11)
            && _hitCellSix
            && _hitCellSeven
            && _hitCellEight
            && _hitCellNine
            && _hitCellTen
            && increaseCellBatterySecond
        )
        {
            increaseCellBatterySecond = false;
            game.increaseBattery();
        }

        if (_currentScreenSequence.Equals(11))
        {
            if (
                _waterMoleculePosition.X is > 72 and < 95
                && _waterMoleculePosition.Y is > 182 and < 225
            )
            {
                _hitCellOne = true;
            }
            if (
                _waterMoleculePosition.X is > 70 and < 90
                && _waterMoleculePosition.Y is > 110 and < 135
            )
            {
                _hitCellTwo = true;
            }
            if (
                _waterMoleculePosition.X is > 82 and < 100
                && _waterMoleculePosition.Y is > 50 and < 82
            )
            {
                _hitCellThree = true;
            }
            if (
                _waterMoleculePosition.X is > 117 and < 145
                && _waterMoleculePosition.Y is > 62 and < 95
            )
            {
                _hitCellFour = true;
            }
            if (
                _waterMoleculePosition.X is > 120 and < 142
                && _waterMoleculePosition.Y is > 165 and < 187
            )
            {
                _hitCellFive = true;
            }
            if (
                _waterMoleculePosition.X is > 342 and < 357
                && _waterMoleculePosition.Y is > 125 and < 152
            )
            {
                _hitCellSix = true;
            }
            if (
                _waterMoleculePosition.X is > 347 and < 367
                && _waterMoleculePosition.Y is > 67 and < 100
            )
            {
                _hitCellSeven = true;
            }
            if (
                _waterMoleculePosition.X is > 437 and < 452
                && _waterMoleculePosition.Y is > 32 and < 52
            )
            {
                _hitCellEight = true;
            }
            if (
                _waterMoleculePosition.X is > 420 and < 445
                && _waterMoleculePosition.Y is > 125 and < 160
            )
            {
                _hitCellNine = true;
            }
            if (
                _waterMoleculePosition.X is > 417 and < 435
                && _waterMoleculePosition.Y is > 180 and < 210
            )
            {
                _hitCellTen = true;
            }
        }

        // make sure health, knowledge, and battery are displayed for relevant scenes
        if (
            _currentScreenSequence.Equals(5)
            || _currentScreenSequence.Equals(9)
            || _currentScreenSequence.Equals(10)
            || _currentScreenSequence.Equals(11)
            || _currentScreenSequence.Equals(12)
            || _currentScreenSequence.Equals(13)
            || _currentScreenSequence.Equals(14)
            || _currentScreenSequence.Equals(15)
            || _currentScreenSequence.Equals(16)
            || _currentScreenSequence.Equals(17)
            || _currentScreenSequence.Equals(18)
            || _currentScreenSequence.Equals(19)
        )
            game.toggleHealthKnowledge(true);
        else
            game.toggleHealthKnowledge(false);

        if (
            _currentScreenSequence.Equals(9)
            || _currentScreenSequence.Equals(10)
            || _currentScreenSequence.Equals(11)
            || _currentScreenSequence.Equals(12)
            || _currentScreenSequence.Equals(13)
            || _currentScreenSequence.Equals(14)
            || _currentScreenSequence.Equals(15)
            || _currentScreenSequence.Equals(16)
            || _currentScreenSequence.Equals(17)
            || _currentScreenSequence.Equals(18)
            || _currentScreenSequence.Equals(19)
        )
            game.toggleBattery(true);
        else
            game.toggleBattery(false);

        if (_beatTheClock > 10 && _currentScreenSequence.Equals(11))
        {
            game.ranOutOfTime();
            game.ChangeState(new GameOverState(game));
        }

        if (_currentScreenSequence.Equals(11) && game.getCurrentBattery() == 2)
        {
            game.endCountDown();
            UpdateCurrentScreenSequence(12);
        }

        if (_currentScreenSequence.Equals(20))
        {
            if (currentKeyboardState.IsKeyDown(Keys.C))
            {
                UpdateCurrentScreenSequence(21);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.B))
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
                WriteFontCentre("LEVEL TWO", 0, 0, Color.White);
                break;
            case 1:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    new Color(50, 50, 50)
                );
                WriteFontCentre("THE LEAF", 0, -20, Color.White);
                WriteFontCentreSmaller(
                    "The leaves are where most of the action of photosynthesis occurs, there are two main",
                    0,
                    20,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "reactions that we will be focusing on, the independent light reaction and the dependent",
                    0,
                    40,
                    Color.White
                );
                WriteFontCentreSmaller(
                    "light reaction. These happen in tandem and we will go over their key components",
                    0,
                    60,
                    Color.White
                );
                break;
            case 2:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input =
                    "We have made it inside the leaf! This right here is a "
                    + "typical chloroplast which is the main site of photosynthesis."
                    + " It is one of the major components of a plant cell and is what "
                    + "gives them their distinctive green colour. Each disc you see "
                    + "is known as a THYLAKOID and a stack of thylakoids is known as a "
                    + "GRANA. We will next be entering the";

                WriteTextBox(spriteBatch, input, _textBox);

                break;
            case 3:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input2 = "thylakoid membrane which is " + "where the actual reaction occurs.";

                WriteTextBox(spriteBatch, input2, _textBox);

                break;
            case 4:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input3 =
                    "We've made it inside thylakoid membrane. I've given "
                    + "you a spare empty battery, you'll need it to complete the LIGHT"
                    + " DEPENDENT REACTION. Those blue organelles are photosystems, it's your job to"
                    + " charge them up so that we can pump those HYDROGEN IONS, the pink balls, across the membrane, that's"
                    + " how we generate our energy over here. Go take a look at the one on the left";

                WriteTextBox(spriteBatch, input3, _textBox);
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
                break;

            case 7:
                game.GraphicsDevice.Clear(Color.White);
                WriteFontCentre("BANG", 0, 0, Color.Black);
                break;
            case 8:
                game.GraphicsDevice.Clear(Color.Black);
                break;
            case 9:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input4 =
                    "Ah I forgot to warn you, you have to be a little bit smaller"
                    + " to get inside the generator. You've now become an ELECTRON, might be "
                    + " best to complete this quickly so that the rest of you doesn't get swept up in anything else";

                WriteTextBox(spriteBatch, input4, _textBox);
                break;

            case 10:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwoPointFive,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                WriteFontCentreSmaller("HOW TO PLAY", 0, -150, Color.White);

                WriteFontCentreSmaller("bump into the green", -110, 0, Color.White);
                WriteFontCentreSmaller("organelles to light", -110, 20, Color.White);
                WriteFontCentreSmaller("up the electron", -110, 40, Color.White);
                WriteFontCentreSmaller("transfer chain", -110, 60, Color.White);

                WriteFontCentreSmaller("charge up the ", 120, 0, Color.White);
                WriteFontCentreSmaller("battery to complete", 120, 20, Color.White);
                WriteFontCentreSmaller("the reaction as", 120, 40, Color.White);
                WriteFontCentreSmaller("quickly as possible", 120, 60, Color.White);

                break;

            case 11:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                if (_hitCellOne)
                {
                    spriteBatch.Draw(
                        _cellOne,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellTwo)
                {
                    spriteBatch.Draw(
                        _cellTwo,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellThree)
                {
                    spriteBatch.Draw(
                        _cellThree,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellFour)
                {
                    spriteBatch.Draw(
                        _cellFour,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellFive)
                {
                    spriteBatch.Draw(
                        _cellFive,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellSix)
                {
                    spriteBatch.Draw(
                        _cellSix,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellSeven)
                {
                    spriteBatch.Draw(
                        _cellSeven,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellEight)
                {
                    spriteBatch.Draw(
                        _cellEight,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellNine)
                {
                    spriteBatch.Draw(
                        _cellNine,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }
                if (_hitCellTen)
                {
                    spriteBatch.Draw(
                        _cellTen,
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White
                    );
                }

                spriteBatch.Draw(
                    _electron,
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
            case 12:
                spriteBatch.Draw(
                    _cutScene,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 13:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneZero,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 14:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 15:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 16:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 17:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneFour,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 18:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneFive,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                break;
            case 19:

                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _cutSceneSix,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                WriteFontCentre("You Made ATP", 0, -100, Color.White);
                WriteFontCentre(">press enter<", 0, -70, Color.White);

                break;
            case 20:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input5 =
                    "Oh wow, that was fast! "
                    + "Well done on completing the light dependent reaction "
                    + "I hope you were paying attention as now it is quiz time! What membrane do the hydrogen ions "
                    + "have to be pumped across?";
                
                var ansA = "A) CHLOROPLAST MEMBRANE";
                var ansB = "B) GRANUM MEMBRANE";
                var ansC = "C) THYLAKOID MEMBRANE";

                WriteTextBox(spriteBatch, input5, ansA, ansB, ansC, _fightTextBox);
                break;
            case 21:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input6 =
                    "Well done! Now we need to put that ATP to good use, "
                    + "let us exit the granum and get started on the light independent "
                    + "reaction!";

                WriteTextBox(spriteBatch, input6, _textBox);

                break;
            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}
