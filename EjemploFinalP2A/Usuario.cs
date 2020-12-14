using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalP2A
{
    class Usuarioop
         { 
        private string nombre;
        private string tipo;
        private string usuario;
        private string password;
        public Usuarioop()
        {



        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public string UsuarioF
        {
            get { return usuario; }
            set { usuario = value; }
        }


        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }


        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        public Usuarioop(string nombre, string tipo, string usuario, string password)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.usuario = usuario;
            this.password = password;
        }

    }
}
