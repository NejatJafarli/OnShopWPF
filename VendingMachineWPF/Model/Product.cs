using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineWPF.Model
{
	public class Product : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(); }
		}
		private double price;
		public double Price
		{
			get { return price; }
			set { price = value; OnPropertyChanged(); }
		}
		private int quantity;
		public int Quantity
		{
			get { return quantity; }
			set { quantity = value; OnPropertyChanged(); }
		}
		private string imagePath;
		public string ImagePath
		{
			get { return imagePath; }
			set { imagePath = value; OnPropertyChanged(); }
		}

		protected void OnPropertyChanged([CallerMemberName] string ProductName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(ProductName));
			}
		}
	}
}
