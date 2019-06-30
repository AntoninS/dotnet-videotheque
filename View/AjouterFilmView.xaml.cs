﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using videotheque.Model;
using videotheque.ViewModel;

namespace videotheque.View
{
    /// <summary>
    /// Logique d'interaction pour AjouterFilmView.xaml
    /// </summary>
    public partial class AjouterFilmView : Window
    {
        public AjouterFilmView() { }

        public AjouterFilmView(ObservableCollection<Media> listFilm)
        {
            InitializeComponent();
            DataContext = new AjouterFilmViewModel(this, listFilm);
        }
    }
}
