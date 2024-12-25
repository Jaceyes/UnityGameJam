using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerate
{
    private List<int> patterns;
    public GameGenerate()
    {
        Generate();
    }

    public void Generate()
    {
        patterns = new List<int>(13);
        for (int i = 0; i < 13; i++)
            patterns.Add(i);
        for (int i = 0; i < 13; i++)
        {
            
        }
    }
}
class Round{
    private int pattern;
    // {1,2,3}{5,1}
    // {1,2,3,4}{6}
    // {1,2,3,4,5}{}
    Round(List<int> pattern){
        this.pattern = pattern[Random.Range(0,pattern.Count)];
    }


}
