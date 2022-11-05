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
            Dictionary<string, string> profiles = new ();
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
                        Console.ReadKey();
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
            bool isAvailable = false;
            string fullUsersName = "";

            while (isAvailable == false)
            {
                Console.WriteLine("Введите ФИО.");
                fullUsersName = Console.ReadLine();

                if (profile.ContainsKey(fullUsersName) == true)
                {
                    Console.WriteLine("Такое досье уже есть");
                }
                else
                {
                    isAvailable = true;
                }
            }

            Console.WriteLine("Введите должность.");
            string usersPosition = Console.ReadLine();
            profile.Add(fullUsersName, usersPosition);
            Console.WriteLine("Досье добавлено.");
        }

        static void WriteProfile(Dictionary<string, string> profiles)
        {
            if (profiles.Count > 0)
            {
                int tempNumberFile = 0;

                foreach (var file in profiles)
                {
                    tempNumberFile++;
                    Console.WriteLine($"{tempNumberFile}. {file.Key} - {file.Value}");
                }
            }
            else
            {
                Console.WriteLine("Empty");
            }
        }

        static void DeleteProfile(Dictionary<string, string> profiles)
        {
            if (profiles.Count > 0)
            {
                Console.WriteLine("Введите ФИО, чтобы удалить:");
                string userInput = Console.ReadLine();

                if (profiles.ContainsKey(userInput) == true)
                {
                    Console.WriteLine($"Досье : {userInput} - {profiles[userInput]} удалено.");
                    profiles.Remove(userInput);
                }
                else
                {
                    Console.WriteLine("Не найден с таким ФИО. Попробуйте еще раз.");
                }
            }
            else
            {
                Console.WriteLine("Empty");
            }
        }
    }
}