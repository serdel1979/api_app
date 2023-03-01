﻿namespace api_app.DTO
{
    public class UserNewDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Dni { get; set; }
        public int Id_Responsability { get; set; }
        public Boolean Leader { get; set; } = false;
    }
}