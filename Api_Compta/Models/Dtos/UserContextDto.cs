namespace Api_Compta.Models.Dtos
{
    public class UserContextDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }

        public List<EtablissementDto> Etablissements { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }

}
