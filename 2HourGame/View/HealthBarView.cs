using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class HealthBarView : ActorView
    {
        private readonly GameObject gameObject;
        private readonly Vector2 offset;
        private readonly ProgressBar progressBar;

        public HealthBarView(World world, GameObject gameObject, TextureManager textureManager, AnimationManager am)
            : base(gameObject, world, textureManager, am)
        {
            this.gameObject = gameObject;
            offset = new Vector2(0, 35);

            progressBar = new ProgressBar();
            progressBar.FillColor = Color.Green;
            progressBar.EmptyColor = Color.Red;
        }

        public override void LoadContent(ContentManager content)
        {
            progressBar.LoadContent(TextureManager);

            base.LoadContent(content);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (gameObject.IsDamaged && gameObject.IsAlive)
            {
                progressBar.Progress = gameObject.HealthPercentage;
                progressBar.Position = gameObject.Position + offset;
                progressBar.Draw(spriteBatch);
            }
        }
    }
}