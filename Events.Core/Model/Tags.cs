﻿using EventsManager.Model;

namespace Events.Core.Model
{
    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Media> Media { get; set; }
    }
}
