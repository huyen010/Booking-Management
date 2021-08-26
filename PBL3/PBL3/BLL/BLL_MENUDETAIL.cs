using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    public class BLL_MENUDETAIL
    {
        private static BLL_MENUDETAIL _Instance;
        public static BLL_MENUDETAIL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_MENUDETAIL();
                }
                return _Instance;
            }
            private set { }
        }
        public FOOD GetFood(int id)
        {
            CSDL da = new CSDL();
            FOOD f = da.FOODs.Find(id);
            return f;
        }
        public List<CBBItem> ADD_CBBFOODCBB(int id)
        {
            CSDL da = new CSDL();
            List<CBBItem> list = new List<CBBItem>();
            if (id == -1)
            {
                foreach (FOODCATEGORY fc in da.FOODCATEGORies)
                {
                    list.Add(new CBBItem { Value = fc.IDFoodCategory, Text = fc.NameFCategory });
                }
                return list;
            }
            else
            {
                foreach (FOOD f in da.FOODs)
                {
                    if (f.IDFoodCategory == id)
                    {
                        list.Add(new CBBItem{ Value = f.IDFood, Text = f.NameFood });
                    }
                }
                return list;
            }
        }
        public MENUDETAIL checkFoodinMN(int idB, int idF)
        {
            CSDL da = new CSDL();
            var l1 = da.MENUDETAILs.Where(p => p.IDBILL == idB && p.IDFood == idF);
            return l1.FirstOrDefault();
        }
        public void AddMenu(MENUDETAIL mnf)
        {
            CSDL da = new CSDL();
            da.MENUDETAILs.Add(mnf);
            da.SaveChanges();
        }
        public void upDateMenu(int idB, int idF, int sl)
        {

            CSDL da = new CSDL();
            MENUDETAIL mnf = da.MENUDETAILs.Where(p => p.IDBILL == idB && p.IDFood == idF).FirstOrDefault();
            mnf.SLFood = sl;
            mnf.TongTien = mnf.SLFood * mnf.ThanhTien;
            da.SaveChanges();
        }
        public void DeleteMenu(int idB, int idF)
        {
            CSDL da = new CSDL();
            MENUDETAIL mnf = da.MENUDETAILs.Where(p => p.IDBILL == idB && p.IDFood == idF).FirstOrDefault();
            da.MENUDETAILs.Remove(mnf);
            da.SaveChanges();
        }
        public List<MenuFoodView> getListMenuF(int idB)
        {
            CSDL da = new CSDL();
            var l1 = da.MENUDETAILs.Where(p => p.IDBILL == idB).Select(m => new MenuFoodView
            {
                IDFood = m.IDFood,
                IdBill = m.IDBILL,
                NameFood = m.FOOD.NameFood,
                PriceFood = m.FOOD.PriceFood,
                SoLuong = m.SLFood,
                TongTien = (double)m.TongTien,
            });
            return l1.ToList();
        }
        public float getTongTienMenuBan(int idB)
        {
            CSDL da = new CSDL();
            var l1 = da.MENUDETAILs.Where(p => p.IDBILL == idB).Select(p => p.TongTien).Sum();
            if (l1 == null)
            {
                return 0;
            }
            return (float)l1;
        }
        public void deleteMenuDetail(int idB)
        {
            CSDL da = new CSDL();
            var l1 = da.MENUDETAILs.Where(p => p.IDBILL == idB);
            foreach (MENUDETAIL mn in l1)
            {
                da.MENUDETAILs.Remove(mn);
                da.SaveChanges();
            }
        }
    }
}
