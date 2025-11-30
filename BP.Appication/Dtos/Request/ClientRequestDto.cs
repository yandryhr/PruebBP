namespace BP.Application.Dtos.Request
{
    public class ClientRequestDto
    {
        public required string Contrasena { get; set; }
        public bool Estado { get; set; }
        public required string Nombre { get; set; } 
        public  string? Genero { get; set; }
        public int Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
