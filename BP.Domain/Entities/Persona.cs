using System;
using System.Collections.Generic;

namespace BP.Domain.Entities;

public partial class Persona
{
    public long PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int Edad { get; set; }

    public string? Identificacion { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
       
}
