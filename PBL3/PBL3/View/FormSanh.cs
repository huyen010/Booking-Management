using PBL3.BLL;
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
    public partial class FormSanh : Form
    {
        public FormSanh(ACCOUNT acc)
        {
            InitializeComponent();
            SetCBB();
            checkAccount(acc);
        }
        public void checkAccount(ACCOUNT a)
        {
            if (a.TypeAcc.Equals("Staff"))
            {
                btnAdd.Visible = false;
                btnDel.Visible = false;
                btnEdit.Visible = false;
                btnReset.Visible = false;
                button1.Visible = false;
            }
        }
        bool check;
        public void SetCBB()
        {
            comboBox1.Items.Add(new CBBItem { Text = "All", Value = 0 });
            using (CSDL db = new CSDL())
            {
                foreach (SANH i in db.SANHs)
                {
                    comboBox1.Items.Add(new CBBItem
                    {
                        Text = i.NameSanh,
                        Value = i.IDSanh
                    });
                }
            }
            comboBox1.SelectedIndex = 0;
        }
        private void ShowDG(int ID)
        {
            dataGridView1.DataSource = BLL_Sanh.Instance.GetListSanh(ID);
        }

        private void UpdateSQL()
        {
            SANH s = new SANH();
            if (txtID.Text == "") s.IDSanh = 0;
            else s.IDSanh = Convert.ToInt32(txtID.Text);

            s.NameSanh = txtName.Text;
            s.PriceSanh = Convert.ToInt32(txtPrice.Text);
            s.SIZE = Convert.ToInt32(txtSize.Text);
            MemoryStream ms = new MemoryStream();
            if (pictureBox1.Image == null)
            {
                s.PhotoSanh = null;
            }
            else
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                s.PhotoSanh = ms.ToArray();
            }
            ms.Dispose();
            if (txtName.Text == "" || txtSize.Text == "")
            {
                MessageBox.Show("Xem lai thong tin");
            }
            else
            {
                if (check == true)
                {
                    if (BLL_Sanh.Instance.GetSanh(s.IDSanh) == null)
                    {
                        BLL_Sanh.Instance.AddSanh(s);
                        comboBox1.Items.Add(new CBBItem { Text = s.NameSanh, Value = s.IDSanh });
                    }
                    else MessageBox.Show("ID Sanh da ton tai");
                }
                else
                {
                    if (BLL_Sanh.Instance.GetSanh(s.IDSanh) != null)
                    {
                        BLL_Sanh.Instance.EditSanh(s);
                        comboBox1.Items.Clear();
                        SetCBB();
                    }
                    else MessageBox.Show("Sanh khong ton tai");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                check = true;
                UpdateSQL();
                ShowDG(0);
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                check = false;
                UpdateSQL();
                ShowDG(0);
            }
            catch { }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection data = dataGridView1.SelectedRows;
            List<int> listID = new List<int>();

            foreach (DataGridViewRow i in data)
            {
                listID.Add(Convert.ToInt32(i.Cells["IDSanh"].Value.ToString()));
            }
            BLL_Sanh.Instance.DelSanh(listID);
            ShowDG(0);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtSize.Text = "";
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = null;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image files (*.png;*.jpeg; *.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            DialogResult r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                filepath = f.FileName;
            }
            if (filepath == null) return;
            pictureBox1.Image = Image.FromFile(filepath.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IDSanh"].Value.ToString());
                SANH s = BLL_Sanh.Instance.GetSanh(ID);
                txtID.Text = ID.ToString();
                txtName.Text = s.NameSanh;
                txtPrice.Text = s.PriceSanh.ToString();
                txtSize.Text = s.SIZE.ToString();
                pictureBox1.Image = null;
                if (s.PhotoSanh != null)
                {

                    MemoryStream ms = new MemoryStream(s.PhotoSanh);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void FormSanh_Load(object sender, EventArgs e)
        {
            ShowDG(0);
            dataGridView1.Columns[0].HeaderText = "Mã Sảnh";
            dataGridView1.Columns[1].HeaderText = "Tên Sảnh";
            dataGridView1.Columns[2].HeaderText = "Số lượng bàn/sảnh";
            dataGridView1.Columns[3].HeaderText = "Giá Sảnh";
            dataGridView1.Columns["PhotoSanh"].Visible = false;
            dataGridView1.Columns["BILLs"].Visible = false;
            txtID.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = ((CBBItem)comboBox1.SelectedItem).Value;
            ShowDG(ID);
        }
        public void SetCBBSort()
        {
            int count = 0;
            cbbSort.Items.Add(new CBBItem { Text = "Giá Sảnh", Value = count++ });
            cbbSort.Items.Add(new CBBItem { Text = "Tên Sảnh", Value = count++ });
            cbbSort.Items.Add(new CBBItem { Text = "Số lượng bàn", Value = count++ });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<SANH> s = BLL_Sanh.Instance.GetListSanh(0);
            BLL_Sanh.Instance.SortSanh(s);
            dataGridView1.DataSource = s;
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
