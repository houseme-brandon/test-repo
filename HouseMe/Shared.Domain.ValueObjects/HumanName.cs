using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Shared.Domain.ValueObjects
{
    public class HumanName
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 20;
        private readonly char[] IllegalCharacters = "0123456789@#$%!^&*()_+=~><?/.,\";:][{}|\\/".ToCharArray();

        private readonly string name;

        public string Name
        {
            get { return this.name; }
        }

        private HumanName(string name)
        {
            name = name.Trim();

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Human names cannot be empty.");
            }

            if (name.Length > MaxNameLength)
            {
                throw new ArgumentOutOfRangeException(nameof(name),
                    $"Given: {name} - Human names cannot be greater than {MaxNameLength}");
            }

            if (name.Length <= MinNameLength)
            {
                throw new ArgumentOutOfRangeException(nameof(name),
                    $"Given: {name} - Human names cannot be less than {MinNameLength}");
            }

            if (name.IndexOfAny(IllegalCharacters) >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(name),
                    $"Given: {name} - Human names cannot contain the following characters {String.Join(",", IllegalCharacters)}");
            }

            if (name.Contains(" "))
            {
                throw new ArgumentOutOfRangeException(nameof(name),
                    $"Given: {name} - Human names cannot contain spaces");
            }

            this.name = name;
        }

        public static HumanName From(string name)
        {
            return new HumanName(name);
        }

        //public static IReadOnlyList<HumanName> FromFullName(string name)
        //{
        //    List<HumanName> listNames;
        //    //split by whitespace
        //    //return listNames;
        //}
    }
}
