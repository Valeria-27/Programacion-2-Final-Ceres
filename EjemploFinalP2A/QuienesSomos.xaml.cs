
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

namespace ProyectoFinalP2A
{
    /// <summary>
    /// Lógica de interacción para QuienesSomos.xaml
    /// </summary>
    public partial class QuienesSomos : Window
    {
        public QuienesSomos()
        {
            InitializeComponent();
        }


        private void btnVolverMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menuPrincipal = new  MenuPrincipal();
            this.Hide();
            menuPrincipal.ShowDialog();
            this.Close();
        }
    }
}
