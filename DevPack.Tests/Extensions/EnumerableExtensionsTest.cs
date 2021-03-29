using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DevPack.Extensions.Tests
{
    public class EnumerableExtensionsTest
    {
        public readonly IReadOnlyCollection<char> alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        [Theory]
        [InlineData(26, new[] { 26 })]
        [InlineData(25, new[] { 25, 1 })]
        [InlineData(13, new[] { 13, 13 })]
        [InlineData(12, new[] { 12, 12, 2 })]
        public void GroupByAmount(int amountToGroup, int[] resultAmountPerGroup)
        {
            //Arrange

            //Act
            var result = alphabet.GroupByAmount(amountToGroup).ToList();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultAmountPerGroup.Count(), result.Count);

            for (int index = 0; index < result.Count; index++)
            {
                Assert.Equal(resultAmountPerGroup[index], result[index].Count());
            }

        }
    }
}
