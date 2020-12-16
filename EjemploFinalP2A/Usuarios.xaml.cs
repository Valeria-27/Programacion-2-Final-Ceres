using System;
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
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario : Window
    {
        string pathName = @"./usuarios.txt";
        string pathNameAuxiliar = @"./auxiliarusuarios.txt";
        public Usuario()
        {
            InitializeComponent();
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
        private void MostrarUsuariosDG()
        {
            try
            {
                Usuarioop usuarioP;
                List<Usuarioop> clientes = new List<Usuarioop>();
                string nombre, tipo, usuario, password;
                string[] datosUsuario;
                if (File.Exists(pathName))
                {
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    string linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosUsuario = linea.Split(',');
                        nombre = datosUsuario[0];
                        tipo = datosUsuario[1];
                        usuario = datosUsuario[2];
                        password = datosUsuario[3];
                        usuarioP = new Usuarioop(nombre, tipo, usuario, password);
                        clientes.Add(usuarioP);
                        linea = tuberiaLectura.ReadLine();
                    }
                    tuberiaLectura.Close();
                    dgUsuarios.ItemsSource = clientes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los clientes " + ex.Message);
                Console.WriteLine(ex);
            }

        }

        private void BtnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(pathName))
                {
                    string nombre = txbNombre.Text.Trim();
                    string tipo = txbTipo.Text.Trim();
                    string usuario = txbUsuario.Text.Trim();
                    string contraseña = txbPassword.Text.Trim();
                    if (nombre != "" && tipo != "" && usuario != "" && contraseña != "")
                    {
                        if (ValidarNombre(nombre))
                        {
                            //el id es unico
                            StreamWriter tuberiaEscritura = File.AppendText(pathName);
                            tuberiaEscritura.WriteLine(nombre + "," + tipo + "," + usuario + "," + contraseña);
                            tuberiaEscritura.Close();
                            MessageBox.Show("El cliente se grabo con exito");
                            txbNombre.Text = "";
                            txbTipo.Text = "";
                            txbUsuario.Text = "";
                            txbPassword.Text = "";
                            MostrarUsuariosDG();
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
                    File.CreateText(pathName).Dispose();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }
        }

        private bool ValidarNombre(string nombre)
        {
            bool respuesta = true;
            string[] datosSeparados;
            StreamReader tuberiaLectura = File.OpenText(pathName);
            string linea = tuberiaLectura.ReadLine();
            while (linea != null)
            {
                datosSeparados = linea.Split(',');
                if (nombre == datosSeparados[0])
                {
                    respuesta = false;
                    break;
                }
                linea = tuberiaLectura.ReadLine();
            }
            tuberiaLectura.Close();
            return respuesta;
        }
        private void BtnModificarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idModificar = txbNombre.Text;
                if (idModificar != "")
                {
                    string nombre = txbNombre.Text.Trim();
                    string tipo = txbTipo.Text.Trim();
                    string usuario = txbUsuario.Text.Trim();
                    string password = txbPassword.Text.Trim();
                    string linea;
                    string[] datosUsuario;
                    char separador = ',';
                    bool modificar = false;
                    StreamReader tuberiaLectura = File.OpenText(pathName);
                    StreamWriter tuberiaEscritura = File.AppendText(pathNameAuxiliar);
                    linea = tuberiaLectura.ReadLine();
                    while (linea != null)
                    {
                        datosUsuario = linea.Split(separador);
                        if (idModificar != datosUsuario[0])
                        {
                            tuberiaEscritura.WriteLine(linea);
                        }
                        else
                        {
                            modificar = true;
                            tuberiaEscritura.WriteLine(nombre + "," + tipo + "," + usuario + "," + password);
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
                        MessageBox.Show("El usuario se modifico con exito");
                        MostrarUsuariosDG();
                        txbNombre.Text = "";
                        txbTipo.Text = "";
                        txbUsuario.Text = "";
                        txbPassword.Text = "";
                        
                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe");
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



        private void BtnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ciEliminar = txbNombre.Text;
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
                        MostrarUsuariosDG();
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
        private void DgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsuarios.SelectedItem != null)
            {
                Usuarioop usuario = new Usuarioop();
                usuario = (Usuarioop)dgUsuarios.SelectedItem;
                txbNombre.Text = usuario.Nombre;
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
