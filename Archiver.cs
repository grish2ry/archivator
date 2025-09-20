using System.Text;

namespace ArchiverApp;

public static class Archiver
{
    /// <summary>
    /// Должна сжимать строку методом RLE.
    /// Текущая реализация не удовлетворяет всем тестам
    /// Также требуется использовать метод WriteRun для записи в outputLine и заменить тип string на StringBuilder.
    /// Пример: "aaabb" → "3a2b"
    /// Пример: "aabccc"   → "2ab3c"
    /// </summary>
    public static string CompressString(string inputLine)
    {
        int count = 1;
        char lastChar = inputLine[0];
        var outputLine = new StringBuilder("");
        inputLine += "\0";
        for (int i = 1; i < inputLine.Length; i++)
        {
            if ((int)lastChar <= 57 && (int)lastChar >= 48 && inputLine[i] != lastChar)
            {
                if (count > 1)
                    outputLine.AppendFormat("{0}{1}\\", count, lastChar);
                else
                    outputLine.AppendFormat("{0}\\", lastChar);

                count = 1;
            }
            else
            {

                if (inputLine[i] == lastChar)
                {
                    count++;
                }
                else if (count > 1)
                {
                    outputLine.AppendFormat("{0}{1}", count, lastChar);
                    count = 1;
                }
                else
                {
                    outputLine.Append(lastChar);
                }
            }
            lastChar = inputLine[i];
        }
        return outputLine.ToString();
    }
    /// <summary>
    /// Заглушка: должна разжимать строку, сжатую методом RLE.
    /// Пример: "3a2b" → "aaabb"
    /// Пример: "2ab3c"   → "aabccc"
    /// Важно: при экранирова
    /// </summary>
    public static string DecompressString(string inputLine)
    {
        var outputLine = new StringBuilder("");
        inputLine += "\0";
        char lastChar = inputLine[0];
        int i = 1;
        while (i < inputLine.Length)
        {
            if (((int)lastChar <= 57 && (int)lastChar >= 48))
            {
                var number = new StringBuilder("").Append(lastChar);
                while ((int)inputLine[i] <= 57 && (int)inputLine[i] >= 48)
                {
                    lastChar = inputLine[i];
                    number.Append(inputLine[i]);
                    i++;
                }
                if (! (inputLine[i]=='\\'))
                {
                    WriteRun(outputLine, inputLine[i], Convert.ToInt32(number.ToString()));
                    lastChar = inputLine[i + 1];
                    i += 2;
                }
                else
                {
                    number.Remove(number.Length - 1, 1);
                    int count_of_number = number.Length > 0 ? Convert.ToInt32(number.ToString()) : 1;
                    WriteRun(outputLine, inputLine[i - 1], count_of_number);
                    lastChar = inputLine[i + 1];
                    i += 2;
                }
            }
            else
            {
                outputLine.Append(lastChar);
                lastChar = inputLine[i];
                i++;

            }
        }
        return outputLine.ToString();

    }
    /// <summary>
    /// Заглушка: должна записывать (count, symbol) в выходной буфер.
    /// Пример: count=3, symbol='a' → "3a"
    /// Пример: count=1, symbol='b' → "b"
    /// </summary>
    public static void WriteRun(StringBuilder output, char symbol, int count)
    {
        for(int i = 0; i < count; i++)
            output.Append(symbol);
    }
}








