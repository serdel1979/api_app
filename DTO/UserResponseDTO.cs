namespace api_app.DTO
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Dni { get; set; }
        public int Id_Responsability { get; set; }
        public Boolean Leader { get; set; } = false;
        public int Id_Responsibility { get; set; }
    }
}
