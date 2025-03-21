using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// Level four inherited state handles all logic for loading and updating content for level four
/// </summary>
public class LevelFour : Level
{
    private double _elapsedTime = 0;
    private double _sequenceTimer = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _screenTwo;
    private Texture2D _textBox;
    private Texture2D _fightTextBox;
    private KeyboardState _previousKeyboardState;
    
    public LevelFour(Game1 game)
        : base(game) { }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/guardCellClosed");
        _screenTwo = game.Content.Load<Texture2D>("Backgrounds/guardCellOpen");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
        _fightTextBox = game.Content.Load<Texture2D>("Other/fightTextBox");
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
                case 7:
                    _sequenceTimer = 0;
                    UpdateCurrentScreenSequence(8);
                    break;
                
            }
        
        _previousKeyboardState = currentKeyboardState;
        
        if (_currentScreenSequence.Equals(3))
        {
            game.startFightMusic();
            if (currentKeyboardState.IsKeyDown(Keys.B))
            {
                _previousKeyboardState = currentKeyboardState;
                UpdateCurrentScreenSequence(4);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.C))
            {
                game.ChangeState(new GameOverState(game));
            }
        }
        if (_currentScreenSequence.Equals(4))
        {
            game.startFightMusic();
            if (currentKeyboardState.IsKeyDown(Keys.C))
            {
                _previousKeyboardState = currentKeyboardState;
                UpdateCurrentScreenSequence(5);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.A) || (currentKeyboardState.IsKeyDown(Keys.B) && !_previousKeyboardState.IsKeyDown(Keys.B)))
            {
                game.ChangeState(new GameOverState(game));
            }
                
        }
        if (_currentScreenSequence.Equals(5))
        {
            game.startFightMusic();
            if (currentKeyboardState.IsKeyDown(Keys.B))
            {
                _previousKeyboardState = currentKeyboardState;
                UpdateCurrentScreenSequence(6);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.A) || (currentKeyboardState.IsKeyDown(Keys.C) && !_previousKeyboardState.IsKeyDown(Keys.C)))
            {
                 game.ChangeState(new GameOverState(game));
            }
               
        }
        if (_currentScreenSequence.Equals(6))
        {
            game.startFightMusic();
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                UpdateCurrentScreenSequence(7);
                game.stopFightMusic();
                game.PlayBackgroundMusic();
                game.increaseKnowledge();
            }

            if (currentKeyboardState.IsKeyDown(Keys.C) || (currentKeyboardState.IsKeyDown(Keys.B) && !_previousKeyboardState.IsKeyDown(Keys.B)))
            {
                game.ChangeState(new GameOverState(game));
            }
                
        }
        
        if (_currentScreenSequence.Equals(8) && _sequenceTimer > 1)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(9);
        }
        if (_currentScreenSequence.Equals(9) && _sequenceTimer > 1.5)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(10);
        }
        if (_currentScreenSequence.Equals(10) && _sequenceTimer > 2)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(11);
        }
        if (_currentScreenSequence.Equals(11) && _sequenceTimer > 2)
        {
            _sequenceTimer = 0;
            UpdateCurrentScreenSequence(12);
        }
        if (_currentScreenSequence.Equals(12) && _sequenceTimer > 2)
        {
            game.Exit();
        }

        // make sure health, knowledge, and battery are displayed for relevant scenes
        if (
            _currentScreenSequence.Equals(3)
            || _currentScreenSequence.Equals(4)
            || _currentScreenSequence.Equals(5)
            || _currentScreenSequence.Equals(6)
        )
            game.toggleHealthKnowledge(true);
        else
            game.toggleHealthKnowledge(false);
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
                WriteFontCentre("LEVEL FOUR", 0, 0, Color.White);
                break;
            case 1:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    new Color(50, 50, 50)
                );
                WriteFontCentre("FINAL BOSS", 0, -20, Color.White);
                WriteFontCentreSmaller(
                    "Have you been paying attention because it's time for a quiz!",
                    0,
                    20,
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
                    "Thank you for all your hard work, you've really helped us out round "
                    + "here, however I can't just let you go without checking that "
                    + "you've really been paying attention now can I? In "
                    + "order for my to open these GUARD CELLS you are going "
                    + "to have to answer these questions.";

                WriteTextBox(spriteBatch, input, _textBox);

                break;
            case 3:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input1 =
                    "What were the main products of the light dependent reaction?";

                var ansA = "A) Water and Carbon dioxide";
                var ansB = "B) ATP and Oxygen";
                var ansC = "C) Glucose and NADP";

                WriteTextBox(spriteBatch, input1, ansA, ansB, ansC, _fightTextBox);
                break;
            case 4:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input2 =
                    "What is the main purpose of PSII?";

                ansA = "A) To produce NADPH for the Calvin cycle";
                ansB = "B) To fix carbon into organic molecules";
                ansC = "C) To split water and generate high-energy electrons";

                WriteTextBox(spriteBatch, input2, ansA, ansB, ansC, _fightTextBox);
                break;
            case 5:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input3 =
                    "What is the main purpose of the Calvin cycle?";

                ansA = "A) To generate ATP from light energy";
                ansB = "B) To produce glucose using carbon dioxide";
                ansC = "C) To release oxygen as a byproduct";

                WriteTextBox(spriteBatch, input3, ansA, ansB, ansC, _fightTextBox);
                break;
            case 6:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input4 =
                    "Do you feel like you understand the photosynthesis cycle slightly better?";

                ansA = "A) Yes, that was really informative!";
                ansB = "B) I'm not sure I could use more practice...";
                ansC = "C) No";

                WriteTextBox(spriteBatch, input4, ansA, ansB, ansC, _fightTextBox);
                break;
            case 7:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input5 =
                    "Wow well done! You did so well, I feel you really deserve"
                    + " to leave now, thank you so much for all your help though!"
                    + " Just let me open the guard cells.";

                WriteTextBox(spriteBatch, input5, _textBox);
                break;
            case 8:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenOne,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );
                
                break;
            case 9:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenTwo,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );
                
                break;
            case 10:
                WriteFontCentre("THANK YOU FOR PLAYING!", 0, 0, Color.White);
                break;
            case 11:
                WriteFontCentre("I HOPE THIS HELPED YOU LEARN A LITTLE", 0, -20, Color.White);
                WriteFontCentre("MORE ABOUT PHOTOSYNTHESIS", 0, 20, Color.White);
                break;
            case 12:
                WriteFontCentre("THE END", 0, 0, Color.White);
                break;
            
            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}
