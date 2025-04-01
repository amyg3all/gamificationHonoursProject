using System;
using System.IO;
using System.Runtime.InteropServices;
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
    private string _contentPath;

    protected State(Game1 game)
    {
        this.game = game;
    }

    public abstract void LoadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime);

    /// <summary>
    /// bakes the font for all states so that redundant code isn't created
    /// </summary>
    public void BakeFont()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory; 
        _contentPath = Path.Combine(basePath, "..","..", "..", "Content");
        var fontPath = Path.Combine(_contentPath, "Fonts", "pixelFont.ttf");
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

    /// <summary>
    /// Positions and renders the font from the x and y points
    /// </summary>
    /// <param name="writeThis"></param> string to be written
    /// <param name="xPosition"></param>
    /// <param name="yPosition"></param>
    /// <param name="colour"></param>
    public void WriteFont(string writeThis, int xPosition, int yPosition, Color colour)
    {
        game.SpriteBatch.DrawString(
            _pixelFont,
            writeThis,
            new Vector2(xPosition, yPosition),
            colour
        );
    }

    /// <summary>
    /// Writes the font default to the centre of the screen but is adjusted by the x and y poisitions
    /// </summary>
    /// <param name="writeThis"></param>
    /// <param name="adjustX"></param>
    /// <param name="adjustY"></param>
    /// <param name="colour"></param>
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
