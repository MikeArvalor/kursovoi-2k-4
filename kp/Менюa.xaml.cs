using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace kp
{
	/// <summary>
	/// Логика взаимодействия для Меню.xaml
	/// </summary>
	public partial class Menua : Window
    {
        public Menua()
        {
            InitializeComponent();
            labell1.Content = Voit.people;
        }
        public static string b_answer1;
        public static string b_answer2;
        public static string b_answer3;
        public static string a_answer1;
        public static string a_answer2;
        public int Value { get; set; }

        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly SqlConnection bd1 = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private readonly SqlConnection bd = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private DataTable tables = new DataTable();
        private  DataTable table = new DataTable();
        private DataTable tabless = new DataTable();
        public DateTime? SelectedFromDate { get; set; }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);//возвращ или задает период времени
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { label8.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void DatePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            SelectedFromDate = picker.SelectedDate;

        }
        public DateTime? SelectedFromDate1 { get; set; }
        public DateTime? SelectedBeforeDate { get; set; }
        private void DatePickerBefore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            SelectedBeforeDate = picker.SelectedDate;
        }

        private void DatePickerFrom1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            SelectedFromDate1 = picker.SelectedDate;
        }
      
        private static async Task SendEmailAsync(string text)
        {

            MailAddress from = new MailAddress("mihail14312@mail.ru", "Заказ");
            MailAddress to = new MailAddress(text);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Подтверждение заказа";
            m.Body = $"Уважаемый пользователь Ваш заказ принят.";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("mihail14312@mail.ru", "sv30041978m");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
        private void Prim_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox == null || comboBox1 == null || textBox.Text == "" || textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("не все данные введены");
            }
            else
            {
                try
                {
                    string bd = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
                    string sqlexp = "SELECT id_машины,Автомобиль,Марка,Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки FROM Заказы";
                    using (SqlConnection bd1 = new SqlConnection(bd))
                    {
                        bd1.Open();
                        SqlCommand command = new SqlCommand(sqlexp, bd1);
                        SqlCommand cm1 = bd1.CreateCommand();

                        if (comboBox.Text == "Седан" && comboBox1.Text == "Volkswagen")
                        {
                            a_answer1 = "Седан";
                            b_answer1 = "Volkswagen";
                            cm1.CommandText = $"INSERT INTO Заказы(Автомобиль,Марка, Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки) VALUES ('{a_answer1}', '{b_answer1}','{mod}','{kol}', '{price}','{price1}','{Date1.Text}');";
                            cm1.ExecuteNonQuery();
                        }
                        if (comboBox.Text == "Седан" && comboBox1.Text == "Skoda")
                        {
                            a_answer1 = "Седан";
                            b_answer2 = "Skoda";
                            cm1.CommandText = $"INSERT INTO Заказы(Автомобиль,Марка, Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки) VALUES ('{a_answer1}', '{b_answer2}','{mod}','{kol}', '{price}','{price1}','{Date1.Text}');";
                            cm1.ExecuteNonQuery();
                        }

                        if (comboBox.Text == "Седан" && comboBox1.Text == "Opel")
                        {
                            a_answer1 = "Седан";
                            b_answer3 = "Opel";
                            cm1.CommandText = $"INSERT INTO Заказы(Автомобиль,Марка, Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки) VALUES ('{a_answer1}', '{b_answer3}','{mod}','{kol}', '{price}','{price1}','{Date1.Text}');";
                            cm1.ExecuteNonQuery();
                        }
                        if (comboBox.Text == "Хэтчбэк" && comboBox1.Text == "Opel")
                        {
                            a_answer2 = "Хэтчбэк";
                            b_answer3 = "Opel";
                            cm1.CommandText = $"INSERT INTO Заказы(Автомобиль,Марка, Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки) VALUES ('{a_answer2}', '{b_answer3}','{mod}','{kol}','{price}', '{price1}','{Date1.Text}');";
                            cm1.ExecuteNonQuery();
                        }
                        if (comboBox.Text == "Хэтчбэк" && comboBox1.Text == "Skoda"|| comboBox.Text == "Хэтчбэк" && comboBox1.Text == "Volkswagen")
                        {
                            MessageBox.Show("Такого автомобиля нет");
                        }
                       

                    }


                    SendEmailAsync(pochta.Text).GetAwaiter();
                    MessageBox.Show("Заказ принят");
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
        }

      

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public string mod;

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            mod = textBox2.Text;
        }
        public string kol;

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            kol = textBox1.Text;
        }
        public string price;
        public int price1;


        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            price = textBox.Text;
            price1 = Convert.ToInt32(price) * Convert.ToInt32(kol);

        }




        private async void View(object sender, RoutedEventArgs e)
        {

            try
            {
                await conn.OpenAsync();
                tables = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * from Заказы  ", conn);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static int numbers = 0;
        public static int id_car = 0;
        public static int zena = 0;
        public static int price2;
        private void Findclick(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExp = $"select Количество,id_машины,Цена_за_одну from Заказы where Модель='{ModelTextBox.Text}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = new SqlCommand(sqlExp, connection);
                SqlDataReader reader = cm1.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        numbers = Convert.ToInt32(reader.GetValue(0));
                        id_car = Convert.ToInt32(reader.GetValue(1));
                        zena = Convert.ToInt32(reader.GetValue(2));
                    }
                    NumberTextBox.Text = numbers.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Модель найдена");
                    connection.Close();


                }
            }

        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            int cen = Convert.ToInt32(price) * Convert.ToInt32(NumberTextBox.Text);
            string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = $"UPDATE Заказы SET Количество='{Convert.ToInt32(NumberTextBox.Text)}' WHERE id_машины='{id_car}'";
                    cm1.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Значение успешно изменено,нажмите на показать");
                }
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    cm1.CommandText = $"UPDATE Заказы set Общая_цена='{cen}'";
                    cm1.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Значение успешно изменено,нажмите на показать");
                }

            }

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cm1 = connection.CreateCommand();
                try
                {
                    
                    cm1.CommandText = $"delete from  Заказы WHERE Модель='{ModelTextBox.Text}'";
                    cm1.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Значение успешно изменено,нажмите на показать");
                    connection.Close();
                }

            }
        }
        //----------------------------------------------------------------------------------------------------------
        private async void V1(object sender, RoutedEventArgs e)
        {

            try
            {
                await bd1.OpenAsync();
                table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * from Склад ", bd1);
                adapter.Fill(table);
                User_Grid1.DataContext = table.DefaultView;
                bd1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

     
        private void Findclick2(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExp = $"select id_машины,Автомобиль,Модель_автомобиля,Цвет,Объем_двигателя,Расход_топлива,Стоимость from Склад";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    bd1.Open();
                    table = new DataTable();
                    SqlCommand cm3 = new SqlCommand(sqlExp, bd1);
                    SqlDataAdapter adapter = new SqlDataAdapter($"select * from Склад where Автомобиль like '%{marka}%' ",bd1);
 
                     adapter.Fill(table);
                    User_Grid1.DataContext = table.DefaultView;
                    MessageBox.Show("Модель найдена");
                    bd1.Close();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }
        public string marka;
        private void mark_TextChanged(object sender, TextChangedEventArgs e)
        {
            marka = mark.Text;
        }

     

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void butto_Click(object sender, RoutedEventArgs e)
        {
            Voit v = new Voit();
            v.Show();
            this.Close();
        }
        
        private void pochta_TextChanged(object sender, TextChangedEventArgs e)
        {
         
        }

        private void User_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
		
	}
}


