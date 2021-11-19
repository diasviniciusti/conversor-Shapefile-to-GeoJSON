using System;
using System.Windows.Forms;
using conversor_Shapefile_to_GeoJSON.View;

namespace conversor_Shapefile_to_GeoJSON.Control
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
