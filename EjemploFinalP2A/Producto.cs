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
        private string nombre;
        private string precioVenta;
        private string precioCompra;
        private string cantidad;

        public Producto()
        {

        }
        public Producto(string id, string nombre, string precioVenta, string precioCompra, string cantidad)
        {
            this.id = id;
            this.nombre = nombre;
            this.precioVenta = precioVenta;
            this.precioCompra = precioCompra;
            this.cantidad = cantidad;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
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
    }
}
