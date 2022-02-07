using System;
using Xunit;

namespace DevPack.Tests
{
    public class EmbeddedTest
    {
        [Fact]
        public void ReadString_ValidFile_ExpectedReadTextFromEmbeddedFile()
        {
            //Arrange
            string resourceName = "DevPack.Tests.Embedded.Example.HelloWorld.txt";

            //Act
            string result = Embedded.ReadString(resourceName);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal("﻿Hello World!", result);
        }
    }
}
