using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3
{
    public partial class ThanhToan : Form
    {
        CSDL db = new CSDL();
        public delegate void MyDel(int ID);
        public MyDel d;
        int ID = 0;
        private void GetData(int _ID)
        {
            ID = _ID;
        }
        public ThanhToan()
        {
            d = new MyDel(GetData);
            InitializeComponent();
        }

        private void bt_Pay_Click(object sender, EventArgs e)
        {
            BILL b = db.BILLs.Find(ID);
            b.STATUS = "Da Thanh Toan";
            b.PayDay = Convert.ToDateTime(label.Text);
            db.SaveChanges();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            BILL b = db.BILLs.Find(ID);
            MENUDETAIL menu = (from p in db.MENUDETAILs where p.IDBILL == ID select p).FirstOrDefault();
            lbNameKH.Text = b.CUSTOMER.NameKH;
            lbCMND.Text = b.CUSTOMER.CMND;
            lbSDT.Text = b.CUSTOMER.SDT;
            lbAddress.Text = b.CUSTOMER.DIACHI;
            label.Text = b.BookingDate.ToString();
            lbTime.Text = b.Time.ToString();
            lbNumber.Text = b.Quantity.ToString();
            lbDeposit.Text = b.DATCOC.ToString();
            lbCreateDate.Text = b.CreateDate.ToString();
            lbParty.Text = b.PARTY.NamePT;
            lbPriceParty.Text = b.PARTY.PricePT.ToString();
            lbHall.Text = b.SANH.NameSanh;
            lbPriceHall.Text = b.SANH.PriceSanh.ToString();
            lbPhatsinh.Text = b.INCUR;
            lbChiPhi.Text = b.INCURCOST.ToString();
            lbTongMenu.Text = menu.TongTien.ToString();
            lbDiscount.Text = b.DISCOUNT.ToString();
            lbTotal.Text = b.TOTAL.ToString();
            lbDay.Text = DateTime.Now.Day.ToString();
            lbMonth.Text = Convert.ToString(DateTime.Now.Month);
            lbYear.Text = Convert.ToString(DateTime.Now.Year);
            var menufood = from p in db.MENUDETAILs
                           where p.IDBILL == b.IDBILL
                           select new
                           {
                               p.FOOD.NameFood,
                               p.FOOD.FOODCATEGORY.NameFCategory,
                               p.FOOD.PriceFood,
                               p.SLFood,
                               p.ThanhTien
                           };
            dtgvMenu.DataSource = menufood.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
