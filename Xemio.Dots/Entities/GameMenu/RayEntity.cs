using System;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Dots.Entities.GameMenu
{
    public class RayEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RayEntity"/> class.
        /// </summary>
        /// <param name="rayPercentage">The ray percentage.</param>
        /// <param name="center">The center.</param>
        public RayEntity(float rayPercentage, Vector2 center)
        {
            this._percentage = rayPercentage;
            this._center = center;

            this.FadeDuration = 2000;
            this.FadeDirection = FadeDirection.FadeIn;

            this.Renderer = new RayEntityRenderer(this);
        }
        #endregion

        #region Fields
        private readonly float _percentage;
        private readonly Vector2 _center;

        private float _elapsed;
        private float _rotation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this instance is picked.
        /// </summary>
        public bool IsPicked { get; private set; }
        /// <summary>
        /// Gets or sets the fade direction.
        /// </summary>
        public FadeDirection FadeDirection { get; set; }
        /// <summary>
        /// Gets or sets the duration of the fade.
        /// </summary>
        public float FadeDuration { get; set; }
        /// <summary>
        /// Gets or sets the rotation speed.
        /// </summary>
        public float RotationSpeed { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                if (!this.IsPicked)
                    return Color.Transparent;

                switch (this.FadeDirection)
                {
                    case FadeDirection.FadeIn:
                        return Color.Lerp(Color.Transparent, Color.DodgerBlue, this._elapsed / this.FadeDuration * 0.5f);
                    case FadeDirection.FadeOut:
                        return Color.Lerp(Color.DodgerBlue, Color.Transparent, 0.5f + this._elapsed / this.FadeDuration * 0.5f);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        /// <summary>
        /// Gets the vertices.
        /// </summary>
        public Vector2[] Vertices
        {
            get
            {
                Vector2 axis = new Vector2(0, -800);
                float angle = MathHelper.ToRadians(360) * this._percentage;

                Vector2 current = Vector2.Rotate(axis, this._rotation + angle);
                Vector2 next = Vector2.Rotate(axis, this._rotation + angle + MathHelper.ToRadians(10));

                return new[] { current + this._center, this._center, next + this._center };
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Picks this ray.
        /// </summary>
        public void Pick()
        {
            this.IsPicked = true;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.IsPicked)
            {
                this._elapsed += elapsed;
                if (this._elapsed >= this.FadeDuration && this.FadeDirection == FadeDirection.FadeIn)
                {
                    this._elapsed = 0;
                    this.FadeDirection = FadeDirection.FadeOut;
                }
                if (this._elapsed >= this.FadeDuration && this.FadeDirection == FadeDirection.FadeOut)
                {
                    this._elapsed = 0;
                    this.IsPicked = false;
                    this.FadeDirection = FadeDirection.FadeIn;
                }
            }

            this._rotation += this.RotationSpeed * elapsed / 6000.0f;
        }
        #endregion
    }
}
