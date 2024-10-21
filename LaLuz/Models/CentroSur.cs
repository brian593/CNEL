using System;
using System.Collections.Generic;

namespace LaLuz.Models
{
    public class CorteDetalle
    {
        public string FechaEjecucion { get; set; }
        public string HoraFin { get; set; }
        public string HoraInicio { get; set; }
        public string Tipo { get; set; }
        public string Detalle { get; set; }
    }

    public class Corte
    {
        public string CUEN { get; set; }
        public string Cedula { get; set; }
        public string CuentaContrato { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Alimentador { get; set; }
        public List<CorteDetalle> Cortes { get; set; } // Corregido para que no se refiera a sí mismo.
    }

    public class Data
    {
        public string SinCortes { get; set; }
        public List<Corte> Cortes { get; set; }
        public string Detalle { get; set; }
    }

    public class CentroSur
    {
        public bool ok { get; set; }
        public Data data { get; set; }
        public string jsonData { get; set; }
    }
}
