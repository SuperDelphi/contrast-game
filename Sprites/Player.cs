using GamePrototype.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GamePrototype.Sprites
{
    public class Player : ShooterEntity
    {
        public bool hasDied = false;

        private float _currentVelocityX, _currentVelocityY;
        private float _velStep = 0.5f;

        public Bullet BigBullet;
        private float _bigShootRate = 1f;
        public int smallRadius = 30;

        public static int maxHealth = 5;
        public int health;

        public HealthBar healthBar;

        public Player(Texture2D texture, HealthBar healthBar) : base(texture)
        {
            health = maxHealth;
            this.healthBar = healthBar;

            shootRate = 0.25f;
            bulletRemColor = new Color(59, 133, 255);
            radius = 50;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Keyboard handling

            _currentKey = Keyboard.GetState();

            var xActive = false;
            var yActive = false;

            if (_currentKey.IsKeyDown(Keys.Z))
            {
                yActive = true;
                _currentVelocityY = MathHelper.Clamp(_currentVelocityY - _velStep, -LinearVelocity, LinearVelocity);
                Position.Y = Position.Y > 0 ? Position.Y + _currentVelocityY : Game1.ScreenHeight;
            }
            if (_currentKey.IsKeyDown(Keys.S))
            {
                yActive = true;
                _currentVelocityY = MathHelper.Clamp(_currentVelocityY + _velStep, -LinearVelocity, LinearVelocity);
                Position.Y = Position.Y < Game1.ScreenHeight ? Position.Y + _currentVelocityY : 0;
            }
            if (_currentKey.IsKeyDown(Keys.Q))
            {
                xActive = true;
                _currentVelocityX = MathHelper.Clamp(_currentVelocityX - _velStep, -LinearVelocity, LinearVelocity);
                Position.X += _currentVelocityX;
            }
            if (_currentKey.IsKeyDown(Keys.D))
            {
                xActive = true;
                _currentVelocityX = MathHelper.Clamp(_currentVelocityX + _velStep, -LinearVelocity, LinearVelocity);
                Position.X += _currentVelocityX;
            }

            if (!xActive) _currentVelocityX = 0;
            if (!yActive) _currentVelocityY = 0;

            Position.X = MathHelper.Clamp(Position.X, _texture.Width / 2, Game1.ScreenWidth - _texture.Width / 2);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            // Mouse handling

            MouseState mState = Mouse.GetState();

            Vector2 dir = mState.Position.ToVector2() - Position;
            dir.Normalize();

            Rotation = (float)Math.Atan2(dir.Y, dir.X);

            _lastShoot += gameTime.GetElapsedSeconds();

            if (mState.LeftButton == ButtonState.Pressed)
            {
                if (_lastShoot >= (Game1.isWhite ? _bigShootRate : shootRate))
                {
                    _lastShoot = 0;
                    AddBullet(sprites);
                }
            }
        }
        protected override void AddBullet(List<Sprite> sprites)
        {
            var bullet = GetBullet().Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position + 110 * this.Direction;
            bullet.LinearVelocity = 20f;
            bullet.LifeSpan = 2.5f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }

        private Bullet GetBullet()
        {
            return Game1.isWhite ? BigBullet : Bullet;
        }

        public void Damage()
        {
            Console.WriteLine(health);
            if (health > 0)
            {
                health--;
            }

            if (health == 0)
            {
                IsRemoved = true;
            }

            healthBar.SetHealth(health);
        }
    }
}
