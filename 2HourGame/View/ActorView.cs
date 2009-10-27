using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View {
    class ActorView {

        protected Actor Actor { get; private set; }
        protected World World { get; private set; }
        
        protected TextureManager TextureManager { get; private set; }
        protected AnimationManager AnimationManager { get; private set; }

        public ActorView(Actor actor, World world, TextureManager textureManager, AnimationManager animationManager) {
            this.Actor = actor;
            this.World = world;
            this.TextureManager = textureManager;
            this.AnimationManager = animationManager;

            Actor.Spawned += new System.EventHandler(Actor_Spawned);
            Actor.Died += new System.EventHandler(Actor_Died);
        }

        void Actor_Spawned(object sender, System.EventArgs e) {
            World.NewActorViews.Add(this);
        }

        void Actor_Died(object sender, System.EventArgs e) {
            World.GarbageActorViews.Add(this);
        }

        public virtual void LoadContent(ContentManager content) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
