namespace Api_Compta.Models.Dtos
{
    public class EtablissementDto
    {
        public Guid EtablissementId { get; set; }
        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;
        
    }
}
