using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixingSystem.UserDefines
{
    public class Rs232Data
    {
        public byte[] Begin { get; set; }
        public byte Id { get; set; }
        public byte Count { get; set; }
        public byte[] Data { get; set; }        //...408 bytes
        public byte[] Crc { get; set; }
        public byte[] End { get; set; }

        public Rs232Data()
        {
            Begin = new byte[2];
            Data = new byte[408];
            Crc = new byte[2];
            End = new byte[2];
        }       
    }
}
