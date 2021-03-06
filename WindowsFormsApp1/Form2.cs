﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        OracleConnection oraConn;
        bool isUpdate = false;
        bool isLimit = false;
        int pid = 0;
        string[] date;
        string[] card;
        string[] name;
        string limit = string.Empty;

        public Form2()
        {
            InitializeComponent();
            OracleConnStr();
        }

        public Form2(string pcode, string pname, string pdate, string pcard, string pmember, string pid, string pmanager, string plimit)
        {
            InitializeComponent();
            OracleConnStr();
            btnLimit.Visible = true;
            txtLimit.Enabled = false;
            isUpdate = true;
            date = pdate.Replace(" ", "").Split('~');
            card = pcard.Replace(" ", "").Split(',');
            name = pmember.Replace(" ", "").Split(',');
            cboCode.Text = pcode;
            txtName.Text = pname;
            cboManager.Text = pmanager;
            txtLimit.Text = limit = plimit;
            dtpStart.Value = Convert.ToDateTime(date[0]);
            dtpEnd.Value = Convert.ToDateTime(date[1]);
            this.pid = Convert.ToInt32(pid);
        }

        private void OracleConnStr()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnLimit.Visible = false;
            List<Code> codelist = getProjectCode();
            foreach (var item in codelist)
            {
                if (item.ProjectCode == null)
                {
                    continue;
                }
                cboCode.Items.Add(item.ProjectCode);
            }

            List<Card> cardlist = getCardNumber();
            foreach (var cardnum in cardlist)
            {
                clbCard.Items.Add(cardnum.CardNumber);
            }

            List<Name> emplist = getEmployee();
            foreach (var name in emplist)
            {
                clbName.Items.Add(name.ENAME);
            }

            List<Name> manlist = getManager();
            foreach (var item in manlist)
            {
                cboManager.Items.Add(item.ENAME);
            }

            if (!isUpdate)
            {
                cboCode.Text = "==선택==";
                cboManager.Text = "==선택==";
            }
            else
            {
                button1.Text = "수정";
                btnLimit.Visible = true;
                for (int i = 0; i < card.Length; i++)
                {
                    for (int j = 0; j < clbCard.Items.Count; j++)
                    {
                        if (card[i] == clbCard.Items[j].ToString())
                        {
                            clbCard.SetItemChecked(j, true);
                        }
                    }
                }

                for (int i = 0; i < name.Length; i++)
                {
                    for (int j = 0; j < clbName.Items.Count; j++)
                    {
                        if (name[i] == clbName.Items[j].ToString())
                        {
                            clbName.SetItemChecked(j, true);
                        }
                    }
                }
            }
        }

        private List<Code> getProjectCode()
        {
            using(OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT DISTINCT PROJECTCODE FROM Card_Receipt";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Code> list = Helper.DataReaderMapToList<Code>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

        private List<Card> getCardNumber()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT DISTINCT CARDNUMBER FROM Card_Data";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Card> list = Helper.DataReaderMapToList<Card>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

       private List<Name> getEmployee()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT Ename FROM employee";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Name> list = Helper.DataReaderMapToList<Name>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

        private List<Name> getManager()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT Ename FROM employee WHERE position_lv < 3";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Name> list = Helper.DataReaderMapToList<Name>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cboCode.Text == "==선택==" || string.IsNullOrEmpty(txtName.Text) || clbName.CheckedItems.Count < 1 || clbCard.CheckedItems.Count < 1)
            {
                MessageBox.Show("항목을 다시 확인하세요.");
                return;
            }

            StringBuilder cardsb = new StringBuilder();
            for (int i = 0; i < clbCard.Items.Count; i++)
            {
                if (clbCard.GetItemChecked(i))
                {
                    cardsb.Append(", ");
                    cardsb.Append(clbCard.Items[i].ToString());
                }
            }
            cardsb.Remove(0, 2);

            StringBuilder namesb = new StringBuilder();
            for (int i = 0; i < clbName.Items.Count; i++)
            {
                if (clbName.GetItemChecked(i))
                {
                    namesb.Append(", ");
                    namesb.Append(clbName.Items[i].ToString());
                }
            }
            namesb.Remove(0, 2);

            Project pj = new Project();
            pj.ProjectCode = cboCode.Text;
            pj.ProjectName = txtName.Text;
            pj.Manager = cboManager.Text;
            pj.ProjectLimit = txtLimit.Text;
            pj.ProjectDate = $"{dtpStart.Value.ToString("yyyy-MM-dd")} ~ {dtpEnd.Value.ToString("yyyy-MM-dd")}";
            pj.Member = namesb.ToString();
            pj.CardNumber = cardsb.ToString();

            using (OracleCommand oraCmd = new OracleCommand())
            {
                try
                {
                    if(!isUpdate)
                    {
                        oraCmd.CommandText = "INSERT INTO Project (projectid, projectcode, projectname, projectdate, cardnumber, member, manager, projectlimit) VALUES (Project_ID.nextval, :projectcode, :projectname, :projectdate, :cardnumber, :member, :manager, :projectlimit)";
                    }
                    else
                    {
                        oraCmd.CommandText = $"UPDATE Project SET projectcode = :projectcode , projectname =:projectname, projectdate=:projectdate, cardnumber=:cardnumber, member =:member, manager=:manager, projectlimit=:projectlimit WHERE ProjectId = {pid}";
                    }
                    oraCmd.Connection = oraConn;
                    oraCmd.Connection.Open();
                    oraCmd.Parameters.Add(new OracleParameter("projectcode", pj.ProjectCode));
                    oraCmd.Parameters.Add(new OracleParameter("projectname", pj.ProjectName));
                    oraCmd.Parameters.Add(new OracleParameter("projectdate", pj.ProjectDate));
                    oraCmd.Parameters.Add(new OracleParameter("cardnumber", pj.CardNumber));
                    oraCmd.Parameters.Add(new OracleParameter("member", pj.Member));
                    oraCmd.Parameters.Add(new OracleParameter("manager", pj.Manager));
                    oraCmd.Parameters.Add(new OracleParameter("projectlimit", pj.ProjectLimit));
                    oraCmd.ExecuteNonQuery();

                    if(isLimit)
                    {
                        oraCmd.CommandText = "INSERT INTO Limit_Log (ProjectCode, Before, After, Dated) VALUES (:ProjectCode, :Before, :After, sysdate)";
                        oraCmd.Parameters.Clear();
                        oraCmd.Parameters.Add(new OracleParameter("ProjectCode", pj.ProjectCode));
                        oraCmd.Parameters.Add(new OracleParameter("Before", limit));
                        oraCmd.Parameters.Add(new OracleParameter("After", pj.ProjectLimit));
                        oraCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("저장완료");
                    this.DialogResult = DialogResult.OK;
                    if (isUpdate)
                    {
                        isUpdate = false;
                        this.Close();
                    }
                    cboCode.SelectedIndex = -1;
                    txtName.Text = "";
                    dtpStart.Value = dtpEnd.Value = DateTime.Now;
                    for (int i = 0; i < clbCard.Items.Count; i++)
                    {
                        clbCard.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < clbName.Items.Count; i++)
                    {
                        clbName.SetItemChecked(i, false);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    oraCmd.Connection.Close();
                }
            }
        }

        private void btnLimit_Click(object sender, EventArgs e)
        {
            txtLimit.Enabled = true;
            isLimit = true;
        }
    }
}
