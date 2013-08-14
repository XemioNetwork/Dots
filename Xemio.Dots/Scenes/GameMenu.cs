using System.Drawing;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.Fonts;
using Xemio.GameLibrary.Rendering.Geometry;
using Color = Xemio.GameLibrary.Rendering.Color;
using Rectangle = Xemio.GameLibrary.Math.Rectangle;

namespace Xemio.Dots.Scenes
{
    public class GameMenu : Scene
    {
        #region Constants
        public const float CircleCount = 420;
        #endregion

        #region Fields
        private float _rotation;
        private float _fogRotation;
        
        private SpriteFont _font;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = SpriteFontGenerator.Create(new Font("Segoe UI", 48, FontStyle.Bold));
            this._font.Kerning = -16;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);

            this._rotation += elapsed / 1200.0f;
            this._fogRotation += elapsed / 6000.0f;
        }
        /// <summary>
        /// Renders the spirale.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="color">The color.</param>
        private void RenderSpirale(Vector2 position, float rotation, Vector2 scale, Color color)
        {
            float x = position.X;
            float y = position.Y;

            float scaleX = scale.X;
            float scaleY = scale.Y;
            
            const int size = 8;
            
            for (int i = 0; i < CircleCount; i++)
            {
                Color renderColor = Color.Lerp(color, Color.Transparent, i / (float)CircleCount);
                IBrush brush = this.Geometry.Factory.CreateSolid(renderColor);
                
                this.Geometry.FillEllipse(brush,
                                          new Rectangle(
                                              MathHelper.Sin(i * 120f - rotation) * 2 * scaleX * (i / 12f) + x,
                                              MathHelper.Cos(i * 120f - rotation) * 2 * scaleY * (i / 12f) + y,
                                              size * scaleX,
                                              size * scaleY));
            }
        }
        /// <summary>
        /// Renders the dots logo.
        /// </summary>
        private void RenderLogo()
        {
            Vector2 center = this.GraphicsDevice.DisplayMode.Center;

            this.RenderSpirale(new Vector2(center.X - 85, 94), this._fogRotation, new Vector2(20), new Color(0, 40, 120, 6));
            this.RenderSpirale(new Vector2(center.X, 184), this._rotation, new Vector2(2), Color.DodgerBlue);

            const string header = "DOTS";
            Vector2 size = this._font.MeasureString(header);

            this.RenderManager.Render(this._font, "DOTS", new Vector2(center.X - size.X * 0.5f, 146), Color.Black);
            this.RenderManager.Render(this._font, "DOTS", new Vector2(center.X - size.X * 0.5f, 144));
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        public override void Render()
        {
            this.RenderLogo();
            base.Render();
        }
        #endregion
    }
}