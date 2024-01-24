using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tweening;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GamePrototype.Sprites
{
    public class Sprite : ICloneable
    {
        protected readonly Tweener _tweener = new Tweener();
        protected Texture2D _texture;
        public float Rotation;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Position;
        public Vector2 Origin;

        public Vector2 Direction;

        public float RotationVelocity = 3f;
        public float LinearVelocity = 8f;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public void Update()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
