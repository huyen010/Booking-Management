using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    public class BLL_Sanh
    {
        private static BLL_Sanh _Instance;
        public static BLL_Sanh Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Sanh();
                }
                return _Instance;
            }
            private set { }
        }
        public BLL_Sanh() { }
        public List<SANH> GetListSanh(int ID)
        {
            CSDL db = new CSDL();
            List<SANH> data = new List<SANH>();
            var l1 = from p in db.SANHs
                     where ID == 0 || p.IDSanh == ID
                     select p;
            data = l1.ToList();
            return data;
        }
        public SANH GetSanh(int ID)
        {
            CSDL db = new CSDL();
            return db.SANHs.Find(ID);
        }
        public List<CBBItem> AddCBBsanh(DateTime ngaytc, int caTC, int sl)
        {

            List<CBBItem> list = new List<CBBItem>();
            foreach (SANH item in GetCbSanh(ngaytc, caTC, sl))
            {
                list.Add(new CBBItem { Value = item.IDSanh, Text = item.NameSanh });
            }
            return list;
        }
        public void DelSanh(List<int> listID)
        {
            CSDL db = new CSDL();
            foreach (int i in listID)
            {
                db.DeleSanh(i);
                db.SaveChanges();
            }
        }
        public void AddSanh(SANH s)
        {
            CSDL db = new CSDL();
            db.AddSanh(s.NameSanh, s.SIZE, s.PriceSanh, s.PhotoSanh);
            db.SaveChanges();
        }
        public void EditSanh(SANH s)
        {
            CSDL db = new CSDL();
            db.EditSanh(s.IDSanh, s.NameSanh, s.SIZE, s.PriceSanh, s.PhotoSanh);
            db.SaveChanges();
        }
        public List<SANH> GetCbSanh(DateTime NgayTC, int Ca, int SLBan)
        {
            List<SANH> data = new List<SANH>();
            CSDL db = new CSDL();
            var l = db.BILLs.Where(p => (DateTime.Compare(NgayTC.Date, p.CreateDate) == 0 && p.Time == (Ca + 1))).Select(p => p.IDSanh).ToList();
            foreach (SANH i in GetListSanh(0))
            {
                bool check = true;
                foreach (int j in l)
                {
                    if (i.IDSanh == j)
                    {
                        check = false;
                    }
                }
                if (check == true && i.SIZE >= SLBan) data.Add(i);
            }
            return data;
        }
        public void SortSanh(List<SANH> l)
        {
            l.Sort();
        }
    }
}
