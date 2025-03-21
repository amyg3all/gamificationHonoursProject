using gamificationHonoursProject.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace gamificationHonoursProject.GameStates;

/// <summary>
/// Game Play Screen inherited state handles all logic for loading and updating the Title Screen
/// </summary>
public class GamePlayState : State
{
    private Level currentLevel;

    public GamePlayState(Game1 game, int levelNumber)
        : base(game)
    {
        switch (levelNumber)
        {
            case 0:
                currentLevel = new LevelZero(game);
                break;
            case 1:
                currentLevel = new LevelOne(game);
                break;
            case 2:
                currentLevel = new LevelTwo(game);
                break;
            case 3:
                currentLevel = new LevelThree(game);
                break;
            case 4:
                currentLevel = new LevelFour(game);
                break;
            default:
                currentLevel = new LevelOne(game); // Default to LevelOne
                break;
        }
    }

    public override void LoadContent()
    {
        currentLevel.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        currentLevel.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        game.GraphicsDevice.Clear(Color.Black);
        currentLevel.Draw(game.SpriteBatch);
    }
}