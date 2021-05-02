using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork8
{
    class XMLFuns
    {
        #region Свойства

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string Path { get; private set; }

        #endregion

        #region Конструкторы

        public XMLFuns(string path)
        {
            this.Path = path;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Сериализовать департамент
        /// </summary>
        /// <param name="department">Департамент</param>
        public XElement DepartmentSerialization(Department department)
        {
            XElement DEPARTMENT = new XElement("DEPARTMENT");

            XAttribute Name = new XAttribute("Name", department.Name);
            XAttribute DateCreation = new XAttribute("DateCreation", department.DateCreation);

            XElement SUBDEPARTMENT = new XElement("SUBDEPARTMENT");

            if (department.SubDepartment != null)
            {
                for (int i = 0; i < department.SubDepartment.Count; i++)
                {
                    XElement tempDepartment = DepartmentSerialization(department.SubDepartment[i]);
                    SUBDEPARTMENT.Add(tempDepartment);
                }
            }

            XElement WORKERS = new XElement("WORKERS");

            if (department.Workers.Count > 0)
            {
                for (int i = 0; i < department.Workers.Count; i++)
                {
                    XElement tempWorker = WorkerSerialization(department.Workers[i]);
                    WORKERS.Add(tempWorker);
                }
            }

            DEPARTMENT.Add(Name);
            DEPARTMENT.Add(DateCreation);
            DEPARTMENT.Add(SUBDEPARTMENT);
            DEPARTMENT.Add(WORKERS);

            return DEPARTMENT;
        }

        /// <summary>
        /// Сериализовать сотрудника
        /// </summary>
        /// <param name="department">Сотрудник</param>
        public XElement WorkerSerialization(Worker worker)
        {
            XElement WORKER = new XElement("WORKER");

            XAttribute Surname = new XAttribute("Surname", worker.Surname);
            XAttribute Name = new XAttribute("Name", worker.Name);
            XAttribute Age = new XAttribute("Age", worker.Age);
            XAttribute Id = new XAttribute("Id", worker.Id);
            XAttribute Salary = new XAttribute("Salary", worker.Salary);

            WORKER.Add(Surname);
            WORKER.Add(Name);
            WORKER.Add(Age);
            WORKER.Add(Id);
            WORKER.Add(Salary);

            return WORKER;
        }

        /// <summary>
        /// Десериализовать департамент
        /// </summary>
        /// <param name="Xdepartment">Департамент</param>
        /// <returns></returns>
        public Department DepartmentDeserialization(XElement Xdepartment)
        {
            Department result = null;
            string name = Xdepartment.Attribute("Name").Value;
            DateTime dateTime = DateTime.Parse(Xdepartment.Attribute("DateCreation").Value);

            List<Department> subDepartments = new List<Department>();

            foreach (var item in Xdepartment.Element("SUBDEPARTMENT").Elements("DEPARTMENT"))
            {
                subDepartments.Add(DepartmentDeserialization(item));
            }

            List<Worker> workers = new List<Worker>();

            foreach (var item in Xdepartment.Element("WORKERS").Elements("WORKER"))
            {
                workers.Add(WorkerDeserialization(item, name));
            }

            result = new Department(name, dateTime, workers, subDepartments);

            return result;
        }

        /// <summary>
        /// Десериализовать сотрудника
        /// </summary>
        /// <param name="XWorker">Сотрудник</param>
        /// <param name="department">Департамент</param>
        /// <returns></returns>
        public Worker WorkerDeserialization(XElement XWorker, string department)
        {
            string surname = XWorker.Attribute("Surname").Value;
            string name = XWorker.Attribute("Name").Value;
            int age = Int32.Parse(XWorker.Attribute("Age").Value);
            int id = Int32.Parse(XWorker.Attribute("Id").Value);
            int salary = Int32.Parse(XWorker.Attribute("Salary").Value);

            return new Worker(surname, name, age, department, id, salary);
        }       

        /// <summary>
        /// Сохранение на диск
        /// </summary>
        public void SaveFile(Department mainDepartment)
        {
            XElement xElement = DepartmentSerialization(mainDepartment);
            xElement.Save(Path);
        }

        /// <summary>
        /// Выгрузка с файла
        /// </summary>
        /// <returns></returns>
        public Department LoadFile()
        {
            string xml = File.ReadAllText(Path);
            XElement xElement = XDocument.Parse(xml).Element("DEPARTMENT");

            return DepartmentDeserialization(xElement);
        }

        #endregion
    }
}
