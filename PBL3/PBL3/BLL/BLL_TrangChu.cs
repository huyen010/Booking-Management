using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    public class BLL_TrangChu
    {
        private static BLL_TrangChu _Instance;
        public static BLL_TrangChu Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_TrangChu();
                }
                return _Instance;
            }
            private set { }
        }
        public BLL_TrangChu() { }
        public BILL GetBill(int IDBill)
        {
            BILL bill = new BILL();
            CSDL db = new CSDL();
            var l = from p in db.BILLs
                    where p.IDBILL == IDBill
                    select p;
            bill = l.FirstOrDefault();
            return bill;
        }
        public CUSTOMER GetCustomer(int idbill)
        {
            CSDL db = new CSDL();
            CUSTOMER s = new CUSTOMER();
            var l = from p in db.BILLs
                    join cus in db.CUSTOMERs on p.IDKH equals cus.IDKH
                    where p.IDBILL == idbill && p.IDKH == cus.IDKH
                    select cus;
            s = l.FirstOrDefault();
            return s;
        }
        public List<ShowBill> ShowTC(DateTime NgayTC)
        {
            List<ShowBill> data = new List<ShowBill>();
            CSDL db = new CSDL();
            var l = from b in db.BILLs
                    join cus in db.CUSTOMERs on b.IDKH equals cus.IDKH
                    join s in db.SANHs on b.IDSanh equals s.IDSanh
                    join p in db.PARTies on b.IDPARTY equals p.IDPARTY
                    where DateTime.Compare((DateTime)b.BookingDate, NgayTC) == 0 && b.STATUS.Contains("Chưa hoàn thành")
                    select new ShowBill
                    {
                        IDBILL = b.IDBILL,
                        NameKH = cus.NameKH,
                        SDT = cus.SDT,
                        CMND = cus.CMND,
                        NamePT = p.NamePT,
                        NameSanh = s.NameSanh,
                        Quantity = (int)b.Quantity,
                        Time = (int)b.Time
                    };
            data = l.ToList();
            return data;
        }
        public void UpdateBill(int idbill, int idsanh, int idca, int slban, float total)
        {
            CSDL db = new CSDL();
            db.EditBill(idbill, idsanh, idca, slban, total);
            db.SaveChanges();
        }
        public void DelBill(int idbill)
        {
            CSDL db = new CSDL();
            db.DelBill(idbill);
            db.SaveChanges();
        }
        public List<ShowBill> SearchByKH(DateTime NgayTC, string CMND, string SDT, int CaTC)
        {
            CSDL db = new CSDL();
            List<ShowBill> data = new List<ShowBill>();
            var l = from p in ShowTC(NgayTC)
                    where p.CMND == CMND || p.Time == CaTC || p.SDT == SDT
                    select p;
            data = l.ToList();
            return data;
        }
        public List<ShowTC> Show(DateTime NgayTC)
        {
            CSDL db = new CSDL();
            List<ShowTC> data = new List<ShowTC>();
            var l = from b in db.BILLs
                    join cus in db.CUSTOMERs on b.IDKH equals cus.IDKH
                    join s in db.SANHs on b.IDSanh equals s.IDSanh
                    join p in db.PARTies on b.IDPARTY equals p.IDPARTY
                    where DateTime.Compare((DateTime)b.BookingDate, NgayTC) == 0 && b.STATUS.Contains("Chưa hoàn thành")
                    select new ShowTC 
                    {
                        IDBILL = b.IDBILL,
                        NameKH = cus.NameKH,
                        NamePT = p.NamePT,
                        NameSanh = s.NameSanh,
                        Time = (int)b.Time,
                        Quantity = (int)b.Quantity
                    };
            data = l.ToList();
            return data;
        }
    }
}
