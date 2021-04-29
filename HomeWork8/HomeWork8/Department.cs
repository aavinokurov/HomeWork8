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
        List<Worker> workers;

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

        #endregion
    }
}
