﻿using api_app.Entities;

namespace api_app.DTO
{
    public class ProjectRespDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public int Id_Job { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Supervisor { get; set; }
        public int Id_Responsibility { get; set; }
        public string Reference { get; set; }
        public int Id_Leader { get; set; }
        public Responsability Responsability { get; set; }
    }
}