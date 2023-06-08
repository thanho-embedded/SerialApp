using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TeraMega.Utils;

namespace TeraMega.Views
{

    public partial class StartupView : Form, INotifyPropertyChanged
    {
        //...Fields
        private int systemInitCount = 0;
        private MainView view;
        private Timer timerCyclicInit;

        public Dictionary<string, Dictionary<string, string>> SystemConfig { get; set; }
        public int SystemInitCount
        {
            get => systemInitCount;
            set
            {
                if (systemInitCount != value)
                {
                    systemInitCount = value;
                    Notify(nameof(systemInitCount));
                    UpdatedProcessControl();
                }
            }
        }

        //...Constance
        const string SYS_CONFIG_FILE_PATH = "SysConfig.ini";

        public event PropertyChangedEventHandler PropertyChanged;

        public StartupView()
        {
            InitializeComponent();

            //...Init timer
            timerCyclicInit = new Timer();
            timerCyclicInit.Interval = 50;
            timerCyclicInit.Enabled = true;

            timerCyclicInit.Tick += (s, e) =>
            {
                SystemInitCount++;
                ShowMainView();
            };

            SystemInit();
        }

        private void SystemInit()
        {
            SystemConfig = SYS_CONFIG_FILE_PATH.ReadIniFile();
        }
        private void ShowMainView()
        {
            if (SystemInitCount >= 100)
            {
                timerCyclicInit.Enabled = false;
                this.Hide();
                view = new MainView();
                view.Show();
            }
        }
        private void UpdatedProcessControl()
        {
            if (progSystemInit.InvokeRequired)
            {
                progSystemInit.Invoke(new Action(() => UpdatedProcessControl()));
            }
            else
            {
                progSystemInit.Value = SystemInitCount;
            }
        }
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
