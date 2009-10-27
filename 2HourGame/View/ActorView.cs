using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using _2HourGame.Model;

namespace _2HourGame.View {
    class ActorView {

        protected Actor Actor { get; private set; }
        protected World World { get; private set; }

        public ActorView(Actor actor, World world) {
            this.Actor = actor;
            this.World = world;

            Actor.Spawned += new System.EventHandler(Actor_Spawned);
            Actor.Died += new System.EventHandler(Actor_Died);
        }

        void Actor_Spawned(object sender, System.EventArgs e) {
            World.ActorViews.Add(this);
        }

        void Actor_Died(object sender, System.EventArgs e) {
            World.GarbageActorViews.Add(this);
        }

        public virtual void LoadContent(ContentManager content) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
