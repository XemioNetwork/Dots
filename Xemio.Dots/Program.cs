using System;
using System.Drawing;
using System.Windows.Forms;
using Xemio.Dots.Scenes;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content.FileSystem;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.GDIPlus;

namespace Xemio.Dots
{
    static class Program
    {
        /// <summary>
        /// The main entrance point.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form mainForm = new Form()
                            {
                                ClientSize = new Size(800, 600),
                                MaximizeBox = false,
                                FormBorderStyle = FormBorderStyle.FixedSingle,
                                StartPosition = FormStartPosition.CenterScreen,
                                Text = "Dots"
                            };

            var config = XGL.Configure()
                .FrameRate(60)
                .BackBuffer(800, 600)
                .Surface(mainForm)
                .Graphics<GDIGraphicsInitializer>(
                    SmoothingMode.AntiAliased,
                    InterpolationMode.Bicubic)
                .Scenes(new GameMenu())
                .DefaultComponents()
                .DefaultInput()
                .DisableSplashScreen()
                .BuildConfiguration();

            XGL.Run(config);
            Application.Run(mainForm);
        }
    }
}
