using System;
using System.Windows;
using System_Programming_Task_Manager_HW.ViewModels;

namespace System_Programming_Task_Manager_HW.Views {
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();
            DataContext = new MainViewModel(ProcessDataGrid, CommandTB);
        }
    }
}
