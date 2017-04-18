using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogoDVD
{
    public class Pais
    {
        private string _nombre;
        private string iso2;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        
        public string Iso2
        {
            get { return iso2; }
            set { iso2 = value; }
        }
    }
}
