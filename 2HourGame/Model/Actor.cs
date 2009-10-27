using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.Model {
    class Actor {
        public virtual void Draw(GameTime gameTime) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void LoadContent(ContentManager content) { }
    }
}
