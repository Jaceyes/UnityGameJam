using System;
using System.Collections.Generic;

public class SpeedboatDiceNPC
{
    private List<List<int>> rolls;
    private int finalScore;
    private Random random;

    public SpeedboatDiceNPC()
    {
        rolls = new List<List<int>>();
        finalScore = 0;
        random = new Random();
    }

    private int RollDice()
    {
        return random.Next(1, 7);
    }

    public Dictionary<string, object> PresetTurn(int targetScore)
    {
        rolls.Clear();
        int currentScore = 0;

        while (currentScore < targetScore)
        {
            List<int> turnRolls = new List<int>();
            int rollSum = 0;

            while (rollSum + currentScore < targetScore)
            {
                int roll = (targetScore - currentScore <= 6) ? targetScore - currentScore : RollDice();
                turnRolls.Add(roll);
                rollSum += roll;
            }

            currentScore += rollSum;
            rolls.Add(turnRolls);
            if (currentScore >= targetScore)
                break;
        }

        finalScore = currentScore;
        return new Dictionary<string, object>
        {
            {"rolls", rolls},
            {"final_score", finalScore}
        };
    }

    public static void Main(string[] args)
    {
        SpeedboatDiceNPC npc = new SpeedboatDiceNPC();
        var result = npc.PresetTurn(15);
        Console.WriteLine("Final Score: " + result["final_score"]);
        Console.WriteLine("Rolls:");
        foreach (var turn in (List<List<int>>)result["rolls"])
        {
            Console.WriteLine(string.Join(", ", turn));
        }
    }
}
