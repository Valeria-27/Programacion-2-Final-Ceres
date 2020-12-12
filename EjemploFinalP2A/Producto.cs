using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalP2A
{
    class Producto
    {

        private string id;
        private string precioVenta;
        private string precioCompra;
        private string cantidad;
        private string nombre;

        public Producto()
        {

        }
        public Producto(string id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public Producto(string i, string n, string pv, string pc, string c)
        {
            id = i;
            nombre = n;
            precioVenta = pv;
            precioCompra = pc;
            cantidad = c;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        public string PrecioCompra
        {
            get { return precioCompra; }
            set { precioCompra = value; }
        }

        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


    }
}
