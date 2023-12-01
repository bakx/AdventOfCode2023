StreamReader reader = new StreamReader("input.txt");

string[] items = reader
    .ReadToEnd()
    .Split(Environment.NewLine)
    .ToArray();

reader.Close();


int sum = 0;

for (int i = 0; i < items.Length; i++)
{
    sum += GetSomething(items[i]);
}

Console.WriteLine($"Part 1: sum {sum}");


int GetSomething(string str)
{
    string first = GetDigit(str);
    string last = GetDigit(str, true);

    return int.Parse(first + last);
}

string GetDigit(string str, bool reverse = false)
{
    if (reverse)
    {
        for (int i = str.Length; i > 0; i--)
        {
            if (Char.IsNumber(str[i - 1]))
                return str[i - 1].ToString();
        }

        return "";
    }

    for (int i = 0; i < str.Length; i++)
    {
        if (Char.IsNumber(str[i]))
            return str[i].ToString();
    }
    
    return "";
}

