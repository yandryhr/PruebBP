namespace BP.Application.Dtos.Response
{
    public class ClientResponseDto
    {
        public long ClienteId { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool Estado { get; set; }
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? EstadoCliente { get; set; }
    }
}
