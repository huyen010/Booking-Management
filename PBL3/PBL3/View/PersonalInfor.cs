using PBL3.BLL;
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

namespace PBL3
{
    public partial class PersionalInfor : Form
    {
        CSDL db = new CSDL();
        ACCOUNT account;
        public PersionalInfor(ACCOUNT acc)
        {
            InitializeComponent();
            account = acc;
            ShowUser();
            CheckAcc();
        }
        public void CheckAcc()
        {
            if (account.TypeAcc.Equals("Staff"))
            {
                txbCMND.Enabled = false;
                btn_LoadImage.Enabled = false;
            }
        }
        public void ShowUser()
        {
            txbSDT.Text = account.SDT;
            txbUsername.Text = account.USERNAME;
            txbPassword.Text = account.PASS;
            txbHoTen.Text = account.NAME;
            txbChucVu.Text = account.CHUCVU;
            txbCMND.Text = account.CMND;
            txbEmail.Text = account.EMAIL;
            txbID.Text = account.IDTK.ToString();
            if(account.PhotoAC != null)
            {
                ptbAvatar.Image = BLL_PersonalInfor.Instance.ConvertBinaryToImage(account.PhotoAC);
            }
            else
            {
                ptbAvatar.Image = null;
            }    
            txbChucVu.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //try
            //{
            BLL_PersonalInfor.Instance.Update(txbID.Text, txbEmail.Text, txbSDT.Text, ptbAvatar.Image);
            //ACCOUNT a = db.ACCOUNTs.Find(Convert.ToInt32(txbID.Text));
            //a.USERNAME = txbUsername.Text;
            //a.PASS = txbPassword.Text;
            //a.NAME = txbHoTen.Text;
            //a.CHUCVU = txbChucVu.Text;
            //a.CMND = txbCMND.Text;
            //a.EMAIL = txbEmail.Text;
            //a.SDT = txbSDT.Text;
            ////a.PhotoAC = BLL_PersonalInfor.Instance.ConvertImageToBinary(ptbAvatar.Image);
            //db.SaveChanges();
            ShowUser();
            //}
            //catch
            //{
            //    MessageBox.Show("Nhap chinh xac thong tin!");
            //}
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ptbAvatar.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
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
