using EjemploFinalP2A;
using ProyectoFinalP2A;
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

namespace EjemploFinalP2A
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
            //PARA OBTENER LA DIRECCION DE INSTALACION txb.Text = Environment.CurrentDirectory;
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
    }
}
