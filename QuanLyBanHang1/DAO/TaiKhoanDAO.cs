using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang1.DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;

        public static TaiKhoanDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TaiKhoanDAO();
                }
                return instance;

            }
            private set => instance = value;
        }

        public bool Login(string taiKhoan, string matKhau)
        {

            string query = "LayTaiKhoanUsers @taiKhoan , @matKhau";

            DataTable ketQua = DataProvaider.Instance.ExcuteQuery(query, new object[] { taiKhoan , matKhau });

            return ketQua.Rows.Count>0;
        }

        public bool LoginAdmin(string taiKhoan, string matKhau)
        {

            string query = "LayTaiKhoanAdmin @taiKhoan , @matKhau";

            DataTable ketQua = DataProvaider.Instance.ExcuteQuery(query, new object[] { taiKhoan, matKhau });

            return ketQua.Rows.Count > 0;
        }

    }
}
