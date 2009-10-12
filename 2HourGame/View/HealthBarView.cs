using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class HealthBarView : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        IDamageableGameObject damageableGameObject;
        Vector2 offset;
        ProgressBar progressBar;

        public HealthBarView(Game game, SpriteBatch spriteBatch, IDamageableGameObject damageableGameObject)
            :base(game)
        {
            this.damageableGameObject = damageableGameObject;
            this.spriteBatch = spriteBatch;
            offset = new Vector2(0, 35);

            progressBar = new ProgressBar();
            progressBar.FillColor = Color.Green;
            progressBar.EmptyColor = Color.Red;
        }

        protected override void LoadContent()
        {
            progressBar.LoadContent((ITextureManager)Game.Services.GetService(typeof(ITextureManager)));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            progressBar.Progress = (float)damageableGameObject.HealthPercentage;
            progressBar.Position = damageableGameObject.Position + offset;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (damageableGameObject.IsDamaged && damageableGameObject.IsAlive)
            {
                progressBar.Draw(spriteBatch);
            }
        }

    }
}
