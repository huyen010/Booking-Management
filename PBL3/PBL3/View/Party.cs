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

namespace PBL3.View
{
    public partial class Party : Form
    {
        public Party(ACCOUNT acc)
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
        private void ShowDG(int ID)
        {
            dataGridView1.DataSource = BLL_Party.Instance.GetListParty(ID);
        }
        public void SetCBB()
        {
            comboBox1.Items.Add(new CBBItem { Text = "All", Value = 0 });
            using (CSDL db = new CSDL())
            {
                foreach (PARTY i in BLL_Party.Instance.GetListParty(0))
                {
                    comboBox1.Items.Add(new CBBItem
                    {
                        Text = i.NamePT,
                        Value = i.IDPARTY
                    });
                }
            }
            comboBox1.SelectedIndex = 0;
        }
        private void UpdateSQL()
        {
            PARTY p = new PARTY();
            if (txtID.Text == "") p.IDPARTY = 0;
            else p.IDPARTY = Convert.ToInt32(txtID.Text);
            p.NamePT = txtName.Text;
            p.PricePT = Convert.ToInt32(txtPrice.Text);
            MemoryStream ms = new MemoryStream();
            if (pictureBox1.Image == null)
            {
                p.PhotoParty = null;
            }
            else
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                p.PhotoParty = ms.ToArray();
            }
            ms.Dispose();
            if (txtName.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Xem lai thong tin");
            }
            else
            {
                if (check == true)
                {
                    if (BLL_Party.Instance.GetParty(p.IDPARTY) == null)
                    {
                        BLL_Party.Instance.AddParty(p);
                        comboBox1.Items.Add(new CBBItem { Text = p.NamePT, Value = p.IDPARTY });
                    }
                    else MessageBox.Show("ID Party da ton tai");
                }
                else
                {
                    if (BLL_Party.Instance.GetParty(p.IDPARTY) != null)
                    {
                        BLL_Party.Instance.EditParty(p);
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
            List<int> ID = new List<int>();
            foreach (DataGridViewRow i in data)
            {
                ID.Add(Convert.ToInt32(i.Cells["IDPARTY"].Value.ToString()));
            }
            BLL_Party.Instance.DelParty(ID);
            ShowDG(0);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IDPARTY"].Value.ToString());
                PARTY p = BLL_Party.Instance.GetParty(ID);
                txtID.Text = ID.ToString();
                txtName.Text = p.NamePT;
                txtPrice.Text = p.PricePT.ToString();
                pictureBox1.Image = null;
                if (p.PhotoParty != null)
                {
                    MemoryStream ms = new MemoryStream(p.PhotoParty);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void Party_Load(object sender, EventArgs e)
        {
            ShowDG(0);
            dataGridView1.Columns["PhotoParty"].Visible = false;
            dataGridView1.Columns["BILLs"].Visible = false;
            dataGridView1.Columns["IDPARTY"].HeaderText = "Mã Tiệc";
            dataGridView1.Columns["NamePT"].HeaderText = "Tên Tiệc";
            dataGridView1.Columns["PricePT"].HeaderText = "Giá Tiệc";
            txtID.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = ((CBBItem)comboBox1.SelectedItem).Value;
            ShowDG(ID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<PARTY> s = BLL_Party.Instance.GetListParty(0);
            BLL_Party.Instance.SortPT(s);
            dataGridView1.DataSource = s;
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
