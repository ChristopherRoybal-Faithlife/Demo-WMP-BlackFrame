﻿using Demo_WMP_BlackFrame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Demo_WMP_BlackFrame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = ViewModel = new MainWindowViewModel();
		}

		public MainWindowViewModel ViewModel { get; }

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			ViewModel.Dispose();
		}

	}
}
