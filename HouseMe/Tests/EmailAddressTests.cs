using System;
using System.Collections.Generic;
using System.Text;
using Shared.Domain.ValueObjects;
using Xunit;

//Test
namespace Tests
{
    public class EmailAddressTests
    {
        public EmailAddress make_EmailAddress(string email)
        {
            return EmailAddress.From(email);
        }

        //[Theory]
        //[InlineData("email@example.com")]
        //[InlineData("firstname.lastname@example.com")]
        //[InlineData("email@subdomain.example.com")]
        //[InlineData("firstname+lastname@example.com")]
        //[InlineData("email@123.123.123.123")]
        //[InlineData("email@[123.123.123.123]")]
        //[InlineData("“email”@example.com")]
        //[InlineData("1234567890@example.com")]
        //[InlineData("email@example-one.com")]
        //[InlineData("_______@example.com")]
        //[InlineData("email@example.name")]
        //[InlineData("email@example.museum")]
        //[InlineData("email@example.co.jp")]
        //[InlineData("firstname-lastname@example.com")]
        //public void From_ValidEmail_NotNull(string testAddress)
        //{
        //    //Arrange
        //    var emailAddress = testAddress;

        //    //Action
        //    var email = make_EmailAddress(emailAddress);

        //    //Assert
        //    Assert.NotNull(email);
        //}

        [Theory]
        [InlineData("plainaddress")]
        //[InlineData("#@%^%#$@#$@#.com")]
        //[InlineData("@example.com")]
        //[InlineData("Joe Smith <email@example.com>")]
        //[InlineData("email.example.com")]
        //[InlineData("email@example@example.com")]
        //[InlineData(".email@example.com")]
        //[InlineData("email.@example.com")]
        //[InlineData("email..email@example.com")]
        //[InlineData("あいうえお@example.com")]
        //[InlineData("email@example.com (Joe Smith)")]
        //[InlineData("email@example")]
        //[InlineData("email@-example.com")]
        //[InlineData("email@example.web")]
        //[InlineData("email@111.222.333.44444")]
        //[InlineData("email@example..com")]
        //[InlineData("Abc..123@example.com")]
        //[InlineData("“(),:;<>[\\]@example.com")]
        //[InlineData("just\"not\"right@example.com")]
        //[InlineData("this\\ is\"really\"not\\allowed@example.com")]
        public void From_InvalidValidEmail_ThrowException(string testAddress)
        {
            //Arrange
            var emailAddress = testAddress;

            //Action
            var createEmail = new Func<EmailAddress>(() => make_EmailAddress(emailAddress));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createEmail);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void From_EmptyEmailAddress_ThrowsException(string testAddress)
        {
            //Arrange
            var emailAddress = testAddress;

            //Action
            var createEmail = new Func<EmailAddress>(() => make_EmailAddress(emailAddress));

            //Assert
            Assert.Throws<ArgumentNullException>(createEmail);
        }

        //TODO: Remove this test line. Test 4
    }
}
