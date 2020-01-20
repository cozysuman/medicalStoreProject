using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using DGVPrinterHelper;
using System.Windows.Forms.DataVisualization.Charting;

namespace Medical_store_management_system
{
    public partial class pharmacy : Form
    {
        public pharmacy(string str1)
        {
            InitializeComponent();
            autocompletetxt();
            label21.Text = str1;
            //label37.Text = str2;
            fillcombobox();
        }

        public void update_fromform_batch(string batch,string expval,string quantity,string rate)
        {
            comboBox6.Text = batch;
            label24.Text = expval;
            label11.Text = quantity;
            comboBox8.Text = rate;
        }
        void fillcombobox()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select* from medicalstore.pharmacyrecord", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string sproduct = myReader.GetString("productname");
                    comboBox3.Items.Add(sproduct);
                    comboBox13.Items.Add(sproduct);
                    comboBox14.Items.Add(sproduct);
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void autocompletetxt()
        { 
            comboBox3.AutoCompleteMode= AutoCompleteMode.SuggestAppend;
            comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox13.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox13.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox14.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox14.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select* from medicalstore.pharmacyrecord", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string sproduct = myReader.GetString("productname");
                    col1.Add(sproduct);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox3.AutoCompleteCustomSource = col1;
            textBox4.AutoCompleteCustomSource = col1;
            textBox8.AutoCompleteCustomSource = col1;
            comboBox13.AutoCompleteCustomSource = col1;
            comboBox14.AutoCompleteCustomSource = col1;
        }
        void search_record()
        {
            comboBox6.Items.Clear();
            String batchno = comboBox6.Text;
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from medicalstore.pharmacyrecord where batch ='" + batchno + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                if (myReader.Read())
                {
                    String rateVal = myReader.GetString("rate");
                    comboBox8.Text = rateVal;
                    String quantityVal = myReader.GetString("quantity");
                    label11.Text = quantityVal;
                    String expdateVal = myReader.GetString("expirydate");
                    label24.Text = expdateVal;
                    String batch = myReader.GetString("batch");
                    comboBox6.Items.Add(batch);

                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (label11.Text == "0")
            {
               textBox11.BackColor = Color.Red;
                label22.Text = "Not Available";
            }
            else
            {
               textBox11.BackColor = Color.Green;
                label22.Text = "Available";
            }
        }
        void update_record()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("update medicalstore.pharmacyrecord set productname='" + this.comboBox3.Text + "',quantity='" + this.label11.Text + "',rate='" + this.comboBox8.Text + "',expirydate='" + this.label24.Text + "',recordedon='" + this.label12.Text + "',batch='" + comboBox6.Text + "' where batch='" + this.comboBox6.Text + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                }
                // MessageBox.Show("Informations Updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void auto_increment()
        {
            String s1 = "SELECT MAX(sn) FROM medicalstore.pharmacy";
            int a1;
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            conDataBase.Open();

            MySqlCommand cmd = new MySqlCommand(s1, conDataBase);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    label7.Text = "1";
                }
                else
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                    a1 = a1 + 1;
                    label7.Text = a1.ToString();
                }
            }
        }
        void load_chart1()
        {

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            //this.chart1.Series["Quantity"]["PieLabelStyle"] = "Outside";
           
             this.chart1.Series["Quantity"]["PieLabelStyle"] = "Disabled";
            //this.chart1.Titles.Add("Product vs Quantity");
            //chart1.Series["Quantity"].SmartLabelStyle.Enabled = true;

            for (int i = 0; i <= dataGridView3.Rows.Count - 2; i++)
            {
                this.chart1.Series["Quantity"].Points.AddXY(dataGridView3.Rows[i].Cells[1].Value, dataGridView3.Rows[i].Cells[2].Value);
                //this.chart1.Series["Rate"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value);
                while (dataGridView3.Rows.Count == 0)
                    continue;

            }
            //this.chart2.Legends.Add("Legend1");
            //chart1.Series[0].Label = "#VALX".ToString();
          //  this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
              //  this.chart1.Series["Quantity"].LegendText = "#VALX(#VALY)";
            this.chart1.DataManipulator.Sort(PointSortOrder.Ascending, chart1.Series["Quantity"]);
        }

        void load_chart2()
        {

            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            //this.chart1.Series["Quantity"]["PieLabelStyle"] = "Outside";

            this.chart2.Series["Quantity"]["PieLabelStyle"] = "Disabled";
            //this.chart1.Titles.Add("Product vs Quantity");
            //chart1.Series["Quantity"].SmartLabelStyle.Enabled = true;

            for (int i = 0; i <= dataGridView2.Rows.Count - 2; i++)
            {
                this.chart2.Series["Quantity"].Points.AddXY(dataGridView2.Rows[i].Cells[1].Value, dataGridView2.Rows[i].Cells[2].Value);
                //this.chart1.Series["Rate"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value);
                while (dataGridView2.Rows.Count == 0)
                    continue;

            }
            //this.chart2.Legends.Add("Legend1");
            //chart1.Series[0].Label = "#VALX".ToString();
            //this.chart2.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
            this.chart2.DataManipulator.Sort(PointSortOrder.Ascending, chart2.Series["Quantity"]);
        }

        void limit_chart1()
        {
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            this.chart1.Series["Quantity"]["PieLabelStyle"] = "Disabled";
            for (int i = 0; i <= 9; i++)
                {
                    this.chart1.Series["Quantity"].Points.AddXY(dataGridView3.Rows[i].Cells[1].Value, dataGridView3.Rows[i].Cells[2].Value);
                    //this.chart1.Series["Rate"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value);
                   // while (i == 0)
                        //continue;

                }
            this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
            this.chart1.DataManipulator.Sort(PointSortOrder.Ascending, chart1.Series["Quantity"]);
            
        }

        void limit_chart2()
        {
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            this.chart2.Series["Quantity"]["PieLabelStyle"] = "Disabled";
            for (int i = 0; i <= 9; i++)
            {
                this.chart2.Series["Quantity"].Points.AddXY(dataGridView2.Rows[i].Cells[1].Value, dataGridView2.Rows[i].Cells[2].Value);
                //this.chart1.Series["Rate"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value);
                // while (i == 0)
                //continue;

            }
            this.chart2.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
            this.chart2.DataManipulator.Sort(PointSortOrder.Ascending, chart2.Series["Quantity"]);

        }

        DataTable dbdataset;
        void load_table()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select* from medicalstore.pharmacy ;", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView2.DataSource = bSource;
               
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        void load_table1()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select* from medicalstore.pharmacyrecord ;", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void backupdata()
        {
            string conn = "server=localhost;user=root;password=root;database=medicalstore";
            string file = "D:\\backup\\medicalstore.sql";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = con;
                        con.Open();
                        mb.ExportToFile(file);
                        con.Close();
                    }
                }
            }
            //MessageBox.Show("Backup completed...!");
        }
        private void pharmacy_Load(object sender, EventArgs e)
        {
            //dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            // dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            auto_increment();
            DateTime datetime = DateTime.Today;
            this.label12.Text = datetime.ToString("yyyy-MM-dd");
            this.label17.Text = datetime.ToString("yyyy-MM-dd");
            load_table();
            load_table1();
            
        }
       // Bitmap bmp;
        private void button6_Click(object sender, EventArgs e)
        {
            //int height = tabPage1.Height;
          //  int weidth = tabPage1.Width;
          //  bmp = new Bitmap(tabPage1.Width, tabPage1.Height);
           // tabPage1.DrawToBitmap(bmp, new Rectangle(0, 0, tabPage1.Width, tabPage1.Height));
         //   printPreviewDialog1.ShowDialog();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "LIFEcare medicalstore"+"\n"+"PHARMACY INVOICE";//Header
            printer.SubTitle = "Invoice no. "+label7.Text+"\n"+string.Format("Date: {0}", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Net amount :  " + label5.Text + "\n" + "Net quantity:  "+label9.Text+"\n"+"Prepared by: "+label21.Text;//Footer

            printer.FooterSpacing = 15;
            //Print landscape mode
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView1);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           // e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            try
            {
                int a = Convert.ToInt32(comboBox7.Text);
                int b = Convert.ToInt32(comboBox8.Text);
                int q = Convert.ToInt32(label11.Text);

                string z = label24.Text;
                DateTime y = Convert.ToDateTime(z);
                //label24.Text = y.ToShortDateString();
                DateTime endDate = DateTime.Today.Date;
                DateTime StartDate = y;
                var totalDays = (endDate - StartDate).TotalDays;


                if (totalDays >= 0)
                {
                    MessageBox.Show("Sorry, the product has already expired.");

                }
                else if (a > q)
                {
                    MessageBox.Show("Sorry, the required quantity of product is not available in the stock.");
                }
                else
                {
                    bool Found = false;
                    if (dataGridView1.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (Convert.ToString(row.Cells[1].Value) == comboBox3.Text && Convert.ToString(row.Cells[5].Value) == comboBox8.Text && Convert.ToString(row.Cells[4].Value) == comboBox7.Text)
                            {
                                //row.Cells[1].Value = Convert.ToString(1 + Convert.ToInt16(row.Cells[2].Value));
                                Found = true;
                            }
                        }

                        int t = a * b;
                        if (!Found)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);

                            //row.Cells[0].Value = "some value";
                            row.Cells[1].Value = comboBox3.Text;
                            row.Cells[2].Value = comboBox6.Text;
                            row.Cells[3].Value = label24.Text;
                            row.Cells[4].Value = comboBox7.Text;
                            row.Cells[5].Value = comboBox8.Text;
                            row.Cells[6].Value = t.ToString();

                            dataGridView1.Rows.Add(row);


                            //dataGridView1.Rows.Add(comboBox3.Text,comboBox31.Text,label24.Text,comboBox7.Text, comboBox8.Text, t.ToString());
                            int c = Convert.ToInt32(label5.Text);
                            int d = t + c;
                            label5.Text = d.ToString();

                            int c1 = Convert.ToInt32(label9.Text);
                            int d1 = a + c1;
                            label9.Text = d1.ToString();

                            int c2 = Convert.ToInt32(label11.Text);
                            int d2 = c2 - a;
                            label11.Text = d2.ToString();
                        }
                        else
                        {

                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);

                            //row.Cells[0].Value = "some value";
                            row.Cells[1].Value = comboBox3.Text;
                            row.Cells[2].Value = comboBox6.Text;
                            row.Cells[3].Value = label24.Text;
                            row.Cells[4].Value = comboBox7.Text;
                            row.Cells[5].Value = comboBox8.Text;
                            row.Cells[6].Value = t.ToString();

                            dataGridView1.Rows.Add(row);

                            //dataGridView1.Rows.Add(comboBox3.Text, comboBox7.Text, comboBox8.Text, t.ToString());
                            int c = Convert.ToInt32(label5.Text);
                            int d = t + c;
                            label5.Text = d.ToString();

                            int c1 = Convert.ToInt32(label9.Text);
                            int d1 = a + c1;
                            label9.Text = d1.ToString();

                            int c2 = Convert.ToInt32(label11.Text);
                            int d2 = c2 - a;
                            label11.Text = d2.ToString();
                        }

                    }
                    if (label11.Text == "0")
                    {
                       textBox11.BackColor = Color.Red;
                        label22.Text = "Not Available";
                    }
                    else
                    {
                       textBox11.BackColor = Color.Green;
                        label22.Text = "Available";
                    }

                    update_record();
                    //  comboBox3.Text = "";
                    //   comboBox7.Text = "0";
                    //   comboBox8.Text = "";

                    label11.Text = "0";
                    comboBox3.Text = "";
                    comboBox7.Text = "0";
                    comboBox8.Text = "";
                    
                    label24.Text = "";
                    label22.Text = "Index";
                   textBox11.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                MySqlCommand cmdDataBase = new MySqlCommand("insert into medicalstore.pharmacy (productname,quantity,rate,total,recordedon,invoiceno,batch) values('" + this.dataGridView1.Rows[i].Cells[1].Value + "','" + this.dataGridView1.Rows[i].Cells[4].Value + "','" + this.dataGridView1.Rows[i].Cells[5].Value + "','" + this.dataGridView1.Rows[i].Cells[6].Value + "','" + this.label12.Text + "','" + this.label7.Text + "','" + this.dataGridView1.Rows[i].Cells[2].Value + "')", conDataBase);
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
            int a1 = Convert.ToInt32(label7.Text);
            a1++;
            label7.Text = a1.ToString();
            button5.PerformClick();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i--;
                    while (dataGridView1.Rows.Count == 0)
                        continue;
                }
                label5.Text = "0";
                label9.Text = "0";
                label11.Text = "0";
                comboBox3.Text = "";
                comboBox7.Text = "0";
                comboBox8.Text = "";
                label24.Text = "";
                label22.Text = "Index";
               textBox11.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int Index = dataGridView1.CurrentCell.RowIndex;
                int r = Convert.ToInt32(label5.Text);
                int p = Convert.ToInt32(dataGridView1.Rows[Index].Cells[6].Value);
                int d = r - p;
                label5.Text = d.ToString();

                int r1 = Convert.ToInt32(label9.Text);
                int p1 = Convert.ToInt32(dataGridView1.Rows[Index].Cells[4].Value);
                int d1 = r1 - p1;
                label9.Text = d1.ToString();

                String s1 = Convert.ToString(dataGridView1.Rows[Index].Cells[1].Value);
                comboBox3.Text = s1.ToString();

                search_record();

                int r2 = Convert.ToInt32(label11.Text);
                int p2 = Convert.ToInt32(dataGridView1.Rows[Index].Cells[4].Value);
                int d2 = r2 + p2;
                label11.Text = d2.ToString();

                update_record();
                if (label11.Text == "0")
                {
                   textBox11.BackColor = Color.Red;
                    label22.Text = "Not Available";
                }
                else
                {
                   textBox11.BackColor = Color.Green;
                    label22.Text = "Available";
                }

                dataGridView1.Rows.RemoveAt(Index);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            { 
                comboBox6.Items.Clear();
                String sname = comboBox3.Text;
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("select * from medicalstore.pharmacyrecord where productname ='" + sname + "';", conDataBase);
                MySqlDataReader myReader;
             
              
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
               /* if (myReader.Read())
                 {
                    String rateVal = myReader.GetString("rate");
                    comboBox8.Text = rateVal;
                   // String quantityVal = myReader.GetString("quantity");
                     //label11.Text = quantityVal;
                     //String expdateVal = myReader.GetString("expirydate");
                     //label24.Text = expdateVal;
                     String batch = myReader.GetString("batch");
                     comboBox6.Items.Add(batch);

                 }*/

                while (myReader.Read())
                {

                   // String quantityVal = myReader.GetString("quantity");
                    //comboBox7.Text = quantityVal;
                    String rateVal = myReader.GetString("rate");
                    comboBox8.Items.Add(rateVal);
                    String batch = myReader.GetString("batch");
                    comboBox6.Items.Add(batch);

                }

              }

                catch (Exception ex)
              {
                   MessageBox.Show(ex.Message);
              }


            if (checkBox4.CheckState == CheckState.Checked)
            {

                int combocount = comboBox6.Items.Count;
                if (combocount > 1)
                {
                    batch bt = new batch(comboBox3.Text, this);
                    bt.ShowDialog();
                }
                else
                {

                    comboBox6.Items.Clear();
                    String sname = comboBox3.Text;
                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("select * from medicalstore.pharmacyrecord where productname ='" + sname + "';", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        if (myReader.Read())
                          {
                             String rateVal = myReader.GetString("rate");
                             comboBox8.Text = rateVal;
                             String quantityVal = myReader.GetString("quantity");
                             label11.Text = quantityVal;
                             String expdateVal = myReader.GetString("expirydate");
                             label24.Text = expdateVal;
                             String batch = myReader.GetString("batch");
                             comboBox6.Text=batch;

                          }

                        
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("insert into medicalstore.pharmacyrecord (productname,quantity,rate,expirydate,recordedon,batch) values('" + this.textBox4.Text + "','" + this.textBox5.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "','" + this.label17.Text + "','" + this.textBox12.Text + "' )", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                 myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Informations successfully Added.");
                while (myReader.Read())
                {
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //comboBox3.Items.Add(textBox4.Text);
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox12.Text = "";


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.textBox7.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("delete from medicalstore.pharmacyrecord where batch='" + this.comboBox9.Text + "'", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                }
                MessageBox.Show("Informations successfully Deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           // comboBox3.Items.Remove(textBox8.Text);
            textBox8.Text = "";
            comboBox9.Text = "";

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            load_table();
            if (comboBox1.Text == "Date")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("recordedon LIKE '%{0}%'", textBox9.Text);
                dataGridView2.DataSource = DV;
             
            }
            else if (comboBox1.Text == "Batch")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("batch LIKE '%{0}%'", textBox9.Text);
                dataGridView2.DataSource = DV;

            }

            else if (comboBox1.Text == "Invoice No.")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("invoiceno LIKE '%{0}%'", textBox9.Text);
                dataGridView2.DataSource = DV;
               
            }

            else if (comboBox1.Text == "Product Name")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("productname LIKE '%{0}%'", textBox9.Text);
                dataGridView2.DataSource = DV;
               
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            load_table();
            fillcombobox();
            autocompletetxt();
            label33.Text = "0";
            label34.Text = "0";
            label25.Text = "0";
            label26.Text = "0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            load_table1();
            fillcombobox();
            autocompletetxt();
            label35.Text = "0";
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Rate")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("rate LIKE '%{0}%'", textBox10.Text);
                dataGridView3.DataSource = DV;
            }


            else if (comboBox2.Text == "Quantity")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("quantity LIKE '%{0}%'", textBox10.Text);
                dataGridView3.DataSource = DV;
            }

            else if (comboBox2.Text == "Expiry Date")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("expirydate LIKE '%{0}%'", textBox10.Text);
                dataGridView3.DataSource = DV;
            }
            else if (comboBox2.Text == "Product Name")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("productname LIKE '%{0}%'", textBox10.Text);
                dataGridView3.DataSource = DV;
            }

            else if (comboBox2.Text == "Recorded on")
            {
                DataView DV = new DataView(dbdataset);
                DV.RowFilter = String.Format("recordedon LIKE '%{0}%'", textBox10.Text);
                dataGridView3.DataSource = DV;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel7.Visible = false;
            load_table();
            load_table1();
          
           
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
			
		}

        private void tabPage3_MouseClick(object sender, MouseEventArgs e)
        {
			

		}  

        private void tabPage5_MouseClick(object sender, MouseEventArgs e)
        {
            panel7.Visible = false;
           
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = barcode.Draw(label7.Text, 50);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lineShape1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("update medicalstore.pharmacyrecord set productname='" + this.textBox4.Text + "',quantity='" + this.textBox5.Text + "',rate='" + this.textBox6.Text + "',expirydate='" + this.textBox7.Text + "',recordedon='" + this.label17.Text + "',batch='" + textBox12.Text + "' where  batch='" + textBox12.Text + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                }
                MessageBox.Show("Informations Updated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

       
        void stock_financialhistory()
        {
            int total = 0;
            int total_q = 0;
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                int p = Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value);
                int q = Convert.ToInt32(dataGridView3.Rows[i].Cells[3].Value);
                total = total + p*q;
                total_q = total_q + p;
                while (dataGridView3.Rows.Count == 0)
                    continue;
            }
            label40.Text = total.ToString();
            label41.Text = total_q.ToString();
        }


        

        void bill_betndate()
        {
            //int a = Convert.ToInt32(label25.Text);
            int total = 0;
            int total_q = 0;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                int p = Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                int q = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                total = total + p;
                total_q = total_q + q;
                while (dataGridView2.Rows.Count == 0)
                    continue;
            }
            label25.Text = total.ToString();
            label26.Text = total_q.ToString();
        }
        void bill_partdate()
        {
           
            int total = 0;
            int total_q = 0;
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                int p = Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                int q = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                total = total + p;
                total_q = total_q + q;
                while (dataGridView2.Rows.Count == 0)
                    continue;
            }
            label33.Text = total.ToString();
            label34.Text = total_q.ToString();
          
        }

        private void button12_Click(object sender, EventArgs e)
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn,productname,sum(quantity) as quantity,rate,total,recordedon,invoiceno From medicalstore.pharmacy where recordedon between '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' And '" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "' group by productname", conDataBase);
            conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView2.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                bill_betndate();

            if (checkBox1.CheckState == CheckState.Checked)
            {
                chart2.Series["Quantity"].ChartType = SeriesChartType.Pie;
                this.chart2.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

                checkbox3_limitfunction();
                panel6.Visible = true;
            }
            else
            {
                panel6.Visible = false;
            }

        }

        private void tabPage4_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = false;

        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bill_partdate();

                
            }
        }

        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
        }

        private void tabPage4_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
        }
        
       

        private void button13_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label21_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void pharmacy_FormClosed(object sender, FormClosedEventArgs e)
        {
            backupdata();
            Application.Exit();
        }
        void stockrecord_count()
        {
            int c = dataGridView3.Rows.Count-1;
            label35.Text = c.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn,productname,sum(quantity) as quantity,rate,expirydate,recordedon from medicalstore.pharmacyrecord where recordedon>=DATE_ADD(now(), INTERVAL -1 day) group by productname order by quantity desc;", conDataBase);
                conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                label44.Text = "Day";
                stockrecord_count();
                stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Pie;
                    this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn,productname,sum(quantity) as quantity,rate,expirydate,recordedon from medicalstore.pharmacyrecord where recordedon>=DATE_ADD(now(), INTERVAL -1 month) group by productname order by quantity desc;", conDataBase);
                conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                label44.Text = "Month";
                stockrecord_count();
                stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Pie;
                    this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn,productname,sum(quantity) as quantity,rate,expirydate,recordedon from medicalstore.pharmacyrecord where recordedon>=DATE_ADD(now(), INTERVAL -1 week) group by productname order by quantity desc;", conDataBase);
                conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                label44.Text = "Week";
                stockrecord_count();
                stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Pie;
                    this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("select sn,productname,sum(quantity) as quantity,rate,expirydate,recordedon from medicalstore.pharmacyrecord group by productname order by quantity desc;", conDataBase);
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView3.DataSource = bSource;

                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                label44.Text = "All Time";
                stockrecord_count();
                stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Pie;
                    this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage5_MouseMove(object sender, MouseEventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            //panel7.Visible = false;
        }

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }
        void combobox4_function()
        {
            if (comboBox4.Text == "Area")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Area;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Bar")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Bar;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Boxplot")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.BoxPlot;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Bubble")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Bubble;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Candlestick")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Candlestick;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Column")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Column;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Doughnut")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Doughnut;
                this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

            }
            //else if (comboBox4.Text == "Errorbar")
            // chart1.Series["Quantity"].ChartType = SeriesChartType.ErrorBar;
            else if (comboBox4.Text == "Fastline")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.FastLine;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Fastpoint")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.FastPoint;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Funnel")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Funnel;
                this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

            }
            else if (comboBox4.Text == "Kagi")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Kagi;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Line")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Line;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Pie")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Pie;
                this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

            }
            else if (comboBox4.Text == "Point")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Point;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Point and Figure")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.PointAndFigure;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Polar")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Polar;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Pyramid")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Pyramid;
                this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";

            }
            else if (comboBox4.Text == "Radar")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Radar;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Range")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Range;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Rangebar")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.RangeBar;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "RangeColumn")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.RangeColumn;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Renko")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Renko;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "Spline")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.Spline;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "SplineArea")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.SplineArea;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "SplineRange")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.SplineRange;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "StackedArea")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.StackedArea;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "StackedBar")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.StackedBar;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
            else if (comboBox4.Text == "StepLine")
            {
                chart1.Series["Quantity"].ChartType = SeriesChartType.StepLine;
                this.chart1.Series["Quantity"].LegendText = "Quantity";
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            combobox4_function();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox5.Text == "Berry")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Berry;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Bright")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Bright;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "BrightPastel")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.BrightPastel;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Chocolate")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Chocolate;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "EarthTones")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.EarthTones;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Excel")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Excel;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Fire")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Fire;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Grayscale")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Grayscale;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Light")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Light;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "Pastel")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.Pastel;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "SeaGreen")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.SeaGreen;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
                else if (comboBox5.Text == "SemiTransparent")
                {
                    chart1.Series["Quantity"].Palette = ChartColorPalette.SemiTransparent;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";
                    limit_chart1();
                    combobox4_function();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void checkbox2_limitfunction()
        {
            if (dataGridView3.Rows.Count > 9)
            {
                if (checkBox2.CheckState == CheckState.Checked)
                {
                    limit_chart1();
                }
                else if (checkBox2.CheckState == CheckState.Unchecked)
                {
                    load_chart1();
                }
            }
            else
            {
                load_chart1();
            }
        }

        void checkbox3_limitfunction()
        {
            if (dataGridView2.Rows.Count > 9)
            {
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    limit_chart2();
                }
                else if (checkBox2.CheckState == CheckState.Unchecked)
                {
                    load_chart2();
                }
            }
            else
            {
                load_chart2();
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkbox2_limitfunction();
            combobox4_function();

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["sno"].Value = (e.RowIndex + 1);
        }

        private void comboBox7_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
				e.Handled = true;
				e.SuppressKeyPress = true;
				label11.Focus();

			}
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select rate,quantity,expirydate from medicalstore.pharmacyrecord where batch='" + comboBox6.Text + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                if (myReader.Read())
                {
                    String rate = myReader.GetString("rate");
                    comboBox8.Text = rate;
                    String qty = myReader.GetString("quantity");
                    label11.Text = qty;
                    String exp = myReader.GetString("expirydate");
                    label24.Text = exp;

                }

                if (label11.Text == "0")
                {
                    textBox11.BackColor = Color.Red;
                    label22.Text = "Not Available";
                }
                else
                {
                    textBox11.BackColor = Color.Green;
                    label22.Text = "Available";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {

                comboBox9.Items.Clear();
                String sname = textBox8.Text;
                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("select * from medicalstore.pharmacyrecord where productname ='" + sname + "';", conDataBase);
                MySqlDataReader myReader;
                try
                {
                    conDataBase.Open();
                    myReader = cmdDataBase.ExecuteReader();
                    

                    while (myReader.Read())
                    {

                        
                        String batch = myReader.GetString("batch");
                        comboBox9.Items.Add(batch);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void panel6_DoubleClick(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void chart2_DoubleClick(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void pharmacy_DoubleClick(object sender, EventArgs e)
        {
            fillcombobox();
            autocompletetxt();
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {

           
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            fillcombobox();
            autocompletetxt();
        }

        void load_chart1_foryearly_monthly()
        {

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            //this.chart1.Series["Quantity"]["PieLabelStyle"] = "Outside";

            this.chart1.Series["Quantity"]["PieLabelStyle"] = "Disabled";
            //this.chart1.Titles.Add("Product vs Quantity");
            //chart1.Series["Quantity"].SmartLabelStyle.Enabled = true;

            for (int i = 0; i <= dataGridView3.Rows.Count - 2; i++)
            {
                this.chart1.Series["Quantity"].Points.AddXY(dataGridView3.Rows[i].Cells[0].Value, dataGridView3.Rows[i].Cells[1].Value);
                //this.chart1.Series["Rate"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value);
                while (dataGridView3.Rows.Count == 0)
                    continue;

            }
            //this.chart2.Legends.Add("Legend1");
            //chart1.Series[0].Label = "#VALX".ToString();
            this.chart1.Series["Quantity"].LegendText = "#VALX (#PERCENT{P2})";
            this.chart1.DataManipulator.Sort(PointSortOrder.Ascending, chart1.Series["Quantity"]);
        }


        private void comboBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {

                if (comboBox12.Text == "Yearly")
                {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT t.sn, year(t.recordedon) as Year, sum(t.quantity) as NetQuantity from (select* from medicalstore.pharmacyrecord where recordedon between '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' And '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "') as t where t.productname='" + comboBox13.Text+"' group by Year order by year;", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView3.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                    label44.Text = comboBox13.Text;
                    stockrecord_count();
                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart1.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart1.Series["Quantity"].LegendText = "Quantity";


                        checkbox2_limitfunction();
                        panel4.Visible = true;
                    }
                    else
                    {
                        panel4.Visible = false;
                    }


                }

                else if (comboBox12.Text == "Monthly")
                {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT t.sn, monthname(t.recordedon) as Month, sum(t.quantity) as NetQuantity from (select* from medicalstore.pharmacyrecord where recordedon between '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' And '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "') as t where t.productname='" + comboBox13.Text + "' group by Month ORDER BY FIELD(month,'January','February','March','April','May','June','July','August','September','October','November','December'); ", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView3.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                    label44.Text = comboBox13.Text;
                    stockrecord_count();
                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart1.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart1.Series["Quantity"].LegendText = "Quantity";


                        checkbox2_limitfunction();
                        panel4.Visible = true;
                    }
                    else
                    {
                        panel4.Visible = false;
                    }


                }
            }
            


        }

        private void button18_Click(object sender, EventArgs e)
        {


            if (comboBox12.Text == "Yearly")
            {


                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn, year(recordedon) as Year, sum(quantity) as NetQuantity from medicalstore.pharmacyrecord group by Year order by year;", conDataBase);
                conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                label44.Text = comboBox13.Text;
                stockrecord_count();
                //stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Column;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";


                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }


            }

            else if (comboBox12.Text == "Monthly")
            {


                String constring = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn, monthname(recordedon) as Month, sum(quantity) as NetQuantity from medicalstore.pharmacyrecord group by Month ORDER BY FIELD(month,'January','February','March','April','May','June','July','August','September','October','November','December'); ", conDataBase);
                conDataBase.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView3.DataSource = bSource;

                sda.Update(dbdataset);

                conDataBase.Close();
                label44.Text = comboBox13.Text;
                stockrecord_count();
                //stock_financialhistory();
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    chart1.Series["Quantity"].ChartType = SeriesChartType.Column;
                    this.chart1.Series["Quantity"].LegendText = "Quantity";


                    checkbox2_limitfunction();
                    panel4.Visible = true;
                }
                else
                {
                    panel4.Visible = false;
                }


            }


        }

        private void label61_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void label56_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel7.Visible = true;
        }

        private void comboBox14_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                if (comboBox15.Text == "Yearly")
                {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT t.sn, year(t.recordedon) as Year, sum(t.quantity) as NetQuantity from (select* from medicalstore.pharmacy where recordedon between '" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "' And '" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "') as t where t.productname='" + comboBox14.Text + "' group by Year order by year;", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView2.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                   // label44.Text = comboBox13.Text;
                 //   stockrecord_count();
                    
                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart2.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart2.Series["Quantity"].LegendText = "Quantity";


                        checkbox3_limitfunction();
                        panel6.Visible = true;
                    }
                    else
                    {
                        panel6.Visible = false;
                    }


                }

                else if (comboBox15.Text == "Monthly")
                {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT t.sn, monthname(t.recordedon) as Month, sum(t.quantity) as NetQuantity from (select* from medicalstore.pharmacy where recordedon between '" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "' And '" + dateTimePicker7.Value.ToString("yyyy-MM-dd") + "') as t where t.productname='" + comboBox14.Text + "' group by Month ORDER BY FIELD(month,'January','February','March','April','May','June','July','August','September','October','November','December'); ", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView2.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                   // label44.Text = comboBox13.Text;
                    //stockrecord_count();
                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart2.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart2.Series["Quantity"].LegendText = "Quantity";


                        checkbox3_limitfunction();
                        panel6.Visible = true;
                    }
                    else
                    {
                        panel6.Visible = false;
                    }


                }
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {

            if (comboBox15.Text == "Yearly")
            {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn, year(recordedon) as Year, sum(quantity) as NetQuantity from medicalstore.pharmacy group by Year order by year;", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView2.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                    // label44.Text = comboBox13.Text;
                    //   stockrecord_count();

                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart2.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart2.Series["Quantity"].LegendText = "Quantity";


                        checkbox3_limitfunction();
                        panel6.Visible = true;
                    }
                    else
                    {
                        panel6.Visible = false;
                    }


                }

                else if (comboBox15.Text == "Monthly")
                {


                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT sn, monthname(recordedon) as Month, sum(quantity) as NetQuantity from medicalstore.pharmacy group by Month ORDER BY FIELD(month,'January','February','March','April','May','June','July','August','September','October','November','December'); ", conDataBase);
                    conDataBase.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    dataGridView2.DataSource = bSource;

                    sda.Update(dbdataset);

                    conDataBase.Close();
                    // label44.Text = comboBox13.Text;
                    //stockrecord_count();
                    //stock_financialhistory();
                    if (checkBox1.CheckState == CheckState.Checked)
                    {
                        chart2.Series["Quantity"].ChartType = SeriesChartType.Column;
                        this.chart2.Series["Quantity"].LegendText = "Quantity";


                        checkbox3_limitfunction();
                        panel6.Visible = true;
                    }
                    else
                    {
                        panel6.Visible = false;
                    }


                }
            

        }

        private void button20_Click(object sender, EventArgs e)
        {
            addmany am = new addmany();
            am.ShowDialog();
        }
	}
}
