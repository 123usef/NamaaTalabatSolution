namespace NamaaTalabat.Core.Entities.Identity
{
    public class Address
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string street { get; set; }
        public string Zone { get; set; }
        public string  District { get; set; }

        public int ApplcaitionUserId { get; set; }
        //public ApplicationUser ApplcaitionUser { get; set; }
    }
}