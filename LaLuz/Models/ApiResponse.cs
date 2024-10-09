using System;

namespace LaLuz.Models;

public class ApiResponse
{
        public string resp { get; set; }
        public string mensaje { get; set; }
        public string mensajeError { get; set; }
        public object extra { get; set; }
        public List<Notificacion> notificaciones { get; set; }

        public string jsonData { get; set; }
 
}
