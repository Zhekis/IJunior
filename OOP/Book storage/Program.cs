using System.Security.Cryptography;

namespace Book_storage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            storage.Work();
        }

        class Storage
        {
            private List<Book> _books = new List<Book>();

            public Storage()
            {
                _books.Add(new Book("Zhek", "Game", 1960, 100));
                _books.Add(new Book("Bilbo", "Haricane", 1990, 200));
            }

            public void Work()
            {
                const string ShowAllBooks = "1";
                const string AddBook = "2";
                const string RemoveBook = "3";
                const string SearchBooks = "4";
                const string Exit = "5";

                bool isWork = true;
                string userInput;

                while (isWork)
                {
                    Console.Clear();
                    Console.WriteLine("Хранилище книг. Что хотите сделать?");
                    Console.WriteLine(ShowAllBooks + " - Показать книги.\n" + AddBook + " - Добавить книгу.");
                    Console.WriteLine(RemoveBook + " - Убрать книгу.\n" + SearchBooks + " - Найти книги по параметру.\n" + Exit + " - Выход");
                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case ShowAllBooks: 
                            Show();
                            break;

                        case AddBook:
                            Add();
                            break;

                        case RemoveBook:
                            Delete();
                            break;

                        case SearchBooks:
                            Search();
                            break;

                        case Exit:
                            isWork = false;
                            break;
                    }
                }
            }

            private void Add()
            {
                Console.WriteLine("Autor :");
                string autorNewBook = Console.ReadLine();
                Console.WriteLine("Title :");
                string titleNewBook = Console.ReadLine();
                Console.WriteLine("Year :");
                int yearNewBook = ReadInt();
                Console.WriteLine("Pages :");
                int pagesNewBook = ReadInt();

                _books.Add(new Book(autorNewBook, titleNewBook, yearNewBook, pagesNewBook));
                Console.ReadKey();
            }

            private void Delete()
            {
                Console.WriteLine("Под каким номером убрать книгу?");
                int removeBook = ReadInt();
                removeBook--;

                for (int i = 0; i < _books.Count; i++)
                {
                    if (removeBook == i)
                    {
                        _books.RemoveAt(i);
                        Console.WriteLine("Книга убрана.");
                    }
                }

                Console.ReadKey();
            }

            private void Show()
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    Console.WriteLine($"Number {i+1}");
                    _books[i].ShowInfo();
                }

                Console.ReadKey();
            }

            private void Search()
            {

            }

            private int ReadInt()
            {
                bool isNumber = false;
                int result = 0;

                while (isNumber == false)
                {
                    string userInput = Console.ReadLine();
                    isNumber = int.TryParse(userInput, out int number);

                    if (isNumber == true)
                        result = number;
                    else
                        Console.WriteLine("Введите число.");
                }

                return result;
            }
        }

        class Book
        {
            private string _autor;
            private string _title;
            private int _year;
            private int _countPages;

            public Book(string autor, string title, int year, int countPages)
            {
                _autor = autor;
                _title = title;
                _year = year;
                _countPages = countPages;
            }
            public void ShowInfo()
            {
                Console.WriteLine($"Автор: {_autor}, название: {_title}, год выпуска: {_year}, количество страниц: {_countPages}.");
            }
        }
    }
}