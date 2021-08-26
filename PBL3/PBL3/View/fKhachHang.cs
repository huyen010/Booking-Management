using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.DAL;
using PBL3.BLL;
namespace PBL3
{
    public partial class fKhachHang : Form
    {
        public fKhachHang()
        {
            InitializeComponent();
            cbbTimKh.SelectedIndex = 0;
        }
        private void btnDat_Click(object sender, EventArgs e)
        {
            if (txbIDKH.Text != "")
            {
                BILL bill = new BILL { IDKH = Convert.ToInt32(txbIDKH.Text), INCURCOST = 0, DATCOC = 0, DISCOUNT = 0, STATUS = "Chưa hoàn thành", INCUR = "Khong co" };
                BLL_Bill.INSTANCE.AddBILL(bill);
                fDatTiec dt = new fDatTiec(bill.IDBILL, txbIDKH.Text);
                this.Hide();
                dt.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Nhập khách hàng cần đặt");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type = cbbTimKh.SelectedItem.ToString();
            DGVKHLoad(type, txbTimKH.Text);
            txbTimKH.Text = "";
        }
        public void DGVKHLoad(string type, string text)
        {
            List<CUSTOMER> list = BLL_KhachHang.INSTANCE.TimKH(type, text);
            if (list.Count == 0)
            {
                MessageBox.Show("Không tồn tại khách hàng");
            }
            else
            {
                dgvKH.DataSource = list;
                dgvKH.Columns["NameKH"].HeaderText = "Tên KH";
                dgvKH.Columns["SDT"].HeaderText = "SĐT";
                dgvKH.Columns["DIACHI"].HeaderText = "Địa chỉ";
                dgvKH.Columns["IDKH"].Visible = false;
                dgvKH.Columns["BILLs"].Visible = false;
                txbCMND.Enabled = false;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txbTenKH.Text != "" && txbSDT.Text != "" && txbCMND.Text != "" && txbDiaChi.Text != "")
            {
                CUSTOMER kh = new CUSTOMER { NameKH = txbTenKH.Text, SDT = txbSDT.Text, CMND = txbCMND.Text, DIACHI = txbDiaChi.Text };
                BLL_KhachHang.INSTANCE.ADD_KHACHHANG(kh);
                MessageBox.Show("Thêm thành công");
                txbIDKH.Text = kh.IDKH.ToString();
                DGVKHLoad("SDT", txbSDT.Text);
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txbTenKH.Text != "" && txbSDT.Text != "" && txbCMND.Text != "" && txbDiaChi.Text != "")
            {
                CUSTOMER ctm = new CUSTOMER { IDKH = Convert.ToInt32(txbIDKH.Text), NameKH = txbTenKH.Text, SDT = txbSDT.Text, CMND = txbCMND.Text, DIACHI = txbDiaChi.Text };
                BLL_KhachHang.INSTANCE.EditKH(ctm);
                MessageBox.Show("Cập nhật thành công");
                DGVKHLoad("CMND", txbCMND.Text);
            }
            else { MessageBox.Show("Nhập đầy đủ thông tin!"); }
        }
        private void Reset()
        {
            txbIDKH.Text = "";
            txbCMND.Text = "";
            txbCMND.Enabled = true;
            txbDiaChi.Text = "";
            txbSDT.Text = "";
            txbTenKH.Text = "";
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txbIDKH.Text != "")
            {
                try
                {
                    BLL_KhachHang.INSTANCE.DeleteKH(Convert.ToInt32(txbIDKH.Text));
                    MessageBox.Show("Xóa thành công");
                    dgvKH.DataSource = null;
                    Reset();
                }
                catch { MessageBox.Show("Khách hàng không thể xóa"); }
            }
            else { MessageBox.Show("Chọn khách hàng cần xóa "); }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dgvKH_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvKH.CurrentRow != null)
            {
                int idkh = Convert.ToInt32(dgvKH.CurrentRow.Cells["IDKH"].Value.ToString());
                CUSTOMER ctm = BLL_KhachHang.INSTANCE.viewCustomer(idkh);
                txbCMND.Text = ctm.CMND;
                txbCMND.Enabled = false;
                txbIDKH.Text = ctm.IDKH.ToString();
                txbDiaChi.Text = ctm.DIACHI;
                txbSDT.Text = ctm.SDT;
                txbTenKH.Text = ctm.NameKH;
                txbThanhVien.Text = BLL_KhachHang.INSTANCE.getChiTieuKH(idkh).ToString();
            }
        }

        private void txbTenKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txbCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
