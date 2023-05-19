using AutoFixture;
using Moq;
using WordAnalyzer.Exceptions;
using WordAnalyzer.Factories;
using WordAnalyzer.Interfaces;
using WordAnalyzer.Providers;

namespace WordAnalyzer.Tests.UnitTests
{
    public class WordFrequencyAnalyzerRegexProviderTests
    {
        public class WordFrequencyAnalyzerRegexProviderTestsCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() =>
                {
                    var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
                    wordFrequencyFactory
                        .Setup(x => x.CreateWordFrequency(
                            It.IsAny<string>(),
                            It.IsAny<int>()))
                        .Returns((string word, int frequency) => WordFrequency.Define(word, frequency));

                    return wordFrequencyFactory.Object;
                });
            }
        }

        [Theory]
        [InlineData("foo", 1)]
        [InlineData("!foo", 1)]
        [InlineData("foo!", 1)]
        [InlineData("!foo!", 1)]
        [InlineData("1foo", 1)]
        [InlineData("foo1", 1)]
        [InlineData("1foo1", 1)]
        [InlineData("_foo", 1)]
        [InlineData("foo_", 1)]
        [InlineData("_foo_", 1)]
        [InlineData(" foo", 1)]
        [InlineData("foo ", 1)]
        [InlineData(" foo ", 1)]
        [InlineData("foo bar", 1)]
        [InlineData(" fooFOO ", 1)]
        [InlineData("Foo_foo bar.", 2)]
        [InlineData(" foo_FOO", 2)]
        [InlineData("foo foo bar bar", 2)]
        [InlineData("foo foo foo bar bar", 3)]
        [InlineData("FoO_fOo_FOO bar bar", 3)]

        public void CalculateHighestFrequency_GivenValidInput_ShouldReturnFrequency(string input, int expectedFrequency)
        {
            // Arrange
            var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory.Object);

            // Act
            var result = sut.CalculateHighestFrequency(input);

            // Assert
            Assert.Equal(expectedFrequency, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void CalculateHighestFrequency_GivenEmptyInput_ShouldReturnZero(string input)
        {
            // Arrange
            var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory.Object);

            // Act
            var result = sut.CalculateHighestFrequency(input);

            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void CalculateFrequencyForWord_GivenEmptyInput_ShouldReturnZero(string input)
        {
            // Arrange
            var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory.Object);

            // Act
            var result = sut.CalculateFrequencyForWord(input, "word");

            // Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("!")]
        [InlineData("â")]
        public void CalculateFrequencyForWord_GivenInvalidWords_ShouldThrowInvalidWordException(string word)
        {
            // Arrange
            var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory.Object);

            // Act and Assert
            Assert.Throws<InvalidWordException>(() => sut.CalculateFrequencyForWord("anything", word));
        }

        [Theory]
        [InlineData("foo", "foo", 1)]
        [InlineData("!foo!", "foo", 1)]
        [InlineData(" foo ", "foo", 1)]
        [InlineData("foofoo", "foo", 0)]
        [InlineData("Foo, bar.", "Foo", 1)]
        [InlineData("foo, bar.", "Foo", 1)]
        [InlineData("foo, foo is a string.", "foo", 2)]
        public void CalculateFrequencyForWord_GivenValidInputAndValidWord_ShouldReturnFrequency(string input, string word, int expectedFrequency)
        {
            // Arrange
            var wordFrequencyFactory = new Mock<IWordFrequencyFactory>();
            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory.Object);

            // Act
            var result = sut.CalculateFrequencyForWord(input, word);

            // Assert
            Assert.Equal(expectedFrequency, result);
        }


        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithSingleWordsAndNumberOfWordsTwo_ShouldReturnTwoAlphabeticallyDescendingOrderedWordsWithCorrespondingFrequencies()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo bar";
            var numberOfWords = 2;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency> {
                WordFrequency.Define("foo", 1),
                WordFrequency.Define("bar", 1)
            };

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithMultipleWordsAndNumberOfWordsTwo_ShouldReturnTwoAlphabeticallyDescendingOrderedWordsWithCorrespondingFrequencies()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo foo bar bar";
            var numberOfWords = 2;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency> {
                WordFrequency.Define("foo", 2),
                WordFrequency.Define("bar", 2)
            };

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithSingleWordsAndNumberOfWordsFour_ShouldReturnFourAlphabeticallyDescendingOrderedWordsWithCorrespondingFrequencies()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo foobar bar";
            var numberOfWords = 4;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency> {
                WordFrequency.Define("foobar", 1),
                WordFrequency.Define("foo", 1),
                WordFrequency.Define("bar", 1)
            };

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithSingleWordAndNumberOfWordsTwo_ShouldReturnSingleWordWithCorrespondingFrequencies()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo";
            var numberOfWords = 2;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency> {
                WordFrequency.Define("foo", 1)
            };

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithSingleWordAndNumberOfWordsZero_ShouldReturnEmptyCollection()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo";
            var numberOfWords = 0;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency>();

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public void CalculateMostFrequentNWords_GivenValidInputWithSingleWordAndNegativeNumberOfWords_ShouldReturnEmptyCollection()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new WordFrequencyAnalyzerRegexProviderTestsCustomization());

            var input = "foo";
            var numberOfWords = -1;
            var wordFrequencyFactory = fixture.Create<IWordFrequencyFactory>();
            var expectedResults = new List<IWordFrequency>();

            var sut = new WordFrequencyAnalyzerRegexProvider(wordFrequencyFactory);

            // Act
            var result = sut.CalculateMostFrequentNWords(input, numberOfWords);

            // Assert
            Assert.Equal(expectedResults, result);
        }
    }
}