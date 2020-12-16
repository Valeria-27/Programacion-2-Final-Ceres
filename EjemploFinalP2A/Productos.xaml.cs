using System;
using ProyectoFinalP2A;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        string pathName = @".\productos.txt";
        string pathNameAuxiliar = @".\auxiliar.txt";
        public Productos()
        {
            InitializeComponent();
            CrearArchivo();
        }
        private void VerificarArchivo()
        {
            try
            {
                if (!(File.Exists(pathName)))
                {
                    CrearArchivo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error" + ex.Message);
            }
        }
        private void CrearArchivo()
        {
            File.CreateText(pathName).Dispose();
        }

        private void MostrarProductosDG()
        {
            try
            {
                Producto producto;
                List<Producto> productos = new List<Producto>();
                string id, nombre, precioVenta, precioCompra, cantidad;
                string[] datosProducto;
                if (File.Exists(pathName))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathName);
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
                    dgListaP.ItemsSource = productos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los clientes " + ex.Message);
                Console.WriteLine(ex);
            }
        }

        private void BtnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (File.Exists(pathName))
                {
                    string id = txbId.Text.Trim();
                    string nombre = txbNombre.Text.Trim();
                    string precioVenta = txbPrecioVenta.Text.Trim();
                    string precioCompra = txbPrecioCompra.Text.Trim();
                    string cantidad = txbCantidad.Text.Trim();
                    if (id != "" && nombre != "" && precioVenta != "" && cantidad != "" && id != "")
                    {
                        if (ValidarId(id))
                        {
                            //el id es unico
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(id + "," + nombre + "," + precioVenta + "," + precioCompra + "," + cantidad);
                            tuberiaEscritura.Close();
                            MessageBox.Show("El producto se grabo con exito");
                            txbNombre.Text = "";
                            txbId.Text = "";
                            txbPrecioVenta.Text = "";
                            txbPrecioCompra.Text = "";
                            txbCantidad.Text = "";
                            MostrarProductosDG();
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


        private bool ValidarId(string id)
        {
            bool respuesta = true;
            string[] datosSeparados;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosSeparados = linea.Split(',');
                if (id == datosSeparados[0])
                {
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }

        private void BtnModificarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idModificar = txbId.Text;
                if (idModificar != "")
                {
                    string id = txbId.Text.Trim();
                    string nombre = txbNombre.Text.Trim();
                    string precioVenta = txbPrecioVenta.Text.Trim();
                    string precioCompra = txbPrecioCompra.Text.Trim();
                    string cantidad = txbCantidad.Text.Trim();
                    string linea;
                    string[] datosProducto;
                    char separador = ',';
                    bool modificar = false;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                    linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosProducto = linea.Split(separador);
                        if (idModificar != datosProducto[0])
                        {
                            tuberiaEscritura.WriteLine(linea);
                        }
                        else
                        {
                            modificar = true;
                            tuberiaEscritura.WriteLine(idModificar + "," + nombre + "," + precioVenta + "," + precioCompra + "," + cantidad);
                        }
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaEscritura.Close();
                    tuberiaLectura.Close();



                    //vamos a copiar todo el contenido del auxiliar en el original
                    File.Delete(pathName);
                    File.Move(pathNameAuxiliar, pathName);
                    File.Delete(pathNameAuxiliar);
                    if (modificar)
                    {
                        MessageBox.Show("El producto se modifico con exito");
                        MostrarProductosDG();
                        txbNombre.Text = "";
                        txbId.Text = "";
                        txbPrecioCompra.Text = "";
                        txbPrecioVenta.Text = "";
                        txbCantidad.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("El producto no existe");
                    }
                }
                else
                {
                    MessageBox.Show("No se pemite vacio");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pemite vacio" + ex.Message);

            }
        }
        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ciEliminar = txbId.Text;
                if (ciEliminar != "")
                {
                    string linea;
                    string[] datosProducto;
                    char separador = ',';
                    bool eliminado = false;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                    linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosProducto = linea.Split(separador);
                        if (ciEliminar != datosProducto[0])
                        {
                            tuberiaEscritura.WriteLine(linea);
                        }
                        else
                        {
                            eliminado = true;
                        }
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaEscritura.Close();
                    tuberiaLectura.Close();

                    //vamos a copiar todo el contenido del auxiliar en el original
                    File.Delete(pathName);
                    File.Move(pathNameAuxiliar, pathName);
                    File.Delete(pathNameAuxiliar);
                    if (eliminado)
                    {
                        MessageBox.Show("El cliente se elimino con exito");

                        MostrarProductosDG();
                    }
                    else
                    {
                        MessageBox.Show("El cliente no existe");
                    }
                    txbNombre.Text = "";
                }
                else
                {
                    MessageBox.Show("No se pemite vacio");
                }

            }
            catch (Exception)
            {

            }
        }

        private void btnVolverMenu2_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            this.Hide();
            menuPrincipal.ShowDialog();
            this.Close();
        }

        
    }



    
}
