using PBL3.BLL;
using PBL3.DAL;
using PBL3.DTO;
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
    public partial class HoaDon : Form
    {
        CSDL db = new CSDL();
        double Tong = 0, Temp = 0;
        ACCOUNT account;
        public HoaDon(ACCOUNT acc)
        {
            InitializeComponent();
            account = acc;
        }
        public void Show(string cmnd, DateTime date, int time, string sdt)
        {
           // CSDL db = new CSDL();
            try
            {
                BILL b = BLL_HoaDon.Instance.ShowInfor(cmnd, date, time, sdt);
                List<MenuView> menufood = BLL_HoaDon.Instance.ShowMenu(cmnd, date, time, sdt);
                txbNameKH.Text = b.CUSTOMER.NameKH;
                cbParty.Text = b.PARTY.NamePT;
                cbHall.Text = b.SANH.NameSanh;
                dtpkDateHold.Value = b.BookingDate.Value;
                if(time == 1)
                {
                    cbHold.Text = "9h00 - 13h00";
                }
                else { cbHold.Text = "16h00 - 20h00"; }
                txbDatcoc.Text = b.DATCOC.ToString();
                txbPhatSinh.Text = b.INCUR;
                txbChiPhi.Text = b.INCURCOST.ToString();
                txbDiscount.Text = b.DISCOUNT.ToString();
                txbTongTien.Text = (b.TOTAL - b.DATCOC).ToString();
                txbNumber.Text = b.Quantity.ToString();
                dtgvMenu.DataSource = menufood.ToList();
                dtgvMenu.Columns[0].HeaderText = "Tên món ăn";
                dtgvMenu.Columns[1].HeaderText = "Loại món ăn";
                dtgvMenu.Columns[2].HeaderText = "Giá món";
                dtgvMenu.Columns[3].HeaderText = "Nguyên liệu";
                Tong = Convert.ToDouble(txbTongTien.Text);
            }
            catch
            {
                MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txbSearchBill.Text == "" || cbbTime.Text == "")
                MessageBox.Show("Nhập đầy đủ thông tin!");
            else
                Show(txbSearchBill.Text, dtpkDate.Value,cbbTime.SelectedIndex + 1, txbSearchBill.Text);
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            BILL b = BLL_HoaDon.Instance.ShowInfor(txbSearchBill.Text, dtpkDate.Value, cbbTime.SelectedIndex + 1, txbSearchBill.Text);
            BLL_HoaDon.Instance.Confirm(b, Convert.ToInt32(txbTongTien.Text), Convert.ToInt32(txbChiPhi.Text), txbPhatSinh.Text, Convert.ToInt32(txbDiscount.Text),
                account.IDTK);
            ThanhToan f = new ThanhToan();
            f.d(b.IDBILL);
            f.ShowDialog();
        }

        private void txbChiPhi_TextChanged(object sender, EventArgs e)
        {
            BILL b = BLL_HoaDon.Instance.ShowInfor(txbSearchBill.Text, dtpkDate.Value, cbbTime.SelectedIndex + 1, txbSearchBill.Text);
            if (txbChiPhi.Text != "")
            {
                Temp = Tong - Convert.ToDouble(txbChiPhi.Text);
                txbTongTien.Text = Temp.ToString();
                //txbTongTien.Text = (BLL_HoaDon.Instance.ChiPhi_Change(b, Convert.ToDouble(txbChiPhi.Text), Tong)).ToString();
                
            }
        }
        private void txbDiscount_TextChanged(object sender, EventArgs e)
        {
            BILL b = BLL_HoaDon.Instance.ShowInfor(txbSearchBill.Text, dtpkDate.Value, cbbTime.SelectedIndex + 1, txbSearchBill.Text);
            if (txbDiscount.Text != "")
            {
                txbTongTien.Text = (Temp - (Temp * (Convert.ToInt32(txbDiscount.Text))/100)).ToString();
            }
        }

        private void txbSearchBill_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbChiPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
