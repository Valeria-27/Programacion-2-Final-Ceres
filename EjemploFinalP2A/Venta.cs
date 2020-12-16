using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalP2A
{
    class Venta
    {
        private string nit;
        private string razonSocial;
        private string fecha;
        private string total;
       

        public Venta()
        {

        }
        public Venta(string nit, string razonSocial, string fecha, string total)
        {
            this.nit = nit;
            this.razonSocial = razonSocial;
            this.fecha = fecha;
            this.total = total;
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

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Total
        {
            get { return total; }
            set { total = value; }
        }
        
    }
}
