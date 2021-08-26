using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DAL;
namespace PBL3
{
    class BLL_Account
    {
        private static BLL_Account _instance;
        public static BLL_Account INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BLL_Account();
                }
                return _instance;
            }
            private set { }
        }
        public ACCOUNT checkAccount(string username, string password)
        {
            CSDL da = new CSDL();
            var l1 = da.ACCOUNTs.Where(p => p.USERNAME.Equals(username) && p.PASS.Equals(password)).FirstOrDefault();
            return l1;
        }
        public void AddACCOUNT(ACCOUNT acc)
        {
            CSDL db = new CSDL();
            db.ACCOUNTs.Add(acc);
            db.SaveChanges();
        }
        public void EditACCOUNT(ACCOUNT acc)
        {
            CSDL db = new CSDL();
            ACCOUNT a = db.ACCOUNTs.Find(acc.IDTK);
            a.NAME = acc.NAME;
            a.CHUCVU = acc.CHUCVU;
            a.TypeAcc = acc.TypeAcc;
            a.SDT = acc.SDT;
            a.EMAIL = acc.EMAIL;
            a.CMND = acc.CMND;
            a.USERNAME = acc.USERNAME;
            a.PASS = acc.PASS;
            a.PhotoAC = acc.PhotoAC;
            db.SaveChanges();
        }
        public void DelACCOUNT(int id)
        {
            CSDL db = new CSDL();
            ACCOUNT acc = db.ACCOUNTs.Where(p => p.IDTK == id).SingleOrDefault();
            db.ACCOUNTs.Remove(acc);
            db.SaveChanges();
        }
        public List<ACCOUNT> ShowACCOUNT()
        {
            CSDL db = new CSDL();
            var result = from c in db.ACCOUNTs
                         select c;
            return result.ToList();
        }
        public List<ACCOUNT> SearchTK(string key)
        {
            CSDL db = new CSDL();
            var result = from c in db.ACCOUNTs.Where(p => p.NAME.Contains(key)
                            || p.SDT.Contains(key) || p.CMND.Contains(key))
                         select c;
            return result.ToList();
        }
    }
}
