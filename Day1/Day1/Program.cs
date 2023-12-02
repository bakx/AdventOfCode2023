StreamReader reader = new StreamReader("input.txt");

string[] items = reader
    .ReadToEnd()
    .Split(Environment.NewLine)
    .ToArray();

reader.Close();

Func<int, int> Inc = (a) => { a++; return a; };
Func<int, int> Dec = (a) => { a--; return a; };

Dictionary<string, List<string>> digitText = new()
{
    { "1", new() { "1", "one" }},
    { "2", new() { "2", "two" }},
    { "3", new() { "3", "three" }},
    { "4", new() { "4", "four" }},
    { "5", new() { "5", "five" }},
    { "6", new() { "6", "six" }},
    { "7", new() { "7", "seven" }},
    { "8", new() { "8", "eight" }},
    { "9", new() { "9", "nine" }},
};


int sumPart1 = 0;
int sumPart2 = 0;

for (int i = 0; i < items.Length; i++)
{
    //sumPart1 += GetPart(items[i]);
    sumPart2 += GetPart(items[i], false);
}

Console.WriteLine($"Part 1: sum {sumPart1}");
Console.WriteLine($"Part 2: sum {sumPart2}");


int GetPart(string str, bool digitOnly = true)
{
    string first = GetDigitOrText(str, digitOnly);
    string last = GetDigitOrText(str, digitOnly, true);

    if (first.Length == 0 || last.Length == 0)
    {
        Console.WriteLine($"---------");
        Console.WriteLine($"Did not retrieve result, str={str} first={first} last={last}");
        Environment.Exit(-1);
    }

    Console.WriteLine($"{str} = {first} + {last}");

    return int.Parse(first + last);
}

string GetDigitOrText(string str, bool digitOnly = true, bool reverse = false)
{
    int startPosition = reverse ? str.Length : -1;
    int endPosition = reverse ? 0 : str.Length;
    Func<int, int> loopDirection = reverse ? Dec : Inc;

    int i = startPosition;

    while (true)
    {
        i = loopDirection(i);

        if (reverse && i < 0)
            break;

        if (i == endPosition)
            break;
        
        if (!reverse && i > endPosition)
            break;

        if (char.IsNumber(str[i]))
            return str[i].ToString();

        if (digitOnly)
            continue;

        string hasWord = "";
        int lengthLeft = 0;

        if (reverse)
        {
            lengthLeft = i - endPosition + 1;

            if (lengthLeft < 1)
            {
                continue; // No words fit
            }

            int maxGet = Math.Min(lengthLeft, 5);
            int minGet = Math.Max(i - maxGet + 1, 0);
            hasWord = str.Substring(minGet, maxGet);
        }
        else
        {
            lengthLeft = endPosition - i;

            if (lengthLeft < 1)
            {
                continue; // No words fit
            }

            int maxGet = Math.Min(lengthLeft, 5);
            hasWord = str.Substring(i, maxGet);
        }

        string hasword = isTextDigit(hasWord, reverse);
        
        if (hasword.Length > 0)
            return hasword;
    }

    return "";
}

string isTextDigit(string str, bool reverse)
{
    foreach(List<string> s in digitText.Values)
    {
        for (int i = 1; i < s.Count; i++)
        {
            string word = s[i];

            if (str.Contains(word))
            {
                // Check what index this is at
                int wordIndex = str.IndexOf(word);
                int digitIndex = -1;

                // Are there any digits in the substring?
                if (reverse)
                {
                    for (int j = str.Length; j < 0; j++)
                    {
                        if (char.IsNumber(str[j]))
                        {
                            digitIndex = j;
                            break;
                        }
                    }

                    if (digitIndex < 0)
                        return s[0];

                    return wordIndex <= digitIndex ? s[0] : str[digitIndex].ToString();
                }
                else
                {
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (char.IsNumber(str[j]))
                        {
                            digitIndex = j;
                            break;
                        }
                    }

                    if (digitIndex < 0)
                        return s[0];

                    return digitIndex <= wordIndex ? str[digitIndex].ToString() : s[0];
                }
            }
        }
    }

    // No matches for any words, how about just digits

    int firstDigitIndex = -1;
    int lastDigitIndex = -1;
    
    for (int j = 0; j < str.Length; j++)
    {
        if (char.IsNumber(str[j]))
        {
            if (firstDigitIndex < 0)
            {
                firstDigitIndex = j;
                continue;
            }
            
            lastDigitIndex = j;
        }
    }

    if (firstDigitIndex >= 0)
    {
        if (firstDigitIndex >= 0 && lastDigitIndex == -1)
        {
            return str[firstDigitIndex].ToString();
        }

        return reverse ? str[lastDigitIndex].ToString() : str[firstDigitIndex].ToString();
    }

    return "";
}

