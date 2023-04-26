using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

namespace AutoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            service.Work();
        }
    }

    class Service
    {
        private int _balanceMoney = 100000;
        private int _fine = 5000;
        private Stock _stock = new Stock();
        private Queue<Car> _cars = new Queue<Car>();

        public Service()
        {
            Random random = new Random();
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
                _stock.Show();
                Console.WriteLine();
                Car car = _cars.Dequeue();
                Console.WriteLine($"Авто на обслуживании. После диагностики детали к замене:");
                car.ShowBrokenDetails();
                Console.WriteLine($"Total price: {car.GetTotalPriceRepair()}");
                Serve(car);
                Console.WriteLine();
                Console.WriteLine("Нажмите для обслуживания следующего авто.");
                Console.ReadKey();
            }
        }

        private void CreateCars(int count, Random random)
        {
            int maxCountBrokenDetails = 5;
            int minCountBrokenDetails = 1;
            int randomCountDetails = random.Next(minCountBrokenDetails, maxCountBrokenDetails);
            int randomIndex;
            List<Detail> brokenDetails;
            Detail detail;

            for (int i = 0; i < count; i++)
            {
                brokenDetails = new List<Detail>();

                for (int j = 0; j < randomCountDetails; j++)
                {
                    randomIndex = random.Next(_stock.CountNamesDetails);
                    detail = new Detail(_stock.GetNameDetail(randomIndex), _stock.GetPriceDetail(randomIndex), _stock.GetPriceRepairDetail(randomIndex));
                    brokenDetails.Add(detail);
                }

               _cars.Enqueue(new Car(brokenDetails));
            }
        }

        private void Serve(Car car)
        {
            if (IsEnoughDetailsInStock(car))
            {
                Console.WriteLine("Repair completed successfully");
                RemovePartsFromStock(car);
                _balanceMoney += car.GetTotalPriceRepair();
            }
            else
            {
                Console.WriteLine("Repair completed unsuccessfully, not enough details. You have been fined.");
                _balanceMoney -= _fine;
            }
        }

        private bool IsEnoughDetailsInStock(Car car)
        {
            string name;
            int counter = 0;
            bool result = false;

            for (int i = 0; i < car.CountBrokenDetails; i++)
            {
                name = car.GetNameDetail(i);

                if (_stock.IsAvailableDetail(name))
                    counter++;
            }

            if (counter == car.CountBrokenDetails)
                result = true;

            return result;
        }

        private void RemovePartsFromStock(Car car)
        {
            string name;

            for (int i = 0; i < car.CountBrokenDetails; i++)
            {
                name = car.GetNameDetail(i);
                _stock.RemoveDetailFromCell(name);
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

        public int CountBrokenDetails => _brokenDetails.Count;

        public string GetNameDetail(int index)
        {
            return _brokenDetails[index].Name;
        }

        public void ShowBrokenDetails()
        {
            int indexAddition = 1;

            for (int i = 0; i < _brokenDetails.Count; i++)
            {
                Console.Write($"{i + indexAddition}. ");
                Console.WriteLine(_brokenDetails[i].GetInfo());
            }
        }

        public int GetTotalPriceRepair()
        {
            int result = 0;

            for (int i = 0; i < _brokenDetails.Count; i++)
                result += _brokenDetails[i].Price + _brokenDetails[i].RepairPrice;

            return result;
        }
    }

    class Stock
    {
        private List<Detail> _details;
        private List<Cell> _cells = new List<Cell>();

        public Stock()
        {
            Random random = new Random();
            _details = new List<Detail>() {new Detail("Колесный диск", 5000, 200), new Detail("Шина", 4000, 200), new Detail("Радиатор", 8000, 2000),
            new Detail("Катализатор", 10000, 5000), new Detail("Масло", 5000, 1000), new Detail("Воздушный фильтр", 1000, 500),
            new Detail("Шарнир колеса", 7000, 3000), new Detail("Прокладка ГБЦ", 1000, 3000), new Detail("Подушка безопасности", 4000, 2000), new Detail("Рычаг подвески", 3000, 1000),
            new Detail("Сайлентблоки", 5000, 2000), new Detail("Пружины", 7000, 3000), new Detail("Стойка стабилизатора", 1000, 1000), new Detail("Тормозные колодки", 3000, 1000),
            new Detail("Лобовое стекло", 15000, 2000), new Detail("Подшипник ступицы", 7000, 2000),};
            Create(random);
        }

        public int CountNamesDetails => _details.Count;

        private void Create(Random random)
        {
            int maxCountDetail = 10;

            for (int i = 0; i < _details.Count; i++)
            {
                _cells.Add(new Cell(_details[i], random.Next(maxCountDetail)));
            }
        }

        public string GetNameDetail(int index)
        {
            return _details[index].Name;
        }

        public int GetPriceDetail(int index)
        {
            return _details[index].Price;
        }

        public int GetPriceRepairDetail(int index)
        {
            return _details[index].RepairPrice;
        }

        public bool IsAvailableDetail(string name)
        {
            foreach (var cell in _cells)
            {
                if (cell.Name == name && cell.Count > 0)
                    return true;
            }

            return false;
        }

        public void RemoveDetailFromCell(string name)
        {
            foreach (var cell in _cells)
            {
                if (cell.Name == name)
                    cell.Remove();
            }
        }

        public void Show()
        {
            foreach (var cell in _cells)
            {
                Console.WriteLine($"Detail: {cell.Name}  value: {cell.Count}");
            }
        }
    }

    class Cell
    {
        private Detail _detail;

        public Cell(Detail detail, int count)
        {
            _detail = detail;
            Count = count;
        }

        public string Name => _detail.Name;
        public int Count { get; private set; }

        public void Remove()
        {
            Count--;
        }
    }

    class Detail
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