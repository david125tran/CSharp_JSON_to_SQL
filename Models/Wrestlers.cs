namespace SQLSeed.Models
{
    // Make the class a partial class so that the class can take functionality from other files.
    public partial class Wrestlers
    {
        public int WrestlerId {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public int WeightClass {get; set;}
    }
}
