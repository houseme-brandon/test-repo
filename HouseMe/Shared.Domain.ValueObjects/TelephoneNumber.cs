using System;
using System.Diagnostics;
using PhoneNumbers;

namespace Shared.Domain.ValueObjects
{
    public class TelephoneNumber
    {
        private readonly int MinNationalNumberLength = 9;
        private readonly int MaxNationalNumberLength = 15;
        public readonly int countryCode = 0;
        public readonly ulong nationalNumber = 0L;

        public static TelephoneNumber From(string telephoneNumber)
        {
            return new TelephoneNumber(telephoneNumber);
        }


        private TelephoneNumber(string telephoneNumber)
        {
            PhoneNumber parsedNumber = PhoneNumberUtil.GetInstance().Parse(telephoneNumber, "ZA");

            if(parsedNumber.HasCountryCode)
                this.countryCode = parsedNumber.CountryCode;
            if(parsedNumber.HasNationalNumber)
                this.nationalNumber = parsedNumber.NationalNumber;

            if (this.nationalNumber.ToString().Length < MinNationalNumberLength)
            {
                throw new ArgumentOutOfRangeException(nameof(parsedNumber), $"Phone number {parsedNumber} is shorter than {MinNationalNumberLength}");
            }

            if (this.nationalNumber.ToString().Length > MaxNationalNumberLength)
            {
                throw new ArgumentOutOfRangeException(nameof(parsedNumber), $"Phone number {parsedNumber} is longer than {MinNationalNumberLength}");
            }
        }

        public override string ToString()
        {
            string fullNumber = countryCode.ToString() + nationalNumber.ToString();
            return fullNumber;
        }
    }
}
