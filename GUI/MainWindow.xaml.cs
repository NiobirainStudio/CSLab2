using DB;
using DB.Models;
using DB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private ServiceManager _serviceManager;

        private Dictionary<string, TextBox> _variableList;
        private Dictionary<Type, ComboBox> _externalKeyList;


        // UTILITY
        private ModelBlock GetSelectedModelBlock()
        {
            return _serviceManager.modelSpace.ElementAt(TableSelect.SelectedIndex).Value;
        }

        private void RefreshTables()
        {
            DeselectAll();

            foreach (var item in _externalKeyList)
            {
                // Refresh external fields
                var src = _serviceManager.modelSpace[item.Key].dataService.GetAllVisible();

                if (src.Count() != 0)
                    item.Value.ItemsSource = src;
            }

            var srcData = GetSelectedModelBlock().dataService.GetAll();
            DataOut.ItemsSource = srcData;
        }

        private void DeselectAll()
        {
            DataOut.UnselectAll();
            foreach (var item in _variableList)
            {
                item.Value.Text = "";
            }

            foreach (var item in _externalKeyList)
            {
                item.Value.SelectedItem = null;
            }
        }

        private int IdOfSelected()
        {
            var selectedItem = DataOut.SelectedItem;

            return Convert.ToInt32(selectedItem.GetType().GetProperties().FirstOrDefault().GetValue(selectedItem));
        }

        private List<object> GatherInput()
        {
            List<object> inputData = new List<object>();

            foreach (var item in _variableList)
            {
                if (item.Value.Text != "")
                {
                    inputData.Add(item.Value.Text);
                }
                else
                {
                    MessageBox.Show($"{item.Key} cannot be empty!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<object>();
                }
            }

            foreach (var item in _externalKeyList)
            {
                if (item.Value.SelectedItem != null)
                {
                    inputData.Add(_serviceManager.modelSpace[item.Key].dataService.GetIdByVisible((string)item.Value.SelectedItem));
                }
                else
                {
                    MessageBox.Show($"Please select the {item.Key.Name}!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<object>();
                }
            }
            return inputData;
        }







        public MainWindow()
        {
            _serviceManager = new ServiceManager();
            _variableList = new Dictionary<string, TextBox>();
            _externalKeyList = new Dictionary<Type, ComboBox>();

            AppDbContext context = new AppDbContext();

            _serviceManager.AddDataService(new ArtistDataService(context));
            _serviceManager.AddDataService(new GenreDataService(context));
            _serviceManager.AddDataService(new AlbumDataService(context));

            _serviceManager.InitSpace();

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Fill the selection box with otions
            if (_serviceManager.modelSpace.Count == 0)
                throw new Exception("At least one service should be connected!");

            TableSelect.ItemsSource = _serviceManager.modelSpace.Keys.Select(t => t.Name);
            TableSelect.SelectedIndex = 0;
        }

        private void TableSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataOut.UnselectAll();
            _variableList.Clear();
            _externalKeyList.Clear();
            
            // Selected data
            var srcData = GetSelectedModelBlock().dataService.GetAll();
            DataOut.ItemsSource = srcData;

            List<Grid> data = new List<Grid>();


            foreach(var item in GetSelectedModelBlock().variables.Skip(1))
            {
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;

                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(100, GridUnitType.Star);
                ColumnDefinition c2 = new ColumnDefinition();
                c2.Width = new GridLength(200, GridUnitType.Auto);

                grid.ColumnDefinitions.Add(c1);
                grid.ColumnDefinitions.Add(c2);


                Label label = new Label();
                label.Content = item.Key;
                grid.Children.Add(label);
                Grid.SetColumn(label, 0);

                TextBox textBox = new TextBox();
                textBox.Width = 100;
                textBox.HorizontalAlignment = HorizontalAlignment.Right;
                grid.Children.Add(textBox);
                _variableList.Add(item.Key, textBox);
                Grid.SetColumn(textBox, 1);

                data.Add(grid);
            }

            foreach (var item in GetSelectedModelBlock().externalKeys)
            {
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;

                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(100, GridUnitType.Star);
                ColumnDefinition c2 = new ColumnDefinition();
                c2.Width = new GridLength(200, GridUnitType.Auto);

                grid.ColumnDefinitions.Add(c1);
                grid.ColumnDefinitions.Add(c2);


                Label label = new Label();
                label.Content = item.Key;
                grid.Children.Add(label);
                Grid.SetColumn(label, 0);

                ComboBox comboBox = new ComboBox();

                var src = _serviceManager.modelSpace
                    .FirstOrDefault(t => t.Key == item.Value).Value.dataService
                    .GetAllVisible();
                
                if (src.Count() != 0)
                    comboBox.ItemsSource = src;

                comboBox.Width = 100;
                comboBox.HorizontalAlignment = HorizontalAlignment.Right;
                grid.Children.Add(comboBox);
                _externalKeyList.Add(item.Value, comboBox);
                Grid.SetColumn(comboBox, 1);

                data.Add(grid);
            }

            InputList.ItemsSource = data;
        }

        private void DataOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selected data
            if (DataOut.SelectedItem != null)
            {
                var requestedData = _serviceManager.modelSpace
                    .ElementAt(TableSelect.SelectedIndex).Value.dataService
                    .Read(IdOfSelected());

                // Insert data of the selected item into the input fields
                foreach (var item in _variableList)
                {
                    foreach (var prop in requestedData.GetType().GetProperties())
                    {
                        if (prop.Name == item.Key)
                        {
                            item.Value.Text = Convert.ToString(prop.GetValue(requestedData));
                        }
                    }
                }

                foreach (var item in _externalKeyList)
                {
                    foreach (var prop in requestedData.GetType().GetProperties())
                    {
                        if (prop.PropertyType == item.Key)
                        {
                            item.Value.SelectedItem = ((IModel)prop.GetValue(requestedData)).GetVisible();
                        }
                    }
                }
            }
        }







        // CRUD FUNCTIONALITY
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> res = GatherInput();

            if (res.Count != 0)
            {
                try
                {
                    GetSelectedModelBlock().dataService.Create(res.ToArray());

                    RefreshTables();
                } catch (FormatException fex)
                {
                    MessageBox.Show("Check the input data formats!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                } catch (DbUpdateException uex)
                {
                    MessageBox.Show("Item already exists!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                } catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> res = GatherInput();

            if (DataOut.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row that needs to be updated!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            res.Insert(0, IdOfSelected());

            GetSelectedModelBlock().dataService.Update(res.ToArray());

            RefreshTables();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataOut.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row that needs to be deleted!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GetSelectedModelBlock().dataService.Delete(IdOfSelected());

            RefreshTables();
        }

        private void DeselectButton_Click(object sender, RoutedEventArgs e)
        {
            DeselectAll();
        }
    }
}
