using System;

namespace LaLuz.Models;

public class DetallePlanificacion
{

        public string alimentador { get; set; }
        public string fechaCorte { get; set; }
        public string horaDesde { get; set; }
        public string horaHasta { get; set; }
        public string comentario { get; set; }
        public string fechaRegistro { get; set; }
        public string fechaHoraCorte { get; set; }
}
