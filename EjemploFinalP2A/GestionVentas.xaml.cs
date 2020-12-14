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
using System.Windows.Shapes;

namespace ProyectoFinalP2A
{
    /// <summary>
    /// Lógica de interacción para GestionVentas.xaml
    /// </summary>
    public partial class GestionVentas : Window
    {
        public GestionVentas()
        {
            InitializeComponent();
        }

        private void btnVolverMen_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            this.Hide();
            menuPrincipal.ShowDialog();
            this.Close();
        }

        private void BtnGuardarVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
