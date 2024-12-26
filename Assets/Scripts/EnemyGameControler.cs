using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGameControler : MonoBehaviour
{
    private DiceActionApi diceActionApi;
    [SerializeField] private Die[] dices;
    [SerializeField] private DiceToRoll diceToRoll;
    private List<int> rollDices = new List<int>(5);
    private Die[] lastRollDices;
    private int[] lastRollNum;
    private int currentRound = 0;
    private WaitForSeconds wait = new WaitForSeconds(1);
    public void GenerateGame()
    {
        GameGenerate.Instance.Generate(Random.Range(5, 10));
        currentRound = 0;
        for (int i = 0; i < GameGenerate.Instance.rounds.Count; i++)
        {
            print("Round " + i + " : " + GameGenerate.Instance.rounds[i].pattern);
            for (int j = 0; j < GameGenerate.Instance.rounds[i].dicesNum.Length; j++)
            {
                print(GameGenerate.Instance.rounds[i].dicesNum[j]);
            }
            print("------------------\n");
        }
    }
    public void EnemyTurn()
    {
        GameManager.Instance.isPlayerTurn = false;
        diceToRoll.Initialize();
        StartCoroutine(EnemyRoll());
    }
    private IEnumerator EnemyRoll()
    {
        yield return wait;
        diceActionApi.RollDiceRandom();
        yield return wait;
        diceActionApi.DiesToHolder(dices, GameGenerate.Instance.rounds[currentRound].dicesNum);
        yield return wait;

        diceActionApi.RollDiceRandom();
        yield return wait;
        lastRollDices = diceActionApi.DiesToHolder(dices, GameGenerate.Instance.rounds[currentRound].dicesNum).ToArray();
        yield return wait;

        print($"Last Roll{lastRollDices.Length}");
        FindLastRollNum();
        print($"Last Roll Num{lastRollNum.Length}");
        diceActionApi.RollDice(lastRollDices, lastRollNum);
        yield return wait;
        diceActionApi.CategorySelected(GameGenerate.Instance.rounds[currentRound].pattern);
        NextRound();
        yield return null;
        GameManager.Instance.isPlayerTurn = true;
    }
    private void FindLastRollNum()
    {
        List<int> list1 = GameGenerate.Instance.rounds[currentRound].dicesNum.ToList();
        List<int> list2 = new List<int>(5);
        bool[] isMoved = new bool[5];
        List<Die> lastRollDices = new List<Die>();
        for (int i = 0; i < 5; i++)
        {
            list2.Add(dices[i].number);
            lastRollDices.Add(dices[i]);
            isMoved[i] = false;
        }

        for (int i = list1.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < list2.Count; j++)
            {
                if (list1[i] == list2[j] && !isMoved[j])
                {
                    list1.RemoveAt(i);
                    isMoved[j] = true;
                    break;
                }
            }
        }
        for(int i = isMoved.Length-1;i>=0;i--)
        {
            if(isMoved[i])
            {
                lastRollDices.RemoveAt(i);
            }
        }
        this.lastRollNum = list1.ToArray();
    }
    public void NextRound()
    {
        currentRound++;
    }
    private void Start()
    {
        diceActionApi = GetComponent<DiceActionApi>();
    }
}
