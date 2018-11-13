﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace BUS
{
    public class KhoBUS
    {
        private static KhoBUS instance;
        int index;
        public static KhoBUS Instance
        {
            get
            {
                if (instance == null) return new KhoBUS();
                return instance;
            }
        }
        public void loadData(DataGridView dt)
        {
            dt.DataSource = DAO.KhoDAO.Instance.loadData();
        }

        public List<string> loadDataMNG()
        {
            List<string> lMng = new List<string>();
            foreach (DataRow dr in DAO.KhoDAO.Instance.loadDataMng().Rows)
                lMng.Add(dr[0].ToString());
            return lMng;
        }

        public void saveInsert(DataGridView dgv)
        {
            DTO.Kho k = new DTO.Kho();
            k = new DTO.Kho(
                    dgv.Rows[0].Cells[1].Value.ToString().Trim(),
                    dgv.Rows[0].Cells[2].Value.ToString().Trim(),
                    dgv.Rows[0].Cells[3].Value.ToString().Trim(),
                    Convert.ToInt32(dgv.Rows[0].Cells[4].Value));
            DAO.KhoDAO.Instance.saveInsert(k);
        }

        public void saveEdit(DataGridView dgv)
        {
            List<DTO.Kho> lKho = new List<DTO.Kho>();
            DTO.Kho k = new DTO.Kho();

            foreach (DataGridViewRow dr in dgv.Rows)
            {
                k = new DTO.Kho(
                    dr.Cells[0].Value.ToString().Trim(),
                    dr.Cells[1].Value.ToString().Trim(),
                    dr.Cells[2].Value.ToString().Trim(),
                    dr.Cells[3].Value.ToString().Trim(),
                    Convert.ToInt32(dr.Cells[4].Value));
                lKho.Add(k);
            }
            DAO.KhoDAO.Instance.saveEdit(lKho);
        }

        public void Insert(DataGridView dgv)
        {
            dgv.DataSource = DAO.KhoDAO.Instance.Insert();
        }

        public void Delete(string id)
        {
            DAO.KhoDAO.Instance.Delete(id);
        }

        public void searchByKeyword(DataGridView dgv, string keyword)
        {
            dgv.DataSource = DAO.KhoDAO.Instance.searchByKeyword(keyword);
        }

        public void searchByNum(DataGridView dgv, int num, bool comp, bool type)
        {
            int compTemp = (comp == true) ? 1 : 0;
            int compType = (type == true) ? 1 : 0;
            dgv.DataSource = DAO.KhoDAO.Instance.searchByNum(num, compTemp, compType);
        }
    }
}
