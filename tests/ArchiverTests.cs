using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace ArchiverApp.Tests
{
    [TestFixture]
    public class ArchiverIntegrationTests
    {
        private string apple = "resources/apple.png";
        private string nature = "resources/nature.png";
        private string car = "resources/car.png";

        private string appleCompressed => apple + ".rle";
        private string natureCompressed => nature + ".rle";
        private string carCompressed => car + ".rle";

        private string appleDecompressed => "resources/apple_out.png";
        private string natureDecompressed => "resources/nature_out.png";
        private string carDecompressed => "resources/car_out.png";

        [TearDown]
        public void Cleanup()
        {
            // удаляем все промежуточные файлы
            foreach (var f in new[] { appleCompressed, natureCompressed, carCompressed,
                                      appleDecompressed, natureDecompressed, carDecompressed })
            {
                if (File.Exists(f))
                    File.Delete(f);
            }
        }


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
            Assert.That(compressed, Does.Match(expectedPrefix));
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
            string decompressed = Archiver.CompressString(input);
            Assert.That(decompressed, Does.Match(expected));
        }


        // --------------------------
        // TESTS compressFile
        // --------------------------

        [TestCase("resources/apple.jpeg")]
        [TestCase("resources/nature.jpeg")]
        [TestCase("resources/car.jpeg")]
        public void Test_CompressFile_FileBecomesSmaller(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                Assert.Ignore($"Файл {inputPath} отсутствует в resources.");
            }

            string outputPath = inputPath + ".rle";

            Archiver.CompressFile(inputPath, outputPath);

            Assert.That(File.Exists(outputPath), "Сжатый файл не создан");

            long originalSize = new FileInfo(inputPath).Length;
            long compressedSize = new FileInfo(outputPath).Length;

          Assert.That(compressedSize, Is.LessThan(originalSize),
    $"Сжатый файл ({compressedSize}) не меньше исходного ({originalSize})");

        }

        // --------------------------
        // TESTS decompressFile
        // --------------------------

        [TestCase("resources/apple.jpeg")]
        [TestCase("resources/nature.jpeg")]
        [TestCase("resources/car.jpeg")]
        public void Test_DecompressFile_MatchesOriginal(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                Assert.Ignore($"Файл {inputPath} отсутствует в resources.");
            }

            string compressedPath = inputPath + ".rle";
            string decompressedPath = Path.Combine(
                Path.GetDirectoryName(inputPath),
                Path.GetFileNameWithoutExtension(inputPath) + "_out" + Path.GetExtension(inputPath)
            );

            // шаг 1: сжать
            Archiver.CompressFile(inputPath, compressedPath);

            // шаг 2: разжать
            Archiver.DecompressFile(compressedPath, decompressedPath);

            Assert.That(File.Exists(decompressedPath), "Разжатый файл не создан");

            byte[] originalBytes = File.ReadAllBytes(inputPath);
            byte[] decompressedBytes = File.ReadAllBytes(decompressedPath);

            Assert.Equals(originalBytes, decompressedBytes);
        }
    }

   
}
