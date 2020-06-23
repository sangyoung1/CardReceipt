using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        OracleConnection oraConn;
        List<Project> prolist;
        string[] date;
        string[] card;

        public Form3()
        {
            InitializeComponent();
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataBinding();
            ComboBinding();
            btnDetails.Visible = false;
        }

        private List<Project> getProject()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT PROJECTCODE,PROJECTNAME, manager, PROJECTDATE, CardNumber, Member, ProjectId FROM Project ORDER BY projectid";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Project> list = Helper.DataReaderMapToList<Project>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pcode = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string pname = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string pmanager = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            string pdate = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            string pcard = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            string pmember = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
            string pid = dataGridView1[6, dataGridView1.CurrentRow.Index].Value.ToString();

            Form2 frm = new Form2(pcode, pname, pdate, pcard, pmember, pid, pmanager);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                DataBinding();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pid = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
            using (OracleCommand oraCmd = new OracleCommand())
            {
                try
                {
                    oraCmd.CommandText = $"DELETE FROM Project WHERE ProjectId = {pid}";
                    oraCmd.Connection = oraConn;
                    oraCmd.Connection.Open();
                    oraCmd.ExecuteNonQuery();
                    MessageBox.Show("삭제 성공");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    oraCmd.Connection.Close();
                    DataBinding();
                }
            }
        }

        private void DataBinding()
        {
            prolist = getProject();
            dataGridView1.DataSource = prolist;
            if (prolist.Count < 1)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            dataGridView1.Columns[0].HeaderText = "프로젝트코드";
            dataGridView1.Columns[1].HeaderText = "프로젝트명";
            dataGridView1.Columns[2].HeaderText = "프로젝트담당자";
            dataGridView1.Columns[3].HeaderText = "프로젝트기간";
            dataGridView1.Columns[4].HeaderText = "카드번호";
            dataGridView1.Columns[5].HeaderText = "투입인력";
            dataGridView1.Columns[6].Visible = false;
            
            dataGridView1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataBinding();
            }
        }

        private void ComboBinding()
        {
            cboName.Text = "==선택==";
            foreach (var item in prolist)
            {
                cboName.Items.Add(item.ProjectName);
            }
        }
        
        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (from project in prolist
                        where project.ProjectName == cboName.Text
                        select project).ToList();

            foreach (var item in list)
            {
               date = item.ProjectDate.Replace(" ", "").Split('~');
               card = item.CardNumber.Replace(" ", "").Split(',');
            }

            List<ProAmount> amountlist = new List<ProAmount>();
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "select username,cr.cardnumber,sum(amount) as amount, (cardlimit-sum(amount)) as remain from card_receipt cr join card_data cd on cr.cardnumber=cd.cardnumber where usedate between :date1 and :date2 or cr.cardnumber = :card1 and cr.cardnumber = :card2 group by username,cardlimit,cr.cardnumber";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                oraCmd.Parameters.Clear();
                oraCmd.Parameters.Add(new OracleParameter("date1", date[0]));
                oraCmd.Parameters.Add(new OracleParameter("date2", date[1]));
                oraCmd.Parameters.Add(new OracleParameter("card1", card[0]));
                if(card.Length > 1)
                {
                    oraCmd.Parameters.Add(new OracleParameter("card2", card[1]));
                }
                else
                {
                    oraCmd.Parameters.Add(new OracleParameter("card2", null));
                }
                OracleDataReader reader = oraCmd.ExecuteReader();
                amountlist = Helper.DataReaderMapToList<ProAmount>(reader);
                oraCmd.Connection.Close();
            }

            var cardlist = (from a in amountlist
                            join p in list on a.UserName equals p.Manager
                            into card
                            from c in card
                            select new { c.ProjectName, c.ProjectDate, a.CardNumber, a.Amount, a.Remain }).ToList();

            dataGridView1.DataSource = cardlist;
            dataGridView1.Columns[0].HeaderText = "프로젝트명";
            dataGridView1.Columns[1].HeaderText = "프로젝트기간";
            dataGridView1.Columns[2].HeaderText = "카드번호";
            dataGridView1.Columns[3].HeaderText = "사용금액";
            dataGridView1.Columns[4].HeaderText = "남은금액";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            btnDetails.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataBinding();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string cardnum = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            DetailForm frm = new DetailForm(date[0], date[1], cardnum);
            frm.Show();
        }
    }
}
