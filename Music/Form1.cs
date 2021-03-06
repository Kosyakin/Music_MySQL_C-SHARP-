using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Music
{
    public partial class Form1 : Form
    {
        string choose_dpt;
        private object openFileDialog1;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }






        //////////ВЫВОД ТЭГОВ
        public void Select()
        {

            string File = textBox1.Text;
            string Edit = textBox3.Text;
            var audioFile = TagLib.File.Create(@File);


            String Album = audioFile.Tag.Album;
            //String Genre = Convert.ToString(audioFile.Tag.FirstGenre);
            String Title = audioFile.Tag.Title;
            String Year = Convert.ToString(audioFile.Tag.Year);
            String Performer = String.Join(", ", audioFile.Tag.Performers);
            String Duration = Convert.ToString(audioFile.Properties.Duration.ToString("hh\\:mm\\:ss"));

            textBox2.Text = Album;      AlbumLabel.Text = Album;                    //Альбои
            textBox3.Text = Year;       YearLabel.Text = Year;                      //год
            textBox4.Text = Performer;  PerformerLabel.Text = Performer;            //Исполнитель
            textBox5.Text = Duration;   DurationLabel.Text = Duration;              //Длительность
            textBox6.Text = Title;      TitleLabel.Text = Title;                    //Название
            //textBox7.Text = Genre;      GenreLabel.Text = Genre;


        }

 


        /////////// ЗАПИСЬ В DATABASE (Плейлист)
        private void Button_Write_to_DB(object sender, EventArgs e)
        {

            string Connect = "Database = music; Data Source =localhost; User Id =root; Password=root;"; //строка подключения к серверу
            
            try
            {
                string File = textBox1.Text;
                var audioFile = TagLib.File.Create(File);
                String Album = textBox2.Text;
                String Year = textBox3.Text;
                String Performer = textBox4.Text;
                String Duration = textBox5.Text;
                String Title = textBox6.Text;


                File = File.Replace(@"\", @"\\");                     //Удвоение слеша для экранирования слешей mysql


                //textBox2.Text = File;

                MySqlConnection connection = new MySqlConnection(Connect);

                string query = "INSERT INTO tegs(Title, Performer, Album, Year, Duration, File)  VALUES ('" + Title + "','" + Performer + "','" + Album + "','" + Year + "','" + Duration + "','" + File + "');";


                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        ///////////// ОБЗОР ФАЙЛОВ
        private void Buttom_OpenFile(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog2.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    textBox1.Text = Convert.ToString(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }

            }


            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
            Select();
        }

       
    }
}
