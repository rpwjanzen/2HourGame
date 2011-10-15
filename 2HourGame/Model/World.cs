using System.Collections.Generic;
using System.Collections.ObjectModel;
using _2HourGame.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Model
{
    internal class World
    {
        private readonly List<Actor> actors;
        public List<ActorView> actorViews;
        private ContentManager content;

        public World()
        {
            NewActors = new List<Actor>();
            actors = new List<Actor>();
            GarbageActors = new List<Actor>();

            NewActorViews = new List<ActorView>();
            actorViews = new List<ActorView>();
            GarbageActorViews = new List<ActorView>();
        }

        public List<Actor> NewActors { get; private set; }

        public ReadOnlyCollection<Actor> Actors
        {
            get { return new ReadOnlyCollection<Actor>(actors); }
        }

        public List<Actor> GarbageActors { get; private set; }

        public List<ActorView> NewActorViews { get; private set; }

        public ReadOnlyCollection<ActorView> ActorViews
        {
            get { return new ReadOnlyCollection<ActorView>(actorViews); }
        }

        public List<ActorView> GarbageActorViews { get; private set; }

        public virtual void LoadContent(ContentManager contentManager)
        {
            content = contentManager;
            foreach (ActorView av in ActorViews)
            {
                av.LoadContent(contentManager);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Actor a in actors)
            {
                a.Update(gameTime);
            }

            foreach (Actor na in NewActors)
            {
                actors.Add(na);
            }
            NewActors.Clear();

            foreach (Actor ga in GarbageActors)
            {
                actors.Remove(ga);
            }
            GarbageActors.Clear();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (ActorView av in actorViews)
            {
                av.Draw(gameTime, spriteBatch);
            }

            foreach (ActorView nav in NewActorViews)
            {
                nav.LoadContent(content);
                actorViews.Add(nav);
            }
            NewActorViews.Clear();

            foreach (ActorView av in GarbageActorViews)
            {
                actorViews.Remove(av);
            }
            GarbageActorViews.Clear();
        }
    }
}