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
using PBL3.BLL;
namespace PBL3
{
    public partial class ThongkeHD : Form
    {
        CSDL db = new CSDL();
        public ThongkeHD()
        {
            InitializeComponent();
            LoadDate();
            AddCBB();
            LoadAllData();
        }
        void LoadData()
        {
            string stt = ((CBBItem)comboBox1.SelectedItem).Text;
            DateTime DateIn = dateTimePicker1.Value;
            DateTime DateOut = dateTimePicker2.Value;
            dgvHoaDon.DataSource = BLL_Bill.INSTANCE.Search(DateIn,DateOut,stt);
            NameColumn();
        }
        void NameColumn()
        {
            dgvHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHoaDon.Columns[1].HeaderText = "Tên khách hàng";
            dgvHoaDon.Columns[2].HeaderText = "CMND";
            dgvHoaDon.Columns[3].HeaderText = "SĐT";
            dgvHoaDon.Columns[4].HeaderText = "Loại tiệc";
            dgvHoaDon.Columns[5].HeaderText = "Sảnh";
            dgvHoaDon.Columns[6].HeaderText = "Số lượng khách";
            dgvHoaDon.Columns[7].HeaderText = "Tình trạng";
            dgvHoaDon.Columns[8].HeaderText = "Ngày tổ chức";
            dgvHoaDon.Columns[9].HeaderText = "Tổng tiền";
        }
        void LoadAllData()
        {
            DateTime DateIn = dateTimePicker1.Value;
            DateTime DateOut = dateTimePicker2.Value;
            dgvHoaDon.DataSource = BLL_Bill.INSTANCE.ThongkeHD(DateIn,DateOut);
            NameColumn();
        }
        void LoadMenu()
        {
            int id = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells["IDBILL"].Value.ToString());
            dgvThucDon.DataSource = BLL_MENUDETAIL.Instance.getListMenuF(id);
            dgvThucDon.Columns["IDFood"].Visible = false;
            dgvThucDon.Columns["IDBill"].Visible = false; ;
            dgvThucDon.Columns["NameFood"].HeaderText = "Tên món";
            dgvThucDon.Columns["PriceFood"].HeaderText = "Đơn giá";
            dgvThucDon.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvThucDon.Columns["TongTien"].HeaderText = "Thành tiền";


        }
        void AddCBB()
        {
            comboBox1.Items.Add(new CBBItem { Value = 0, Text = "Tất cả" });
            comboBox1.Items.Add(new CBBItem { Value = 1, Text = "Hoàn Thành" });
            comboBox1.Items.Add(new CBBItem { Value = 2, Text = "Chưa hoàn thành" });
            comboBox2.Items.Add(new CBBItem { Value = 0, Text = "Tất cả" });
            comboBox2.Items.AddRange(BLL_Party.Instance.AddCBBParty().ToArray());
        }
        void LoadDate()
        {
            DateTime today = DateTime.Now;
            dateTimePicker1.Value = new DateTime(today.Year, today.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
        }
        int CountParty(string par)
        {
            int dem = 0;
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells["TIEC"].Value.ToString() == par)
                {
                    dem++;
                }
            }

            return dem;
        }
        void showCount()
        {
            string result = (((CBBItem)comboBox2.SelectedItem).Text);
            if (((CBBItem)comboBox2.SelectedItem).Value != 0)
            {
                label1.Text = CountParty(result).ToString();
            }
            else label1.Text = dgvHoaDon.Rows.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (((CBBItem)comboBox1.SelectedItem).Value == 0)
                LoadAllData();
            else LoadData();
            showCount();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadMenu();
        }

        private void ThongkeHD_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label1.Text = dgvHoaDon.Rows.Count.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            showCount();
        }
    }
}
