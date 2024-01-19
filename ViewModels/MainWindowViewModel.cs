using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ViewModels.Common;

namespace Test.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            DepartmentGridVM = new DepartmentGridViewModel();
            EmployeeGridVM = new EmployeeGridViewModel();
        }

        public DepartmentGridViewModel DepartmentGridVM { get; set; } = new();
        public EmployeeGridViewModel EmployeeGridVM { get; set; } = new();
    }
}
