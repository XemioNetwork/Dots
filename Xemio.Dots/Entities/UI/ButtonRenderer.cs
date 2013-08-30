using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.Dots.Entities.UI
{
    public class ButtonRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonRenderer"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        public ButtonRenderer(Button button) : base(button)
        {
        }
        #endregion


        #region Properties
        /// <summary>
        /// Gets the button.
        /// </summary>
        public Button Button
        {
            get { return (Button)this.Entity; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            Color backColor = this.Button.IsHovered ? this.Button.HoverColor : this.Button.BackColor;
            Vector2 offset = this.Button.IsPressed ? new Vector2(0, 2) : Vector2.Zero;

            Vector2 shadowOffset = new Vector2(0, 5);
            Rectangle shadowBounds = new Rectangle(
                this.Button.Position.X + offset.X,
                this.Button.Position.Y + offset.Y,
                this.Button.Width + shadowOffset.X - offset.X,
                this.Button.Height + shadowOffset.Y - offset.Y);

            Vector2 textPosition = this.Button.Bounds.Center - this.Button.Font.MeasureString(this.Button.Text) * 0.5f + offset;

            IBrush brush = this.GeometryFactory.CreateSolid(backColor);
            IBrush black = this.GeometryFactory.CreateSolid(Color.Lerp(backColor, new Color(0, 0, 0, backColor.A), 0.6f));
            IPen pen = this.GeometryFactory.CreatePen(new Color(1, 1, 1, 0.2f));

            this.GeometryManager.FillRoundedRectangle(black, shadowBounds, 3);

            this.GeometryManager.FillRoundedRectangle(brush, this.Button.Bounds + offset, 3);
            this.GeometryManager.DrawRoundedRectangle(pen, this.Button.Bounds + offset, 3);

            this.RenderManager.Render(this.Button.Font, this.Button.Text, textPosition + new Vector2(0, 1), Color.Black);
            this.RenderManager.Render(this.Button.Font, this.Button.Text, textPosition);
        }
        #endregion
    }
}
