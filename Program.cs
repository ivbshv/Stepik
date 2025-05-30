using MySql.Data.MySqlClient;
using Stepik.Models;
using Stepik.Models.Services;

public class Program
{

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Войти
2. Зарегистрироваться
3. Закрыть приложение

************************************************
");


            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UsersService.LoginUser();
                    break;
                case "2":
                    RegisterUser();
                    break;
                default:
                    Console.WriteLine("До свидания!");
                    return;
            }
        }
    }

    public static void RegisterUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string fullName = Console.ReadLine();

        User user = new User { FullName = fullName };

        bool isAdded = UsersService.Add(user);

        if (isAdded)
        {
            Console.WriteLine($"Пользователь '{fullName}' успешно добавлен.\n");
        }
        else
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
        }
    }

}