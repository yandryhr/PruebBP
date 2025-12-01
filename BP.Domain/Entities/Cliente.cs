using System;
using System.Collections.Generic;

namespace BP.Domain.Entities;

public partial class Cliente: Persona
{
   
    public string? Contrasena { get; set; }

    public bool Estado { get; set; }   

}
