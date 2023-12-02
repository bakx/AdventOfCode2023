using System.ComponentModel;

StreamReader reader = new StreamReader("input.txt");

string[] items = reader
    .ReadToEnd()
    .Split(Environment.NewLine)
    .ToArray();

reader.Close();


string[] part1SplitList = { "blue", "red", "green" };

int sumPart1 = 0;
int sumPart2 = 0;

for (int i = 0; i < items.Length; i++)
{
    Game game = new Game(items[i]);

    //bool result = Part1(game, 12, 13, 14);
    //if (result)
    //{
    //    sumPart1 += game.ID;
    //}
    
    sumPart2 += Part2(game, 12, 13, 14);

    Console.WriteLine($"Game {game.ID} sum {sumPart2}");
}

Console.WriteLine($"Part 1: sum {sumPart1}");
Console.WriteLine($"Part 2: sum {sumPart2}");


bool Part1(Game game, int maxR, int maxG, int maxB)
{
    game.ID = int.Parse(game.Line.Split(": ")[0].Trim().Split(' ')[1]); // Splits twice, could re-use.. But why bother ..
    string gameOutcome = game.Line.Split(": ")[1].Trim();

    // Sum up all the games
    string[] outcome = gameOutcome.Split(';');

    foreach (string s in outcome)
    {
        string[] gameResult = s.Split(',');

        if (gameResult.Length == 0)
        {
            // Is this going to be a scenario?
            System.Diagnostics.Debugger.Break();
        }

        foreach (string s1 in gameResult)
        {
            foreach (string s2 in part1SplitList)
            {
                if (s1.Contains(s2))
                {
                    if (s2 == "red")
                    {
                        if (int.Parse(s1.Split(s2)[0]) > maxR)
                        {
                            return false;
                        }
                    }

                    if (s2 == "green")
                    {
                        if (int.Parse(s1.Split(s2)[0]) > maxG)
                        {
                            return false;
                        }
                    }

                    if (s2 == "blue")
                    {
                        if (int.Parse(s1.Split(s2)[0]) > maxB)
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }

    return true;
}



int Part2(Game game, int maxR, int maxG, int maxB)
{
    game.ID = int.Parse(game.Line.Split(": ")[0].Trim().Split(' ')[1]); // Splits twice, could re-use.. But why bother ..
    string gameOutcome = game.Line.Split(": ")[1].Trim();

    // Sum up all the games
    string[] outcome = gameOutcome.Split(';');

    int maxRed = -1;
    int maxGreen = -1;
    int maxBlue = -1;

    foreach (string s in outcome)
    {
        string[] gameResult = s.Split(',');

        foreach (string s1 in gameResult)
        {
            foreach (string s2 in part1SplitList)
            {
                if (s1.Contains(s2))
                {
                    int value = int.Parse(s1.Split(s2)[0]);

                    if (s2 == "red")
                    {
                        game.RedCount = Math.Max(value, game.RedCount);
                    }

                    if (s2 == "green")
                    {
                        game.GreenCount = Math.Max(value, game.GreenCount);
                    }

                    if (s2 == "blue")
                    {
                        game.BlueCount = Math.Max(value, game.BlueCount);
                    }
                }
            }
        }
    }

    return game.RedCount * game.GreenCount * game.BlueCount;
}

//   Game 1:
//   4 red, 8 green;
//   8 green, 6 red;
//   13 red, 8 green;
//   2 blue, 4 red, 4 green



class Game
{
    public Game(string line)
    {
        Line = line;
    }

    public string Line { get; set; }
    public int ID { get; set; }
    public int BlueCount { get; set; }
    public int RedCount { get; set; }
    public int GreenCount { get; set; }
}