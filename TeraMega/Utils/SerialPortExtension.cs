using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixingSystem.UserDefines;

namespace MixingSystem.Utils
{
    public static class SerialPortExtension
    {
        /// <summary>
        /// Open serial port
        /// </summary>
        /// <param name="serial"></param>
        public static int Connect(this SerialPort serial)
        {
            try
            {
                if (!serial.IsOpen && serial != null)
                {
                    serial.Open();
                    return 0;
                }
            }
            catch { }

            return -1;
        }
        public static Rs232Data Received(this SerialPort serial)
        {
             
            if (serial.IsOpen)
            {
                string line = serial.ReadExisting();
                serial.DiscardInBuffer();
                return Rs232DataExchange(line);
            }
            
            return null;
        }

        public static int Send(this SerialPort serial, Rs232Data frame)
        {
            try
            {
                List<byte> tmp = new List<byte>();
                tmp.Add(frame.Begin[0]);
                tmp.Add(frame.Begin[1]);
                tmp.Add(frame.Id);
                tmp.Add(frame.Count);
                for (int i = 0; i < tmp.Count; i++)
                {
                    tmp.Add(frame.Data[i]);
                }
                tmp.Add(frame.Crc[0]);
                tmp.Add(frame.Crc[1]);
                tmp.Add(frame.End[0]);
                tmp.Add(frame.End[1]);

                byte[] buffer = tmp.ToArray();

                if (serial.IsOpen && serial != null)
                {
                    serial.Write(buffer, 0, buffer.Length);                  
                }
                return 0;
            }
            catch { }

            return -1;
        }
        private static Rs232Data Rs232DataExchange(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                Rs232Data frame = new Rs232Data();
                int ind = 0;
                byte[] buffer = Encoding.UTF8.GetBytes(line);

                frame.Begin[0] = buffer[0];
                frame.Begin[1] = buffer[1];
                frame.Id = buffer[2];
                frame.Count = buffer[3];
                for (int i = 4; i < buffer.Length - 4; i++)
                {
                    frame.Data[i - 4] = buffer[i];
                }
                ind = buffer.Length - frame.Data.Length - 4;
                frame.Crc[0] = buffer[ind];
                frame.Crc[1] = buffer[++ind];
                frame.End[0] = buffer[++ind];
                frame.End[1] = buffer[++ind];
                ind--;

                return frame;
            }
            
            return null;
        }
    }
}
