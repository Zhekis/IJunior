using System;

namespace VocabDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string searchWord;
            string getWord = "";
            Dictionary <string, string> exlanatoryDict = new Dictionary<string, string> ()
            {
                { "Игра", "Один из видов активности человека и животных в процессе их жизнедеятельности."},
                { "Смартфон", "Устройство, объединяющее в себе функции персонального органайзера и мобильного телефона."},
                { "Шпонка", "Соединительный элемент, устанавливаемый в пазах двух деталей."}
            };
            Console.WriteLine("Введите слово :");
            searchWord = Console.ReadLine();

            foreach (var words in exlanatoryDict)
            {
                if (searchWord == words.Key)
                    getWord = words.Key;
            }

            if (searchWord == getWord)
                Console.WriteLine($"{searchWord} - {exlanatoryDict[getWord]}");
            else
                Console.WriteLine("Слово не найдено");
        }
    }
}