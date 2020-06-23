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
    public partial class DetailForm : Form
    {
        OracleConnection oraConn;
        string fdate;
        string sdate;
        string cardnum;
        List<Details> detaillist;

        public DetailForm(string date1, string date2, string cardnum)
        {
            InitializeComponent();
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);

            fdate = date1;
            sdate = date2;
            this.cardnum = cardnum;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<Details> getDetails()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "select usedate, useplace, cardnumber, amount, purpose, classification, content, username from card_receipt where usedate between :date1 and :date2 and cardnumber = :cardnumber";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                oraCmd.Parameters.Clear();
                oraCmd.Parameters.Add(new OracleParameter("date1", fdate));
                oraCmd.Parameters.Add(new OracleParameter("date2", sdate));
                oraCmd.Parameters.Add(new OracleParameter("cardnumber", cardnum));
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<Details> list = Helper.DataReaderMapToList<Details>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            detaillist = getDetails();
            dataGridView1.DataSource = detaillist;
            dataGridView1.Columns[0].HeaderText = "사용일자";
            dataGridView1.Columns[1].HeaderText = "사용처";
            dataGridView1.Columns[2].HeaderText = "카드번호";
            dataGridView1.Columns[3].HeaderText = "사용금액";
            dataGridView1.Columns[4].HeaderText = "용도";
            dataGridView1.Columns[5].HeaderText = "코드분류";
            dataGridView1.Columns[6].HeaderText = "내용";
            dataGridView1.Columns[7].HeaderText = "사용자";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
