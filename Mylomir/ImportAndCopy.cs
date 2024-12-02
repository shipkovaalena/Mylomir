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
using System.IO;

namespace Mylomir
{
    public partial class ImportAndCopy : Form
    {
        public ImportAndCopy()
        {
            InitializeComponent();
        }
        string con = Data.con;
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите восстановить БД?", "Сообщение пользователю", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                MySqlConnection mySqlConnection = new MySqlConnection(con);
                mySqlConnection.Open();

                string pathFile = Directory.GetCurrentDirectory() + @"\structure.sql";
                string textFile = File.ReadAllText(pathFile);
                MySqlCommand mySqlCommand = new MySqlCommand(textFile, mySqlConnection);
                mySqlCommand.ExecuteNonQuery();

                mySqlConnection.Close();

                MessageBox.Show("Структура базы данных успешно восстановлена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = -1;

            comboBox1.Items.Add("Товары");
            //MySqlConnection mySqlConnection = new MySqlConnection(con);
            //mySqlConnection.Open();
            //MySqlCommand mySqlCommand = new MySqlCommand("SHOW TABLES", mySqlConnection);
            //IDataReader dataReader = mySqlCommand.ExecuteReader();

            //while (dataReader.Read())
            //{
            //    comboBox1.Items.Add(dataReader.GetValue(0).ToString());
            //}
            //mySqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MySqlConnection mySqlConnection = new MySqlConnection(con);
                    mySqlConnection.Open();

                    string filePath = openFileDialog1.FileName;
                    string tableName = comboBox1.SelectedItem.ToString();

                    int importRows = ImportCSV(filePath, tableName, mySqlConnection);
                    if (importRows != 0)
                    {
                        MessageBox.Show($"Успешно импортировано {importRows} записей в таблицу {tableName}!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    mySqlConnection.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка импортирования данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int ImportCSV(string csvFilePath, string tableName, MySqlConnection connection)
        {
            int res = 0;
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string headerLine = reader.ReadLine();
                string[] headers = headerLine.Split(';');

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    string query = $"INSERT INTO {tableName} ({string.Join(",", headers)}) VALUES ({string.Join(",", values)})";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    res += command.ExecuteNonQuery();
                }
            }
            return res;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void ImportAndCopy_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
    }
}
