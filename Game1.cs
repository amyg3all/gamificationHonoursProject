using gamificationHonoursProject.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject;

/// <summary>
/// handles all main game logic and implementation
/// Music by Eric Matyas
/// www.soundimage.org
/// </summary>
public class Game1 : Game
{
    private State _currentState;

    public readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public SpriteBatch SpriteBatch { get; private set; }
    private SoundEffect _backgroundMusic;
    private SoundEffectInstance _backgroundMusicInstance;
    private SoundEffect _fightMusic;
    private SoundEffectInstance _fightMusicInstance;

    private bool _displayHealthKnowledge = false;
    private bool _displayBattery = false;
    private int _currentHealth = 4;
    private int _currentKnowledge = 0;
    private int _currentBattery = 0;
    private Texture2D _emptyHealth;
    private Texture2D _emptyKnowledge;
    private Texture2D _oneHealth;
    private Texture2D _oneKnowledge;
    private Texture2D _twoHealth;
    private Texture2D _twoKnowledge;
    private Texture2D _threeHealth;
    private Texture2D _threeKnowledge;
    private Texture2D _fullHealth;
    private Texture2D _fullKnowledge;
    private Texture2D _emptyBattery;
    private Texture2D _halfBattery;
    private Texture2D _fullBattery;
    private Texture2D _countDownOne;
    private Texture2D _countDownTwo;
    private Texture2D _countDownThree;
    private Texture2D _countDownFour;
    private Texture2D _countDownFive;
    private Texture2D _countDownSix;
    private Texture2D _countDownSeven;
    private Texture2D _countDownEight;
    private Texture2D _countDownNine;
    private Texture2D _countDownTen;

    private double _countDown = 10;
    private bool _startCountDown = false;
    private bool _startCountDown8 = false;
    private bool _endCountDown = true;
    private bool _ranOutOfTime = false;

    private int _selectedLevel = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public int GetCurrentLevel()
    {
        return _selectedLevel;
    }

    public void ChangeLevel()
    {
        _selectedLevel++;
        ChangeState(new GamePlayState(this, _selectedLevel));
    }

    public void StartCountDown()
    {
        _startCountDown = true;
    }

    public void StartCountDown8()
    {
        _startCountDown8 = true;
    }

    public void EndCountDown()
    {
        _endCountDown = true;
    }

    public int GetCurrentBattery()
    {
        return _currentBattery;
    }

    public void IncreaseBattery()
    {
        // not allowed to increase battery indefinitely
        if (_currentBattery < 2)
        {
            _currentBattery++;
        }
    }

    public void SetBattery(int batteryPercentage)
    {
        // required so that if you die you can reset the battery to zero
        _currentBattery = batteryPercentage;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void DecreaseHealth()
    {
        // kills you if the health goes to zero
        if (_currentHealth != 1)
            _currentHealth--;
        else
            ChangeState(new GameOverState(this));
    }

    public void IncreaseKnowledge()
    {
        _currentKnowledge++;
    }

    public void ToggleHealthKnowledge(bool trueOrFalse)
    {
        _displayHealthKnowledge = trueOrFalse;
    }

    public void ToggleBattery(bool trueOrFalse)
    {
        _displayBattery = trueOrFalse;
    }

    public void ChangeState(State newState)
    {
        _currentState = newState;
        _currentState.LoadContent();
    }

    public void RanOutOfTime()
    {
        _ranOutOfTime = true;
    }

    public void UnsetRanOutOfTime()
    {
        _ranOutOfTime = false;
    }

    public bool GetRanOutOfTime()
    {
        return _ranOutOfTime;
    }

    public void PlayBackgroundMusic()
    {
        if (
            _backgroundMusicInstance != null
            && _backgroundMusicInstance.State != SoundState.Playing
        )
            _backgroundMusicInstance.Play();
    }

    public void StartFightMusic()
    {
        if (
            _backgroundMusicInstance is { State: SoundState.Playing }
        )
            _backgroundMusicInstance.Stop();
        if (_fightMusicInstance != null && _fightMusicInstance.State != SoundState.Playing)
            _fightMusicInstance.Play();
    }

    public void StopFightMusic()
    {
        if (_fightMusicInstance is { State: SoundState.Playing })
            _fightMusicInstance.Stop();
        if (
            _backgroundMusicInstance != null
            && _backgroundMusicInstance.State != SoundState.Playing
        )
            _backgroundMusicInstance.Play();
    }

    public void StopBackgroundMusic()
    {
        if (_backgroundMusicInstance is { State: SoundState.Playing })
            _backgroundMusicInstance.Stop();
    }

    protected override void Initialize()
    {
        ChangeState(new TitleScreenState(this));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundMusic = Content.Load<SoundEffect>("SoundEffects/backgroundMusic");
        _backgroundMusicInstance = _backgroundMusic.CreateInstance();
        _backgroundMusicInstance.IsLooped = true;
        _backgroundMusicInstance.Play();
        _fightMusic = Content.Load<SoundEffect>("SoundEffects/fight");
        _fightMusicInstance = _fightMusic.CreateInstance();
        _fightMusicInstance.IsLooped = true;

        _emptyHealth = Content.Load<Texture2D>("Objects/emptyHealth");
        _emptyKnowledge = Content.Load<Texture2D>("Objects/emptyKnowledge");
        _oneHealth = Content.Load<Texture2D>("Objects/oneHealth");
        _oneKnowledge = Content.Load<Texture2D>("Objects/oneKnowledge");
        _twoHealth = Content.Load<Texture2D>("Objects/twoHealth");
        _twoKnowledge = Content.Load<Texture2D>("Objects/twoKnowledge");
        _threeHealth = Content.Load<Texture2D>("Objects/threeHealth");
        _threeKnowledge = Content.Load<Texture2D>("Objects/threeKnowledge");
        _fullHealth = Content.Load<Texture2D>("Objects/fullHealth");
        _fullKnowledge = Content.Load<Texture2D>("Objects/fullKnowledge");
        _emptyBattery = Content.Load<Texture2D>("Objects/emptyBattery");
        _halfBattery = Content.Load<Texture2D>("Objects/halfBattery");
        _fullBattery = Content.Load<Texture2D>("Objects/fullBattery");

        _countDownOne = Content.Load<Texture2D>("Objects/one");
        _countDownTwo = Content.Load<Texture2D>("Objects/two");
        _countDownThree = Content.Load<Texture2D>("Objects/three");
        _countDownFour = Content.Load<Texture2D>("Objects/four");
        _countDownFive = Content.Load<Texture2D>("Objects/five");
        _countDownSix = Content.Load<Texture2D>("Objects/six");
        _countDownSeven = Content.Load<Texture2D>("Objects/seven");
        _countDownEight = Content.Load<Texture2D>("Objects/eight");
        _countDownNine = Content.Load<Texture2D>("Objects/nine");
        _countDownTen = Content.Load<Texture2D>("Objects/ten");
    }

    protected override void Update(GameTime gameTime)
    {
        _countDown -= gameTime.ElapsedGameTime.TotalSeconds;

        // starts a countdown of 10 seconds
        if (_startCountDown)
        {
            _countDown = 10;
            _startCountDown = false;
            _endCountDown = false;
        }

        // starts a countdown of 8 seconds
        if (_startCountDown8)
        {
            _countDown = 8;
            _startCountDown8 = false;
            _endCountDown = false;
        }

        _currentState.Update(gameTime);

        // quits the game if the user presses escape
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _currentState.Draw(gameTime);

        _spriteBatch.Begin();

        // draws the current knowledge and health bars according to their current fullness
        if (_displayHealthKnowledge)
        {
            switch (_currentKnowledge)
            {
                case 0:
                    _spriteBatch.Draw(_emptyKnowledge, Vector2.Zero, Color.White);
                    break;
                case 1:
                    _spriteBatch.Draw(_oneKnowledge, Vector2.Zero, Color.White);
                    break;
                case 2:
                    _spriteBatch.Draw(_twoKnowledge, Vector2.Zero, Color.White);
                    break;
                case 3:
                    _spriteBatch.Draw(_threeKnowledge, Vector2.Zero, Color.White);
                    break;
                case 4:
                    _spriteBatch.Draw(_fullKnowledge, Vector2.Zero, Color.White);
                    break;
                default:
                    _spriteBatch.Draw(_fullKnowledge, Vector2.Zero, Color.White);
                    break;
            }

            switch (_currentHealth)
            {
                case 0:
                    _spriteBatch.Draw(_emptyHealth, Vector2.Zero, Color.White);
                    break;
                case 1:
                    _spriteBatch.Draw(_oneHealth, Vector2.Zero, Color.White);
                    break;
                case 2:
                    _spriteBatch.Draw(_twoHealth, Vector2.Zero, Color.White);
                    break;
                case 3:
                    _spriteBatch.Draw(_threeHealth, Vector2.Zero, Color.White);
                    break;
                case 4:
                    _spriteBatch.Draw(_fullHealth, Vector2.Zero, Color.White);
                    break;
            }
        }

        // displays the battery according to current charge
        if (_displayBattery)
        {
            switch (_currentBattery)
            {
                case 0:
                    _spriteBatch.Draw(_emptyBattery, Vector2.Zero, Color.White);
                    break;
                case 1:
                    _spriteBatch.Draw(_halfBattery, Vector2.Zero, Color.White);
                    break;
                case 2:
                    _spriteBatch.Draw(_fullBattery, Vector2.Zero, Color.White);
                    break;
            }
        }

        // starts drawing the countdown in relation to the beat the clock timer in each level
        if (!_endCountDown)
        {
            switch (_countDown)
            {
                case < 10
                and > 9:
                    _spriteBatch.Draw(_countDownTen, Vector2.Zero, Color.White);
                    break;
                case < 9
                and > 8:
                    _spriteBatch.Draw(_countDownNine, Vector2.Zero, Color.White);
                    break;
                case < 8
                and > 7:
                    _spriteBatch.Draw(_countDownEight, Vector2.Zero, Color.White);
                    break;
                case < 7
                and > 6:
                    _spriteBatch.Draw(_countDownSeven, Vector2.Zero, Color.White);
                    break;
                case < 6
                and > 5:
                    _spriteBatch.Draw(_countDownSix, Vector2.Zero, Color.White);
                    break;
                case < 5
                and > 4:
                    _spriteBatch.Draw(_countDownFive, Vector2.Zero, Color.White);
                    break;
                case < 4
                and > 3:
                    _spriteBatch.Draw(_countDownFour, Vector2.Zero, Color.White);
                    break;
                case < 3
                and > 2:
                    _spriteBatch.Draw(_countDownThree, Vector2.Zero, Color.White);
                    break;
                case < 2
                and > 1:
                    _spriteBatch.Draw(_countDownTwo, Vector2.Zero, Color.White);
                    break;
                case < 1
                and > 0:
                    _spriteBatch.Draw(_countDownOne, Vector2.Zero, Color.White);
                    break;
                case 0:
                    _endCountDown = true;
                    break;
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
