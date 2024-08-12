using System;

namespace appClinica_TC1.Models
{
   public class Paciente
{
    public int Id { get; set; } // Propiedad Id añadida
    public string Cedula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public int Edad { get; set; }
    public string Direccion { get; set; } = string.Empty;

}


}
