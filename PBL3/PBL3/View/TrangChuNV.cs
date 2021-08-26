using PBL3.View;
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
    public partial class TrangChuNV : Form
    {
        public static ACCOUNT aCC;
        public TrangChuNV(ACCOUNT acc)
        {
            InitializeComponent();
            aCC = acc;
            checkACC();
        }
        public void checkACC()
        {
            if (aCC.TypeAcc.Equals("Staff"))
            {
                quảnLýToolStripMenuItem.Visible = false;
            }
        }
        private void AddForm(Form f)
        {
            this.panel1.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(f);
            f.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kháchHàngToolStripMenuItem.BackColor = Color.LightSkyBlue;
            fKhachHang f = new fKhachHang();
            AddForm(f);
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTC();
        }
        public void LoadTC()
        {
            TrangChu f = new TrangChu();
            AddForm(f);
        }

        private void TrangChuNV_Load(object sender, EventArgs e)
        {
            LoadTC();
        }

        private void hoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon f = new HoaDon(aCC);
            AddForm(f);
        }

        private void thôngTinSảnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSanh f = new FormSanh(aCC);
            AddForm(f);
        }

        private void thôngTinPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Party f = new Party(aCC);
            AddForm(f);
        }

        private void thôngTinThựcĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThucDon f = new ThucDon( aCC);
            AddForm(f);
        }

        private void thốngKêHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongkeHD f = new ThongkeHD();
            AddForm(f);
        }

        private void thốngKêKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongkeKH f = new ThongkeKH();
            AddForm(f);
        }

        private void thốngKêDanhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongkeTK f = new ThongkeTK();
            AddForm(f);
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersionalInfor f = new PersionalInfor(aCC);
            AddForm(f);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn đăng xuất khỏi hệ thống?", "Thông báo",MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
