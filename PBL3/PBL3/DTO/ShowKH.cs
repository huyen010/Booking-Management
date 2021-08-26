using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class ShowKH
    {
        public int IDBILL { get; set; }
        public string NameKH { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public DateTime PayDay { get; set; }
        public float TOTAL { get; set; }
    }
}
