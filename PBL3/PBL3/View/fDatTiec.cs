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
using PBL3.DAL;
namespace PBL3
{
    public partial class fDatTiec : Form
    {
        public static int ID_bill;
        public static int ID_kh;
        public fDatTiec(int id_bill, string id_kh)
        {
            InitializeComponent();
            ID_bill = id_bill;
            ID_kh = Convert.ToInt32(id_kh);
            SetTTForm();
            AddCBBPT();
            SetTXBTT(txbTD.Text, txbGiaSanh.Text, txbGiatiec.Text);
        }
        public void SetTTForm()
        {
            txbName.Text = BLL_KhachHang.INSTANCE.viewCustomer(ID_kh).NameKH;
            txbCMND.Text = BLL_KhachHang.INSTANCE.viewCustomer(ID_kh).CMND;
            cbbCaTC.SelectedIndex = 0;
            txbTD.Text = 0.ToString();
            dtpNgayDat.Value = DateTime.Now;
            dtpTimeIn.MinDate = DateTime.Now;
            dtpTimeIn.MaxDate = DateTime.Now.AddDays(90);
        }
        public void AddCBBPT()
        {
            cbbParty.Items.AddRange(BLL_Party.Instance.AddCBBParty().ToArray());
            cbbParty.SelectedIndex = 0;
        }
        private void AddCBBsanh(DateTime NgayTC, int caTC, int sl)
        {
            cbbChonSanh.Items.AddRange(BLL_Sanh.Instance.AddCBBsanh(dtpTimeIn.Value, cbbCaTC.SelectedIndex, Convert.ToInt32(txbSL.Text)).ToArray());
            try
            {
                cbbChonSanh.SelectedIndex = 0;
            }
            catch { }
        }
        private void btnChonMon_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(txbSL.Text);
                if(Convert.ToInt32(txbSL.Text) != 0){
                    Menu menu = new Menu(ID_bill);
                    menu.tt = GiaThucDon;
                    menu.ShowDialog();
                }
            }
            catch { MessageBox.Show("Vui lòng nhập số bàn"); }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txbSL.Text != "" && txbDatCoc.Text != "" && txbTD.Text != 0.ToString() && txbGiaSanh.Text != "")
            {
                BILL bill = new BILL { IDBILL = ID_bill, IDKH = ID_kh, Time = cbbCaTC.SelectedIndex + 1, DATCOC = (float)Convert.ToDouble(txbDatCoc.Text), IDPARTY = ((CBBItem)cbbParty.SelectedItem).Value, BookingDate = dtpTimeIn.Value, CreateDate = dtpNgayDat.Value, IDSanh = ((CBBItem)cbbChonSanh.SelectedItem).Value, Quantity = Convert.ToInt32(txbSL.Text), TOTAL = (float)Convert.ToDouble(txbTongTien.Text) };
                BLL_Bill.INSTANCE.UpdateBill(bill);
                MessageBox.Show("Đã lưu thông tin tiệc");
                BillDattiec blldt = new BillDattiec(ID_bill);
                blldt.ShowDialog();
                this.Close();
            }
            else { MessageBox.Show("Nhập đầy đủ thông tin"); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                BLL_MENUDETAIL.Instance.deleteMenuDetail(ID_bill);
                BLL_Bill.INSTANCE.DeleteBill(ID_bill);
                MessageBox.Show("Hủy tiệc thành công");
                BLL_KhachHang.INSTANCE.DeleteKH(ID_kh);
            }
            catch { }
            this.Close();
        }
        private void SetTXBTT(string txtboxTD, string txbGiasanh, string txbGiaTiec)
        {
            decimal a = 0, b = 0, c = 0;
            try
            {
                a = Convert.ToDecimal(txtboxTD);
                c = Convert.ToDecimal(txbGiaTiec);
                b = Convert.ToDecimal(txbGiasanh);
            }
            catch { }
            txbTongTien.Text = (a + b + c).ToString();
        }
        public void GiaThucDon(float a)
        {
            txbTD.Text = (Convert.ToInt32(txbSL.Text) *(decimal)a).ToString();
        }

        private void txbSL_TextChanged(object sender, EventArgs e)
        {
            cbbChonSanh.Items.Clear();
            if (txbSL.Text != "")
            {
                AddCBBsanh(dtpTimeIn.Value, cbbCaTC.SelectedIndex, Convert.ToInt32(txbSL.Text));
            }
        }

        private void dtpTimeIn_ValueChanged(object sender, EventArgs e)
        {
            cbbChonSanh.Items.Clear();
            if (txbSL.Text != "")
            {
                AddCBBsanh(dtpTimeIn.Value, cbbCaTC.SelectedIndex, Convert.ToInt32(txbSL.Text));
            }
        }

        private void cbbCaTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbChonSanh.Items.Clear();
            if (txbSL.Text != "")
            {
                AddCBBsanh(dtpTimeIn.Value, cbbCaTC.SelectedIndex, Convert.ToInt32(txbSL.Text));
            }
        }

        private void txbGiatiec_TextChanged(object sender, EventArgs e)
        {
            SetTXBTT(txbTD.Text, txbGiaSanh.Text, txbGiatiec.Text);
        }

        private void txbTongTien_TextChanged(object sender, EventArgs e)
        {
            txbDatCoc.Text = (Convert.ToDecimal(txbTongTien.Text) * 25 / 100).ToString();
        }

        private void cbbChonSanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbGiaSanh.Text = BLL_Sanh.Instance.GetSanh(((CBBItem)cbbChonSanh.SelectedItem).Value).PriceSanh.ToString();

        }

        private void cbbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbGiatiec.Text = BLL_Party.Instance.GetParty(((CBBItem)cbbParty.SelectedItem).Value).PricePT.ToString();

        }

        private void txbGiaSanh_TextChanged(object sender, EventArgs e)
        {
            SetTXBTT(txbTD.Text, txbGiaSanh.Text, txbGiatiec.Text);
        }

        private void txbSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
