using System;
using System.Collections.Generic;
using System.Text;
using Shared.Domain.ValueObjects;
using Xunit;


namespace Tests
{
    public class RsaIdNumberTests
    {
        private RsaIdNumber make_RsaIdNumber(string idNumber)
        {
            return RsaIdNumber.From(idNumber);
        }

        [Fact]
        public void From_IdNumberTooShort_ThrowException()
        {
            //Arrange
            var shortId = "1234";

            //Action
            var createId = new Func<RsaIdNumber>(() => make_RsaIdNumber(shortId));
            
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createId);
        }

        [Fact]
        public void From_IdNumberTooLong_ThrowException()
        {
            //Arrange
            var longId = "123456789101214";

            //Action
            var createId = new Func<RsaIdNumber>(() => make_RsaIdNumber(longId));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createId);
        }

        [Fact]
        public void From_IdNumberContainsNonInt_ThrowException()
        {
            //Arrange
            var alphabeticId = "012345678912A";

            //Action
            var createId = new Func<RsaIdNumber>(() => make_RsaIdNumber(alphabeticId));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createId);
        }

        [Fact]
        public void From_IdNumberContainsNotValid_ThrowException()
        {
            //Arrange
            var invalidId = "1012345678912";

            //Action
            var createId = new Func<RsaIdNumber>(() => make_RsaIdNumber(invalidId));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createId);
        }

        [Fact] public void From_IdNumberValid_Equal()
        {
            //Arrange
            var validId = "8501016184086";

            //Action
            var createdId = make_RsaIdNumber(validId);

            //Assert
            Assert.Equal(validId, createdId.ToString());
        }
        [Fact]
        public void From_IdNumberEndSpaces_Equal()
        {
            //Arrange
            var validId = "8501016184086 ";

            //Action
            var createdId = make_RsaIdNumber(validId);

            //Assert
            Assert.Equal("8501016184086", createdId.ToString());
        }

        [Fact]
        public void From_IdNumberContainsSpaces_Equal()
        {
            //Arrange
            var validId = " 85 01 01 618 40 86 ";

            //Action
            var createdId = make_RsaIdNumber(validId);

            //Assert
            Assert.Equal("8501016184086", createdId.ToString());
        }

        [Fact]
        public void From_IdNumberCheckDateOfBirth_Equal()
        {
            //Arrange
            var validId = "8501016184086";

            //Action
            var createdId = make_RsaIdNumber(validId);

            //Assert
            Assert.Equal("85-01-01", createdId.BirthDate.ToString("yy-MM-dd"));
        }

        [Fact]
        public void From_IdNumberCheckAge_Equal()
        {
            //Arrange
            var validId = "8501016184086";

            //Action
            var createdId = make_RsaIdNumber(validId);

            //Assert
            Assert.Equal(2017-1985, createdId.Age);
        }
    }
}
