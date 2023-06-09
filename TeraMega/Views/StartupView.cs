using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MixingSystem.Utils;
using TeraMega.Presentors;
using TeraMega.Services;

namespace MixingSystem.Views
{

    public partial class StartupView : Form, INotifyPropertyChanged
    {
        //...Fields
        private int systemInitCount = 0;
        private string info = "Initializing";
        private Timer timerCyclicInit;

        public Dictionary<string, Dictionary<string, string>> SystemConfig { get; set; }

        //...Propperties
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
            timerCyclicInit.Interval = 100;
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

                PlcSyncService plcSyncService = new PlcSyncService(S7.Net.CpuType.S71500, "192.168.0.1", 0, 1);
                MainView view = new MainView();

                new PlcSyncPresenter(plcSyncService, view);
            }
            else
            {
                SearchForUpdateLabelProperty("INIT", info);
                if (info == "Initializing")
                    info = "Initializing.";
                else if (info == "Initializing.")
                    info = "Initializing..";
                else if (info == "Initializing..")
                    info = "Initializing...";
                else if (info == "Initializing...")
                    info = "Initializing....";
                else if (info == "Initializing....")
                    info = "Initializing.....";
                else if (info == "Initializing.....")
                    info = "Initializing";
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

        private void SearchForUpdateLabelProperty(string tag, string val)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label ctrl)
                {
                    if ((string)ctrl.Tag == tag)
                    {
                        UpdateLabelProperty(ctrl, tag, val);
                    }
                }
            }
        }

        private void UpdateLabelProperty(Label ctrl, string tag, string val)
        {
            if ((string)ctrl.Tag == tag)
            {
                if (ctrl.InvokeRequired)
                {
                    ctrl.Invoke(new Action(() => UpdateLabelProperty(ctrl, tag, val)));
                }
                else
                {
                    ctrl.Text = val;
                }
            }
        }
    }
}
