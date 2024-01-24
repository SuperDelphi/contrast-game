using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GamePrototype.Sprites
{
    public class ShooterEntity : Sprite
    {
        public int radius;
        public Bullet Bullet;

        public float shootRate = 1f;
        protected float _lastShoot;

        public float bulletVelocity = 20f;
        public Color bulletRemColor = Color.White;

        public ShooterEntity(Texture2D texture) : base(texture)
        {

        }

        protected virtual void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position + 110 * this.Direction;
            bullet.LinearVelocity = 20f;
            bullet.LifeSpan = 2.5f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
