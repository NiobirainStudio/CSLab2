using DB;
using DB.Models;
using DB.Services;
using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            _serviceManager = new ServiceManager();
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
            // Selected data
            DataOut.ItemsSource = _serviceManager.modelSpace.ElementAt(TableSelect.SelectedIndex).Value.dataService.GetAll();

            List<Grid> data = new List<Grid>();


            foreach(var item in _serviceManager.modelSpace.ElementAt(TableSelect.SelectedIndex).Value.variables)
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
                Grid.SetColumn(textBox, 1);

                data.Add(grid);
            }

            foreach (var item in _serviceManager.modelSpace.ElementAt(TableSelect.SelectedIndex).Value.externalKeys)
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
                Grid.SetColumn(comboBox, 1);

                data.Add(grid);
            }

            InputList.ItemsSource = data;
        }

        private void DataOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selected data
            _serviceManager.modelSpace.ElementAt(TableSelect.SelectedIndex).Value.dataService.Read(DataOut.SelectedIndex);


        }







        // CRUD FUNCTIONALITY
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
