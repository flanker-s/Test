using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic;
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
    internal class EmployeeViewModel : BaseViewModel, IDisposable
    {
        DepartmentRepository departmentRepository = new();
        EmployeeRepository employeeRepository = new();
        Employee employee; //Storing model data. Being used to update DB instance

        public EmployeeViewModel(Employee employee)
        {
            this.employee = employee;

            departmentVM = new DepartmentViewModel(employee.Department);
            departmentVM.OnDepartmentUpdated += UpdateDepartmentInfo;
            selectedDepartmentVM = new DepartmentViewModel(employee.Department);
            DepartmentVMs.Add(SelectedDepartmentVM);
        }

        private void UpdateDepartmentInfo(Department department)
        {
            employee.Department = department;
            OnPropertyChanged(nameof(DepartmentName));
            DepartmentVMs.Add(DepartmentVM);
            setSelectedDepartmentVM(DepartmentVM);
        }

        public int Id => employee.Id;
        public string FirstName 
        {
            get => employee.FirstName ?? "";
            set
            {
                employee.FirstName = value;
                employeeRepository.Update(employee);
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        { 
            get => employee.LastName ?? ""; 
            set
            {
                employee.LastName = value;
                employeeRepository.Update(employee);
                OnPropertyChanged(nameof(LastName));
            }
        }
        DepartmentViewModel departmentVM;
        public DepartmentViewModel DepartmentVM //Also affects the employee model
        {
            get => departmentVM;
            set
            {
                if(value != null)
                {
                    departmentVM.OnDepartmentUpdated -= UpdateDepartmentInfo;
                    departmentVM = value;
                    departmentVM.OnDepartmentUpdated += UpdateDepartmentInfo;
                    employee.Department = departmentVM.Department;
                    OnPropertyChanged(nameof(DepartmentVM));
                }
            }
        }
        public string DepartmentName { get => departmentVM.Name; }

        public ObservableCollection<DepartmentViewModel> DepartmentVMs { get; set; } = new();

        DepartmentViewModel selectedDepartmentVM;
        public DepartmentViewModel SelectedDepartmentVM 
        { 
            get => selectedDepartmentVM; 
            set
            {
                if(value != null)
                {
                    selectedDepartmentVM = value;
                    DepartmentVM = selectedDepartmentVM; //Also affects the employee model
                    employeeRepository.Update(employee);

                    OnPropertyChanged(nameof(DepartmentName));
                    OnPropertyChanged(nameof(SelectedDepartmentVM));
                }
            } 
        }
        void setSelectedDepartmentVM(DepartmentViewModel departmentViewModel)
        { 
            //Because the propetry is used to update DB instance
            selectedDepartmentVM = departmentViewModel;
            OnPropertyChanged(nameof(SelectedDepartmentVM));
        }

        public ICommand GetDepartmentsCommand
        {
            get { return new UICommand(executeGetDepartments); }
        }

        void executeGetDepartments(object parameter)
        {
            DepartmentVMs.Clear();
            List<Department> departments = departmentRepository.GetAll();

            foreach (var department in departments)
            {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel(department);
                DepartmentVMs.Add(departmentViewModel);

                if (department.Id == DepartmentVM.Id)
                {
                    setSelectedDepartmentVM(departmentViewModel);
                }
            }
        }

        public void Dispose() //Just in case of further disign
        {
            departmentVM.OnDepartmentUpdated -= UpdateDepartmentInfo;
        }
    }
}
