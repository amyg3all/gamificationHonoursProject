using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace gamificationHonoursProject.Levels;

public abstract class Level
{
    private SpriteFont _pixelFont;
    protected Game1 game;

    protected Level(Game1 game)
    {
        this.game = game;
    }

    public abstract void LoadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

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
        game.SpriteBatch.DrawString(
            _pixelFont,
            writeThis,
            new Vector2(xPosition, yPosition),
            colour
        );
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

    public void WriteFontCentreSmaller(string writeThis, int adjustX, int adjustY, Color colour)
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;
        var stringWidth = _pixelFont.MeasureString(writeThis);
        var stringHeight = _pixelFont.LineSpacing; // Get the height of the text
        var scale = 0.5f; // Adjust as needed
        var centrePosition = new Vector2(
            (screenWidth - stringWidth.X * scale) / 2 + adjustX, // Center horizontally with scaling
            (screenHeight - stringHeight * scale) / 2 + adjustY // Center vertically with scaling
        );

        game.SpriteBatch.DrawString(
            _pixelFont,
            writeThis,
            centrePosition,
            colour,
            0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0f
        );
    }

    // the text can only be max 420 char long equal to six sentences of 70 chars
    public void WriteTextBox(SpriteBatch spriteBatch, String inputText, Texture2D textBox)
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;
        var boxWidth = (int)(screenWidth * 0.9);
        var boxHeight = screenHeight;

        var posX = (screenWidth - boxWidth) / 2 - 40;
        var posY = screenHeight - boxHeight + 20;

        var words = inputText.Split(' ').ToList();
        var result = new List<string>();
        var currentString = "";

        foreach (var word in words)
        {
            // Check if adding the current word exceeds the max length
            if ((currentString.Length + word.Length + 1) > 69)
            {
                // Add the current string to the result and start a new string
                result.Add(currentString.Trim());
                currentString = "";
            }

            // Add the word to the current string
            currentString += word + " ";
        }

        if (!string.IsNullOrWhiteSpace(currentString))
        {
            result.Add(currentString.Trim());
        }

        var allSentences = result.Take(6).ToList();

        // Draw the texture at the bottom center
        spriteBatch.Draw(textBox, new Rectangle(posX, posY, screenWidth, boxHeight), Color.White);

        switch (allSentences.Count)
        {
            case 1:
                WriteFontCentreSmaller(allSentences[0], -75, 160, Color.Black);
                break;
            case 2:
                var initialPos = 150;
                foreach (var sentence in allSentences)
                {
                    WriteFontCentreSmaller(sentence, -75, initialPos, Color.Black);
                    initialPos += 20;
                }

                break;
            case 3:
                var initialPos1 = 140;
                foreach (var sentence in allSentences)
                {
                    WriteFontCentreSmaller(sentence, -75, initialPos1, Color.Black);
                    initialPos1 += 20;
                }

                break;
            default:
                var initialPos2 = 110;
                foreach (var sentence in allSentences)
                {
                    WriteFontCentreSmaller(sentence, -75, initialPos2, Color.Black);
                    initialPos2 += 20;
                }

                break;
        }
    }
}
