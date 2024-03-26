using QuanLyBanHang1.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang1
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void fDangNhap_Load(object sender, EventArgs e)
        {

        }

      

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = textBoxTenDangNhap.Text;
            string matKhau = textBoxMatKhau.Text;
            if (login(taiKhoan, matKhau))
            {
                fDangNhapLaNhanVien f = new fDangNhapLaNhanVien();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            }
            

        }



        private void btlDangNhapAdmin_Click(object sender, EventArgs e)
        {
            fDangNhapLaAdmin f = new fDangNhapLaAdmin();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {

        }

        public bool login(string taiKhoan, string matKhau)
        {

            return TaiKhoanDAO.Instance.login(taiKhoan, matKhau);
        }
    }
}
