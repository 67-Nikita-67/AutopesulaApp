using AutopesulaApp.Models;
using AutopesulaApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace AutopesulaApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<CarWashOrder> orders =
            new ObservableCollection<CarWashOrder>();

        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();

            Title = WpfApp1.Properties.Resources.AppTitle;

            OrdersDataGrid.ItemsSource = orders;

            VehicleTypeComboBox.ItemsSource =
                Enum.GetValues(typeof(VehicleType));

            WashProgramComboBox.ItemsSource =
                Enum.GetValues(typeof(WashProgram));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInputValid())
            {
                return;
            }

            VehicleType vehicleType =
                (VehicleType)VehicleTypeComboBox.SelectedItem;

            WashProgram washProgram =
                (WashProgram)WashProgramComboBox.SelectedItem;

            decimal price =
                PriceCalculator.CalculatePrice(
                    vehicleType,
                    washProgram);

            int duration =
                PriceCalculator.CalculateDuration(
                    washProgram);

            CarWashOrder newOrder = new CarWashOrder
            {
                Id = nextId,
                VehicleType = vehicleType,
                WashProgram = washProgram,
                Price = price,
                Duration = duration
            };

            orders.Add(newOrder);

            nextId++;

            ClearForm();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem
                is not CarWashOrder selectedOrder)
            {
                MessageBox.Show(
                    WpfApp1.Properties.Resources.SelectEditError,
                    WpfApp1.Properties.Resources.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!IsInputValid())
            {
                return;
            }

            VehicleType vehicleType =
                (VehicleType)VehicleTypeComboBox.SelectedItem;

            WashProgram washProgram =
                (WashProgram)WashProgramComboBox.SelectedItem;

            selectedOrder.VehicleType = vehicleType;
            selectedOrder.WashProgram = washProgram;

            selectedOrder.Price =
                PriceCalculator.CalculatePrice(
                    vehicleType,
                    washProgram);

            selectedOrder.Duration =
                PriceCalculator.CalculateDuration(
                    washProgram);

            OrdersDataGrid.Items.Refresh();

            ClearForm();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem
                is not CarWashOrder selectedOrder)
            {
                MessageBox.Show(
                    WpfApp1.Properties.Resources.SelectDeleteError,
                    WpfApp1.Properties.Resources.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                WpfApp1.Properties.Resources.DeleteConfirmation,
                WpfApp1.Properties.Resources.ConfirmationTitle,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                orders.Remove(selectedOrder);

                ClearForm();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void OrdersDataGrid_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (OrdersDataGrid.SelectedItem
                is CarWashOrder selectedOrder)
            {
                VehicleTypeComboBox.SelectedItem =
                    selectedOrder.VehicleType;

                WashProgramComboBox.SelectedItem =
                    selectedOrder.WashProgram;
            }
        }

        private bool IsInputValid()
        {
            if (VehicleTypeComboBox.SelectedItem == null ||
                WashProgramComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    WpfApp1.Properties.Resources.SelectDataError,
                    WpfApp1.Properties.Resources.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            VehicleTypeComboBox.SelectedIndex = -1;
            WashProgramComboBox.SelectedIndex = -1;
            OrdersDataGrid.SelectedItem = null;
        }
    }
}