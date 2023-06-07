using System;
using System.Windows.Forms;

namespace TeraMega.Views
{

    public partial class StartupView : Form
    {
        //...Fields
        private int init_percentage = 0;
        private MainView view;
        private System.Windows.Forms.Timer timer;

        public StartupView()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += delegate { SystemInit(); };          
        }

        private void SystemInit()
        {
            if (init_percentage < 100)
            {
                update_control_processbar(progSystemInit, ++init_percentage);
            }               
            else
            {
                timer.Enabled = false;
                this.Hide();
                view = new MainView();
                view.Show(); 
            }
        }

        private void update_control_processbar(ProgressBar control, int val)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => update_control_processbar(control, val)));
            }
            else
            {
                control.Value = val;
            }
        }
    }
}
