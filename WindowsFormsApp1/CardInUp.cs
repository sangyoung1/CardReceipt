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
    public partial class CardInUp : Form
    {
        OracleConnection oraConn;
        string[] cardnum;
        string user;
        string limit;
        bool isUpdate = false;
        int cid = 0;
        public CardInUp()
        {
            InitializeComponent();
            OracleConnStr();
        }

        public CardInUp(string cardNum, string user, string limit , string cardID)
        {
            InitializeComponent();
            OracleConnStr();
            isUpdate = true;

            this.user = user;
            this.limit = limit;
            this.cid = Convert.ToInt32(cardID);
            cardnum = cardNum.Split('-');
        }

        private void OracleConnStr()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            oraConn = new OracleConnection(connStr);
        }

        private void KeypressNum(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCardNum1_TextChanged(object sender, EventArgs e)
        {
            if(txtCardNum1.TextLength == 4)
            {
                txtCardNum2.Focus();
            }
        }

        private void txtCardNum2_TextChanged(object sender, EventArgs e)
        {
            if (txtCardNum2.TextLength == 4)
            {
                txtCardNum3.Focus();
            }
        }

        private void txtCardNum3_TextChanged(object sender, EventArgs e)
        {
            if (txtCardNum3.TextLength == 4)
            {
                txtCardNum4.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtLimit.Text) || string.IsNullOrEmpty(txtCardNum1.Text) || string.IsNullOrEmpty(txtCardNum2.Text) || string.IsNullOrEmpty(txtCardNum3.Text) || string.IsNullOrEmpty(txtCardNum4.Text))
            {
                MessageBox.Show("항목을 다시 확인하세요.");
                return;
            }

            CardData cd = new CardData();
            cd.CardNumber = $"{txtCardNum1.Text}-{txtCardNum2.Text}-{txtCardNum3.Text}-{txtCardNum4.Text}";
            cd.CardUser = txtUser.Text;
            cd.CardLimit = txtLimit.Text;

            using (OracleCommand oraCmd = new OracleCommand())
            {
                try
                {
                    if (!isUpdate)
                    {
                        oraCmd.CommandText = "INSERT INTO Card_Data (cardid, cardnumber, carduser, cardlimit) VALUES (Card_ID.nextval, :cardnumber, :carduser, :cardlimit) ";
                    }
                    else
                    {
                        oraCmd.CommandText = $"UPDATE Card_Data SET  cardnumber = :cardnumber , carduser =:carduser, cardlimit=:cardlimit WHERE CardID = {cid}";
                    }
                    oraCmd.Connection = oraConn;
                    oraCmd.Connection.Open();
                    oraCmd.Parameters.Add(new OracleParameter("cardnumber", cd.CardNumber));
                    oraCmd.Parameters.Add(new OracleParameter("carduser", cd.CardUser));
                    oraCmd.Parameters.Add(new OracleParameter("cardlimit", cd.CardLimit));
                    oraCmd.ExecuteNonQuery();

                    MessageBox.Show("저장완료");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    oraCmd.Connection.Close();
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void CardInUp_Load(object sender, EventArgs e)
        {
            if(isUpdate)
            {
                button2.Text = "수정";
                
                txtCardNum1.Text = cardnum[0];
                txtCardNum2.Text = cardnum[1];
                txtCardNum3.Text = cardnum[2];
                txtCardNum4.Text = cardnum[3];
                txtUser.Text = user;
                txtLimit.Text = limit;
            }
        }
    }
}
