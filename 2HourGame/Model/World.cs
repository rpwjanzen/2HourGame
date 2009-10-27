using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.View;

namespace _2HourGame.Model {
    class World {
        public List<Actor> Actors { get; private set; }
        public List<Actor> GarbageActors { get; private set; }

        public List<ActorView> ActorViews { get; private set; }
        public List<ActorView> GarbageActorViews { get; private set; }

        public World() {
            Actors = new List<Actor>();
            GarbageActors = new List<Actor>();

            ActorViews = new List<ActorView>();
            GarbageActorViews = new List<ActorView>();
        }

        public virtual void LoadContent(ContentManager contentManager) {
            foreach (var av in ActorViews) {
                av.LoadContent(contentManager);
            }
        }

        public virtual void Update(GameTime gameTime) {
            foreach (var a in Actors) {
                a.Update(gameTime);
            }

            foreach (var ga in GarbageActors) {
                Actors.Remove(ga);
            }
            GarbageActors.Clear();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (var av in ActorViews) {
                av.Draw(gameTime, spriteBatch);
            }

            foreach (var av in GarbageActorViews) {
                ActorViews.Remove(av);
            }
            GarbageActorViews.Clear();
        }
    }
}
