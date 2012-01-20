namespace RecreateMe.Profiles
{
    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }

        public string FullName
        {
            get { return First + " " + Last; }
        }

        public Name()
        {
        }

        public Name(string first)
        {
            First = first;
        }

        public Name(string first, string last)
        {
            First = first;
            Last = last;
        }
    }
}