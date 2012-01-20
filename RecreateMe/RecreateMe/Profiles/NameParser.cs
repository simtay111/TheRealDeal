using System.Linq;

namespace RecreateMe.Profiles
{
    public static class NameParser
    {
        public static string GetFirstname(string name)
        {
            return name.Split(' ').First();
        }

        public static string GetLastName(string name)
        {
            if (name.Contains(" "))
                return name.Split(' ').Last();
            return string.Empty;
        }

        public static Name CreateName(string name)
        {
            if (name == null) return null;

            var first = GetFirstname(name);
            var last = GetLastName(name);

            return new Name {First = first, Last = last};
        }
    }
}