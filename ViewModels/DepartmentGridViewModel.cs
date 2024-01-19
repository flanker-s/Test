using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Models;
using Test.Repositories;
using Test.ViewModels.Common;

namespace Test.ViewModels
{
    internal class DepartmentGridViewModel : BaseViewModel
    {
        DepartmentRepository departmentRepository = new();

        public DepartmentGridViewModel() { }

        public ObservableCollection<DepartmentViewModel> DepartmentVMs { get; set; } = new();

        public ICommand AddDepartmentCommand => new UICommand(executeAddDepartment);

        internal void Init()
        {
            List<Department> departments = departmentRepository.GetAll();
            foreach (var department in departments)
            {
                var departmentViewModel = new DepartmentViewModel(department);
                DepartmentVMs.Add(departmentViewModel);
            }
        }

        private void executeAddDepartment(object parameter)
        {
            int id = departmentRepository.Create();
            Department department = departmentRepository.Get(id);
            DepartmentViewModel departmentViewModel = new DepartmentViewModel(department);
            DepartmentVMs.Insert(0, departmentViewModel);
        }

        public void Clear()
        {
            DepartmentVMs.Clear();
        }
    }
}
