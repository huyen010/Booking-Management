using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PBL3
{
    public partial class ThongkeTK : Form
    {
        CSDL db = new CSDL();
        public ThongkeTK()
        {
            InitializeComponent();
            LoadData();
            AddBinding();
        }
        void AddBinding()
        {
            txbIDNV.DataBindings.Clear();
            txbIDNV.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "IDTK", true, DataSourceUpdateMode.Never));
            txbNameNV.DataBindings.Clear();
            txbNameNV.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "NAME", true, DataSourceUpdateMode.Never));
            txbChucVu.DataBindings.Clear();
            txbChucVu.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "CHUCVU", true, DataSourceUpdateMode.Never));
            comboBox1.DataBindings.Clear();
            comboBox1.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "TypeAcc", true, DataSourceUpdateMode.Never));
            txbSDT.DataBindings.Clear();
            txbSDT.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            txbEmail.DataBindings.Clear();
            txbEmail.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "EMAIL", true, DataSourceUpdateMode.Never));
            txbCMND.DataBindings.Clear();
            txbCMND.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "CMND", true, DataSourceUpdateMode.Never));
            txbTK.DataBindings.Clear();
            txbTK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "USERNAME", true, DataSourceUpdateMode.Never));
            txbMK.DataBindings.Clear();
            txbMK.DataBindings.Add(new Binding("Text", dgvTK.DataSource, "PASS", true, DataSourceUpdateMode.Never));
            pictureBox1.DataBindings.Clear();
            pictureBox1.DataBindings.Add(new Binding("Image", dgvTK.DataSource, "PhotoAC", true, DataSourceUpdateMode.Never));
        }

        void LoadData()
        {
            dgvTK.DataSource = BLL_Account.INSTANCE.ShowACCOUNT();
            dgvTK.Columns["PhotoAC"].Visible = false;
            dgvTK.Columns["BILLs"].Visible = false;
            NameColumn();
        }
        void DelTK()
        {
            try
            {
                int id = Convert.ToInt32(dgvTK.CurrentRow.Cells["IDTK"].Value.ToString());
                BLL_Account.INSTANCE.DelACCOUNT(id);
                LoadData();
            }
            catch { }

        }
        void SearchTK()
        {
            dgvTK.DataSource = BLL_Account.INSTANCE.SearchTK(txbSearch.Text);
            NameColumn();

        }
        void ClearTK()
        {
            txbCMND.Clear();
            txbEmail.Clear();
            txbIDNV.Clear();
            txbMK.Clear();
            txbNameNV.Clear();
            txbSDT.Clear();
            txbSearch.Clear();
            txbTK.Clear();
            txbChucVu.Clear();
            comboBox1.Text = "";
            pictureBox1.Image = null;
        }
        void NameColumn()
        {
            dgvTK.Columns["IDTK"].HeaderText = "Mã tài khoản";
            dgvTK.Columns["NAME"].HeaderText = "Tên nhân viên";
            dgvTK.Columns["CHUCVU"].HeaderText = "Chức vụ";
            dgvTK.Columns["TypeAcc"].HeaderText = "Loại tài khoản";
            dgvTK.Columns["USERNAME"].HeaderText = "Tên đăng nhập";
            dgvTK.Columns["PASS"].HeaderText = "Mật khẩu";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SearchTK();
            AddBinding();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] byteIMG = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                if (txbNameNV.Text != "" && txbChucVu.Text != "" && txbSDT.Text != "" && txbCMND.Text != "" && txbTK.Text != "" && txbMK.Text != "")
                {
                    
                    ACCOUNT acc = new ACCOUNT()
                    {
                        NAME = txbNameNV.Text,
                        CHUCVU = txbChucVu.Text,
                        TypeAcc = comboBox1.Text,
                        SDT = txbSDT.Text,
                        EMAIL = txbEmail.Text,
                        CMND = txbCMND.Text,
                        USERNAME = txbTK.Text,
                        PASS = txbMK.Text,
                        PhotoAC = byteIMG,
                    };
                    if (txbIDNV.Text == "") BLL_Account.INSTANCE.AddACCOUNT(acc);
                    else MessageBox.Show("Xem lai ma tai khoan");
                }
                else MessageBox.Show("Xem lai thong tin");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Hãy nhập lại");
            }
            finally
            {
                AddBinding();
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTK.SelectedCells[0].OwningRow.Cells[0].Value.ToString());
            byte[] byteIMG = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
            try
            {
                if (txbNameNV.Text != "" && txbChucVu.Text != "" && txbSDT.Text != "" && txbCMND.Text != "" && txbTK.Text != "" && txbMK.Text != "")
                {
                    ACCOUNT acc = new ACCOUNT
                    {
                        IDTK = id,
                        NAME = txbNameNV.Text,
                        CHUCVU = txbChucVu.Text,
                        TypeAcc = comboBox1.Text,
                        SDT = txbSDT.Text,
                        EMAIL = txbEmail.Text,
                        CMND = txbCMND.Text,
                        USERNAME = txbTK.Text,
                        PASS = txbMK.Text,
                        PhotoAC = byteIMG,
                    };
                    BLL_Account.INSTANCE.EditACCOUNT(acc);
                }
                else MessageBox.Show("Xem lai thong tin");
                LoadData();
            }
            catch
            {
                MessageBox.Show("Hãy nhập lại");
            }
            finally
            {
                AddBinding();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DelTK();
            }
            catch
            {
                MessageBox.Show("Hãy làm lại");
            }
            finally
            {
                AddBinding();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearTK();
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            string image;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                image = openFile.FileName;
                FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                byte[] bicyle = new byte[fs.Length];
                fs.Read(bicyle, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                pictureBox1.Image = (Bitmap)(new ImageConverter()).ConvertFrom(bicyle);
            }
        }

        private void txbIDNV_TextChanged(object sender, EventArgs e)
        {
            if (txbIDNV.Text != "")
            {
                ACCOUNT acc = db.ACCOUNTs.Find(Convert.ToInt32(txbIDNV.Text));
                try
                {
                    pictureBox1.Image = (Bitmap)(new ImageConverter()).ConvertFrom(acc.PhotoAC);
                }
                catch {
                    pictureBox1.Image = null;
                }
            }
        }

        private void txbNameNV_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
