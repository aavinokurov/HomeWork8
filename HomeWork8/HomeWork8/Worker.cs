using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    public struct Worker
    {
        #region Свойства

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Департамент
        /// </summary>
        public string NameDepartment { get; private set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Размер заработной платы
        /// </summary>
        public float Salary { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создает нового работник
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Департамент</param>
        /// <param name="id">Идентификатор</param>
        /// <param name="salary">Размер заработной платы</param>
        public Worker(string surname, string name, int age, string nameDepartment, int id, int salary)
        {
            this.Surname = surname;
            this.Name = name;
            this.Age = age;
            this.NameDepartment = nameDepartment;
            this.Id = id;
            this.Salary = salary;
        }

        #endregion
    }
}
