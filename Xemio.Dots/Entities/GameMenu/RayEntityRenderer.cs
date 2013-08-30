using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.Dots.Entities.GameMenu
{
    public class RayEntityRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RayEntityRenderer"/> class.
        /// </summary>
        /// <param name="ray">The ray.</param>
        public RayEntityRenderer(RayEntity ray) : base(ray)
        {
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the ray.
        /// </summary>
        public RayEntity Ray
        {
            get { return this.Entity as RayEntity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the specified ray.
        /// </summary>
        public override void Render()
        {
            IBrush brush = this.GeometryFactory.CreateSolid(this.Ray.Color);
            this.GeometryManager.FillPolygon(brush, this.Ray.Vertices);
        }
        #endregion
    }
}
