using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
using PBL3.DTO;

namespace PBL3.BLL
{
    class BLL_KhachHang
    {
        private static BLL_KhachHang _Instance;
        public static BLL_KhachHang INSTANCE
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_KhachHang();
                }
                return _Instance;
            }
            private set { }
        }
        public void ADD_KHACHHANG(CUSTOMER Kh)
        {
            CSDL da = new CSDL();
            da.CUSTOMERs.Add(Kh);
            da.SaveChanges();
        }
        public List<CUSTOMER> TimKH(string type, string text)
        {
            CSDL da = new CSDL();
            if (type.Equals("SDT"))
            {
                var l1 = da.CUSTOMERs.Where(p => p.SDT.Equals(text) == true);
                return l1.ToList();
            }
            else
            {
                var l2 = da.CUSTOMERs.Where(p => p.CMND.Equals(text));
                return l2.ToList();
            }
        }
        public CUSTOMER viewCustomer(int maKH)
        {
            CSDL da = new CSDL();
            return da.CUSTOMERs.Find(maKH);
        }
        public void EditKH(CUSTOMER ctm)
        {
            CSDL da = new CSDL();
            CUSTOMER kh = da.CUSTOMERs.Find(ctm.IDKH);
            kh.NameKH = ctm.NameKH;
            kh.CMND = ctm.CMND;
            kh.SDT = ctm.SDT;
            kh.DIACHI = ctm.DIACHI;
            da.SaveChanges();
        }
        public void DeleteKH(int maKH)
        {
            CSDL da = new CSDL();
            CUSTOMER ctm = da.CUSTOMERs.Find(maKH);
            da.CUSTOMERs.Remove(ctm);
            da.SaveChanges();
        }
        public decimal getChiTieuKH(int idKH)
        {
            CSDL da = new CSDL();
            var l1 = da.BILLs.Where(p => p.IDKH == idKH).Select(p => p.TOTAL).Sum();
            if (l1 == null) return 0;
            else return (decimal)l1;
        }
        public List<ShowKH> GetListKH(DateTime DateIn, DateTime DateOut)
        {
            CSDL db = new CSDL();
            var l = from c in db.BILLs
                    where (DateTime.Compare((DateTime)c.BookingDate, DateIn) >= 0 && DateTime.Compare((DateTime)c.BookingDate, DateOut) <= 0 && c.STATUS == "Đã hoàn Thành")
                    select new ShowKH
                    {
                        IDBILL = c.IDBILL,
                        NameKH = c.CUSTOMER.NameKH,
                        CMND = c.CUSTOMER.CMND,
                        SDT = c.CUSTOMER.SDT,
                        DIACHI = c.CUSTOMER.DIACHI,
                        PayDay = (DateTime)c.PayDay,
                        TOTAL =(float) c.TOTAL
                    };
            return l.ToList();
        }
    }
}