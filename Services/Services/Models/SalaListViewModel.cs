namespace Servicios.Models
{
    public class SalaListViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; }
        public int TotalEquipos { get; set; }
        public int EquiposDisponibles { get; set; }
    }
}
