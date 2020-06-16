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
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        OracleConnection oraConn;
        List<Project> prolist;
        public Form3()
        {
            InitializeComponent();
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataBinding();
            dataGridView1.Columns[0].HeaderText = "프로젝트코드";
            dataGridView1.Columns[1].HeaderText = "프로젝트명";
            dataGridView1.Columns[2].HeaderText = "프로젝트기간";
            dataGridView1.Columns[3].HeaderText = "카드번호";
            dataGridView1.Columns[4].HeaderText = "투입인력";
            dataGridView1.Columns[5].Visible = false;
        }

        private List<Project> getProject()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT PROJECTCODE,PROJECTNAME, PROJECTDATE, CardNumber, Member, ProjectId FROM Project";
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
            string pdate = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            string pcard = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            string pmember = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            string pid = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();

            Form2 frm = new Form2(pcode, pname, pdate, pcard, pmember, pid);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataBinding();
            }
        }
    }
}
