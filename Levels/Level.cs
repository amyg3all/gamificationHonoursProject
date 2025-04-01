using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace gamificationHonoursProject.Levels;

/// <summary>
/// abstract state machine to handle all levels,
/// each state is intended to be independent of one another for code clarity
/// </summary>
public abstract class Level
{
    private SpriteFont _pixelFont;
    protected Game1 game;
    public int _currentScreenSequence;
    private string _contentPath;

    protected Level(Game1 game)
    {
        this.game = game;
    }

    public abstract void LoadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

    /// <summary>
    /// bakes the font for all the subsequent level classes
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

    // adjusts the font size
    public void WriteFontCentreSmaller(string writeThis, int adjustX, int adjustY, Color colour)
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;
        var stringWidth = _pixelFont.MeasureString(writeThis);
        var stringHeight = _pixelFont.LineSpacing;
        var scale = 0.5f;
        var centrePosition = new Vector2(
            (screenWidth - stringWidth.X * scale) / 2 + adjustX,
            (screenHeight - stringHeight * scale) / 2 + adjustY
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

    // writes a textbox formats so that the text can only be max 420 char long equal to six sentences of 70 chars
    public void WriteTextBox(SpriteBatch spriteBatch, string inputText, Texture2D textBox)
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
            // Check to see if adding the current word exceeds the max length
            if (currentString.Length + word.Length + 1 > 69)
            {
                // Add the current string to the result and start a new string
                result.Add(currentString.Trim());
                currentString = "";
            }

            currentString += word + " ";
        }

        if (!string.IsNullOrWhiteSpace(currentString))
            result.Add(currentString.Trim());

        var allSentences = result.Take(6).ToList();

        spriteBatch.Draw(textBox, new Rectangle(posX, posY, screenWidth, boxHeight), Color.White);

        // renders the resulting sentences and adjusts them accordingly so that they're in the centre of the textbox
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

    // writes the textbox for a fight scene so that each answer option is on a new line
    public void WriteTextBox(
        SpriteBatch spriteBatch,
        string question,
        string optionA,
        string optionB,
        string optionC,
        Texture2D textBox
    )
    {
        var screenWidth = game.GraphicsDevice.Viewport.Width;
        var screenHeight = game.GraphicsDevice.Viewport.Height;
        var boxWidth = (int)(screenWidth * 0.9);
        var boxHeight = screenHeight;

        var posX = (screenWidth - boxWidth) / 2 - 40;
        var posY = screenHeight - boxHeight + 20;

        var words = question.Split(' ').ToList();
        var result = new List<string>();
        var currentString = "";

        foreach (var word in words)
        {
            if (currentString.Length + word.Length + 1 > 69)
            {
                result.Add(currentString.Trim());
                currentString = "";
            }
            currentString += word + " ";
        }

        if (!string.IsNullOrWhiteSpace(currentString))
            result.Add(currentString.Trim());

        var allSentences = result.Take(6).ToList();

        spriteBatch.Draw(textBox, new Rectangle(posX, posY, screenWidth, boxHeight), Color.White);

        // Print the question (centered)
        var initialPos = 120;
        foreach (var sentence in allSentences)
        {
            WriteFontCentreSmaller(sentence, -75, initialPos, Color.Black);
            initialPos += 20;
        }

        // Print the answer options on separate lines
        WriteFontCentreSmaller(optionA, -75, initialPos + 0, Color.Black);
        WriteFontCentreSmaller(optionB, -75, initialPos + 20, Color.Black);
        WriteFontCentreSmaller(optionC, -75, initialPos + 40, Color.Black);
    }
    
    // updates the scene sequence
    public void UpdateCurrentScreenSequence(int newScreenSequence)
    {
        if (newScreenSequence == _currentScreenSequence + 1)
            _currentScreenSequence = newScreenSequence;
    }
}
