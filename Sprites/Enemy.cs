using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace GamePrototype.Sprites
{
    public class Enemy : ShooterEntity
    {
        public Enemy(Texture2D texture) : base(texture)
        {
            LinearVelocity = 1f;
            radius = 30;
            Rotation = (float)Math.PI;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            _lastShoot += gameTime.GetElapsedSeconds();

            if (_lastShoot >= shootRate)
            {
                _lastShoot = 0;
                AddBullet(sprites);
            }

            UpdateFollow();
        }

        private void UpdateFollow()
        {
            Vector2 newDir = Game1.player.Position - Position;
            newDir.Normalize();

            Rotation = (float)Math.Atan2(newDir.Y, newDir.X);

            Position += newDir * LinearVelocity;
        }

        protected override void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position + 110 * this.Direction;
            bullet.LinearVelocity = 10f;
            bullet.LifeSpan = 5f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
