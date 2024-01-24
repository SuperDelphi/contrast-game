using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.UI
{
    public class HealthBar
    {
        private Rectangle rectangle;
        private int _maxHealth;
        public int currentHealth;

        private Texture2D _texture;

        public HealthBar(Rectangle rectangle, Texture2D texture, int maxHealth, int currentHealth)
        {
            this.rectangle = rectangle;
            this._texture = texture;
            this._maxHealth = maxHealth
            this.currentHealth = currentHealth;
        }

        public void SetHealth(int health)
        {
            currentHealth = health;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(
                _texture,
                new Rectangle(rectangle.X, rectangle.Y, (int)(rectangle.Width * ((float)currentHealth / _maxHealth)), rectangle.Height),
                new Rectangle(0, 0, _texture.Width, _texture.Height),
                Color.White
                );
        }
    }
}
