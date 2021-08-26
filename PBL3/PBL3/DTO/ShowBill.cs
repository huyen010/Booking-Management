using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class ShowBill
    {
        public int IDBILL { get; set; }
        public string NameKH { get; set; }
        public string SDT { get; set; }
        public string CMND { get; set; }
        public string NamePT { get; set; }
        public string NameSanh { get; set; }
        public int Quantity { get; set; }
        public int Time { get; set; }
    }
}
