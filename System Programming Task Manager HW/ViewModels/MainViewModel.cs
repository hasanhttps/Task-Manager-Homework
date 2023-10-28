using System;
using System.Data;
using System.Timers;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using System.Windows.Input;
using System_Programming_Task_Manager_HW.Commands;
using System.Windows;

namespace System_Programming_Task_Manager_HW.ViewModels;

public class MainViewModel {

    // Private Fields

    private TextBox CommandTB;
    private DataGrid ProcessesDG;
    private DataRowView? selectedRow;
    private DataTable DataTable = new DataTable();

    // Binding Properties

    public DataRowView? SelectedRow { get => selectedRow;
        set {
            selectedRow = value;
        }
    }
    public ICommand? CreateProcessBtCommand { get; set; }
    public ICommand? EndProcessBtCommand { get; set; }

    // Constructor

    public MainViewModel(DataGrid processesDG, TextBox textBox) {

        ProcessesDG = processesDG;
        CommandTB = textBox;

        CreateProcessBtCommand = new RelayCommand(CreateProcess);
        EndProcessBtCommand = new RelayCommand(EndProcess);

        GetAllProcesses();
    }


    // Functions

    public void GetAllProcesses() {

        var list = Process.GetProcesses();

        DataTable.Columns.Add("Id", typeof(int));
        DataTable.Columns.Add("Name", typeof(string));
        DataTable.Columns.Add("Handle Count", typeof(int));
        DataTable.Columns.Add("Thread Count", typeof(int));
        DataTable.Columns.Add("Machine Name", typeof(string));

        foreach (var item in list) {
            DataTable.Rows.Add(item.Id, item.ProcessName, item.HandleCount, item.Threads.Count, item.MachineName);
        }
        ProcessesDG.ItemsSource = DataTable.DefaultView;
    }

    private void CreateProcess(object? param) {
        Process newProcess = Process.Start(CommandTB.Text);
        DataTable.Rows.Add(newProcess.Id, newProcess.ProcessName, newProcess.HandleCount, newProcess.Threads.Count, newProcess.MachineName);
    }

    private void EndProcess(object? param) {
        try {

            Process process = Process.GetProcessById(Convert.ToInt32(SelectedRow["Id"]));
            process.Kill();
            selectedRow.Delete();

        }
        catch(Exception ex) {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}