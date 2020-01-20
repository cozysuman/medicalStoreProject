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

namespace Medical_store_management_system
{
    public partial class batch : Form
    {
        pharmacy pharm = null;

        public batch(string str1,pharmacy mainform)
        {
            pharm = mainform;
            InitializeComponent();
            label1.Text = str1;
        }

       

        DataTable dbdataset;
        private pharmacy pharmacy;

        void load_table()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select batch as Batch,expirydate as ExpiryDate,quantity as Quantity,rate as Rate from medicalstore.pharmacyrecord where productname='"+label1.Text+"' ;", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;

                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void batch_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            load_table();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                int p = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                p = i + 1;
                dataGridView1.Rows[i].Cells[0].Value = p;
               

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //pharmacy ph = new pharmacy(dataGridView1.CurrentRow.Cells[2].Value.ToString());

            // ph.comboBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            pharm.update_fromform_batch(dataGridView1.CurrentRow.Cells[1].Value.ToString(),dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString());
            this.Close();

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            pharm.update_fromform_batch(dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString());
            this.Close();
        }
    }
}
