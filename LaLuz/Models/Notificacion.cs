using System;

namespace LaLuz.Models;

public class Notificacion
{
        public int idUnidadNegocios { get; set; }
        public string cuentaContrato { get; set; }
        public string alimentador { get; set; }
        public string cuen { get; set; }
        public string direccion { get; set; }
        public string fechaRegistro { get; set; }

        public List<DetallePlanificacion> detalleplanificacion { get; set; }

}
