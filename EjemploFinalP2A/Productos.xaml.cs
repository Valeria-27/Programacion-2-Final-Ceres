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
        string pathName = @"./Producto.txt";
        string pathNameAuxiliar = @"./ProductoAuxiliar.txt";
        public Productos()
        {
            InitializeComponent();
            MostrarProducto();
        }

        private void MostrarProducto()
        {
            try
            {
                txtListaProductos.Text = "";
                if (File.Exists(pathName))
                {
                    string fila;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    fila = tuberiaLectura.ReadLine();
                    while (fila != null)
                    {
                        txtListaProductos.AppendText(fila + "\r\n");
                        fila = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                }
            }
            catch (Exception ex){

                MessageBox.Show("Ocurrio un error" + ex.Message);
            }

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

                MessageBox.Show("Error al verificar si existe el archivo");
                Console.WriteLine("error: " + ex.Message);

            }
        }

        private void CrearArchivo()
        {
            File.CreateText(pathName);
        }

        private void btnVolverMenu2_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            this.Hide();
            menuPrincipal.ShowDialog();
            this.Close();
        }

        private void BtnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                if (File.Exists(pathName))
                {
                    string idProducto = txbIdProducto.Text.Trim();
                    string datosProducto = txbNuevoProducto.Text.Trim();
                    if (datosProducto != "" && idProducto!="")
                    {
                        if (ValidarId(idProducto))
                        {
                            //si es verdad no existen productos con ese idMascota
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(idProducto + "," + datosProducto);
                            tuberiaEscritura.Close();
                            MessageBox.Show("Producto creada con exito");
                            txbNuevoProducto.Text = "";
                            MostrarProducto();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un producto con ese id, pruebe con otro");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se permite vacio");
                    }
                }
                else
                {
                    CrearArchivo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
        private bool ValidarId(string idProducto)
        {
            bool respuesta = true;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string[] datos;
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datos = linea.Split(',');
                if (datos[0] == idProducto)
                {
                    //ya hay un id con ese valor
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
                string idProductoModificar = txbIdProducto.Text;
                string datosModificados = txbNuevoProducto.Text;
                string linea;
                string[] datosProducto;
                char separador = ',';
                bool modificado = false;
                StreamReader tuberiaLectura = File.OpenText(pathName);
                StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                linea = tuberiaLectura.ReadLine();
                while (linea != null)
                {
                    datosProducto = linea.Split(separador);
                    if (idProductoModificar == datosProducto[0])
                    {
                        //esta es la linea que contiene la mascota que se quiere eliminar
                        modificado = true;
                        //entonces debemos de enviar los nuevos datos y el id en el formato correcto
                        tuberiaEscritura.WriteLine(idProductoModificar + "," + datosModificados);
                    }
                    else
                    {
                        //esta mascota no es la que se quiere eliminar asi que la vamos a copiar al txt aux
                        tuberiaEscritura.WriteLine(linea);
                    }
                    linea = tuberiaLectura.ReadLine();
                }
                if (modificado)
                {
                    MessageBox.Show("El Producto se modificó con éxito");
                }
                else
                {
                    MessageBox.Show("Producto no encontrado");
                }
                tuberiaEscritura.Close();
                tuberiaLectura.Close();
                //Ahora debemos copiar todo el contenido del txt auxiliar al txt original
                // File.Delete(pathName)  borra
                // File.Move(pathName)  reemplaza el contenido
                File.Delete(pathName);
                File.Move(pathNameAuxiliar, pathName);
                File.Delete(pathNameAuxiliar);
                MostrarProducto();
                txbIdProducto.Text = "";
                txbNuevoProducto.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error al modificar el Producto" + ex.Message);
            }
        }
    }
}
