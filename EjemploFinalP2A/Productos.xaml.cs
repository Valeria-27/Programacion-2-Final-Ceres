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
            MostrarProductosDG();
            VerificarArchivo();
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
                List<Producto> clientes = new List<Producto>();
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
                        producto = new Producto(id, nombre);
                        clientes.Add(producto);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgListaP.ItemsSource = clientes;
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
                    string idProducto = txbIdProducto.Text.Trim();
                    string producto = txbDatosProducto.Text.Trim();
                    if (producto != "" && idProducto != "")
                    {
                        if (ValidarId(idProducto))
                        {
                            //el id es unico
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(idProducto + "," + producto);
                            tuberiaEscritura.Close();
                            MessageBox.Show("El cliente se grabo con exito");
                            txbDatosProducto.Text = "";
                            txbIdProducto.Text = "";
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

        private bool ValidarId(string idCliente)
        {
            bool respuesta = true;
            string[] datosSeparados;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosSeparados = linea.Split(',');
                if (idCliente == datosSeparados[0])
                {
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ciEliminar = txbIdProducto.Text;
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
                    txbIdProducto.Text = "";
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

        private void BtnModificarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idModificar = txbIdProducto.Text;
                if (idModificar != "")
                {
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
                            string productoModificado = txbDatosProducto.Text;
                            tuberiaEscritura.WriteLine(idModificar + "," + productoModificado);
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
                        MessageBox.Show("El cliente se modifico con exito");
                        MostrarProductosDG();
                        txbIdProducto.Text = "";
                        txbDatosProducto.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("El cliente no existe");
                    }
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

        private void DgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgListaP.SelectedItem != null)
            {
                Producto producto = new Producto();
                producto = (Producto)dgListaP.SelectedItem;
                txbIdProducto.Text = producto.Id;
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
