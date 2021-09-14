using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VendingMachineWPF.Model;

namespace VendingMachineWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public ObservableCollection<Product> Products { get; set; }

		private ObservableCollection<ProductUC> productsUC;
		public ObservableCollection<ProductUC> ProductsUC
		{
			get { return productsUC; }
			set { productsUC = value; OnPropertyRaised(); }
		}
		private void OnPropertyRaised([CallerMemberName] string propertyname = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		public Border AddProductBorder { get; set; }
		public MainWindow()
		{
			InitializeComponent();

			Button AddProductButton = new Button();

			AddProductButton = new Button();

			AddProductButton.Click += AddProductButton_Click;

			AddProductButton.Content = "+";
			AddProductButton.FontSize = 130;
			AddProductButton.FontWeight = FontWeights.Bold;
			AddProductButton.Foreground = Brushes.Black;
			AddProductButton.Background = Brushes.Transparent;
			AddProductButton.HorizontalAlignment = HorizontalAlignment.Center;
			AddProductButton.VerticalAlignment = VerticalAlignment.Center;
			AddProductButton.Width = 142;
			AddProductButton.Height = 185;
			AddProductButton.BorderThickness = new Thickness(0);


			AddProductBorder = new Border();
			AddProductBorder.Margin = new Thickness(3);
			AddProductBorder.Width = 142;
			AddProductBorder.Height = 185;
			AddProductBorder.Background = new SolidColorBrush(Color.FromRgb((byte)246, (byte)245, (byte)251));
			AddProductBorder.CornerRadius = new CornerRadius(25);

			AddProductBorder.Child = AddProductButton;

			DataContext = this;

			PreviewKeyDown += EscHandle;

			ProductsUC = new ObservableCollection<ProductUC>();

			Products = new ObservableCollection<Product>();

			Product P1 = new Product
			{
				Name = "Kola",
				Price = "1 AZN",
				Quantity = 25,
				ImagePath = "Images/kola.png"
			};
			Product P2 = new Product
			{
				Name = "Fanta",
				Price = "1 AZN",
				Quantity = 25,
				ImagePath = "Images/fanta.png"
			};
			Product P3 = new Product
			{
				Name = "Sprite",
				Price = "1 AZN",
				Quantity = 35,
				ImagePath = "Images/sprite.png"
			};
			Product P4 = new Product
			{
				Name = "Cheetos",
				Price = "1.20 AZN",
				Quantity = 50,
				ImagePath = "Images/cheetos.png"
			};
			Product P5 = new Product
			{
				Name = "Kartoska Fri",
				Price = "0.80 AZN",
				Quantity = 50,
				ImagePath = "Images/kartof.png"
			};
			Product P6 = new Product
			{
				Name = "Lays",
				Price = "2.50 AZN",
				Quantity = 50,
				ImagePath = "Images/lays.png"
			};
			Product P7 = new Product
			{
				Name = "Qened",
				Price = "19.90 AZN",
				Quantity = 50,
				ImagePath = "Images/qened.png"
			};
			Product P8 = new Product
			{
				Name = "Snickers",
				Price = "0.70 AZN",
				Quantity = 25,
				ImagePath = "Images/snickers.png"
			};
			Products.Add(P1);
			Products.Add(P2);
			Products.Add(P3);
			Products.Add(P4);
			Products.Add(P5);
			Products.Add(P6);
			Products.Add(P7);
			Products.Add(P8);

			for (int i = 0; i < Products.Count; i++) ProductsUC.Add(new ProductUC { Product = Products[i] });

			for (int i = 0; i < ProductsUC.Count; i++)
			{
				ProductsUC[i].Window.Width = 135;
				ProductsUC[i].Window.Height = 180;

				ProductsUC[i].Window.Margin = new Thickness(3, 0, 0, 0);
			}
			//ProductsUC.Add(AddProductBorder);
		}

		private void AddProductButton_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void EscHandle(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Application.Current.Shutdown();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => SearchTxt.Focus();
		private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();

		private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox txt)
			{
				if (!string.IsNullOrWhiteSpace(txt.Text))
				{
					var keyword = txt.Text.ToLower();
					var Temp = new List<Product>(Products);
					Temp = Temp.Where(p => p.Name.ToLower().Contains(keyword)).ToList();

					ProductsUC = new ObservableCollection<ProductUC>();
					for (int i = 0; i < Temp.Count; i++)
					{
						ProductsUC.Add(new ProductUC { Product = Temp[i] });

						ProductsUC[i].Window.Width = 135;
						ProductsUC[i].Window.Height = 180;

						ProductsUC[i].Window.Margin = new Thickness(3, 0, 0, 0);
					}
				}
				else
				{
					ProductsUC = new ObservableCollection<ProductUC>();
					for (int i = 0; i < Products.Count; i++) 
					{
						ProductsUC.Add(new ProductUC { Product = Products[i] });
						ProductsUC[i].Window.Width = 135;
						ProductsUC[i].Window.Height = 180;

						ProductsUC[i].Window.Margin = new Thickness(3, 0, 0, 0);
					}
				}
			}
		}
	}
}
