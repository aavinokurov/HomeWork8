using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    class Program
    {
        static void Main(string[] args)
        {
            Department department = new Department("Department1", DateTime.Parse("30.04"));

            for (int i = 0; i < 2; i++)
            {
                department.AddWorker();
                Console.Clear();
            }

            department.AddSubDepartment();

            for (int i = 0; i < 2; i++)
            {
                department.SubDepartment[0].AddWorker();
                Console.Clear();
            }

            for (int i = 0; i < 2; i++)
            {
                department.SubDepartment[0].AddSubDepartment();
                Console.Clear();
            }

            department.PrintAllInfoDepartment();
        }
    }
}
