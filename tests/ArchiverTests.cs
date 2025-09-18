using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace ArchiverApp.Tests
{
    [TestFixture]
    public class ArchiverIntegrationTests
    {

        // --------------------------
        // TESTS CompressText (строки)
        // --------------------------

        [TestCase("aaabb", "3a2b")]
        [TestCase("aabccc", "2ab3c")]
        [TestCase("44455", "3\\42\\5")]
        [TestCase("abc", "abc")]
        [TestCase("a", "a")]
        [TestCase("111", "3\\1")]
        [TestCase("22", "2\\2")]
        [TestCase("\\\\", "2\\\\")]
        [TestCase("aaa111bbb", "3a3\\13b")]
        [TestCase("xyz", "xyz")]
        public void Test_CompressText_String(string input, string expectedPrefix)
        {
            string compressed = Archiver.CompressString(input);
            Assert.That(compressed, Is.EqualTo(expectedPrefix));
        }

        // --------------------------
        // TESTS decompressString (строки)
        // --------------------------

        [TestCase("3a2b", "aaabb")]
        [TestCase("2ab3c", "aabccc")]
        [TestCase("3\\42\\5", "44455")]
        [TestCase("a", "a")]
        [TestCase("abc", "abc")]
        [TestCase("3\\1", "111")]
        [TestCase("2\\2", "22")]
        [TestCase("2\\\\", "\\\\")]
        [TestCase("3a3\\13b", "aaa111bbb")]
        [TestCase("xyz", "xyz")]
        public void Test_DecompressString_String(string input, string expected)
        {
            string decompressed = Archiver.DecompressString(input);
            Assert.That(decompressed, Is.EqualTo(expected));
        }
    }

}
