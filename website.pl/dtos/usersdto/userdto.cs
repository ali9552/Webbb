namespace website.pl.dtos.usersdto
{
    public class userdto
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public string fname { get; set; }
        public string lname { get; set; }
        public IEnumerable<string>? roles { get; set; }

    }
}
