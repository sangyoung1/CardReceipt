using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        List<CardTable> cardlist = new List<CardTable>();
        bool isSubtotal = false;
        bool isArrange = false;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            string fileExtension = Path.GetExtension(filePath);
            string connectionString = string.Empty;
            string sheetName = string.Empty;

            // 확장자로 구분하여 커넥션 스트링을 가져옮
            switch (fileExtension)
            {
                case ".xls":    //Excel 97-03
                    connectionString = string.Format(Excel03ConString, filePath);
                    break;
                case ".xlsx":  //Excel 07
                    connectionString = string.Format(Excel07ConString, filePath);
                    break;
            }

            // 첫 번째 시트의 이름을 가져옮
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            // 첫 번째 쉬트의 데이타를 읽어서 datagridview 에 보이게 함.
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isArrange = true;
            string pCode = string.Empty;
            string classification = string.Empty;
            string content = string.Empty;

            for (int i = 2; i < dt.Rows.Count; i++)
            {
                CardTable ct = new CardTable();
                ct.TYPE = dataGridView1[0, i].Value.ToString();
                ct.USEDATE = dataGridView1[1, i].Value.ToString();
                ct.USEPLACE = dataGridView1[2, i].Value.ToString();
                ct.CARD = dataGridView1[3, i].Value.ToString();
                ct.CARDNUMBER = dataGridView1[4, i].Value.ToString();
                ct.AMOUNT = dataGridView1[5, i].Value.ToString();
                ct.PURPOSE = dataGridView1[6, i].Value.ToString();
                ct.CONTENT = dataGridView1[7, i].Value.ToString();
                ct.DEPARTMENT = dataGridView1[8, i].Value.ToString();
                ct.USERNAME = dataGridView1[9, i].Value.ToString();
                ct.RESOLUTION = dataGridView1[10, i].Value.ToString();
                ct.OTHERS = dataGridView1[11, i].Value.ToString();

                content = dataGridView1[7, i].Value.ToString();
                if (content.Contains('['))
                {
                    string[] con = content.Replace(']', '[').Split('[');
                    ct.PROJECTCODE = con[1].ToString();
                    ct.CONTENT = con[2].ToString();
                    if (con.Length > 3)
                    {
                        ct.CLASSIFICATION = con[3].ToString();
                        ct.CONTENT = con[4].ToString();
                    }
                }
                cardlist.Add(ct);
            }
            dataGridView1.DataSource = cardlist;
            dataGridView1.Columns[0].HeaderText = "종류";
            dataGridView1.Columns[1].HeaderText = "사용일시";
            dataGridView1.Columns[2].HeaderText = "사용처";
            dataGridView1.Columns[3].HeaderText = "카드사";
            dataGridView1.Columns[4].HeaderText = "카드번호";
            dataGridView1.Columns[5].HeaderText = "사용금액";
            dataGridView1.Columns[6].HeaderText = "용도";
            dataGridView1.Columns[7].HeaderText = "프로젝트코드";
            dataGridView1.Columns[8].HeaderText = "코드분류";
            dataGridView1.Columns[9].HeaderText = "내용";
            dataGridView1.Columns[10].HeaderText = "공용카드부서";
            dataGridView1.Columns[11].HeaderText = "사용자";
            dataGridView1.Columns[12].HeaderText = "결의정보";
            dataGridView1.Columns[13].HeaderText = "기타정보";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnDBSave_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            oraConn.Open();
            OracleTransaction sTrans = oraConn.BeginTransaction();
            OracleCommand oraCmd = new OracleCommand();
            try
            {
                oraCmd.Transaction = sTrans;
                oraCmd.CommandText = @"insert into card_receipt (type, usedate, useplace, card, cardnumber, amount, purpose, projectcode, classification, content, department, username, resolution, others) 
select :type, :usedate, :useplace, :card, :cardnumber, :amount, :purpose, :projectcode, :classification, :content, :department, :username, :resolution, :others
from dual
where not exists(select 1 from card_receipt where usedate = :usedate)";
                oraCmd.Connection = oraConn;
                foreach (var item in cardlist)
                {
                    oraCmd.Parameters.Clear();
                    oraCmd.Parameters.Add(new OracleParameter("type", item.TYPE));
                    oraCmd.Parameters.Add(new OracleParameter("usedate", item.USEDATE));
                    oraCmd.Parameters.Add(new OracleParameter("useplace", item.USEPLACE));
                    oraCmd.Parameters.Add(new OracleParameter("card", item.CARD));
                    oraCmd.Parameters.Add(new OracleParameter("cardnumber", item.CARDNUMBER));
                    oraCmd.Parameters.Add(new OracleParameter("amount", item.AMOUNT));
                    oraCmd.Parameters.Add(new OracleParameter("purpose", item.PURPOSE));
                    oraCmd.Parameters.Add(new OracleParameter("projectcode", item.PROJECTCODE));
                    oraCmd.Parameters.Add(new OracleParameter("classification", item.CLASSIFICATION));
                    oraCmd.Parameters.Add(new OracleParameter("content", item.CONTENT));
                    oraCmd.Parameters.Add(new OracleParameter("department", item.DEPARTMENT));
                    oraCmd.Parameters.Add(new OracleParameter("username", item.USERNAME));
                    oraCmd.Parameters.Add(new OracleParameter("resolution", item.RESOLUTION));
                    oraCmd.Parameters.Add(new OracleParameter("others", item.OTHERS));
                    oraCmd.ExecuteNonQuery();
                }
                oraCmd.Transaction.Commit();
                MessageBox.Show("기록저장완료");
            }
            catch (Exception err)
            {
                oraCmd.Transaction.Rollback();
                MessageBox.Show(err.Message);
            }
            finally
            {
                oraConn.Close();
            }
        }

        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            Excel.Application ap = new Excel.Application();
            Excel.Workbook eworkbook = ap.Workbooks.Add();

            if (isArrange)
            {
                isArrange = false;
                DataSet dsGrid = ToDataSet<CardTable>(cardlist);
                foreach (DataTable dt in dsGrid.Tables)
                {
                    Excel.Worksheet ws = eworkbook.Sheets.Add();
                    ws.Name = "Card_Receipt";
                    ws.Cells[1, 1] = "종류";
                    ws.Cells[1, 2] = "사용일시";
                    ws.Cells[1, 3] = "사용처";
                    ws.Cells[1, 4] = "카드사";
                    ws.Cells[1, 5] = "카드번호";
                    ws.Cells[1, 6] = "사용금액";
                    ws.Cells[1, 7] = "용도";
                    ws.Cells[1, 8] = "프로젝트코드";
                    ws.Cells[1, 9] = "코드분류";
                    ws.Cells[1, 10] = "내용";
                    ws.Cells[1, 11] = "공용카드부서";
                    ws.Cells[1, 12] = "사용자";
                    ws.Cells[1, 13] = "결의정보";
                    ws.Cells[1, 14] = "기타정보";

                    for (int colHeaderindex = 1; colHeaderindex <= dt.Columns.Count; colHeaderindex++)
                    {
                        //ws.Cells[1, colHeaderindex] = dt.Columns[colHeaderindex - 1].ColumnName;
                        ws.Cells[1, colHeaderindex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    }

                    for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
                    {
                        for (int colindex = 0; colindex < dt.Columns.Count; colindex++)
                        {
                            ws.Cells[rowindex + 2, colindex + 1] = dt.Rows[rowindex].ItemArray[colindex].ToString();
                        }
                    }
                    ws.Columns.AutoFit();
                } 
            }
            else if(isSubtotal)
            {
                isSubtotal = false;
                foreach (DataTable dt in ds.Tables)
                {
                    Excel.Worksheet ws = eworkbook.Sheets.Add();
                    ws.Name = "Card_Receipt";

                    for (int colHeaderindex = 1; colHeaderindex <= dt.Columns.Count; colHeaderindex++)
                    {
                        ws.Cells[1, colHeaderindex] = dataGridView1.Columns[colHeaderindex - 1].HeaderText;
                        ws.Cells[1, colHeaderindex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    }

                    for (int rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
                    {
                        for (int colindex = 0; colindex < dt.Columns.Count; colindex++)
                        {
                            ws.Cells[rowindex + 2, colindex + 1] = dt.Rows[rowindex].ItemArray[colindex].ToString();
                        }
                    }
                    ws.Columns.AutoFit();
                }
            }

            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog1.Title = "Excel 저장위치 지정";
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Xlsx files(*.xlsx)|*.xlsx|Xls files(*.xls)|*.xls";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName.Length > 0)
            {
                try
                {
                    foreach (var item in saveFileDialog1.FileNames)
                    {
                        string savePath = item;
                        if (Path.GetExtension(savePath) == ".xls")
                        {
                            eworkbook.SaveAs(savePath, Excel.XlFileFormat.xlWorkbookNormal);
                        }
                        else if (Path.GetExtension(savePath) == ".xlsx")
                        {
                            eworkbook.SaveAs(savePath, Excel.XlFileFormat.xlOpenXMLWorkbook);
                        }
                        eworkbook.Close();
                        ap.Quit();
                    }
                    MessageBox.Show("Excel 저장 완료");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private DataSet ToDataSet<T>(List<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }
                t.Rows.Add(row);
            }
            return ds;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds.Tables.Clear();
            isSubtotal = true;
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            OracleCommand oraCmd = oraConn.CreateCommand();
            oraCmd.CommandText = "SELECT DECODE(cardnumber,null,'Total',cardnumber) as cardnumber, SUM(amount) as amount FROM Card_Receipt GROUP BY ROLLUP (cardnumber)";
            oraConn.Open();
            OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
            adapt.SelectCommand = oraCmd;
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "카드번호";
            dataGridView1.Columns[1].HeaderText = "합계";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ds.Tables.Clear();
            isSubtotal = true;
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            OracleCommand oraCmd = oraConn.CreateCommand();
            oraCmd.CommandText = "SELECT DECODE(GROUPING(SUBSTR(usedate,6,2)),1,'Total',SUBSTR(usedate,6,2)) as Month, SUM(amount) FROM Card_Receipt GROUP BY ROLLUP(SUBSTR(usedate,6,2))";
            oraConn.Open();
            OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
            adapt.SelectCommand = oraCmd;
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "월";
            dataGridView1.Columns[1].HeaderText = "합계";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ds.Tables.Clear();
            isSubtotal = true;
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            OracleCommand oraCmd = oraConn.CreateCommand();
            oraCmd.CommandText = "SELECT DECODE(GROUPING(SUBSTR(usedate,6,2)),1,'Total',SUBSTR(usedate,6,2)) as Month, DECODE(GROUPING(cardnumber),1,'Total',cardnumber), SUM(amount) FROM Card_Receipt GROUP BY ROLLUP(SUBSTR(usedate,6,2),cardnumber)";
            oraConn.Open();
            OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
            adapt.SelectCommand = oraCmd;
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "월";
            dataGridView1.Columns[1].HeaderText = "카드번호";
            dataGridView1.Columns[2].HeaderText = "합계";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.ShowDialog();
        }

        private void OracleSelect(string sql)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            OracleCommand oraCmd = oraConn.CreateCommand();
            oraCmd.CommandText = sql; 
            oraConn.Open();
            OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
            adapt.SelectCommand = oraCmd;
            adapt.Fill(ds);
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            CardInfo frm = new CardInfo();
            frm.ShowDialog();
        }
    }
}
