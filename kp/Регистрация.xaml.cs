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
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.ComponentModel;

namespace kp
{
	/// <summary>
	/// Логика взаимодействия для Регистрация.xaml
	/// </summary>
	public partial class Entry : Window
	{



		public Entry()
		{
			InitializeComponent();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			MainWindow m1 = new MainWindow();
			m1.Show();
			this.Close();
		}

		private void Registration(object sender, RoutedEventArgs e)
		{
			if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" || Pravo.Text == ""||pochta.Text=="")
			{
				MessageBox.Show("Не все данные введены");
			}
			else
			{
				if (TextBox1.Text.Length > 15)
				{
					MessageBox.Show("Имя не должно быть больше 15 символов");
				}
				else
				{
					if (TextBox2.Text.Length > 15)
					{
						MessageBox.Show("Фамилия не должна быть больше 15 символов");
					}
					else
					{
						if (TextBox4.Text.Length > 10 || TextBox4.Text.Length < 3)
						{
							MessageBox.Show("Логин должен быть не больше 10  и не меньше ");
						}
						else
						{
							if (TextBox5.Text.Length > 8 || TextBox5.Text.Length < 3)
							{
								MessageBox.Show("пароль не должен иметь меньше 3 символов и не больше 8");
							}
							else
							{
								string ConnectionString = @"Data Source=LENOVO-ПК\SQLEXPRESS;Initial Catalog=Kursovoi;Integrated Security=True";
								string sqlexp = "SELECT r.id,r.name from roles as r";
								try {
									using (SqlConnection reg = new SqlConnection(ConnectionString))
									{
										reg.Open();
										SqlCommand command = new SqlCommand(sqlexp, reg);
										SqlDataReader reader = command.ExecuteReader();
										SqlCommand cm1 = reg.CreateCommand();

										try
										{
										
											int rol = 0;
											while (reader.Read())
											{
										
												if (reader.GetValue(1).Equals(Pravo.Text))
												{
													
													object z = reader.GetValue(0);
													
													rol = (int)z;
												}

											}
											reader.Close();
											cm1.CommandText = $"INSERT INTO registr(name,surname,login_, password_,email, role_id) VALUES ('{name}', '{surname}','{login}','{password.GetHashCode()}','{email}', '{rol}');";
											cm1.ExecuteNonQuery();
											


											MessageBox.Show("Вы успешно зарегестрированы!");


										}
										catch (Exception ex)
										{

											MessageBox.Show(ex.Message);

										}
										reader.Close();

									}
								}
								catch(Exception ex)
								{
									MessageBox.Show(ex.Message);
								}
								}
							

								
						}

					}
				}



			}
		}
		public string name;
		private void nametext(object sender, TextChangedEventArgs e)
		{
			name = TextBox1.Text;
		}
		public string surname;
		private void surnametext(object sender, TextChangedEventArgs e)
		{
			surname = TextBox2.Text;
		}
		public string login;
		private void logintetx(object sender, TextChangedEventArgs e)
		{
			login = TextBox4.Text;
		}
		public string password;
		private void passwordtext(object sender, TextChangedEventArgs e)
		{
			password = TextBox5.Text;
		}
		public string rol;
		private void Pravo_TextChanged(object sender, TextChangedEventArgs e)
		{
		rol = Pravo.Text;
		}
        public string email;
        private void pochta_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = pochta.Text;
        }
    }

}
