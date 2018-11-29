﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;

namespace DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;
        public static TaiKhoanDAO INSTANCE
        {
            get
            {
                if (instance == null) return new TaiKhoanDAO();
                return instance;
            }
        }

        public int CheckAccount(string tenTk, string passWd)
        {
            string str = "SELECT dbo.FN_TaiKhoan_CheckLogin('" + tenTk + "','" + passWd + "')";
            return Convert.ToInt32(DataConn.INSTANCE.ExecuteQueryScalar(str));
        }

        public void updateAvatar(string id, byte[] avt)
        {
            string str = "sp_Update_TaiKhoan_AVT";
            DataConn.INSTANCE.ExecuteQueryScalar(str, id , avt, "@avt");
        }

        public DTO.TaiKhoan loadData(string tentk)
        {
            DTO.TaiKhoan tk = new DTO.TaiKhoan();
            DateTime date = new DateTime();
            string str = "SELECT * FROM dbo.FN_TaiKhoan_GetInfo('" + tentk + "')"; //gọi Function
            DataTable data = DataConn.INSTANCE.ExecuteQueryTable(str);
            tk.ID = data.Rows[0]["id"].ToString();
            tk.TENTK = data.Rows[0]["tentk"].ToString();
            tk.PASSWD = BitConverter.ToString((byte[])data.Rows[0]["passwd"]);
            if (data.Rows[0]["avt"].ToString() == "")
                tk.AVT = null;
            else
                tk.AVT = (byte[])data.Rows[0]["avt"];
            tk.HOVATENDEM = data.Rows[0]["hovatendem"].ToString();
            tk.TEN = data.Rows[0]["ten"].ToString();
            date = (DateTime)data.Rows[0]["ngaysinh"];
            tk.NGAYSINH = date.ToString("d", new System.Globalization.CultureInfo("es-ES"));
            tk.DIACHI = data.Rows[0]["diachi"].ToString();
            if (data.Rows[0]["lastlogin"].ToString() == "")
                tk.LASTLOGIN = "";
            else
            {
                date = (DateTime)data.Rows[0]["lastlogin"];
                tk.LASTLOGIN = date.ToString("G", new System.Globalization.CultureInfo("es-ES"));
            }
            date = (DateTime)data.Rows[0]["createday"];
            tk.CREATEDAY = date.ToString("d", new System.Globalization.CultureInfo("es-ES"));
            tk.TINHTRANG = Convert.ToInt32(data.Rows[0]["tinhtrang"]);
            tk.PERS = Convert.ToInt32(data.Rows[0]["pers"]);
            tk.QUANLY = data.Rows[0]["quanly"].ToString();
            tk.TENQUANLY = data.Rows[0]["tenquanly"].ToString();
            return tk;
        }

        public void updateLastLogin(string tentk)
        {
            string str = "EXEC dbo.sp_Update_LastLogin @tentk = '" + tentk + "'";
            DataConn.INSTANCE.ExecuteQueryTable(str);
        }

        public void updateInfo(string id, string hovatendem, string ten, string ngaysinh, string diachi, int pers, int tinhtrang)
        {
            string str = "EXEC dbo.sp_Update_TaiKhoan_Info @id = '" + id + "'," +                       
                            "@hovatendem = N'" + hovatendem + "'," +
                            "@ten = N'" + ten + "'," +
                            "@ngaysinh = '" + DateTime.Parse(ngaysinh) + "'," +
                            "@diachi = N'" + diachi + "'," +
                            "@per = " + pers + "," +
                            "@tinhtrang = " + tinhtrang;
            
            DataConn.INSTANCE.ExecuteQueryTable(str);
        }

        public void updatePW(string id, string opw, string npw)
        {
            string str = "EXEC dbo.sp_Update_TaiKhoan_Password " +
                         "@opw = '" + opw + "'," +
                         "@npw = '" + npw + "'," +
                         "@id = '" + id + "'";
            DataConn.INSTANCE.ExecuteQueryTable(str);
        }
    }
}
