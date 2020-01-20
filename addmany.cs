using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace Medical_store_management_system
{
    public partial class addmany : Form
    {
        public addmany()
        {
            InitializeComponent();
        }

        private void addmany_Load(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Today;
            this.label17.Text = datetime.ToString("yyyy-MM-dd");
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            Color temp = Color.FromArgb(0xFFFFFF);
            
            panel4.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel5.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel6.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel7.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0x000000);
           
            label4.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label5.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label6.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label7.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);

        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {

            Color temp = Color.FromArgb(0x0066CC);
            panel6.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label6.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);

        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel4.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label4.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel5.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label5.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel7.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label7.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        void clearfunc()
        {
            panel3.Visible = false;

            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                dataGridView1.Rows.RemoveAt(j);
                j--;
                while (dataGridView1.Rows.Count == 0)
                    continue;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            clearfunc();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            clearfunc();
        }

        void addrecord()
        {
            try
            {
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    MySqlCommand cmdDataBase = new MySqlCommand("insert into medicalstore.pharmacyrecord (productname,batch,expirydate,quantity,rate,recordedon) values('" + this.dataGridView1.Rows[i].Cells[0].Value + "','" + this.dataGridView1.Rows[i].Cells[1].Value + "','" + this.dataGridView1.Rows[i].Cells[2].Value + "','" + this.dataGridView1.Rows[i].Cells[3].Value + "','" + this.dataGridView1.Rows[i].Cells[4].Value + "','" + this.label17.Text + "' )", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        
                        while (myReader.Read())
                        {
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conDataBase.Close();
                }
                MessageBox.Show("Informations successfully Added.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            addrecord();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            addrecord();
        }

        void updaterec()
        {
            panel3.Visible = false;
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                MySqlCommand cmdDataBase = new MySqlCommand("update medicalstore.pharmacyrecord set productname='" + this.dataGridView1.Rows[i].Cells[0].Value + "',quantity='" + this.dataGridView1.Rows[i].Cells[3].Value + "',rate='" + this.dataGridView1.Rows[i].Cells[4].Value + "',expirydate='" + this.dataGridView1.Rows[i].Cells[2].Value + "',recordedon='" + this.label17.Text + "',batch='" + dataGridView1.Rows[i].Cells[1].Value + "' where  batch='" + dataGridView1.Rows[i].Cells[1].Value + "';", conDataBase);
                MySqlDataReader myReader;
                try
                {
                    conDataBase.Open();
                    myReader = cmdDataBase.ExecuteReader();
                    while (myReader.Read())
                    {
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Informations Updated.");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            updaterec();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            updaterec();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opemfiledialog = new OpenFileDialog();
            if (opemfiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = opemfiledialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;


            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                dataGridView1.Rows.RemoveAt(j);
                j--;
                while (dataGridView1.Rows.Count == 0)
                    continue;
            }

            Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks workbooks = app.Workbooks;

            Excel.Workbook workbook = workbooks.Open(textBox1.Text);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            try
            {
                int rcount = worksheet.UsedRange.Rows.Count;

                int i = 0;

                //Initializing Columns
                dataGridView1.ColumnCount = worksheet.UsedRange.Columns.Count;



                for (i = 1; i < rcount; i++)
                {

                    //dataGridView1.Rows.Add(worksheet.Cells[i + 1, 1].Value, worksheet.Cells[i + 1, 2].Value, worksheet.Cells[i + 1, 3].Value, worksheet.Cells[i + 1, 4].Value, worksheet.Cells[i + 1, 5].Value, worksheet.Cells[i + 1, 6].Value, worksheet.Cells[i + 1, 7].Value, worksheet.Cells[i + 1, 8].Value, worksheet.Cells[i + 1, 9].Value);
                    dataGridView1.Rows.Add(worksheet.Cells[i + 1, 1].Value, worksheet.Cells[i + 1, 2].Value, worksheet.Cells[i + 1, 3].Value, worksheet.Cells[i + 1, 4].Value, worksheet.Cells[i + 1, 5].Value);
                }

                workbook.Close();
                app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(worksheet);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                workbook.Close();
                app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(worksheet);
            }


        }

        private void panelStudent_Paint(object sender, PaintEventArgs e)
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            // dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }
    }
}
