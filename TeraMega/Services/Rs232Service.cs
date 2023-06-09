using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixingSystem.UserDefines;

namespace MixingSystem.Services
{
    public class Rs232Service
    {
        //...Fields
        public SerialPort Serial;
        public Rs232Data Frame = new Rs232Data();

        public Rs232Service() 
        {
            this.Serial = new SerialPort();
        }
        public Rs232Service(string port) 
        {             
            this.Serial = new SerialPort(port);
        }
        public Rs232Service(string port, int speed)
        {
            this.Serial = new SerialPort(port, speed);
        }       
    }
}
