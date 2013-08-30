using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.Dots.Entities.GameMenu
{
    public class SpiraleEntityRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpiraleEntityRenderer"/> class.
        /// </summary>
        /// <param name="spirale">The spirale.</param>
        public SpiraleEntityRenderer(SpiraleEntity spirale) : base(spirale)
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the spirale.
        /// </summary>
        public SpiraleEntity Spirale
        {
            get { return this.Entity as SpiraleEntity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the spirale.
        /// </summary>
        public override void Render()
        {
            float x = this.Spirale.Position.X;
            float y = this.Spirale.Position.Y;

            float scaleX = this.Spirale.Scale.X;
            float scaleY = this.Spirale.Scale.Y;

            const int size = 8;
            
            for (int i = 0; i < this.Spirale.CircleCount; i++)
            {
                Color renderColor = Color.Lerp(this.Spirale.Color, Color.Transparent, i / (float)this.Spirale.CircleCount);
                IBrush brush = this.GeometryFactory.CreateSolid(renderColor);

                this.GeometryManager.FillEllipse(brush,
                                          new Rectangle(
                                              MathHelper.Sin(i * 120f - this.Spirale.Rotation) * 2 * scaleX * (i / 12f) + x,
                                              MathHelper.Cos(i * 120f - this.Spirale.Rotation) * 2 * scaleY * (i / 12f) + y,
                                              size * scaleX,
                                              size * scaleY));
            }
        }
        #endregion
    }
}
