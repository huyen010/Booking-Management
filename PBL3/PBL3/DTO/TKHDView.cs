using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class TKHDView
    {
        public int IDBILL { get; set; }
        public string NAMEKH { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }
        public string TIEC { get; set; }
        public string SANH { get; set; }
        public int SOLUONG { get; set; }
        public string STATUS { get; set; }
        public DateTime BookingDate { get; set; }
        public double TOTAL { get; set; }

    }
}
