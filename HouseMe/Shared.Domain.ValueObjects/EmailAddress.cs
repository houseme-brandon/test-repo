using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.ValueObjects
{
    public class EmailAddress
    {
        public static EmailAddress From(string emailAddress)
        {
            return new EmailAddress(emailAddress);
        }

        private EmailAddress(string emailAddress)
        {
            var cleanEmailAddress = "";
            try
            {
                cleanEmailAddress = emailAddress.Trim();
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(nameof(cleanEmailAddress), "Email address cannot be empty.");
            }

            if (String.IsNullOrWhiteSpace(cleanEmailAddress))
            {
                throw new ArgumentNullException(nameof(cleanEmailAddress), "Email address cannot be empty.");
            }

            var positionOfAt = cleanEmailAddress.IndexOf("@");
            if (positionOfAt < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cleanEmailAddress), "Email address does not contain an '@'");
            }
        }
    }
}
