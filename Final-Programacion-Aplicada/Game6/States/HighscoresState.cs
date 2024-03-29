﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game6.Controls;
using Game6.Managers;
using Game6.Sprites;

namespace Game6.States
{
    public class HighscoresState : State
    {
        private List<Component> _components;

        private SpriteFont _font;

        private ScoreManager _scoreManager;

        public HighscoresState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");

            _scoreManager = ScoreManager.Load();

            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");
  

            _components = new List<Component>()
      {
        new Sprite(_content.Load<Texture2D>("Background/MenuFinal"))
        {
          Layer = 0f,
          Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2),
        },

        new Button(buttonTexture, buttonFont)
        {
          Text = "Menu Principal",
          Position = new Vector2(Game1.ScreenWidth / 2, 560),
          Click = new EventHandler(Button_MainMenu_Clicked),
          Layer = 0.1f
        },
      };
        }

        private void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Clicked(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.DrawString(_font, "Puntajes Altos:\n" + string.Join("\n", _scoreManager.HighScores.Select(c => c.PlayerName + ": " + c.Value)), new Vector2(400, 100), Color.Black);

            spriteBatch.End();
        }
    }
}
