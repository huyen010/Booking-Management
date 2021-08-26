using PBL3.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.BLL
{
    class BLL_PersonalInfor
    {
        CSDL db = new CSDL();
        private static BLL_PersonalInfor _Instance;
        public static BLL_PersonalInfor Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_PersonalInfor();
                return _Instance;
            }
            private set { }
        }
        public void Update(string ID, string Email, string SDT, Image image)
        {
            ACCOUNT a = db.ACCOUNTs.Find(Convert.ToInt32(ID));
            //a.USERNAME = txbUsername.Text;
            //byte[] i = null;
            //if(image != null)
            //{
            //    i = BLL_PersonalInfor.Instance.ConvertImageToBinary(image);
            //}    
            a.EMAIL = Email;
            a.SDT = SDT;
            //a.PhotoAC = i;
            //a.PhotoAC = BLL_PersonalInfor.Instance.ConvertImageToBinary(image);
            db.SaveChanges();
        }
        public Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        public byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
