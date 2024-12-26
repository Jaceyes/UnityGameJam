using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGenerate
{
    private static GameGenerate instance;
    public static GameGenerate Instance
    {
        get
        {
            if (instance == null)
                instance = new GameGenerate();
            return instance;
        }
    }
    private List<int> patterns;
    public List<Round> rounds;
    public GameGenerate(){}

    public void Generate(int difficulty)
    {
        patterns = new List<int>(13);
        rounds = new List<Round>(13);
        for (int i = 0; i < 13; i++)
            patterns.Add(i);
        for (int i = 0; i < 13; i++)
        {
            rounds.Add(new Round(patterns, difficulty));
        }
    }
}
public class Round{
    /// <summary>
    /// 0: Aces, 1: Twos, 2: Threes, 3: Fours, 4: Fives, 5: Sixes, 6: ThreeOfAKind, 7: FourOfAKind, 8: FullHouse, 9: SmallStraight, 10: LargeStraight, 11: Yahtzee, 12: Chance
    /// </summary>
    public int pattern;
    private int difficulty;
    public int[] dicesNum;
    public Round(List<int> pattern,int difficulty){
        this.difficulty = difficulty;
        int RandomNum = Random.Range(0,pattern.Count);
        this.pattern = pattern[RandomNum];
        pattern.RemoveAt(RandomNum);
        dicesNum = new int[5];
        switch(this.pattern){
            case 0:
                GenerateAces();
                break;
            case 1:
                GenerateTwos();
                break;
            case 2:
                GenerateThrees();
                break;
            case 3:
                GenerateFours();
                break;
            case 4:
                GenerateFives();
                break;
            case 5:
                GenerateSixes();
                break;
            case 6:
                GenerateThreeOfAKind();
                break;
            case 7:
                GenerateFourOfAKind();
                break;
            case 8:
                GenerateFullHouse();
                break;
            case 9:
                GenerateSmallStraight();
                break;
            case 10:
                GenerateLargeStraight();
                break;
            case 11:
                GenerateYahtzee();
                break;
            case 12:
                GenerateChance();
                break;
        }
    }
    private void GenerateAces(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 1;
            else
                dicesNum[i] = Random.Range(2,7);
        }
    }
    private void GenerateTwos(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 2;
            else{
                dicesNum[i] = Random.Range(1,7);
                if(dicesNum[i] == 2)
                    dicesNum[i] = 3;
            }
        }
    }
    private void GenerateThrees(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 3;
            else{
                dicesNum[i] = Random.Range(1,7);
                if(dicesNum[i] == 3)
                    dicesNum[i] = 4;
            }
        }
    }
    private void GenerateFours(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 4;
            else{
                dicesNum[i] = Random.Range(1,7);
                if(dicesNum[i] == 4)
                    dicesNum[i] = 5;
            }
        }
    }
    private void GenerateFives(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 5;
            else{
                dicesNum[i] = Random.Range(1,7);
                if(dicesNum[i] == 5)
                    dicesNum[i] = 6;
            }
        }
    }
    private void GenerateSixes(){
        for(int i = 0; i < 5; i++){
            if(Random.Range(1,11)<=difficulty)
                dicesNum[i] = 6;
            else
                dicesNum[i] = Random.Range(1,6);
        }
    }
    private void GenerateThreeOfAKind(){
        int num = Random.Range((difficulty+1)/2,7);
        for(int i = 0; i<3;i++){
            dicesNum[i] = num;
        }
        dicesNum[3] = Random.Range(1,num);
        dicesNum[4] = Random.Range(math.min(num+1,6),7);
    }
    private void GenerateFourOfAKind(){
        int num = Random.Range((difficulty+1)/2,7);
        for(int i = 0; i<4;i++){
            dicesNum[i] = num;
        }
        if(Random.Range(0,2) == 0)
            dicesNum[4] = Random.Range(1,num);
        else
            dicesNum[4] = Random.Range(math.min(num+1,6),7);
    }
    private void GenerateFullHouse(){
        int num1 = Random.Range((difficulty+1)/2,7);
        int num2 = Random.Range((difficulty+1)/2,7);
        if(num1 == num2){
            if(Random.Range(0,2) == 0)
                num1 = Random.Range(1,num2);
            else
                num1 = Random.Range(math.min(num2+1,6),7);
        }
        for(int i = 0; i<2;i++){
            dicesNum[i] = num1;
        }
        for(int i = 2; i<5;i++){
            dicesNum[i] = num2;
        }
    }
    private void GenerateSmallStraight(){
        int beginNum = 1;
        switch(Random.Range(0,3)){
            case 0:
                beginNum = 1;
                break;
            case 1:
                beginNum = 2;
                break;
            case 2:
                beginNum = 3;
                break;
        }
        for(int i = 0; i<4;i++)
            dicesNum[i] = beginNum + i;
        dicesNum[4] = Random.Range(beginNum,beginNum+4);
    }
    private void GenerateLargeStraight(){
        int beginNum = 1;
        if(Random.Range(0,2) == 0)
            beginNum = 1;
        else
            beginNum = 2;
        for(int i = 0; i<5;i++)
            dicesNum[i] = beginNum + i;
    }
    private void GenerateYahtzee(){
        if(Random.Range(1,10)<=difficulty){
            int num = Random.Range(1,7);
            for(int i = 0; i<5;i++)
                dicesNum[i] = num;
        }
        else{
            for(int i = 0; i<5;i++)
                dicesNum[i] = Random.Range(1,7);
        }
    }
    private void GenerateChance(){
        for(int i = 0; i<5;i++)
            dicesNum[i] = Random.Range((difficulty+1)/2,7);
    }


}
