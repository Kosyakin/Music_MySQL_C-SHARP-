using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Music
{
    public partial class Form2 : Form
    {
        // запрос

        private void Update_Grid(int g)
        {
            
            this.dataGridView1.Rows.Clear();  // удаление всех строк
            int count = this.dataGridView1.Columns.Count;
            for (int i = 0; i < count; i++)     // цикл удаления всех столбцов
            {
                this.dataGridView1.Columns.RemoveAt(0);
            }

            string Connect = "Database = music; Data Source =localhost; User Id =root; Password=root;";
            MySqlConnection connection = new MySqlConnection(Connect);
            connection.Open();
            // запрос
            string query = "SELECT id,Title, Performer, Album, Year, Duration FROM tegs;"; 
            if (g == 0)
            { query = "SELECT id,Title, Performer, Album, Year, Duration FROM tegs;"; }
        
            
            if (g == 1)
            {
                query = "SELECT id,Title, Performer, Album, Year, Duration FROM tegs WHERE Year < 1990 AND Year>1979;";
            }
            if (g == 2) { query = "SELECT id,Title, Performer, Album, Year, Duration FROM tegs WHERE Year < 2000 AND Year>1989;";
            }
            if (g == 3) { query = "SELECT id,Title, Performer, Album, Year, Duration FROM tegs WHERE Year >2000;"; }

            // запрос на чтение
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();



            // добавляем столбцы и их название
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns["id"].Width = 30;
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns["Title"].Width = 120;
            dataGridView1.Columns.Add("Performer", "Performer");
            dataGridView1.Columns["Performer"].Width = 120;
            dataGridView1.Columns.Add("Album", "Album");
            dataGridView1.Columns["Album"].Width = 120;
            dataGridView1.Columns.Add("Year", "Year");
            dataGridView1.Columns["Year"].Width = 40;
            dataGridView1.Columns.Add("Duration", "Duration");
            dataGridView1.Columns["Duration"].Width = 50;



            // читаем

            try
            {

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["id"].ToString(),
                   reader["Title"].ToString(), reader["Performer"].ToString(), reader["Album"].ToString(), reader["Year"].ToString(), reader["Duration"].ToString());

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Connection.Close();
            }
        }
        
        public Form2()
        {

            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            dataGridView1.AllowUserToAddRows = false;

            Update_Grid(0);
        }


    
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int g = 0;
            Update_Grid(g);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int g = 2;
            Update_Grid(g);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int g = 3;
            Update_Grid(g);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int g = 1;
            Update_Grid(g);
        }
    }

}


