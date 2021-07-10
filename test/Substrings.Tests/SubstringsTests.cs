using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Substrings.Tests
{
    public class Patterns_PatternsShould
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("", 5)]
        [InlineData("abcabc", 0)]
        public void Substrings_EmptyInputs_EmptyOutput(string input, uint length)
        {
            Assert.Empty(Patterns.Substrings(input, length));
        }

        [Theory]
        [InlineData("a", 1, new string[] {"a"})]
        [InlineData("ab", 2, new string[] {"ab"})]
        [InlineData("ab", 1, new string[] {"a", "b"})]
        public void Substrings_GivenInput_ExpectSubstrings(string input, uint length, IEnumerable<string> expectedSubstrings)
        {
            var result = Patterns.Substrings(input, length);

            Assert.Equal(expectedSubstrings.Count(), result.Count());
            foreach(var expected in expectedSubstrings)
            {
                Assert.Contains(expected, result);
            }
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("", 5)]
        [InlineData("abcabc", 0)]
        public void CountPatterns_GivenEmptyInput_ExpectEmptyOutput(string input, uint length)
        {
            Assert.Empty(Patterns.CountPatterns(input, length));
        }

        [Theory]
        [InlineData("abcabc", 3, "abc", 2)]
        [InlineData("abc_abc", 3, "abc", 2)]
        [InlineData("_abcabc", 3, "abc", 2)]
        [InlineData("abcabc_", 3, "abc", 2)]
        [InlineData("a_bcabcabc", 3, "abc", 2)]
        [InlineData("abcabcab_c", 3, "abc", 2)]
        public void CountPatterns_GivenInput_ExpectedSingleOutput(string input, uint length, string checkPattern, uint outputCount)
        {
            IEnumerable<(string pattern, uint count)> result = Patterns.CountPatterns(input, length);
            Assert.Equal(result.Single(r => r.pattern == checkPattern), (checkPattern, outputCount));
        }
    }
}
