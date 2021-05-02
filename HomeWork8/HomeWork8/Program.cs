using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    class Program
    {
        static Stack<Department> stackDepartments = new Stack<Department>();
        static Department currentDepartment = null;

        static int answer;
        static int answerDepartment;

        static void Main(string[] args)
        {
            bool isRun = true;

            List<Department> departments = new List<Department>();

            departments.Add(new Department("Department1",new DateTime(2021,1,1)));
            departments.Add(new Department("Department2",new DateTime(2021,1,1)));
            departments.Add(new Department("Department3",new DateTime(2021,1,1)));

            Department mainDepartment = new Department("MainDepartment", DateTime.Now,departments);

            currentDepartment = mainDepartment;

            while (isRun)
            {
                Console.Clear();

                if (currentDepartment == mainDepartment)
                {
                    for (int i = 0; i < currentDepartment.SubDepartment.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {currentDepartment.SubDepartment[i].Name}");
                    }

                    do
                    {
                        do
                        {
                            Console.WriteLine("Выберите команду:\n" +
                                              "1 - Далее\n" +
                                              "2 - Добавить департамент");
                        } while (!Int32.TryParse(Console.ReadLine(), out answer));
                    } while (answer < 1 || answer > 2);

                    switch (answer)
                    {
                        case 1:
                            if (currentDepartment.SubDepartment.Count > 0)
                            {
                                Next();
                            }
                            else
                            {
                                Console.WriteLine("Нет доступных департаментов!");
                            }
                            break;
                        case 2:
                            currentDepartment.AddSubDepartment();
                            break;
                    }
                }
                else
                {
                    currentDepartment.PrintInfoDepartment();

                    do
                    {
                        do
                        {
                            Console.WriteLine("Выберите команду:\n" +
                                              "1 - Назад\n" +
                                              "2 - Далее\n" +
                                              "3 - Добавить вложенный департамент\n" +
                                              "4 - Редактировать текущий департамент\n" +
                                              "5 - Удалит текущий департамент\n" +
                                              "6 - Добавить сотрудника\n" +
                                              "7 - Редактировать сотрудника\n" +
                                              "8 - Удалить сотрудника\n" +
                                              "9 - Вывести всю информацию о департаменте включаю вложенные.");
                        } while (!Int32.TryParse(Console.ReadLine(), out answer));
                    } while (answer < 1 || answer > 9);

                    int idWorker;

                    switch (answer)
                    {
                        case 1:
                            currentDepartment = stackDepartments.Pop();
                            break;
                        case 2:
                            Next();
                            break;
                        case 3:
                            currentDepartment.AddSubDepartment();
                            break;
                        case 4:
                            List<Worker> tempWorkers = currentDepartment.Workers;
                            List<Department> tempDepartments = currentDepartment.SubDepartment;

                            currentDepartment = stackDepartments.Pop();
                            currentDepartment.EditSubDepartment(answerDepartment - 1, tempWorkers, tempDepartments);
                            stackDepartments.Push(currentDepartment);
                            currentDepartment = currentDepartment.SubDepartment[answerDepartment - 1];
                            break;
                        case 5:
                            currentDepartment = stackDepartments.Pop();
                            currentDepartment.RemoveDepartment(answerDepartment - 1);
                            break;
                        case 6:
                            currentDepartment.AddWorker();
                            break;
                        case 7:
                            if (currentDepartment.Workers.Count > 0)
                            {
                                do
                                {
                                    do
                                    {
                                        Console.WriteLine("Введите номер сотрудника:");
                                    } while (!Int32.TryParse(Console.ReadLine(), out idWorker));
                                } while (idWorker <= 0);

                                Console.WriteLine(currentDepartment.EditWorker(idWorker) ? "Успех!" : "Такого сотрудника нет!");
                            }
                            else
                            {
                                Console.WriteLine("Нет доступных сотрудников!");
                            }
                            break;
                        case 8:
                            if (currentDepartment.Workers.Count > 0)
                            {
                                do
                                {
                                    do
                                    {
                                        Console.WriteLine("Введите номер сотрудника:");
                                    } while (!Int32.TryParse(Console.ReadLine(), out idWorker));
                                } while (idWorker <= 0);

                                Console.WriteLine(currentDepartment.RemoveWorker(idWorker) ? "Успех!" : "Такого сотрудника нет!");
                            }
                            else
                            {
                                Console.WriteLine("Нет доступных сотрудников!");
                            }
                            break;
                        case 9:
                            currentDepartment.PrintAllInfoDepartment();
                            break;
                    }
                }

                Console.WriteLine("Нажмите клавишу, чтобы продолжить:");
                Console.ReadKey();

            }
        }

        /// <summary>
        /// Перейти на уровень ниже
        /// </summary>
        /// <param name="department">Текущий департамент</param>
        public static void Next()
        {
            if (currentDepartment.SubDepartment != null)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("Выберите номер департамент: ");
                    } while (!Int32.TryParse(Console.ReadLine(), out answerDepartment));
                } while (answerDepartment < 1 || answerDepartment > currentDepartment.SubDepartment.Count);

                stackDepartments.Push(currentDepartment);

                currentDepartment = currentDepartment.SubDepartment[answerDepartment - 1];
            }
            else
            {
                Console.WriteLine("В департаменте нет вложенных департаментов!");
            }
        }
    }
}
