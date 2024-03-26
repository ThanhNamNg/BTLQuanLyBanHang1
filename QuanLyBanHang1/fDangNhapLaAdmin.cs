using QuanLyBanHang1.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang1
{
    public partial class fDangNhapLaAdmin : Form
    {
        public fDangNhapLaAdmin()
        {
            InitializeComponent();
            LoadTKMK();
        }

        public void LoadTKMK()
        {

            string query = "EXEC GetUsersByTaiKhoan @TaiKhoan";

          

            dataGridViewTKMK.DataSource = DataProvaider.Instance.ExcuteQuery(query, new object[] { "NguyenThanhNam" });


        }

       
    }


}
