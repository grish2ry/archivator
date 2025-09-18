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
        char? lastC = null;
        int curCnt = 0;
        string outputLine = "";
        foreach (char c in inputLine)
        {
            if (lastC == null)
            {
                lastC = c;
                curCnt = 1;
                continue;
            }
            if (c != lastC)
            {
                if (Char.IsDigit((char)lastC) && lastC != '\\')
                {
                    outputLine += String.Format("{0}\\{1}", curCnt, lastC);
                }
                else
                {
                    outputLine += String.Format("{0}{1}", curCnt, lastC);
                }
                curCnt = 1;
                lastC = c;
                continue;
            }
            curCnt += 1;
        }

        if (Char.IsDigit((char)lastC) && lastC != '\\')
        {
            outputLine += String.Format("{0}\\{1}", curCnt, lastC);
        }
        else
        {
            outputLine += String.Format("{0}{1}", curCnt, lastC);
        }

        return outputLine;
    }

    /// <summary>
    /// Заглушка: должна разжимать строку, сжатую методом RLE.
    /// Пример: "3a2b" → "aaabb"
    /// Пример: "2ab3c"   → "aabccc"
    /// Важно: при экранирова
    /// </summary>
    public static string DecompressString(string compressed)
    {
        throw new NotImplementedException("implement me");
    }

    /// <summary>
    /// Заглушка: должна записывать (count, symbol) в выходной буфер.
    /// Пример: count=3, symbol='a' → "3a"
    /// Пример: count=1, symbol='b' → "b"
    /// </summary>
    public static void WriteRun(StringBuilder output, char symbol, int count)
    {
        throw new NotImplementedException("implement me");
    }
    
}
