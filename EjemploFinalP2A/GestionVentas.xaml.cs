using System;
using System.IO;
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
        string pathName = @".\ventas.txt";
        string pathNameP = @".\productoss.txt";
        string pathNameT = @".\total.txt";

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
            try
            {
                string productoid = txbID.Text;
                string cantidad = txbCantidad.Text;
                string linea;
                int total;
                string[] datosProductoo;
                char separador = ',';
                bool encontrado = false;
                StreamReader tuberiaLectura = File.OpenText(pathNameP);
                StreamWriter tuberiaEscritura = File.AppendText(pathNameT);
                linea = tuberiaLectura.ReadLine();
                while (linea != null)
                {
                    datosProductoo = linea.Split(separador);
                    if (datosProductoo[0] == productoid)
                    {
                        string datosV = datosProductoo[2];
                        total = Convert.ToInt32(txbCantidad.Text) * Convert.ToInt32(datosV);
                        tuberiaEscritura.WriteLine(datosProductoo[0] + "," + datosProductoo[1] + "," + datosProductoo[2] + "," + datosProductoo[3] + "," + datosProductoo[4]);
                        tuberiaEscritura.Close();
                        MessageBox.Show("Producto encontrado\n id: " +
                        datosProductoo[0] + "\nnombre: " + datosProductoo[1] +
                        "\nprecioV: " + datosProductoo[2] + "\nprecioC: " + datosProductoo[3] + "\ncantidad: " + datosProductoo[4] + "\ntotal: " + total);
                        MostrarFinalDG();
                        encontrado = true;
                    }
                    linea = tuberiaLectura.ReadLine();
                }
                if (!encontrado)
                {
                    MessageBox.Show(" no encontrada " + productoid);
                }
                tuberiaLectura.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la busqueda" + ex.Message);
            }
        }
        private bool ValidarNit(string nit)
        {
            bool respuesta = true;
            string[] datosSeparados;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosSeparados = linea.Split(',');
                if (nit == datosSeparados[0])
                {
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(pathName))
                {
                    string nit = txbNit.Text.Trim();
                    string razonSocial = txbRazonSocial.Text.Trim();
                    string fecha = DateTime.Now.ToString();
                    StreamReader tuberiaLectura = File.OpenText(pathNameP);
                    string linea;
                    int total = 0;
                    string[] datosProductoo;
                    char separador = ',';
                    linea = tuberiaLectura.ReadLine();
                    if (nit != "" && razonSocial != "" && fecha != "" )
                    {
                        datosProductoo = linea.Split(separador);
                        if (ValidarNit(nit))
                        { 
                            string datosV = datosProductoo[2];
                            total = Convert.ToInt32(txbCantidad.Text) * Convert.ToInt32(datosV);
                            tuberiaLectura.Close();
                            //el id es unico
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(nit + "," + razonSocial + "," + fecha + "," + total);
                            tuberiaEscritura.Close();
                            MessageBox.Show("La factura se grabo con exito");
                            txbNit.Text = "";
                            txbRazonSocial.Text = "";
                            txbID.Text = " ";
                            txbCantidad.Text = " ";
                            MostrarVentasDG();
                        }
                        else
                        {
                            MessageBox.Show("El id debe de ser unico");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se permite vacio");
                    }
                }
                else
                {
                    //crear el archivo
                    File.CreateText(pathName).Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }


        }
        private void MostrarVentasDG()
        {
            try
            {
                Venta venta;
                List<Venta> ventas = new List<Venta>();
                string nit, razonSocial, fecha, total;
                string[] datosVenta;
                if (File.Exists(pathName))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosVenta = linea.Split(',');
                        nit = datosVenta[0];
                        razonSocial = datosVenta[1];
                        fecha = datosVenta[2];
                        total = datosVenta[3];

                        venta = new Venta(nit, razonSocial, fecha, total);
                        ventas.Add(venta);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgMostrarVentas.ItemsSource = ventas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar las ventas " + ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void MostrarFinalDG()
        {
            try
            {
                Producto producto;
                List<Producto> productos = new List<Producto>();
                string id, nombre, precioVenta, precioCompra, cantidad;
                string[] datosProducto;
                if (File.Exists(pathNameT))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathNameT);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosProducto = linea.Split(',');
                        id = datosProducto[0];
                        nombre = datosProducto[1];
                        precioVenta = datosProducto[2];
                        precioCompra = datosProducto[3];
                        cantidad = datosProducto[4];
                        producto = new Producto(id, nombre, precioVenta, precioCompra, cantidad);
                        productos.Add(producto);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgVentas.ItemsSource = productos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los clientes " + ex.Message);
                Console.WriteLine(ex);
            }

        }
    }
}
