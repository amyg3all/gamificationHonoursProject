using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace gamificationHonoursProject.GameStates;

/// <summary>
/// abstract state machine to handle all game states,
/// each state is intended to be independent of one another for code clarity
/// </summary>
public abstract class State
{
    protected Game1 game;
    private SpriteFont _pixelFont;

    protected State(Game1 game)
    {
        this.game = game;
    }

    public abstract void LoadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime);

    public void BakeFont()
    {
        var fontPath =
            "/Users/amygeall/Documents/diss/gamificationHonoursProject/Content/Fonts/pixelFont.ttf";
        var fontBytes = File.ReadAllBytes(fontPath);
        var bakedFont = TtfFontBaker.Bake(
            fontBytes,
            25,
            1024,
            1024,
            new[] { CharacterRange.BasicLatin, CharacterRange.Latin1Supplement }
        );
        _pixelFont = bakedFont.CreateSpriteFont(game.GraphicsDevice);
    }

    public void WriteFont(string writeThis, int xPosition, int yPosition, Color colour)
    {
        game.SpriteBatch.DrawString(_pixelFont, writeThis, new Vector2(xPosition, yPosition), colour);
    }

    public void WriteFont(string writeThis, Vector2 position, Color colour)
    {
        game.SpriteBatch.DrawString(_pixelFont, writeThis, position, colour);
    }

    public void WriteFontCentre(string writeThis, int adjustX, int adjustY, Color colour)
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;
        var stringWidth = _pixelFont.MeasureString(writeThis);
        var centrePosition = new Vector2(
            (screenWidth - stringWidth.X) / 2 + adjustX,
            (screenHeight - stringWidth.Y) / 2 + adjustY
        );
        game.SpriteBatch.DrawString(_pixelFont, writeThis, centrePosition, colour);
    }
}