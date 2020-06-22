using Oracle.ManagedDataAccess.Client;
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

namespace WindowsFormsApp1
{
    public partial class CardInfo : Form
    {
        OracleConnection oraConn;
        
        List<CardData> cardlist;

        public CardInfo()
        {
            InitializeComponent();
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);
        }

        private void CardInfo_Load(object sender, EventArgs e)
        {
            cardlist = getCardData();
            dataGridView1.DataSource = cardlist;
            
            dataGridView1.Columns[0].HeaderText = "카드번호";
            dataGridView1.Columns[1].HeaderText = "카드담당자";
            dataGridView1.Columns[2].HeaderText = "카드한도";
            dataGridView1.Columns[3].Visible = false;
        }

        private List<CardData> getCardData()
        {
            using (OracleCommand oraCmd = new OracleCommand())
            {
                oraCmd.CommandText = "SELECT CardNumber, CardUser, CardLimit, CardID FROM Card_Data";
                oraCmd.Connection = oraConn;
                oraCmd.Connection.Open();
                OracleDataReader reader = oraCmd.ExecuteReader();
                List<CardData> list = Helper.DataReaderMapToList<CardData>(reader);
                oraCmd.Connection.Close();
                return list;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            CardInUp frm = new CardInUp();
            if(frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string cardNum = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string cardUser = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string cardLimit = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            string cardID = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString(); 

            CardInUp frm = new CardInUp(cardNum, cardUser, cardLimit, cardID);
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
