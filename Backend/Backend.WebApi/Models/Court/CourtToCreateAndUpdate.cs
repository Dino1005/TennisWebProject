
namespace Backend.WebApi.Models.Court
{
    public class CourtToCreateAndUpdate
    {
        public string Name { get; set; }
        public CourtToCreateAndUpdate(string name)
        {
            Name = name;
        }
    }
}