using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
	/// Interaction logic for ProductUC.xaml
	/// </summary>
	public partial class ProductUC : UserControl, INotifyPropertyChanged
	{
		private Product product;


		public bool EditPanelsIsOpen { get; set; }
		public Product Product
		{
			get { return product; }
			set { product = value; OnPropertyRaised(); }
		}
		private void OnPropertyRaised([CallerMemberName] string propertyname = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public ProductUC()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			EditPanelsIsOpen = true;
		}

		
	}
}
