using System.Collections.Generic;
using System.Linq;
using Xemio.Dots.Entities;
using Xemio.Dots.Entities.GameMenu;
using Xemio.Dots.Entities.UI;
using Xemio.Dots.Properties;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.Dots.Scenes
{
    public class GameMenu : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameMenu"/> class.
        /// </summary>
        public GameMenu()
        {
            this._random = new RandomProxy();

            this._backgroundEnvironment = new EntityEnvironment();
            this._uiEnvironment = new EntityEnvironment();
        }
        #endregion

        #region Fields
        private readonly IRandom _random;
        private readonly EntityEnvironment _backgroundEnvironment;
        private readonly EntityEnvironment _uiEnvironment;

        private float _pickedElapsed = 1000;
        private ITexture _logo;

        private SpriteFont _font;
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates the rays.
        /// </summary>
        private void CreateRays()
        {
            Vector2 center = this.GraphicsDevice.DisplayMode.Center;
            Vector2 spiraleCenter = new Vector2(center.X, 160);

            const int rayCount = 30;
            for (int i = 0; i < rayCount; i++)
            {
                this._backgroundEnvironment.Add(new RayEntity(i / (float)rayCount, spiraleCenter)
                {
                    RotationSpeed = this._random.Next(2, 5)
                });
            }
        }
        /// <summary>
        /// Loads the user interface.
        /// </summary>
        private void LoadUserInterface()
        {
            Vector2 position = new Vector2(this.GraphicsDevice.DisplayMode.Center.X - 129, 300);

            this._uiEnvironment.Add(new Button
            {
                Bounds = new Rectangle(position.X, position.Y, 260, 50),
                Font = this._font,
                Text = "Singleplayer"
            });

            this._uiEnvironment.Add(new Button
            {
                Bounds = new Rectangle(position.X, position.Y + 70, 260, 50),
                Font = this._font,
                Text = "Multiplayer"
            });

            this._uiEnvironment.Add(new Button
            {
                Bounds = new Rectangle(position.X, position.Y + 140, 260, 50),
                Font = this._font,
                Text = "Options"
            });
        }
        /// <summary>
        /// Picks a ray.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        private void TryPickRay(float elapsed)
        {
            this._pickedElapsed += elapsed;
            if (this._pickedElapsed >= 1000)
            {
                float entityCount = this._backgroundEnvironment.Count;
                RayEntity ray = this._backgroundEnvironment
                    .OfType<RayEntity>()
                    .FirstOrDefault(r => this._random.NextBoolean(1.0f / entityCount));

                if (ray != null)
                {
                    ray.Pick();
                }

                this._pickedElapsed = 0;
            }
        }
        /// <summary>
        /// Renders the dots logo.
        /// </summary>
        private void RenderLogo()
        {
            Vector2 center = this.GraphicsDevice.DisplayMode.Center;
            Vector2 size = new Vector2(this._logo.Width, this._logo.Height);

            this.RenderManager.Render(this._logo, new Vector2(center.X - size.X * 0.5f, 124));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this.CreateRays();
            
            this._font = new SpriteFont("Segoe UI", 12)
                             {
                                 Kerning = -5
                             };

            this._backgroundEnvironment.Add(new SpiraleEntity()
                                                {
                                                    Position = new Vector2(this.GraphicsDevice.DisplayMode.Center.X, 160)
                                                });

            this.LoadUserInterface();

            this._logo = this.Content.Load<ITexture>(Resources.DotsLogo.ToStream());
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);
            this.TryPickRay(elapsed);

            this._backgroundEnvironment.Tick(elapsed);
            this._uiEnvironment.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.Clear(Color.Black);

            this._backgroundEnvironment.Render();
            this._uiEnvironment.Render();

            this.RenderLogo();
            base.Render();
        }
        #endregion
    }
}