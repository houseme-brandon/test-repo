using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared.Domain.ValueObjects
{
   
    public class RsaIdNumber
    {
        private const int IdNumberLength = 13;

        public string IdentityNumber { get; }

        public DateTime BirthDate { get; }

        public String Gender { get;  }

        public int Age { get; }

        public string AgeToLongString { get; }

        public bool IsSouthAfrican { get; }

        public static RsaIdNumber From(string idNumber)
        {
            return new RsaIdNumber(idNumber);
        }

        private RsaIdNumber(string identityNumber)
        {
            if (IsValid(identityNumber) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(identityNumber), $"Not a valid identity number - {identityNumber}");
            }

            this.IdentityNumber = (identityNumber ?? String.Empty).Replace(" ", "");

            this.BirthDate = DateTime.ParseExact(this.IdentityNumber.Substring(0, 6), "yyMMdd", null);

            var genderIndicator = 5;
            this.Gender = IdentityNumber.ElementAt(6) < genderIndicator ? "Female" : "Male";

            this.IsSouthAfrican = IdentityNumber.ElementAt(10) == 0;
            this.Age = CalculateAge(BirthDate);
            this.AgeToLongString = CalculateAgeToLongString(BirthDate);
        }

        public bool IsValid(string identityNumber)
        {
            var cleanIdNumber = (identityNumber ?? String.Empty).Replace(" ", "");

            if (cleanIdNumber.Length != IdNumberLength)
            {
                throw new ArgumentOutOfRangeException(nameof(identityNumber), $"Given: {cleanIdNumber} - Identity number given isn't 13 digits.");
            }

            var containsNonDigit = !cleanIdNumber.ToList().All(Char.IsDigit);
            if (containsNonDigit)
            {
                throw new ArgumentOutOfRangeException(nameof(identityNumber), $"Given: {cleanIdNumber} - Non-digit character given");
            }

            var idDigits = cleanIdNumber.Select(x => Int32.Parse(x.ToString())).ToList();

            var oddDigitsExceptLast = new Func<int, int, bool>((value, index) => index % 2 == 0 && index < 12);
            var evenDigitsExceptLast = new Func<int, int, bool>((value, index) => (index + 1) % 2 == 0 && index < 12);

            var firstControlPartial = idDigits.Where(oddDigitsExceptLast).Sum();

            var combinedNumber = idDigits.Where(evenDigitsExceptLast).Select(x => x.ToString()).Aggregate((i, j) => i + j);
            var doubledControl = (Int32.Parse(combinedNumber) * 2).ToString();

            var secondControlPartial = doubledControl.Select(x => Int32.Parse(x.ToString())).Sum();

            var control = (10 - ((firstControlPartial + secondControlPartial) % 10)) % 10;

            return idDigits.Last() == control;
        }

        public int CalculateAge(DateTime birthDate)
        {
            var now = DateTime.Now;
            var age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }

        private string CalculateAgeToLongString(DateTime birthDay)
        {
            var difference = DateTime.Now.Subtract(birthDay);
            var currentAge = DateTime.MinValue + difference;
            var years = currentAge.Year - 1;
            var months = currentAge.Month - 1;
            var days = currentAge.Day - 1;

            return String.Format("{0} years, {1} months and {2} days.", years, months, days);
        }

        public override string ToString()
        {
            return IdentityNumber;
        }
    }
}
