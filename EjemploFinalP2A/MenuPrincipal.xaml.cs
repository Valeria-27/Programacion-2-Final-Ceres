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
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void BtnQS_Click(object sender, RoutedEventArgs e)
        {
            QuienesSomos quienesSomos = new QuienesSomos();
            this.Hide();
            quienesSomos.ShowDialog();
            this.Close();

        }

        private void BtnAceptarCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Desea salir de la página", "Atención!!", MessageBoxButton.YesNo);
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            Productos productosM = new Productos();
            this.Hide();
            productosM.ShowDialog();
            this.Close();
        }
    }
}
