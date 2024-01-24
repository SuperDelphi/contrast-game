using GamePrototype.Sprites;
using GamePrototype.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;
using System;
using System.Collections.Generic;

namespace GamePrototype
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont gameFont;

        private List<Sprite> _sprites;

        private Texture2D enemyTexture;
        private Texture2D playerTexture;
        private Texture2D bulletTexture;
        private Texture2D bigBulletTexture;
        private Texture2D startingScreen;
        private Texture2D noticeTexture;
        private Sprite notice;
        public static Player player;
        private readonly Tweener _tweener = new Tweener();

        public HealthBar healthBar;

        public static Random Random;

        public static int ScreenWidth = 1500;
        public static int ScreenHeight = 900;

        public static bool isWhite = false;
        public static bool hasStarted = false;

        private bool _rightClickState = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Restart();
        }

        private void Restart()
        {
            //gameFont = Content.Load<SpriteFont>("nerdFont");
            playerTexture = Content.Load<Texture2D>("player");
            enemyTexture = Content.Load<Texture2D>("enemy");
            bulletTexture = Content.Load<Texture2D>("bullet");
            startingScreen = Content.Load<Texture2D>("starting_screen");
            noticeTexture = Content.Load<Texture2D>("notice");
            bigBulletTexture = Content.Load<Texture2D>("big_bullet");

            notice = new Sprite(noticeTexture)
            {
                Position = new Vector2(502, 930),
                Origin = Vector2.Zero
            };

            _tweener.TweenTo(
                target: notice,
                expression: notice => notice.Position,
                toValue: new Vector2(502, 730),
                duration: 1,
                delay: 1
                );

            healthBar = new HealthBar(new Rectangle(40, 40, 500, 25), Content.Load<Texture2D>("health"), Player.maxHealth, Player.maxHealth);

            player = new Player(playerTexture, healthBar);
            player.Position = new Vector2(100, ScreenHeight / 2);
            player.Bullet = new Bullet(bulletTexture, true, player.bulletRemColor);
            player.BigBullet = new Bullet(bigBulletTexture, true, player.bulletRemColor)
            {
                radius = 12
            };

            _sprites = new List<Sprite>()
            {
                player,
                new Enemy(enemyTexture)
                {
                    Position = new Vector2(ScreenWidth - 100, ScreenHeight / 4),
                    Bullet = new Bullet(bulletTexture, false, Color.Red)
                },
                new Enemy(enemyTexture)
                {
                    Position = new Vector2(ScreenWidth - 100, ScreenHeight / 4 * 3),
                    Bullet = new Bullet(bulletTexture, false, Color.Red)
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            _tweener.Update(gameTime.GetElapsedSeconds());

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) hasStarted = true;

            if (!hasStarted) return;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Switch
            
            MouseState mState = Mouse.GetState();

            if (mState.RightButton == ButtonState.Pressed && !_rightClickState)
            {
                isWhite = !isWhite;
                _rightClickState = true;
            } else if (mState.RightButton == ButtonState.Released)
            {
                _rightClickState = false;
            }

            // Sprite update

            foreach (var sprite in _sprites.ToArray())
            {
                sprite.Update(gameTime, _sprites);
            }

            PostUpdate();

            if (player.IsRemoved)
            {
                Restart();
            }

            base.Update(gameTime);
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);


            if (!hasStarted)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Draw(startingScreen, Vector2.Zero, Color.White);
                notice.Draw(_spriteBatch);
            } else
            {
                GraphicsDevice.Clear(isWhite ? Color.White : Color.Black);

                foreach (var sprite in _sprites)
                {
                    sprite.Draw(_spriteBatch);
                }

                healthBar.Draw(_spriteBatch, _graphics);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void GameOver()
        {
            Console.WriteLine("Game Over!");
        }
    }
}
