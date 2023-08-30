using System;

namespace Backend.Model
{
    public class Court
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Court(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
