using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalP2A
{
    class Final
    {
        private string nit;
        private string razonSocial;
        private string id;
        private string cantidad;

        public Final()
        {

        }
        public Final(string nit, string razonSocial, string id, string cantidad)
        {
            this.nit = nit;
            this.razonSocial = razonSocial;
            this.id = id;
            this.cantidad = cantidad;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nit
        {
            get { return nit; }
            set { nit = value; }
        }

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}
