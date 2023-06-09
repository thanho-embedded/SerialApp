using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MixingSystem.Services;
using MixingSystem.Utils;
using TeraMega.Presentors;
using TeraMega.Services;
using TeraMega.Views;

namespace MixingSystem.Views
{
    public partial class MainView : Form,IMainView
    {
        //...Fields
        private Rs232Service rs232Handle;
        
        //...Threadings
        const string ROOT_THREAD_NAME = "MAIN_VIEW_THREAD_";
        const string THREAD_1ST_NAME = "MAIN_VIEW_THREAD_1";
        const string THREAD_2ND_NAME = "MAIN_VIEW_THREAD_2";
        const string THREAD_3RD_NAME = "MAIN_VIEW_THREAD_3";
        const string THREAD_4TH_NAME = "MAIN_VIEW_THREAD_4";

        private Thread[] threadHandles = null;
        private bool isThreadsLoop = true;

        private int[] percentages = null;

        public MainView()
        {
            InitializeComponent();

            this.FormClosing += (s, e) =>
            {
                ThreadsJoin();
                Application.Exit();
            };

            rs232Handle = new Rs232Service("COM3", 115200);
            if (rs232Handle.Serial.Connect() != -1)
            {
                MessageBox.Show("Open Serial Port is successful!");
            }
            else
            {
                MessageBox.Show("Failed to open serial port!");
            }

            ThreadInitialize(4);
        }

        private void ThreadInitialize(int count)
        {
            threadHandles = new Thread[count];
            percentages = new int[count];

            for (int i = 0; i < count; i++)
            {
                threadHandles[i] = ThreadsStart($"{ROOT_THREAD_NAME}{i+1}", ThreadCallback);               
            }
        }
        private Thread ThreadsStart(string name, Action callback)
        {
            Thread handle = null;

            handle = new Thread(new ThreadStart(callback));
            handle.Name = name;
            handle.Start();

            return handle;
        }
        private void ThreadsJoin() 
        {
            isThreadsLoop = false;
            for (int i = 0; i < threadHandles.Length;i++)
            {
                threadHandles[i].Join();
            }
        }
        private void ThreadCallback()
        {
            while(isThreadsLoop)
            {
                switch (Thread.CurrentThread.Name)
                {
                    case THREAD_1ST_NAME:
                        {
                            SearchForUpdateProcessBarProperty("PROCESS_1", percentages[0]);
                            SearchForUpdateLabelProperty("PERCENT_1", percentages[0].ToString() + " %");
                            PercentageSimulation(0);
                        }
                        break;
                    case THREAD_2ND_NAME:
                        {
                            SearchForUpdateProcessBarProperty("PROCESS_2", percentages[1]);
                            SearchForUpdateLabelProperty("PERCENT_2", percentages[1].ToString() + " %");
                            PercentageSimulation(1);
                        }
                        break;
                    case THREAD_3RD_NAME:
                        {
                            SearchForUpdateProcessBarProperty("PROCESS_3", percentages[2]);
                            SearchForUpdateLabelProperty("PERCENT_3", percentages[2].ToString() + " %");
                            PercentageSimulation(2);
                        }
                        break;
                    case THREAD_4TH_NAME:
                        {
                            SearchForUpdateProcessBarProperty("PROCESS_4", percentages[3]);
                            SearchForUpdateLabelProperty("PERCENT_4", percentages[3].ToString() + " %");
                            PercentageSimulation(3);
                        }
                        break;
                    default:
                        break;
                }
                Thread.Sleep(30);
            }
        }
        //...Updated control properties
        private void PercentageSimulation(int index)
        {
            if (percentages[index] < 100) percentages[index]++; //else percentages[index] = 0;
        }
        private void SearchForUpdateProcessBarProperty(string tag, int val)
        {
            foreach (Control control in MainPanel.Controls)
            {
                if (control is ProgressBar ctrl)
                {
                    if ((string)ctrl.Tag == tag)
                    {
                        UpdateProcessBarProperty(ctrl, tag, val);
                    }
                }
            }
        }
        private void SearchForUpdateLabelProperty(string tag, string val)
        {
            foreach (Control control in MainPanel.Controls)
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

        private void UpdateProcessBarProperty(ProgressBar ctrl, string tag, int val)
        {
            if ((string)ctrl.Tag == tag)
            {
                if (ctrl.InvokeRequired)
                {
                    ctrl.Invoke(new Action(() => UpdateProcessBarProperty(ctrl, tag, val)));
                }
                else
                {
                    ctrl.Value = val;
                }
            }
        }

        public void Send()
        {
            throw new NotImplementedException();
        }
    }
}
