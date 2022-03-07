namespace TesteTecnicoDigiStart.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedIn { get; set; }
        public string LastAccess { get; set; }
        public UserRank UserRank { get; set; }
    }
}
