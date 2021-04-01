using System;
using Xunit;

namespace DevPack.Tests.Objects
{
    public class CpfTest
    {
        [Theory]
        [InlineData("395.706.828-25", "395.706.828-25")]
        [InlineData("39570682825", "395.706.828-25")]
        [InlineData("395.706.828-25", "39570682825")]
        public void Equals_ValidCpfs_LeftObject_RightString_ExpectedTrue(string left, string right)
        {
            Cpf leftCpf = new(left);

            Assert.True(leftCpf == right);
            Assert.False(leftCpf != right);
            Assert.True(leftCpf.Equals(right));
        }

        [Theory]
        [InlineData("395.706.828-25", "395.706.828-25")]
        [InlineData("39570682825", "395.706.828-25")]
        [InlineData("395.706.828-25", "39570682825")]
        public void Equals_ValidCpfs_LeftString_RightObject_ExpectedTrue(string left, string right)
        {
            Cpf rightCpf = new(right);

            Assert.True(left == rightCpf);
            Assert.False(left != rightCpf);
        }

        [Theory]
        [InlineData("395.706.828-25", "395.706.828-25")]
        [InlineData("39570682825", "395.706.828-25")]
        [InlineData("395.706.828-25", "39570682825")]
        public void Equals_ValidCpfsObjects_ExpectedTrue(string left, string right)
        {
            Cpf leftCpf = new(left);
            Cpf rightCpf = new(right);

            Assert.True(leftCpf == rightCpf);
            Assert.False(leftCpf != rightCpf);
            Assert.True(leftCpf.Equals(rightCpf));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("           ")]
        public void New_EmptyCpf_ExpectedExpection(string input)
        {
            void action() => new Cpf(input);

            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("395.706.828-29")]
        [InlineData("asjkfghsjkdaf")]
        [InlineData("999.999.999-99")]
        public void New_InvalidCpf_ExpectedExpection(string input)
        {
            void action() => new Cpf(input);

            Assert.Throws<FormatException>(action);
        }

        [Theory]
        [InlineData("395.706.828-25", "39570682825", "395.706.828-25")]
        [InlineData("39570682825", "39570682825", "395.706.828-25")]
        [InlineData("395706828-25", "39570682825", "395.706.828-25")]
        [InlineData("622.467.300-40", "62246730040", "622.467.300-40")]
        public void New_ValidCpf_ExpectedCreateObject(string input, string rawValue, string formatedValue)
        {
            Cpf cpf = new(input);

            Assert.Equal(cpf.ToString(), rawValue);
            Assert.Equal(cpf.ToStringFormated(), formatedValue);
        }
        [Theory]
        [InlineData("395.706.828-29")]
        [InlineData("asjkfghsjkdaf")]
        [InlineData("999.999.999-99")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("           ")]
        public void TryParse_InvalidCpf_ExpectedResult(string value)
        {
            var valid = Cpf.TryParse(value, out Cpf cpf);

            Assert.False(valid);
            Assert.True(cpf == default);
        }

        [Theory]
        [InlineData("395.706.828-25")]
        [InlineData("39570682825")]
        public void TryParse_ValidCpf_ExpectedResult(string value)
        {
            var valid = Cpf.TryParse(value, out Cpf cpf);

            Assert.True(valid);
            Assert.True(cpf != default);
        }
    }
}
