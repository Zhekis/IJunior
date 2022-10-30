using System;

namespace VocabDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string searchWord;
            bool wordExists;
            Dictionary <string, string> exlanatoryDictionary = new Dictionary<string, string> ()
            {
                { "Игра", "Один из видов активности человека и животных в процессе их жизнедеятельности."},
                { "Смартфон", "Устройство, объединяющее в себе функции персонального органайзера и мобильного телефона."},
                { "Шпонка", "Соединительный элемент, устанавливаемый в пазах двух деталей."}
            };
            Console.WriteLine("Введите слово :");
            searchWord = Console.ReadLine();

            wordExists = exlanatoryDictionary.ContainsKey(searchWord);

            if (wordExists == true)
                Console.WriteLine($"{searchWord} - {exlanatoryDictionary[searchWord]}");
            else
                Console.WriteLine("Слово не найдено");
        }
    }
}