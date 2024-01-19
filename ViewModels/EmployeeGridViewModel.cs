using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Test.Models;
using Test.Repositories;
using Test.ViewModels.Common;

namespace Test.ViewModels
{
    internal class EmployeeGridViewModel : BaseViewModel
    {
        EmployeeRepository employeeRepository = new();

        public EmployeeGridViewModel() { }

        public ObservableCollection<EmployeeViewModel> EmployeeVMs { get; set; } = new();

        public ICommand AddEmpoyeeCommand
        {
            get { return new UICommand(executeAddEmployee); }
        }

        internal void Init()
        {
            var employees = employeeRepository.GetAll();

            foreach (var employee in employees)
            {
                var employeeViewModel = new EmployeeViewModel(employee);
                EmployeeVMs.Add(employeeViewModel);
            }
        }

        internal void Clear()
        {
            foreach(var employeeVM in EmployeeVMs)
            {
                employeeVM.Dispose();
            }
            EmployeeVMs.Clear();
        }

        private void executeAddEmployee(object parameter)
        {
            int id = employeeRepository.Create();
            Employee employee = employeeRepository.Get(id);
            EmployeeViewModel employeeViewModel = new EmployeeViewModel(employee);
            EmployeeVMs.Insert(0, employeeViewModel);
        }
    }
}
