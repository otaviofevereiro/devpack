using System;
using Xunit;

namespace DevPack.Tests.Extensions
{
    public class StringExtensionsTest
    {
        const string _allSpecialChars = @"áàâãªÁÀÂÃéèêÉÈÊóòôõºÓÒÔÕúùûÚÙÛçÇíïìîÍÌÎÏ%@&$#*!?<>+=^~\/|()[]{}-',.§¬ abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";
        const string _withoutAccents = @"aaaaªAAAAeeeEEEooooºOOOOuuuUUUcCiiiiIIII%@&$#*!?<>+=^~\/|()[]{}-',.§¬ abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";
        const string _withoutSpecial = "aaaaAAAAeeeEEEooooOOOOuuuUUUcCiiiiIIII abcdefghijklmnopqrstuvxzywABCDEFGHIJKLMNOPQRSTUVXZYW0123456789";
        const string _onlyNumber = "0123456789";

        [Fact]
        public void RemoveSpecialCharacteres_StringWithSpecialCharacteres_MustReturnStringWithoutSpecialCharacteres()
        {
            string result = _allSpecialChars.RemoveSpecialCharacteres();

            Assert.Equal(_withoutSpecial, result);
        }

        [Fact]
        public void RemoveAccents_StringWithAccents_MustReturnStringWithoutAccents()
        {
            string result = _allSpecialChars.RemoveAccents();

            Assert.Equal(_withoutAccents, result);
        }


        [Fact]
        public void ToNumeric_StringWithAccents_MustReturnOnlyNumbers()
        {
            string result = _allSpecialChars.ToNumeric();

            Assert.Equal(_onlyNumber, result);
        }
    }
}
