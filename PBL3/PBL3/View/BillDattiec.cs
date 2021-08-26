using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.BLL;
namespace PBL3
{
    public partial class BillDattiec : Form
    {
        private static int IDBILL;
        public BillDattiec(int IDbill)
        {
            InitializeComponent();
            IDBILL = IDbill;
            setTTKH();
            SetTTTiec();
            SetDGVTD();
        }
        private void setTTKH()
        {
            lbSo.Text = lbSo.Text + IDBILL.ToString();
            lbNTN.Text = "Hôm nay ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            lbHTen.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).CUSTOMER.NameKH;
            lbCMND.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).CUSTOMER.CMND;
            lbDC.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).CUSTOMER.DIACHI;
            lbSDT.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).CUSTOMER.SDT;
        }
        private void SetTTTiec()
        {
            lbLoaiHinhTiec.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).PARTY.NamePT;
            lbGiaT.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).PARTY.PricePT.ToString();
            lbSLban.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).Quantity.ToString();
            lbTenSanh.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).SANH.NameSanh;
            lbGiaSanh.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).SANH.PriceSanh.ToString();
            lbNgayDat.Text = BLL_Bill.INSTANCE.getTTBill(IDBILL).CreateDate.ToShortDateString();
            if (BLL_Bill.INSTANCE.getTTBill(IDBILL).Time == 1)
            {
                lbgio.Text = "9h00 - 13h00";
            }
            else lbgio.Text = "16h00 - 20h00";
            lbGTD.Text = BLL_MENUDETAIL.Instance.getTongTienMenuBan(IDBILL).ToString();
        }
        private void SetDGVTD()
        {
            dgvDSTD.DataSource = BLL_MENUDETAIL.Instance.getListMenuF(IDBILL);
            dgvDSTD.Columns["IdBill"].Visible = false;
            dgvDSTD.Columns["IDFood"].Visible = false;
            dgvDSTD.Columns["NameFood"].HeaderText = "Tên món";
            dgvDSTD.Columns["PriceFood"].HeaderText = "Giá món";
            dgvDSTD.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDSTD.Columns["TongTien"].HeaderText = "Thành tiền";
        }
    }
}
