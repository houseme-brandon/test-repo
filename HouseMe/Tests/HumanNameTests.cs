using System;
using Shared.Domain.ValueObjects;
using Xunit;

namespace Tests
{
    public class HumanNameTests
    {
        private HumanName make_HumanName(string name)
        {
            return HumanName.From(name);
        }

        [Fact]
        public void From_NameTooShort_ExpectExcption()
        {
            //Arrange
            var shortName = "a";

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(shortName));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createHuman);
        }

        [Fact]
        public void From_NameTooLong_ExpectException()
        {
            //Arrange
            var longName = "123456789012345678901";

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(longName));

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(createHuman);
        }

        [Theory]
        [InlineData("aaaa-aaaa")]
        [InlineData("aaaa")]
        [InlineData("012345867890123456789")]
        [InlineData("ji")]
        public void From_ValidNames_NotNull(string testName)
        {
            //Arrange
            var firstName = testName;

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(testName));

            //Asset
            Assert.NotNull(createHuman);
        }

        [Theory]
        [InlineData("@BobbyTom")]
        [InlineData("aaa^")]
        [InlineData("aaaa!")]
        [InlineData("!!!")]
        [InlineData("$aaa")]
        [InlineData("%qwe")]
        [InlineData("1qqqqq")]
        [InlineData("aaaa(")]
        [InlineData("aaaa(")]
        [InlineData("aaaa*")]
        [InlineData("aaaa&")]
        public void From_SpecialCharacter_ThrowsException(string testName)
        {
            //Arrange
            var firstName = testName;

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(testName));

            //Asset
            Assert.Throws<ArgumentOutOfRangeException>(createHuman);
        }

        [Fact]
        public void From_TwoNamesInOne_ThrowsException()
        {
            //Arrange
            var twoNames = "aaa aaa";

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(twoNames));

            //Asset
            Assert.Throws<ArgumentOutOfRangeException>(createHuman);
        }

        [Fact]
        public void From_ContainsWhiteSpace_RemovedTheSpaces()
        {
            //Arrange
            var nameWithSpace = "aaa ";

            //Action
            var createHuman = new Func<HumanName>(() => make_HumanName(nameWithSpace));
            var human = createHuman.Invoke();

            //Asset
            Assert.Equal("aaa", human.Name);
        }
    }
}
