namespace UserMngt.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public bool Active { get; set; }
        public bool Master { get; set; }
    }
}