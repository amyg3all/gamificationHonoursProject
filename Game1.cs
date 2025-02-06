using gamificationHonoursProject.GameStates;
using gamificationHonoursProject.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject
{
    /// <summary>
    /// handles all main game logic and implementation
    /// </summary>
    public class Game1 : Game
    {
        private State _currentState;

        public readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteBatch SpriteBatch { get; private set; }
        private Texture2D _waterMolecule;
        private Vector2 _waterMoleculePosition;
        private float _waterMoleculeSpeed;
        private SoundEffect _backgroundMusic;
        private SoundEffectInstance _backgroundMusicInstance;
        private SoundEffect _fightMusic;
        private SoundEffectInstance _fightMusicInstance;

        private bool displayHealthKnowledge = false;
        private int _currentHealth = 4;
        private int _currentKnowledge = 0;
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

        private int _selectedLevel = 1;

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

        public int getCurrentHealth()
        {
            return _currentHealth;
        }
        
        public void decreaseHealth()
        {
            if (_currentHealth != 1)
            {
                _currentHealth--;
            }
            else
            {
                ChangeState(new GameOverState(this));
            }
        }

        public void increaseKnowledge()
        {
            _currentKnowledge++;
        }

        public void toggleHealthKnowledge(bool trueOrFalse)
        {
            if (trueOrFalse == true)
            {
                displayHealthKnowledge = true;
            }
            else
            {
                displayHealthKnowledge = false;
            }
        }

        public void ChangeState(State newState)
        {
            _currentState = newState;
            _currentState.LoadContent();
        }

        protected override void Initialize()
        {
            ChangeState(new TitleScreenState(this));

            _waterMoleculePosition = new Vector2(
                _graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2
            );
            _waterMoleculeSpeed = 100f;

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

            _waterMolecule = Content.Load<Texture2D>("Sprites/water");
        }

        public void PlayBackgroundMusic()
        {
            if (
                _backgroundMusicInstance != null
                && _backgroundMusicInstance.State != SoundState.Playing
            )
            {
                _backgroundMusicInstance.Play();
            }
        }

        public void startFightMusic()
        {
            if (
                _backgroundMusicInstance != null
                && _backgroundMusicInstance.State == SoundState.Playing
            )
            {
                _backgroundMusicInstance.Stop();
            }
            if (_fightMusicInstance != null && _fightMusicInstance.State != SoundState.Playing)
            {
                _fightMusicInstance.Play();
            }
        }

        public void stopFightMusic()
        {
            if (_fightMusicInstance != null && _fightMusicInstance.State == SoundState.Playing)
            {
                _fightMusicInstance.Stop();
            }
            if (
                _backgroundMusicInstance != null
                && _backgroundMusicInstance.State != SoundState.Playing
            )
            {
                _backgroundMusicInstance.Play();
            }
        }

        public void StopBackgroundMusic()
        {
            if (
                _backgroundMusicInstance != null
                && _backgroundMusicInstance.State == SoundState.Playing
            )
            {
                _backgroundMusicInstance.Stop();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
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
            if (displayHealthKnowledge)
            {
                switch (_currentKnowledge)
                {
                    case 0: _spriteBatch.Draw(_emptyKnowledge, Vector2.Zero, Color.White);
                        break;
                    case 1: _spriteBatch.Draw(_oneKnowledge, Vector2.Zero, Color.White);
                        break;
                    case 2: _spriteBatch.Draw(_twoKnowledge, Vector2.Zero, Color.White);
                        break;
                    case 3: _spriteBatch.Draw(_threeKnowledge, Vector2.Zero, Color.White);
                        break;
                    case 4: _spriteBatch.Draw(_fullKnowledge, Vector2.Zero, Color.White);
                        break;
                }
                switch(_currentHealth)
                {
                    case 0: _spriteBatch.Draw(_emptyHealth, Vector2.Zero, Color.White);
                        break;
                    case 1: _spriteBatch.Draw(_oneHealth, Vector2.Zero, Color.White);
                        break;
                    case 2: _spriteBatch.Draw(_twoHealth, Vector2.Zero, Color.White);
                        break;
                    case 3: _spriteBatch.Draw(_threeHealth, Vector2.Zero, Color.White);
                        break;
                    case 4: _spriteBatch.Draw(_fullHealth, Vector2.Zero, Color.White);
                        break;
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
