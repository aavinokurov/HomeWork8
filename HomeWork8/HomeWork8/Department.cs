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

        /// <summary>
        /// Сотрудники
        /// </summary>
        public List<Worker> Workers { get { return workers; } }

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

        /// <summary>
        /// Создать новый департамент
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dateCreation"></param>
        /// <param name="workers"></param>
        /// <param name="subDepartments"></param>
        public Department(string name, DateTime dateCreation, List<Worker> workers, List<Department> subDepartments)
            : this(name, dateCreation, subDepartments)
        {
            this.workers = workers;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавить нового работника
        /// </summary>
        public void AddWorker()
        {
            SortById();

            int tempId = workers.Count > 0 ? workers[workers.Count - 1].Id + 1 : 1 ;

            AddWorker(workers.Count, tempId);
        }

        /// <summary>
        /// Добавить нового работника
        /// </summary>
        /// <param name="idWorker">Идентификатор сотрудника</param>
        private void AddWorker(int count, int idWorker)
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

                workers.Insert(count, new Worker(surname, name, age, this.Name, idWorker, salary));
            }
            else
            {
                Console.WriteLine("В департаменте больше миллиона сотрудников!");
            }
        }

        /// <summary>
        /// Удалить сотрудника. Возращает True если сотрудник удален
        /// </summary>
        /// <param name="idWorker">Идентификатор сотрудника</param>
        public bool RemoveWorker(int idWorker)
        {
           return RemoveWorker(idWorker, out int x);
        }

        /// <summary>
        /// Удалить сотрудника. Возращает True если сотрудник удален
        /// </summary>
        /// <param name="idWorker">Идентификатор сотрудника</param>
        public bool RemoveWorker(int idWorker, out int index)
        {
            index = -1;

            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].Id == idWorker)
                {
                    workers.Remove(workers[i]);

                    index = i;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Редактировать сотрудника. Возращает True если получилось отредактировать сотрудника
        /// </summary>
        /// <param name="idWorker"></param>
        /// <returns></returns>
        public bool EditWorker(int idWorker)
        {
            int index;

            if (RemoveWorker(idWorker, out index))
            {
                AddWorker(index,idWorker);

                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        public void SortByAge()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = i + 1; j < workers.Count; j++)
                {
                    if (workers[i].Age > workers[j].Age)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Сортировка по Id
        /// </summary>
        public void SortById()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = i + 1; j < workers.Count; j++)
                {
                    if (workers[i].Id > workers[j].Id)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Сортировка по зарплате
        /// </summary>
        public void SortBySalary()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = i + 1; j < workers.Count; j++)
                {
                    if (workers[i].Salary > workers[j].Salary)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Сортировка по Имени
        /// </summary>
        public void SortByName()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = i + 1; j < workers.Count; j++)
                {
                    if (workers[i].Name.CompareTo(workers[j].Name) > 0)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Сортировка по фамилии
        /// </summary>
        public void SortBySurname()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = i + 1; j < workers.Count; j++)
                {
                    if (workers[i].Surname.CompareTo(workers[j].Surname) > 0)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Добавить вложенный департамент
        /// </summary>
        /// <returns></returns>
        public bool AddSubDepartment()
        {
            if (SubDepartment == null)
            {
                SubDepartment = new List<Department>();
            }

            return AddSubDepartment(SubDepartment.Count, new List<Worker>(), null);
        }

        /// <summary>
        /// Добавить вложенный департамент
        /// </summary>
        /// <param name="index">Индекс записи</param>
        /// <returns></returns>
        private bool AddSubDepartment(int index, List<Worker> workers, List<Department> departments)
        {
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

            bool canAdd = true;

            for (int i = 0; i < SubDepartment.Count; i++)
            {
                if (SubDepartment[i].Name == name)
                {
                    canAdd = false;
                    break;
                }
            }

            if (canAdd)
            {
                SubDepartment.Insert(index, new Department(name, date, workers, departments));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Удалит вложенный департамент по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns></returns>
        public bool RemoveDepartment(int index)
        {
            if (index < SubDepartment.Count)
            {
                SubDepartment.RemoveAt(index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Редактировать вложенный департамент
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool EditSubDepartment(int index, List<Worker> workers, List<Department> departments)
        {
            if (RemoveDepartment(index))
            {
                AddSubDepartment(index, workers, departments);
            }

            return false;
        }

        /// <summary>
        /// Вывод всех сотрудников департамента
        /// </summary>
        public void PrintAllWorkers()
        {
            Console.WriteLine($"{"Фамилия",15}{"Имя",15}{"Возраст",10}{"Департамент",15}{"Идентификатор",15}{"Зарплата",10}");

            foreach (var item in workers)
            {
                Console.WriteLine($"{item.Surname,15}{item.Name,15}{item.Age,10}{item.NameDepartment,15}{item.Id,15}{item.Salary,10}");
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

                Console.WriteLine("Вложенные департаменты: ");

                for (int i = 1; i <= SubDepartment.Count; i++)
                {
                    Console.WriteLine($"{i} - {SubDepartment[i - 1].Name}");
                }
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
