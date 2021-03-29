using System;
using Xunit;

namespace DevPack.Extensions.Tests
{
    public class StringExtensionsTest
    {
        [Fact]
        public void RemoveSpecialCharacteres_StringWithSpecialCharacteres_MustReturnStringWithoutSpecialCharacteres()
        {
            string text = @"áàâãªÁÀÂÃéèêÉÈÊóòôõºÓÒÔÕúùûÚÙÛçÇíïìîÍÌÎÏ%@&$#*!?<>+=^~\/|()[]{}-',.§¬ abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";
            string expectedResult = "aaaaAAAAeeeEEEooooOOOOuuuUUUcCiiiiIIII abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";

            string result = text.RemoveSpecialCharacteres();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void RemoveAccents_StringWithAccents_MustReturnStringWithoutAccents()
        {
            string text = @"áàâãªÁÀÂÃéèêÉÈÊóòôõºÓÒÔÕúùûÚÙÛçÇíïìîÍÌÎÏ%@&$#*!?<>+=^~\/|()[]{}-',.§¬ abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";
            string expectedResult = @"aaaaªAAAAeeeEEEooooºOOOOuuuUUUcCiiiiIIII%@&$#*!?<>+=^~\/|()[]{}-',.§¬ abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";

            string result = text.RemoveAccents();

            Assert.Equal(expectedResult, result);
        }
    }
}
