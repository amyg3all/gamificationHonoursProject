using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.Levels;

public class LevelTwo : Level
{
    private double _elapsedTime = 0;
    private int _currentScreenSequence = 0;
    private Texture2D _screenOne;
    private Texture2D _screenTwo;
    private Texture2D _screenThree;
    private Texture2D _screenFour;
    private Texture2D _waterMolecule;
    private Texture2D _textBox;
    private KeyboardState _previousKeyboardState;
    private Vector2 _waterMoleculePosition;
    private float _waterMoleculeSpeed;

    public LevelTwo(Game1 game)
        : base(game) { }

    public override void LoadContent()
    {
        BakeFont();
        _screenOne = game.Content.Load<Texture2D>("Backgrounds/leaf");
        _screenTwo = game.Content.Load<Texture2D>("Backgrounds/PSII");
        _screenThree = game.Content.Load<Texture2D>("Backgrounds/etc");
        _screenFour = game.Content.Load<Texture2D>("Backgrounds/hydroPump");
        _waterMolecule = game.Content.Load<Texture2D>("Sprites/water");
        _textBox = game.Content.Load<Texture2D>("Other/pixelTextBox");
        _waterMoleculePosition = new Vector2(
            game._graphics.PreferredBackBufferWidth / 2,
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

    public override void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime > 3)
        {
            UpdateCurrentScreenSequence(1);
        }

        var currentKeyboardState = Keyboard.GetState();

        // Check if Enter is pressed and not held down
        if (
            currentKeyboardState.IsKeyDown(Keys.Enter) && _previousKeyboardState.IsKeyUp(Keys.Enter)
        )
        {
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
            }
        }
        _previousKeyboardState = currentKeyboardState;

        var updatedBallSpeed = _waterMoleculeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;

        if (currentKeyboardState.IsKeyDown(Keys.Up) && _waterMoleculePosition.Y > 5)
        {
            _waterMoleculePosition.Y -= updatedBallSpeed;
        }

        if (
            currentKeyboardState.IsKeyDown(Keys.Down)
            && _waterMoleculePosition.Y < screenHeight - 30
        )
        {
            _waterMoleculePosition.Y += updatedBallSpeed;
        }

        if (currentKeyboardState.IsKeyDown(Keys.Left) && _waterMoleculePosition.X > 26)
        {
            _waterMoleculePosition.X -= updatedBallSpeed;
        }

        if (
            currentKeyboardState.IsKeyDown(Keys.Right)
            && _waterMoleculePosition.X < screenWidth - 18
        )
        {
            _waterMoleculePosition.X += updatedBallSpeed;
        }

        // complete moving up the capillary
        if (_currentScreenSequence.Equals(4) && _waterMoleculePosition.Y < 10)
        {
            UpdateCurrentScreenSequence(5);
            game.startFightMusic();
        }

        // answer question
        if (_currentScreenSequence.Equals(5))
        {
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                UpdateCurrentScreenSequence(6);
            }

            if (currentKeyboardState.IsKeyDown(Keys.B) || currentKeyboardState.IsKeyDown(Keys.C))
            {
                game.ChangeState(new GameOverState(game));
            }
        }

        if (_currentScreenSequence.Equals(4))
        {
            game.toggleHealthKnowledge(true);
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
                    + "It is one of the major components of a plant cell and is what"
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
            case 5:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input3 =
                    "Well done for going up the capillary! "
                    + "Let's hope you didn't just skip through my explanations, "
                    + "as it's now quiz time! What features allow you (water) to "
                    + "easily flow up the capillary? press A, B, or C  A) HYDROGEN BONDING B) "
                    + "GOOD SOLUBILITY C) LATENT HEAT OF EVAPORATION";

                WriteTextBox(spriteBatch, input3, _textBox);

                break;

            case 6:
                game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(
                    _screenThree,
                    new Rectangle(0, 0, screenWidth, screenHeight),
                    Color.White
                );

                var input4 =
                    "Good Job! Clearly you were paying attention,"
                    + " are you ready to get some reactions started?"
                    + " ha ha sorry you don't really get a choice!";

                WriteTextBox(spriteBatch, input4, _textBox);

                break;

            default:
                game.GraphicsDevice.Clear(Color.Black);
                break;
        }

        spriteBatch.End();
    }
}
