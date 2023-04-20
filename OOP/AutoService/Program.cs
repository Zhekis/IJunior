using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;

namespace AutoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Service service = new Service();
            service.Work();
        }
    }

    class Service
    {
        private int _balanceMoney = 100000;
        private int _fine = 1000;
        private List<Detail> _details;
        private Dictionary<string, int> _countParts = new Dictionary<string, int>();
        private Queue<Car> _cars = new Queue<Car>();

        public Service()
        {
            Random random = new Random();
            _details = new List<Detail>() {new Detail("Колесный диск", 5000, 200), new Detail("Шина", 4000, 200), new Detail("Радиатор", 8000, 2000), 
            new Detail("Катализатор", 10000, 5000), new Detail("Масло", 5000, 1000), new Detail("Воздушный фильтр", 1000, 500),
            new Detail("Шарнир колеса", 7000, 3000), new Detail("Прокладка ГБЦ", 1000, 3000), new Detail("Подушка безопасности", 4000, 2000), new Detail("Рычаг подвески", 3000, 1000),
            new Detail("Сайлентблоки", 5000, 2000), new Detail("Пружины", 7000, 3000), new Detail("Стойка стабилизатора", 1000, 1000), new Detail("Тормозные колодки", 3000, 1000),
            new Detail("Лобовое стекло", 15000, 2000), new Detail("Подшипник ступицы", 7000, 2000),};
            CreateStock(random);
            CreateCars(30, random);
        }

        public void Work()
        {
            while (_cars.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"Balance: {_balanceMoney}.В очереди {_cars.Count} авто.");
                Console.WriteLine();
                Console.WriteLine("Stock.");
                ShowStock();
                Console.WriteLine();
                Car car = _cars.Dequeue();
                Console.WriteLine($"Авто на обслуживании. После диагностики детали к замене:");
                car.ShowBrokenDetails();
                Console.WriteLine($"Total price: {GetTotalPriceRepairCar(car.BrokenDetails)}");
                ServiceAndShowResult(car.BrokenDetails);
                Console.WriteLine();
                Console.WriteLine("Нажмите для обслуживания следующего авто.");
                Console.ReadKey();
            }
        }

        public void ShowStock()
        {
            foreach (var detail in _countParts)
            {
                Console.WriteLine($"Detail: {detail.Key}  value: {detail.Value}");
            }
        }

        private void CreateCars(int count, Random random)
        {
            int maxCountBrokenDetails = 5;
            int minCountBrokenDetails = 1;
            List<Detail> brokenDetails;

            for (int i = 0; i < count; i++)
            {
                brokenDetails = new List<Detail>();

                for (int j = 0; j < random.Next(minCountBrokenDetails, maxCountBrokenDetails); j++)
                {
                    brokenDetails.Add(_details[random.Next(_details.Count)]);
                }

               _cars.Enqueue(new Car(brokenDetails));
            }
        }

        private void CreateStock(Random random)
        {
            int maxCountDetail = 10;

            for (int i = 0; i < _details.Count; i++)
            {
                _countParts.Add(_details[i].Name, random.Next(maxCountDetail));
            }
        }

        private int GetTotalPriceRepairCar(List<Detail> brokenDetails)
        {
            int result = 0;

            for (int i = 0; i < brokenDetails.Count; i++)
                result = brokenDetails[i].Price + brokenDetails[i].RepairPrice;

            return result;
        }

        private void ServiceAndShowResult(List<Detail> brokenDetails)
        {
            if (IsEnoughDetailsInStock(brokenDetails))
            {
                Console.WriteLine("Successfully");
                ChangePartAvailability(brokenDetails);
                _balanceMoney += GetTotalPriceRepairCar(brokenDetails);
            }
            else
            {
                Console.WriteLine("Not successful, no details");
                _balanceMoney -= _fine;
            }
        }

        private bool IsEnoughDetailsInStock(List<Detail> brokenDetails)
        {
            int counter = 0;
            bool result = false;

            for (int i = 0; i < brokenDetails.Count; i++)
            {
                if (_countParts[brokenDetails[i].Name] > 0)
                    counter++;
            }

            if (counter == brokenDetails.Count)
                result = true;

            return result;
        }

        private void ChangePartAvailability(List<Detail> brokenDetails)
        {
            for (int i = 0; i < brokenDetails.Count; i++)
            {
                _countParts[brokenDetails[i].Name]--;
            }
        }
    }

    class Car
    {
        private List<Detail> _brokenDetails;

        public Car (List<Detail> brokenDetails)
        {
            _brokenDetails = brokenDetails;
        }

        public List<Detail> BrokenDetails => _brokenDetails;

        public void ShowBrokenDetails()
        {
            int indexAddition = 1;

            for (int i = 0; i < _brokenDetails.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                Console.WriteLine(_brokenDetails[i].GetInfo());
            }
        }
    }

    public struct Detail
    {
        public Detail(string name, int price, int repairPrice)
        {
            Name = name;
            Price = price;
            RepairPrice = repairPrice;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
        public int RepairPrice { get; private set; }

        public string GetInfo()
        {
            return $"{Name}, priceDetail: {Price}, priceReplace: {RepairPrice}";
        }
    }
}