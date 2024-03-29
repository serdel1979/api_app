﻿using System.Runtime.InteropServices;

namespace api_app.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Observation Observation { get; set; }
        public int ObservationId { get; set; }

        public byte[] Image { get; set; } = null;
    }
}
