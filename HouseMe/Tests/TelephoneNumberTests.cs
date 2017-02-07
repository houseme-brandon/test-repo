using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shared.Domain.ValueObjects;

namespace Tests
{
    public class TelephoneNumberTests
    {
        public TelephoneNumber make_TelephoneNumber(string telephoneNumber)
        {
            return TelephoneNumber.From(telephoneNumber);
        }

        [Fact]
        public void From_NumberTooShort_ThrowsException()
        {
            //Arrange
            var shortNumber = "052972";

            //Action
            var createNumber = new Func<TelephoneNumber>(() => make_TelephoneNumber(shortNumber));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createNumber);
        }

        [Fact]
        public void From_NumberTooLong_ThrowsException()
        {
            //Arrange
            var shortNumber = "1234567891234567";

            //Action
            var createNumber = new Func<TelephoneNumber>(() => make_TelephoneNumber(shortNumber));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createNumber);
        }

        [Theory]
        [InlineData("07225529 72")]
        [InlineData("0722552972")]
        [InlineData("0027722552972")]
        [InlineData("+27722552972")]
        public void From_ValidSANumbers_NotNull(string testNumber)
        {
            //Arrange
            //testNumbers;

            //Action
            var number = make_TelephoneNumber(testNumber);

            //Assert
            Assert.Equal("27722552972", number.ToString());
        }

        [Theory]
        [InlineData("00 44 1992 632222")]
        [InlineData("+441992632222")]
        public void From_ValidUKNumbers_NotNull(string testNumber)
        {
            //Arrange
            //testNumbers;

            //Action
            var number = make_TelephoneNumber(testNumber);

            //Assert
            Assert.Equal("441992632222", number.ToString());
        }
    }
}
