using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeraMega.Services;
using TeraMega.Utils;

namespace TeraMega.Views
{
    public partial class MainView : Form
    {
        //...Fields
        private Rs232Service Rs232Handle;
        public MainView()
        {
            InitializeComponent();

            this.FormClosing += (s, e) =>
            {
                Application.Exit();
            };

            Rs232Handle = new Rs232Service("COM3", 115200);
            if (Rs232Handle.Serial.Connect() != -1)
            {
                MessageBox.Show("Open Serial Port is successful!");
            }
            else
            {
                MessageBox.Show("Failed to open serial port!");
            }          
        }
    }
}
