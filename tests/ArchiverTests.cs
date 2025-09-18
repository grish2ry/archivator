using NUnit.Framework;

namespace ArchiverApp.Tests
{
    [TestFixture]
    public class ArchiverStringTests
    {
        [TestCase("aaabb")]
        [TestCase("aabccc")]
        [TestCase("44455")]
        [TestCase("abc")]
        [TestCase("a")]
        [TestCase("111")]
        [TestCase("22")]
        [TestCase("\\\\")]
        [TestCase("aaa111bbb")]
        [TestCase("xyz")]
        [TestCase("a1a1a1")]
        [TestCase("111aaa222bbb")]
        [TestCase("abc123abc123")]
        [TestCase("!!!!????!!!!")]
        [TestCase("aAaAaA")]
        [TestCase("ababababab")]
        [TestCase("1234567890")]
        [TestCase("zzzzzzzzzzzzzzzz")]
        [TestCase("hello_world_2024")]
        [TestCase("RLE_test_case_###")]
        public void Test_CompressString_IsShorterOrEqual(string input)
        {
            string compressed = Archiver.CompressString(input);

            // Проверяем, что длина сжатой строки не больше исходной
            Assert.That(compressed.Length, Is.LessThanOrEqualTo(input.Length));
        }

        [TestCase("aaabb")]
        [TestCase("aabccc")]
        [TestCase("44455")]
        [TestCase("abc")]
        [TestCase("a")]
        [TestCase("111")]
        [TestCase("22")]
        [TestCase("\\\\")]
        [TestCase("aaa111bbb")]
        [TestCase("xyz")]
        [TestCase("a1a1a1")]
        [TestCase("111aaa222bbb")]
        [TestCase("abc123abc123")]
        [TestCase("!!!!????!!!!")]
        [TestCase("aAaAaA")]
        [TestCase("ababababab")]
        [TestCase("1234567890")]
        [TestCase("zzzzzzzzzzzzzzzz")]
        [TestCase("hello_world_2024")]
        [TestCase("RLE_test_case_###")]
        public void Test_CompressThenDecompress_EqualsOriginal(string input)
        {
            string compressed = Archiver.CompressString(input);
            string decompressed = Archiver.DecompressString(compressed);

            // Проверяем, что после декомпрессии строка совпадает с оригиналом
            Assert.That(decompressed, Is.EqualTo(input));
        }
    }
}
