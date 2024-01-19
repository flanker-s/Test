using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Models;
using Test.Repositories;
using Test.ViewModels.Common;

namespace Test.ViewModels
{
    internal delegate void DepartmentUpdated(Department department);

    internal class DepartmentViewModel : BaseViewModel
    {
        public event DepartmentUpdated? OnDepartmentUpdated;
        void RaiseDepartmentUpdated(Department department)
        {
            if (OnDepartmentUpdated != null) OnDepartmentUpdated(department);
        }

        DepartmentRepository departmentRepository { get; set; } = new();

        Department department; //Storing model data. Being used to update DB instance
        public Department Department => department;

        public DepartmentViewModel(Department department)
        {
            this.department = department;
        }
        public int Id => department.Id;
        public string Name
        {
            get => department.Name ?? "";
            set
            {
                if (department.Name != value)
                {
                    department.Name = value;
                    OnPropertyChanged(nameof(Name));
                    updateDepartment();
                }
            }
        }
        public string Address
        {
            get => department.Address ?? "";
            set
            {
                if (department.Address != value)
                {
                    department.Address = value;
                    OnPropertyChanged(nameof(Address));
                    updateDepartment();
                }
            }
        }
        public string Phone
        {
            get => department.Phone ?? "";
            set
            {
                if (department.Phone != value)
                {
                    department.Phone = value;
                    OnPropertyChanged(nameof(Phone));
                    updateDepartment();
                }
            }
        }

        void updateDepartment()
        {
            departmentRepository.Update(department);
            RaiseDepartmentUpdated(department);
        }
    }
}
