﻿namespace Events.Core.DTOs
{
    public class CityReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string? Code { get; set; }
        public string? Region { get; set; }
        public string? Coordinates { get; set; }
    }
}
