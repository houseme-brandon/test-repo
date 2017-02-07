using System;
using Shared.Domain.ValueObjects;

namespace AccessControl.Domain.UserAggregate
{
    public class User
    {
        public Guid Id { get; }
        public RsaIdNumber IdNumber { get; }
        public HumanName FirstName { get; }
        public HumanName LastName { get; }
        public EmailAddress EmailAddress { get; }
        public TelephoneNumber MobileNumber { get; }
        
        private User(Guid id, RsaIdNumber idNumber, HumanName firstName, HumanName lastName, EmailAddress emailAddress, TelephoneNumber mobileNumber)
        {
            Id = id;
            IdNumber = idNumber;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            MobileNumber = mobileNumber;
        }

        public static User From(Guid id, RsaIdNumber idNumber, HumanName firstName, HumanName lastName, EmailAddress emailAddress, TelephoneNumber mobileNumber)
        {
            return new User(id, idNumber, firstName, lastName , emailAddress, mobileNumber);
        } 
    }
}
