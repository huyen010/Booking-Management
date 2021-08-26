using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBL3.DTO;
namespace PBL3.BLL
{
    public class BLL_Party
    {
        private static BLL_Party _Instance;
        public static BLL_Party Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_Party();
                }
                return _Instance;
            }
            private set { }
        }
        public BLL_Party() { }
        public List<PARTY> GetListParty(int ID)
        {
            CSDL db = new CSDL();
            List<PARTY> data = new List<PARTY>();

            var l = from p in db.PARTies
                    where ID == 0 || p.IDPARTY == ID
                    select p;
            data = l.ToList();
            return data;
        }
        public PARTY GetParty(int ID)
        {
            CSDL db = new CSDL();
            return db.PARTies.Find(ID);
        }
        public void DelParty(List<int> ListParty)
        {
            using (CSDL db = new CSDL())
            {
                foreach (int i in ListParty)
                {
                    db.DelPT(i);
                    db.SaveChanges();
                }
            }
        }
        public void AddParty(PARTY p)
        {
            CSDL db = new CSDL();
            db.AddParty(p.NamePT, p.PricePT, p.PhotoParty);
            db.SaveChanges();
        }
        public void EditParty(PARTY p)
        {
            CSDL db = new CSDL();
            db.EditParty(p.IDPARTY, p.NamePT, p.PricePT, p.PhotoParty);
            db.SaveChanges();
        }
        public void SortPT(List<PARTY> l)
        {
            l.Sort();
        }
        public List<CBBItem> AddCBBParty()
        {
            CSDL da = new CSDL();
            List<CBBItem> list = new List<CBBItem>();
            foreach (PARTY pt in da.PARTies)
            {
                list.Add(new CBBItem { Value = pt.IDPARTY, Text = pt.NamePT });
            }
            return list;
        }
    }
}
