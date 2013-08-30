using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Dots.Entities.GameMenu
{
    public class SpiraleEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpiraleEntity"/> class.
        /// </summary>
        public SpiraleEntity()
        {
            this.Scale = new Vector2(2);
            this.CircleCount = 520;
            this.Color = Color.DodgerBlue;

            this.Renderer = new SpiraleEntityRenderer(this);
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public Vector2 Scale { get; set; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Gets or sets the circle count.
        /// </summary>
        public int CircleCount { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Rotation += elapsed / 1800.0f;
        }
        #endregion
    }
}
