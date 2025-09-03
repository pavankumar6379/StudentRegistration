using StudentRegistration.Models;

namespace StudentRegistration.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<State> State { get; set; }
    }
}

