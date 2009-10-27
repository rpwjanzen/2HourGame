using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.View;
using System.Collections.ObjectModel;

namespace _2HourGame.Model {
    class World {
        public List<Actor> NewActors { get; private set; }
        private List<Actor> actors;
        public ReadOnlyCollection<Actor> Actors {
            get { return new ReadOnlyCollection<Actor>(actors); }
        }
        public List<Actor> GarbageActors { get; private set; }

        public List<ActorView> NewActorViews { get; private set; }
        public List<ActorView> actorViews;
        public ReadOnlyCollection<ActorView> ActorViews {
            get { return new ReadOnlyCollection<ActorView>(actorViews); }
        }
        public List<ActorView> GarbageActorViews { get; private set; }

        ContentManager content;

        public World() {
            NewActors = new List<Actor>();
            actors = new List<Actor>();
            GarbageActors = new List<Actor>();

            NewActorViews = new List<ActorView>();
            actorViews = new List<ActorView>();
            GarbageActorViews = new List<ActorView>();
        }

        public virtual void LoadContent(ContentManager contentManager) {
            this.content = contentManager;
            foreach (var av in ActorViews) {
                av.LoadContent(contentManager);
            }
        }

        public virtual void Update(GameTime gameTime) {
            foreach (var a in Actors) {
                var t = a as Tower;
                if (t != null) {
                    a.Update(gameTime);
                }
            }

            foreach (var na in NewActors) {
                actors.Add(na);
            }
            NewActors.Clear();

            foreach (var ga in GarbageActors) {
                actors.Remove(ga);
            }
            GarbageActors.Clear();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (var av in ActorViews) {
                av.Draw(gameTime, spriteBatch);
            }

            foreach (var nav in NewActorViews) {
                nav.LoadContent(content);
                actorViews.Add(nav);
            }
            NewActorViews.Clear();

            foreach (var av in GarbageActorViews) {
                actorViews.Remove(av);
            }
            GarbageActorViews.Clear();
        }
    }
}
