﻿using PcBuilder.DAL.MySQL.Entities;

namespace PcBuilder.DAL.MSSQL.Entities
{
    public class Component
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public List<ComponentCharact> characts { get; set; } = [];

        public List<Build> builds { get; set; } = [];

        public Guid typeID { get; set; }

        public ComponentType? Type { get; set; }

    }
}
