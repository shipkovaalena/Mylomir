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

namespace Mylomir
{
    public partial class ClientsTable : Form
    {
        public ClientsTable()
        {
            InitializeComponent();
        }
        string role = Data.role;
        string con = Data.con;
        private void button3_Click(object sender, EventArgs e)
        {
            if (role == "1")
            {
                Admin admin = new Admin();
                this.Visible = false;
                admin.ShowDialog();
                this.Close();
            }
            else
            {
                Salesman salesman = new Salesman();
                this.Visible = false;
                salesman.ShowDialog();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                int r = e.RowIndex, c = e.ColumnIndex;
                string id = dataGridView1.Rows[r].Cells[0].Value.ToString();

                switch (c)
                {
                    case 5:
                        ClientEdit edit = new ClientEdit(id);
                        this.Visible = false;
                        edit.ShowDialog();
                        this.Close();
                        break;
                    case 6:
                        DialogResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteData(id);
                            MessageBox.Show("Запись успешно удалена!", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FillTableData();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Ошибка выделения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Pagination()
        {
            try
            {
                // удаляем LinkLabel служащий для пагинации
                // каждый раз будем создавать новую пагинацию
                for (int j = 0, count = this.Controls.Count; j < count; ++j)
                {
                    this.Controls.RemoveByKey("page" + j);
                }

                // узнаём сколько страниц будет
                int size = dataGridView1.Rows.Count / 20; // на каждой странице по 20 зиписей
                if (Convert.ToBoolean(dataGridView1.Rows.Count % 20)) size += 1; // ситуакиця когда при делении получаем не целое число
                LinkLabel[] ll = new LinkLabel[size]; // пагинация на основе элемента ссылка(можно использовать и другой элемент)
                int x = 421, y = 621, step = 15; // место на форме для меню пагинации и расстояние между номерами страниц

                for (int i = 0; i < size; ++i)
                {
                    ll[i] = new LinkLabel();
                    ll[i].Text = Convert.ToString(i + 1); // текст(номер старницы) который видет пользователь
                    ll[i].Name = "page" + i;
                    ll[i].AutoSize = true; //!!!
                    ll[i].Location = new Point(x, y);
                    ll[i].Click += new EventHandler(LinkLabel_Click); // один обработчик для всех пунктов пагинации
                    this.Controls.Add(ll[i]); // добавление на форму

                    x += step;
                }

                // чтобы понять на какой странице пользователь убираем подчеркивание для активной странице
                // по умолчанию первая страница активна
                ll[0].LinkBehavior = LinkBehavior.NeverUnderline;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        // выбор страницы пагинации
        // те строки которые нам не нужны на выбраной странице - скрываем
        private void LinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                // возвращаем всем LinkLabel подчеркивание
                foreach (var ctrl in this.Controls)
                {
                    if (ctrl is LinkLabel)
                    {
                        (ctrl as LinkLabel).LinkBehavior = LinkBehavior.AlwaysUnderline;
                    }
                }

                // узнаём какая страница выбрана и убираем подчеркивание для неё
                LinkLabel l = sender as LinkLabel;
                l.LinkBehavior = LinkBehavior.NeverUnderline;

                // узнаём с какой и по какую строку отображать информацию в таблицу
                // другие строки будем скрывать
                int numPage = Convert.ToInt32(l.Text) - 1;
                int countRows = dataGridView1.Rows.Count;
                int sizePage = 20;
                int start = numPage * sizePage;
                int stop = (countRows - start) >= sizePage ? start + sizePage : countRows;

                for (int j = 0; j < countRows; ++j)
                {
                    if (j < start || j > stop)
                    {
                        dataGridView1.CurrentCell = null;
                        dataGridView1.Rows[j].Visible = false;
                    }
                    else
                    {
                        dataGridView1.Rows[j].Visible = true;
                    }
                }            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClientsTable_Load(object sender, EventArgs e)
        {
            FillTableData();
            Pagination();
        }
        private void FillTableData()
        {
            dataGridView1.Columns.Clear();
            string cmd = @"SELECT * FROM client";
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand(cmd, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["ClientID"].Visible = false;
            dataGridView1.Columns["ClientSurname"].HeaderText = "Фамилия";
            dataGridView1.Columns["ClientName"].HeaderText = "Имя";
            dataGridView1.Columns["ClientPatronumic"].HeaderText = "Отчество";
            dataGridView1.Columns["ClientNumberPhone"].HeaderText = "Номер телефона";

            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn1);
            buttonColumn1.UseColumnTextForButtonValue = true;
            buttonColumn1.Text = "Изменить";
            
            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(buttonColumn2);
            buttonColumn2.UseColumnTextForButtonValue = true;
            buttonColumn2.Text = "Удалить";
            connection.Close();
        }
        private void DeleteData(string id)
        {
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand command = new MySqlCommand($@"DELETE FROM client WHERE ClientID = '{id}'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientAdd add = new ClientAdd();
            this.Visible = false;
            add.ShowDialog();
            this.Close();
        }
    }
}
