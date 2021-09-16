using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

		private ProductUC selectedProduct;

		public ProductUC SelectedProduct
		{
			get { return selectedProduct; }
			set { selectedProduct = value; OnPropertyRaised(); }
		}


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
				Price = 1,
				Quantity = 25,
				ImagePath = "Images/kola.png"
			};
			Product P2 = new Product
			{
				Name = "Fanta",
				Price = 1,
				Quantity = 25,
				ImagePath = "Images/fanta.png"
			};
			Product P3 = new Product
			{
				Name = "Sprite",
				Price = 1,
				Quantity = 35,
				ImagePath = "Images/sprite.png"
			};
			Product P4 = new Product
			{
				Name = "Pepsi",
				Price = 1,
				Quantity = 45,
				ImagePath = "Images/pepsi.png"
			};

			Product P5 = new Product
			{
				Name = "Kartoska Fri",
				Price = 0.80,
				Quantity = 50,
				ImagePath = "Images/kartof.png"
			};
			Product P6 = new Product
			{
				Name = "Lays",
				Price = 2.50,
				Quantity = 50,
				ImagePath = "Images/lays.png"
			};
			Product P7 = new Product
			{
				Name = "Qened",
				Price = 19.90,
				Quantity = 50,
				ImagePath = "Images/qened.png"
			};
			Product P8 = new Product
			{
				Name = "Snickers",
				Price = 0.70,
				Quantity = 25,
				ImagePath = "Images/snickers.png"
			};
			Product P9 = new Product
			{
				Name = "Cheetos",
				Price = 1.20,
				Quantity = 50,
				ImagePath = "Images/cheetos.png"
			};
			Products.Add(P1);
			Products.Add(P2);
			Products.Add(P3);
			Products.Add(P4);
			Products.Add(P5);
			Products.Add(P6);
			Products.Add(P9);
			Products.Add(P7);
			Products.Add(P8);

			for (int i = 0; i < Products.Count; i++) ProductsUC.Add(new ProductUC { Product = Products[i] });

			for (int i = 0; i < ProductsUC.Count; i++)
			{
				ProductsUC[i].Window.Width = 130;
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

						ProductsUC[i].Window.Width = 130;
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
						ProductsUC[i].Window.Width = 130;
						ProductsUC[i].Window.Height = 180;

						ProductsUC[i].Window.Margin = new Thickness(3, 0, 0, 0);
					}
				}
			}
		}


		private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (sender is ListBox list)
			{
				if (list.SelectedItem is ProductUC product)
				{
					SelectedProduct = product;
					if (product.EditPanelsIsOpen)
					{
						EditPanel.Visibility = Visibility.Visible;
					}
					else
						EditPanel.Visibility = Visibility.Collapsed;
				}
			}
		}

		private void EditPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (SelectedProduct is null)
			{
				EditPanel.Visibility = Visibility.Collapsed;

			}
			else
			{
				EditPanel.Visibility = Visibility.Collapsed;

				SelectedProduct.EditPanelsIsOpen = false;
			}
		}

		private void Image_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Bitmap))
			{
				e.Effects = DragDropEffects.Copy;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}
		}
		private void Image_Drop(object sender, DragEventArgs e)
		{
			string path = "";
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			foreach (var item in files)
			{
				path = item;
			}

			SelectedProduct.Product.ImagePath = path;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!(SelectedProduct is null))
				SelectedProduct.EditPanelsIsOpen = false;
			SelectedProduct = null;
			if (EditPanel.Visibility == Visibility.Collapsed)
				EditPanel.Visibility = Visibility.Visible;
			SelectedProduct = new ProductUC();
			SelectedProduct.Product = new Product();
			SelectedProduct.Product.ImagePath = "Images/dragdrop.png";
			SelectedProduct.Product.Name = "Name";
			SelectedProduct.Product.Quantity = 0;
			SelectedProduct.Product.Price = 0;

			AddProductButton.Visibility = Visibility.Visible;

		}

		private void AddProductButton_Click_1(object sender, RoutedEventArgs e)
		{
			Products.Add(new Product
			{
				ImagePath = SelectedProduct.Product.ImagePath,
				Name = SelectedProduct.Product.Name,
				Quantity = SelectedProduct.Product.Quantity,
				Price = SelectedProduct.Product.Price,
			});

			ProductsUC.Add(new ProductUC { Product = Products[Products.Count-1] });
			ProductsUC[ProductsUC.Count-1].Window.Width = 130;
			ProductsUC[ProductsUC.Count-1].Window.Height = 180;

			ProductsUC[ProductsUC.Count-1].Window.Margin = new Thickness(3, 0, 0, 0);

		}
	}
}
