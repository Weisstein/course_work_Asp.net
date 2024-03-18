﻿namespace PcBuilder.DAL.MSSQL.Entities
{
    public class ComponentType
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Component> components { get; set; } = [];
    }
}
