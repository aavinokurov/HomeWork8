using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    public class Department
    {
        #region Поля

        /// <summary>
        /// Сотрудники
        /// </summary>
        private List<Worker> workers;

        #endregion

        #region Свойства

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreation { get; private set; }

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public int CountWorkers { get { return workers.Count; } }

        /// <summary>
        /// Вложенные департаменты
        /// </summary>
        public List<Department> SubDepartment { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создать новый департамент
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="dateCreation">Дата создания</param>
        public Department(string name, DateTime dateCreation)
        {
            this.Name = name;
            this.DateCreation = dateCreation;
            this.workers = new List<Worker>();
        }

        /// <summary>
        /// Создать новый департамент
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="dateCreation">Дата создания</param>
        /// <param name="subDepartments"></param>
        public Department(string name, DateTime dateCreation, List<Department> subDepartments)
            :this(name, dateCreation)
        {
            this.SubDepartment = subDepartments;
        }


        #endregion

        #region Методы

        /// <summary>
        /// Добавить нового работника
        /// </summary>
        public void AddNewWorker()
        {
            if (workers.Count <= 1_000_000)
            {
                string surname;
                string name;
                int age;
                int salary;

                do
                {
                    Console.WriteLine("Введите фамилию: ");
                    surname = Console.ReadLine();
                } while (string.IsNullOrEmpty(surname));

                do
                {
                    Console.WriteLine("Введите имя: ");
                    name = Console.ReadLine();
                } while (string.IsNullOrEmpty(name));

                do
                {
                    do
                    {
                        Console.WriteLine("Введите возраст: ");
                    } while (!Int32.TryParse(Console.ReadLine(), out age));
                } while (age <= 0 || age > 110);

                do
                {
                    do
                    {
                        Console.WriteLine("Введите размер заработной платы: ");
                    } while (!Int32.TryParse(Console.ReadLine(), out salary));
                } while (salary <= 0);

                workers.Add(new Worker(surname, name, age, this, workers.Count + 1, salary));
            }
            else
            {
                Console.WriteLine("В департаменте больше миллиона сотрудников!");
            }
        }

        /// <summary>
        /// Добавить вложенный департамент
        /// </summary>
        public void AddSubDepartment()
        {
            if (SubDepartment == null)
            {
                SubDepartment = new List<Department>();
            }

            string name;
            DateTime date;

            do
            {
                Console.WriteLine("Введите название департамента:");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));

            do
            {
                Console.WriteLine("Введите дату создания департамента:");
            } while (!DateTime.TryParse(Console.ReadLine(), out date));

            SubDepartment.Add(new Department(name, date));
        }

        /// <summary>
        /// Вывод всех сотрудников департамента
        /// </summary>
        public void PrintAllWorkers()
        {
            Console.WriteLine($"{"Фамилия",15}{"Имя",15}{"Возраст",10}{"Департамент",15}{"Идентификатор",15}{"Зарплата",10}");

            foreach (var item in workers)
            {
                Console.WriteLine($"{item.Surname,15}{item.Name,15}{item.Age,10}{item.Department.Name,15}{item.Id,15}{item.Salary,10}");
            }
        }

        /// <summary>
        /// Вывод информации о департаменте
        /// </summary>
        public void PrintInfoDepartment()
        {
            Console.WriteLine($"Имя департамента: {Name}.\n" +
                              $"Дата создания: {DateCreation.ToShortDateString()}.");

            if (SubDepartment != null)
            {
                Console.WriteLine($"Количество вложенных департаментов {SubDepartment.Count}.");
            }

            if (workers.Count > 0)
            {
                this.PrintAllWorkers();
            }
            else
            {
                Console.WriteLine("Сотрудников нет.");
            }
        }

        /// <summary>
        /// Вывод информации о департаменте c вложенным департаментами
        /// </summary>
        public void PrintAllInfoDepartment()
        {
            this.PrintInfoDepartment();
            
            if (SubDepartment != null)
            {
                foreach (var item in SubDepartment)
                {
                    Console.WriteLine($"Вышестоящий департамент: {this.Name}");

                    item.PrintAllInfoDepartment();
                }
            }
        }

        #endregion
    }
}
