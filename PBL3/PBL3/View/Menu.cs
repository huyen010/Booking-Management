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
    public partial class Menu : Form
    {
        public delegate void PriceMenu(float total);
        public PriceMenu tt { get; set; }
        public int ID_bill { get; set; }
        public Menu(int idbill)
        {
            InitializeComponent();
            ID_bill = idbill;
            ADDCBB_LoaiMon();
            LoadDGV();
            decimal ttien = (decimal)BLL_MENUDETAIL.Instance.getTongTienMenuBan(ID_bill);
            txtTotal.Text = ttien.ToString();
        }
        public void ADDCBB_LoaiMon()
        {
            cbLoaiMon.Items.AddRange(BLL_MENUDETAIL.Instance.ADD_CBBFOODCBB(-1).ToArray());
            cbLoaiMon.SelectedIndex = 0;
        }
        public void ADDCBB_Mon()
        {
            cbbTenMon.Items.AddRange(BLL_MENUDETAIL.Instance.ADD_CBBFOODCBB(((CBBItem)cbLoaiMon.SelectedItem).Value).ToArray());
            cbbTenMon.SelectedIndex = 0;
        }
        private void ThongTinMon(int id)
        {
            FOOD f = BLL_MENUDETAIL.Instance.GetFood(id);
            txbGia.Text = f.PriceFood.ToString();
            txbNguyenLieu.Text = f.Material;
            try
            {
                ptbAnh.Image = (Bitmap)(new ImageConverter()).ConvertFrom(f.PhotoFood);
            }
            catch(Exception)
            {

            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (tt != null) tt(Convert.ToInt32(txtTotal.Text));
        }
        private void LoadDGV()
        {
            dgvDatMon.DataSource = BLL_MENUDETAIL.Instance.getListMenuF(ID_bill);
            dgvDatMon.Columns["IdBill"].Visible = false;
            dgvDatMon.Columns["IDFood"].Visible = false;
            dgvDatMon.Columns["NameFood"].HeaderText = "Tên món";
            dgvDatMon.Columns["PriceFood"].HeaderText = "Giá món";
            dgvDatMon.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDatMon.Columns["TongTien"].HeaderText = "Thành tiền";
        }
        private void setTXB()
        {
            float ttien = BLL_MENUDETAIL.Instance.getTongTienMenuBan(ID_bill);
            txtTotal.Text = ((decimal)ttien).ToString();
            tt(ttien);
        }

        private void cbLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbTenMon.Items.Clear();
            ADDCBB_Mon();
            cbbTenMon.SelectedIndex = 0;
        }

        private void cbbTenMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThongTinMon(((CBBItem)cbbTenMon.SelectedItem).Value);
            if (BLL_MENUDETAIL.Instance.checkFoodinMN(ID_bill, ((CBBItem)cbbTenMon.SelectedItem).Value) != null)
            {
                txbSL.Text = BLL_MENUDETAIL.Instance.checkFoodinMN(ID_bill, ((CBBItem)cbbTenMon.SelectedItem).Value).SLFood.ToString();

            }
            else
            {
                txbSL.Text = "";
            }
        }

        private void buttonDatMon_Click(object sender, EventArgs e)
        {
            int idf = ((CBBItem)cbbTenMon.SelectedItem).Value;
            FOOD f = BLL_MENUDETAIL.Instance.GetFood(idf);
            if (txbSL.Text != "" || txbSL.Text != 0.ToString())
            {
                if (BLL_MENUDETAIL.Instance.checkFoodinMN(ID_bill, idf) == null)
                {
                    MENUDETAIL mnf = new MENUDETAIL { IDBILL = ID_bill, IDFood = f.IDFood, SLFood = Convert.ToInt32(txbSL.Text), ThanhTien = f.PriceFood, TongTien = Convert.ToInt32(txbSL.Text) * f.PriceFood };
                    BLL_MENUDETAIL.Instance.AddMenu(mnf);
                }
                else
                {
                    BLL_MENUDETAIL.Instance.upDateMenu(ID_bill, idf, Convert.ToInt32(txbSL.Text));
                }
                LoadDGV();
                setTXB();
            }
            else { MessageBox.Show("Nhập số lượng phù hợp "); }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            int idf = ((CBBItem)cbbTenMon.SelectedItem).Value;
            if (BLL_MENUDETAIL.Instance.checkFoodinMN(ID_bill, idf) == null)
            {
                MessageBox.Show("Món cần xóa không tồn tại trong thực đơn");
            }
            else
            {
                BLL_MENUDETAIL.Instance.DeleteMenu(ID_bill, idf);
                txbSL.Text = "";
                LoadDGV();
                setTXB();
            }
        }

        private void txbSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDatMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatMon.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDatMon.CurrentRow.Cells["IDFOOD"].Value.ToString());
                FOOD f = BLL_MENUDETAIL.Instance.GetFood(id);
                cbbTenMon.Text = f.NameFood;
                cbLoaiMon.Text = f.FOODCATEGORY.NameFCategory;
                ThongTinMon(id);
                //ptbAnh.Image = (Bitmap)(new ImageConverter()).ConvertFrom(f.PhotoFood);
            }
        }
    }
}
