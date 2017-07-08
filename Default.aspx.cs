using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Prueba2.Models;
using System.Xml.Linq;

namespace Prueba2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Catalogos();
        }

        private void Catalogos()
        {
            ddlTorneo.DataSource = LeerXML();
            ddlTorneo.DataTextField = "Nombre";
            ddlTorneo.DataValueField = "Id";
            ddlTorneo.DataBind();

            
        }

        private List<Torneos> LeerXML()
        {
            ddlTorneo.Items.Clear();
            List<Torneos> dato = new List<Torneos>();
            dato.Add(new Torneos { Id = -1, Nombre = "-- Seleccionar --" });
            string ruta = HttpContext.Current.Server.MapPath("/XML") + @"\soccer.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(ruta);

            XmlNodeList xScores = xml.GetElementsByTagName("scores");
            XmlNodeList xLista = ((XmlElement)xScores[0]).GetElementsByTagName("category");

            foreach (XmlElement nodo in xLista)
            {
                dato.Add(new Torneos { Id = Convert.ToInt32(nodo.GetAttribute("id")),
                    Nombre = nodo.GetAttribute("name") });
            }
            return dato;
        }

        private List<Partidos> LeerPartidos(int id)
        {
            
            List<Partidos> dato = new List<Partidos>();
            string ruta = HttpContext.Current.Server.MapPath("/XML") + @"\soccer.xml";
            XDocument xml = XDocument.Load(ruta);
                       

            IEnumerable<XElement> maches = xml.Descendants().Where(a => a.Name == "category" && (string)a.Attribute("id").Value == id.ToString())
                .Descendants().Where(c => c.Name == "matches");

            foreach (XElement xEle in maches)
            {
                IEnumerable<XElement> mach = xEle.Descendants().Where(d => d.Name == "match");
                foreach (XElement xE in mach)
                {
                    XElement local = xE.Descendants().Where(d => d.Name == "localteam").First();
                    XElement visitante = xE.Descendants().Where(d => d.Name == "visitorteam").First();
                    
                    dato.Add(new Partidos
                    {
                        Id = id,
                        Local = local.Attribute("name").Value,
                        Visitante = visitante.Attribute("name").Value,
                        Fecha = xE.Attribute("formatted_date").Value,
                        Hora = xE.Attribute("time").Value,
                        Estatus = xE.Attribute("status").Value,
                        GolesLocal = Convert.ToInt32(local.Attribute("goals").Value),
                        GolesVisitante = Convert.ToInt32(visitante.Attribute("goals").Value),
                        Id_Match = Convert.ToInt32(xE.Attribute("id").Value)
                    });
                }
            }

            return dato;
        }

        private List<Evento> LeerEventos(int id, int id_match)
        {

            List<Evento> dato = new List<Evento>();
            string ruta = HttpContext.Current.Server.MapPath("/XML") + @"\soccer.xml";
            XDocument xml = XDocument.Load(ruta);


            IEnumerable<XElement> maches = xml.Descendants().Where(a => a.Name == "category" && (string)a.Attribute("id").Value == id.ToString())
                .Descendants().Where(c => c.Name == "matches")
                .Descendants().Where(d => d.Name == "match" && (string)d.Attribute("id").Value == id_match.ToString());


            foreach (XElement xE in maches)
            {
                IEnumerable<XElement> evento = xE.Descendants().Where(d => d.Name == "event");
                foreach (XElement xEv in evento)
                {
                    dato.Add(new Evento
                    {
                        Id = xEv.Attribute("eventid").Value,
                        Id_Jugador = xEv.Attribute("playerId").Value,
                        Jugador = xEv.Attribute("player").Value,
                        Equipo = xEv.Attribute("team").Value,
                        Minuto = xEv.Attribute("minute").Value,
                        Accion = xEv.Attribute("type").Value,
                        Resultado = xEv.Attribute("result").Value
                    });
                }
            }
            

            return dato.OrderBy(p => p.Minuto).ToList();
        }

        protected void ddlTorneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTorneo.SelectedValue == "-1")
            {
                grdPartidos.DataSource = null;
                grdPartidos.DataBind();
                grdEventos.DataSource = null;
                grdEventos.DataBind();
            }
            else
            {

                grdPartidos.DataSource = LeerPartidos(Convert.ToInt32(this.ddlTorneo.SelectedValue));
                grdPartidos.DataBind();
            }
            
        }        
        
        protected void grdPartidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPartidos.PageIndex = e.NewPageIndex;
            grdPartidos.DataSource = LeerPartidos(Convert.ToInt32(this.ddlTorneo.SelectedValue));
            grdPartidos.DataBind();
        }

        protected void grdEventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPartidos.PageIndex = e.NewPageIndex;
            grdPartidos.DataSource = LeerPartidos(Convert.ToInt32(this.ddlTorneo.SelectedValue));
            grdPartidos.DataBind();
        }

        protected void grdPartidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(grdPartidos.SelectedDataKey.Value);

            grdEventos.DataSource = LeerEventos(Convert.ToInt32(this.ddlTorneo.SelectedValue), id);
            grdEventos.DataBind();
        }
    }
}