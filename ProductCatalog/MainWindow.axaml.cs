using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;

namespace ProductCatalog;

public partial class MainWindow : Window
{
    private List<Product> _products;
    public MainWindow()
    {
        InitializeComponent();
        LoadProducts();
        FilterProducts();
    }

    public void LoadProducts()
    {
        _products = new List<Product>
        {
            new Product { Name = "Produkt 1", Price = 10, IsAvailable = true, ImageSource = "produkt1.png", Category = "Elektronika" },
            new Product { Name = "Produkt 2", Price = 20, IsAvailable = true, ImageSource = "produkt2.png", Category = "Jedzenie" },
            new Product { Name = "Produkt 3", Price = 30, IsAvailable = true, ImageSource = "produkt3.png", Category = "Narzędzia" },
            new Product { Name = "Produkt 4", Price = 40, IsAvailable = false, ImageSource = "produkt4.png", Category = "Elektronika" },
        };
    }

    public void FilterProducts()
    {
        string searchText = SearchBar.Text?.ToLower() ?? string.Empty;
        string selectedCategory = (MyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Wszystko";
        
        MyListBox.Items.Clear();
        
        foreach (var product in _products)
        {
            if((selectedCategory == "Wszystko" || product.Category == selectedCategory) && (string.IsNullOrEmpty(searchText) || product.Name.ToLower().Contains(searchText)))
            {
                var item = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                var image = new Image
                {
                    Height = 50,
                    Width = 50,
                    Source = new Bitmap(product.ImageSource),
                    Margin = new Thickness(10),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment =  HorizontalAlignment.Center
                };
                var nameBlock = new TextBlock
                {
                    Text = product.Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment =  HorizontalAlignment.Center,
                    Margin = new Thickness(10),
                };
                var priceBlock = new TextBlock
                {
                    Text = $"Cena: {product.Price:C}",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment =  HorizontalAlignment.Center,
                    Margin = new Thickness(10),
                };
                var availabilityBlock = new TextBlock
                {
                    Text = $"Dostępność: {(product.IsAvailable ? "Tak" : "Nie")}",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment =  HorizontalAlignment.Center,
                    Margin = new Thickness(10),
                };
                var checkBox = new CheckBox
                {
                    IsChecked = product.IsAvailable,
                    Margin = new Thickness(10),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment =  HorizontalAlignment.Center
                };
                checkBox.Checked += (s, e) => UpdateProductAvailability(product, true);
                checkBox.Unchecked += (s, e) => UpdateProductAvailability(product, false);
                
                item.Children.Add(image);
                item.Children.Add(nameBlock);
                item.Children.Add(priceBlock);
                item.Children.Add(availabilityBlock);
                item.Children.Add(checkBox);

                MyListBox.Items.Add(item);
            }
        }
    }
    
    private void UpdateProductAvailability(Product product, bool isAvailable)
    {
        product.IsAvailable = isAvailable;
        FilterProducts();
    }
    
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageSource { get; set; }
        public string Category { get; set; }
        
    }


    private void MyComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        FilterProducts();
    }

    private void SearchBar_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        FilterProducts();
    }
}