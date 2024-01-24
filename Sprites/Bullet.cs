using GamePrototype.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePrototype.Sprites
{
    public class Bullet : Sprite
    {
        private float _timer;

        private int _maxRemanence = 30;
        private int _remCount;

        public bool fromPlayer;
        public Color remColor;

        public int radius = 5;

        public Bullet(Texture2D texture, bool fromPlayer, Color remColor) : base(texture)
        {
            this.fromPlayer = fromPlayer;
            this.remColor = remColor;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            Position += Direction * LinearVelocity;

            CheckCollision(sprites);
        }

        private void CheckCollision(List<Sprite> sprites)
        {
            if (!fromPlayer)
            {
                if (Vector2.Distance(Game1.player.Position, Position) <= radius + (Game1.isWhite ? Game1.player.smallRadius : Game1.player.radius))
                {
                    Game1.player.Damage();
                    IsRemoved = true;
                }
            }
            else
            {
                foreach (Sprite sprite in sprites.ToArray())
                {
                    if (sprite is Enemy && Vector2.Distance(sprite.Position, Position) <= ((Enemy)sprite).radius + radius)
                    {
                        sprite.IsRemoved = true;
                        IsRemoved = true;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Remanence

            Vector2 norDir = new Vector2(Direction.X, Direction.Y);
            norDir.Normalize();

            for (int i = 0; i < _remCount; i++)
            {
                spriteBatch.Draw(_texture, Position - (norDir * LinearVelocity * i) / 5, null, remColor * (1 - (i / (float)_remCount)) * 0.5f, Rotation, Origin, 1, SpriteEffects.None, 0);
            }

            if (_remCount < _maxRemanence)
            {
                _remCount++;
            }

            spriteBatch.Draw(_texture, Position, null, Game1.isWhite ? Color.Black : Color.White, Rotation, Origin, 1, SpriteEffects.None, 0);
        }
    }
}
