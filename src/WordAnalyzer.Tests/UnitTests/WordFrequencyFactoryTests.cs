using WordAnalyzer.Factories;

namespace WordAnalyzer.Tests.UnitTests
{
    public class WordFrequencyFactoryTests
    {
        [Fact]
        public void CreateWordFrequency_GivenWordAndFrequency_ShouldReturnWordFrequency()
        {
            // Arrange
            var word = "foo";
            var frequency = 1;
            var expectedResult = WordFrequency.Define(word, frequency);
            var sut = new WordFrequencyFactory();

            // Act
            var result = sut.CreateWordFrequency(word, frequency);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
