using System.Collections.Generic;

namespace HotelSys.DataSources
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
    }

    public class EmployeeDataSource
    {
        private List<Employee> employees = new List<Employee>() {
            new Employee() {
                Name = "Antonio Moreno",
                Position = "CEO"
            },
            new Employee() {
                Name = "Thomas Hardy",
                Position = "Sales Representative"
            },
            new Employee() {
                Name = "Christina Berglund",
                Position = "Order Administrator"
            },
            new Employee() {
                Name = "Frédérique Citeaux",
                Position = "Marketing Manager"
            },
            new Employee() {
                Name = "Hanna Moos",
                Position = "Software Developer"
            }
        };
        public List<Employee> Items { get; set; }

        public EmployeeDataSource()
        {
            Items = employees;
        }

        public EmployeeDataSource(string employeePosition)
        {
            Items = employees;
        }
        public IEnumerable<Employee> GetEmployeeList()
        {
            foreach (var employee in employees)
                yield return employee;
        }
    }
}
