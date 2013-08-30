using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Dots.Entities.GameMenu;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Fonts;

namespace Xemio.Dots.Entities.UI
{
    public class Button : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
            this.BackColor = new Color(220, 220, 220, 100);
            this.HoverColor = Color.Lerp(this.BackColor, Color.DodgerBlue, 0.6f);

            this.Text = "Button";

            this.Renderer = new ButtonRenderer(this);
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is pressed.
        /// </summary>
        public bool IsPressed { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is hovered.
        /// </summary>
        public bool IsHovered { get; private set; }
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(this.Position.X, this.Position.Y, this.Width, this.Height); }
            set
            {
                this.Position = new Vector2(value.X, value.Y);

                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        public Color HoverColor { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public SpriteFont Font { get; set; }
        #endregion

        #region Methods
        public override void Tick(float elapsed)
        {
            InputManager inputManager = XGL.Components.Require<InputManager>();
            PlayerInput localInput = inputManager.LocalInput;

            this.IsHovered = this.Bounds.Contains(localInput.MousePosition);
            this.IsPressed = this.IsHovered && localInput.IsKeyDown(Keys.LeftMouse);

            base.Tick(elapsed);
        }
        #endregion
    }
}
