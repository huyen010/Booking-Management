using PBL3.DAL;
using PBL3.BLL;
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
    public partial class ThongkeKH : Form
    {
        CSDL db = new CSDL();
        public ThongkeKH()
        {
            InitializeComponent();
            LoadDate();
            
        }
        void LoadHD()
        {
            DateTime DateIn = dateTimePicker1.Value;
            DateTime DateOut = dateTimePicker2.Value;
            dgvThongKeKH.DataSource = BLL_KhachHang.INSTANCE.GetListKH(DateIn, DateOut);
            NameColumn();
            txbTotal.Text = "0";
            for (int i = 0; i < dgvThongKeKH.Rows.Count; i++)
            {
                txbTotal.Text = Convert.ToString(int.Parse(txbTotal.Text) +
                    int.Parse(dgvThongKeKH.Rows[i].Cells["TOTAL"].Value.ToString()));
            }
        }
        void NameColumn()
        {
            dgvThongKeKH.Columns[0].HeaderText = "Mã hóa đơn";
            dgvThongKeKH.Columns[1].HeaderText = "Tên khách hàng";
            dgvThongKeKH.Columns[2].HeaderText = "CMND";
            dgvThongKeKH.Columns[3].HeaderText = "SĐT";
            dgvThongKeKH.Columns[4].HeaderText = "Địa chỉ";
            dgvThongKeKH.Columns[5].HeaderText = "Ngày hoàn thành";
            dgvThongKeKH.Columns[6].HeaderText = "Tổng tiền";
        }
        void LoadDate()
        {
            DateTime today = DateTime.Now;
            dateTimePicker1.Value = new DateTime(today.Year, today.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadHD();
        }

        private void ThongkeKH_Load(object sender, EventArgs e)
        {
            LoadHD();
        }
    }
}
