using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba2.Models
{
    public class Partidos
    {
        public int Id_Match { get; set; }
        public int Id { get; set; }
        public string Local { get; set; }
        public string Visitante { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estatus { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }

    }
}