namespace Test.Tests.UnitTests
{
    public class WordFrequencyTests
    {
        [Fact]
        public void Define_GivenValidWordAndFrequency_ShouldReturnWordFrequency()
        {
            // Arrange
            var word = "foo";
            var frequency = 1;

            // Act
            var result = WordFrequency.Define(word, frequency);

            // Assert
            Assert.Equal(result.Word, word);
            Assert.Equal(result.Frequency, frequency);
        }

        [Fact]
        public void Define_GivenInvalidWordAndValidFrequency_ShouldThrowArgumentException()
        {
            // Arrange
            var word = "   ";
            var frequency = 1;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => WordFrequency.Define(word, frequency));
        }

        [Fact]
        public void Define_GivenValidWordAndInvalidFrequency_ShouldThrowArgumentException()
        {
            // Arrange
            var word = "foo";
            var frequency = -1;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => WordFrequency.Define(word, frequency));
        }

        [Fact]
        public void Define_GivenNonLowerCasedWordAndFrequency_ShouldReturnWordFrequencyWithLowerCasedWord()
        {
            // Arrange
            var word = "fOoBaR";
            var frequency = 1;
            var expectedWord = "foobar";

            // Act
            var result = WordFrequency.Define(word, frequency);

            // Assert
            Assert.Equal(result.Word, expectedWord);
            Assert.Equal(result.Frequency, frequency);
        }
    }
}
