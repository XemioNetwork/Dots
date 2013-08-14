using System;
using System.Drawing;
using System.Windows.Forms;
using Xemio.Dots.Scenes;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content.FileSystem;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form mainForm = new Form()
                            {
                                ClientSize = new Size(800, 600),
                                MaximizeBox = false,
                                FormBorderStyle = FormBorderStyle.FixedSingle,
                                StartPosition = FormStartPosition.CenterScreen,
                                Text = "Dots"
                            };

            var config = XGL.Configure()
                .DefaultComponents()
                .DefaultInput()
                .FrameRate(60)
                .FileSystem<DiskFileSystem>()
                .Graphics<GDIGraphicsInitializer>()
                .Scenes(new GameMenu())
                .BackBuffer(800, 600)
                .BuildConfiguration();

            XGL.Run(mainForm, config);
        }
    }
}
