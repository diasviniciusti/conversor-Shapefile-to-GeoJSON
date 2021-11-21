using System;
using System.Windows.Forms;
using conversor_Shapefile_to_GeoJSON.Control;

namespace conversor_Shapefile_to_GeoJSON.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            IControl Convert = new Conversion();
            Convert.Converting(textBox1.Text, textBox2.Text);
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "ESRI Shapefile (*.shp)|*.shp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "GeoJSON|*.geojson|JSON|*.json";
            saveFileDialog1.Title = "Geometry JSON File|JSON File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                textBox2.Text = saveFileDialog1.FileName;
        }
    }
}
