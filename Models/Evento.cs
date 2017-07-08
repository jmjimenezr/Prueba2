using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba2.Models
{
    public class Evento
    {
        public string Id { get; set; }
        public string Id_Jugador { get; set; }
        public string Resultado { get; set; }
        public string Jugador { get; set; }
        public string Equipo { get; set; }
        public string Minuto { get; set; }
        public string Accion { get; set; }
    }
}