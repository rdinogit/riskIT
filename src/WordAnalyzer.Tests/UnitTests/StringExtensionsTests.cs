using WordAnalyzer.Extensions;

namespace WordAnalyzer.Tests.UnitTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("a", '_', "a")]
        [InlineData("a.", '_', "a_")]
        [InlineData("a_", '_', "a_")]
        [InlineData("a1", '_', "a_")]
        [InlineData(".a.", '_', "_a_")]
        [InlineData("a..", '_', "a__")]
        [InlineData("a . a", '_', "a___a")]
        public void ReplaceSpecialCharacters(string s, char replacement, string expected)
        {
            // Act
            var result = s.ReplaceNonAlphabeticalCharacters(replacement);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
