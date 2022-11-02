namespace Accaunt_profiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddForm = "1";
            const string WriteForm = "2";
            const string DeleteForm = "3";
            const string Exit = "4";
            Dictionary<string, string> profiles = new Dictionary<string, string>();
            bool isOpen = true;
            string userInput;

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine(AddForm + " - Добавить досье\n" + WriteForm + " - Вывести досье\n" + DeleteForm + " - Удалить досье\n" + Exit + " - Выход");
                Console.Write("Введите команду: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddForm:
                        AddProfile(profiles);
                        break;
                    case WriteForm:
                        WriteProfile(profiles);
                        Console.ReadKey();
                        break;
                    case DeleteForm:
                        DeleteProfile(profiles);
                        Console.ReadKey();
                        break;
                    case Exit:
                        isOpen = false;
                        break;
                }
            }
        }

        static void AddProfile(Dictionary<string,string> profile)
        {
            Console.WriteLine("Введите ФИО.");
            string fullUsersName = Console.ReadLine();
            Console.WriteLine("Введите должность.");
            string usersPosition = Console.ReadLine();
            profile.Add(fullUsersName, usersPosition);
            Console.WriteLine("Done");
        }

        static void WriteProfile(Dictionary<string, string> profiles)
        {
            int i = 0;

            foreach (var file in profiles)
            {
                i++;
                Console.WriteLine($"{i}. {file.Key} - {file.Value}");
            }
        }

        static void DeleteProfile(Dictionary<string, string> profile)
        {
            //if (profile.Count > 0)
            //{
            //    Console.WriteLine("Какой номер досье удалить?");
            //    int numberUser = Convert.ToInt32(Console.ReadLine()) - 1;

            //    if (numberUser < fullUsersNames.Length)
            //    {
            //        DeleteProfile(ref fullUsersNames, numberUser);
            //        DeleteProfile(ref usersPositions, numberUser);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Нет такого");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Empty");
            //}
            //phoneBook.Remove("+123456");
        }
    }
}