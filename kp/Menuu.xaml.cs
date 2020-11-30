using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Configuration;
using System.Data.Common;
using System.Collections.Specialized;

namespace kp
{
    /// <summary>
    /// Логика взаимодействия для Menuu.xaml
    /// </summary>
    public partial class Menuu : Window
    {
        public DateTime? SelectedFromDate1 { get; set; }
        public DateTime? SelectedBeforeDate { get; set; }
        private readonly SqlConnection bd = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private DataTable tabless = new DataTable();
        DispatcherTimer timer;
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

        public Menuu()
        {
            InitializeComponent();
            labell1.Content = Voit.people;
        }


        private void butto_Click(object sender, RoutedEventArgs e)
        {
            Voit m2 = new Voit();
            m2.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
        private DataTable tables = new DataTable();
        private async void View_CLick(object sender, RoutedEventArgs e)
        {

            try
            {
                await conn.OpenAsync();
                tables = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * from  registr ", conn);
                adapter.Fill(tables);
                User_Grid.DataContext = tables.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Otchet_click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=kursovoi;Integrated Security=True";
            string sqlExp = $"select id_машины,Автомобиль,Марка,Модель,Количество,Цена_за_одну,Общая_цена,Дата_поставки  from Склад";
            using (SqlConnection connec = new SqlConnection(ConnectionString))
            {
                connec.Open();
                SqlCommand cm1 = connec.CreateCommand();
                try
                {
                    bd.Open();
                    tabless = new DataTable();
                    SqlCommand cm3 = new SqlCommand(sqlExp, bd);
                    SqlDataAdapter adapte = new SqlDataAdapter($"select * from Заказы where Дата_поставки between " + "'" + SelectedFromDate1 + "'" + " and " + "'" + SelectedBeforeDate + "'", bd);

                    adapte.Fill(tabless);
                    User_Grid2.DataContext = tabless.DefaultView;

                    bd.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { label1.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void User_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

