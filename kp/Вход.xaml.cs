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
using System.Configuration;
using System.Data.Common;

namespace kp
{
    /// <summary>
    /// Логика взаимодействия для Вход.xaml
    /// </summary>
    public partial class Voit : Window
    {
        public static int id = 0;
        public static int identy;
        public static string people;
        public Voit()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m2 = new MainWindow();
            m2.Show();
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bd = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=Kursovoi;Integrated Security=True";
            string sqlexp = "SELECT login_,password_,role_id,name,surname,id FROM registr";
            using (SqlConnection bd1 = new SqlConnection(bd))
            {
               bd1.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Count(*) from registr where login_='" + loginBox.Text + "'AND password_='" + passwordBox.Password + "'", bd);
                DataTable td = new DataTable();
                SqlCommand command = new SqlCommand(sqlexp, bd1);
                SqlDataReader reader = command.ExecuteReader();
                SqlCommand cm1 =bd1.CreateCommand();
                da.Fill(td);

                bool a = false;//b
                bool q = false;//c
                while (reader.Read())
                {

                   if (reader.GetValue(0).Equals(loginBox.Text))
                    {
                        a = true;
                        if (reader.GetValue(1).Equals(passwordBox.Password.GetHashCode()))
                        {
                            q = true;
                            object s = reader.GetValue(2);
                            id = (int)s;
                            object ident = reader.GetValue(5);
                            identy = Convert.ToInt32(ident);
                            object surnam = reader.GetValue(4);
                            object nam = reader.GetValue(3);
                            people = String.Concat((string)surnam + " ", (string)nam);

                            if (id == 1)
                            {
                                Menua men = new Menua();
                                men.Show();
                                this.Close();
                            }
                            if (id == 2)
                            {
                                Menuu me = new Menuu();
                                me.Show();
                                this.Close();
                            }
                        }
                        break;
                    }
                }
                if (!a)
                {
                    MessageBox.Show("Такого логина не существует! Проверьте введенные данные.");
                    reader.Close();
                    bd1.Close();
                    return;
                }
                if (!q)
                {
                    MessageBox.Show("Пароль неправильный! Проверьте введенные данные.");
                    reader.Close();
                    bd1.Close();
                    return;
                }
                reader.Close();
                bd1.Close();
            }
        }
    }
    }

    
