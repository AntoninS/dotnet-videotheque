using System.Windows;
using videotheque.ViewModel;

namespace videotheque.View
{
    /// <summary>
    /// Logique d'interaction pour ModificationMediaView.xaml
    /// </summary>
    public partial class ModificationMediaView : Window
    {
        public ModificationMediaView(){}

        public ModificationMediaView(object param)
        {
            InitializeComponent();
            DataContext = new ModificationMediaViewModel((int)param, this);
        }
    }
}
