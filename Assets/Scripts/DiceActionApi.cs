using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceActionApi: MonoBehaviour
{
    [SerializeField]private RollDice rollDice;
    [SerializeField]private DiceToHold diceToHolder;
    [SerializeField]private DiceToRoll diceToRoll;
    [SerializeField]private ScoringBoxAces scoringBoxAces;
    [SerializeField]private ScoringBoxTwos scoringBoxTwos;
    [SerializeField]private ScoringBoxThrees scoringBoxThrees;
    [SerializeField]private ScoringBoxFours scoringBoxFours;
    [SerializeField]private ScoringBoxFives scoringBoxFives;
    [SerializeField]private ScoringBoxSixes scoringBoxSixes;
    [SerializeField]private ScoringBoxThreeOfAKind scoringBoxThreeOfAKind;
    [SerializeField]private ScoringBoxFourOfAKind scoringBoxFourOfAKind;
    [SerializeField]private ScoringBoxFullHouse scoringBoxFullHouse;
    [SerializeField]private ScoringBoxSmallStraight scoringBoxSmallStraight;
    [SerializeField]private ScoringBoxLargeStraight scoringBoxLargeStraight;
    [SerializeField]private ScoringBoxYahtzee scoringBoxYahtzee;
    [SerializeField]private ScoringBoxChance scoringBoxChance;
    [SerializeField]private ScoringBoxBonus scoringBoxBonus;
    [SerializeField]private ScoringBoxTopTotal scoringBoxTopTotal;
    [SerializeField]private ScoringBoxBottomTotal scoringBoxBottomTotal;
    [SerializeField]private ScoringBoxGrandTotal scoringBoxGrandTotal;

    bool[] isMoved = new bool[5];
    public void SetDice(Die die, int value){
        die.SetDie(value);
    }
    /// <summary>
    /// 投骰子,并设置骰子的值
    /// </summary>
    /// <param name="dies"></param>
    /// <param name="number"></param>
    public void RollDice(Die[] dies,int[] number){
        rollDice.StartRollDice();
        for(int i = 0; i < dies.Length; i++){
            if(dies[i].DieInHolder())
                continue;
            SetDice(dies[i],number[i]);
        }
    }
    public void RollDiceRandom(){
        rollDice.StartRollDice();
    }
    /// <summary>
    /// 移动骰子到holder区域
    /// </summary>
    /// <param name="dies"></param>
    /// <param name="numbers"></param>
    public List<Die> DiesToHolder(Die[] dies,int[] numbers){
        for(int i = 0; i < 5; i++){
            isMoved[i] = false;
        }
        List<Die> dieList = new List<Die>();
        for(int i = 0; i < dies.Length; i++){
            bool isMoveToHolder = false;
            for(int j=0;j<numbers.Length;j++){
                if(dies[i].number == numbers[j] && !isMoved[j]){
                    DieToHolder(dies[i]);
                    isMoved[j] = true;
                    isMoveToHolder = true;
                    break;
                }
            }
            if(!isMoveToHolder){
                DieToRoll(dies[i]);
                dieList.Add(dies[i]);
            }
        }
        return dieList;
    }

    private void DieToHolder(Die die){
        if(die.DieInHolder() || die.DieInRollHolder())
            return;
        die.MoveDieIntoHolder();
        die.MoveDie(diceToHolder.transform.position, 0.3f);
    }
    private void DieToRoll(Die die){
        if(die.DieInHolder() || die.DieInRollHolder())
            return;
        die.MoveDieIntoRollHolder();
        die.MoveDie(diceToRoll.transform.position, 0.3f);
    }
    /// <summary>
    /// 设置分数
    /// </summary>
    /// <param name="index"></param>
    public void CategorySelected(int index){
        switch(index){
            case 0:
                scoringBoxAces.CategorySelected();
                break;
            case 1:
                scoringBoxTwos.CategorySelected();
                break;
            case 2:
                scoringBoxThrees.CategorySelected();
                break;
            case 3:
                scoringBoxFours.CategorySelected();
                break;
            case 4:
                scoringBoxFives.CategorySelected();
                break;
            case 5:
                scoringBoxSixes.CategorySelected();
                break;
            case 6:
                scoringBoxThreeOfAKind.CategorySelected();
                break;
            case 7:
                scoringBoxFourOfAKind.CategorySelected();
                break;
            case 8:
                scoringBoxFullHouse.CategorySelected();
                break;
            case 9:
                scoringBoxSmallStraight.CategorySelected();
                break;
            case 10:
                scoringBoxLargeStraight.CategorySelected();
                break;
            case 11:
                scoringBoxYahtzee.CategorySelected();
                break;
            case 12:
                scoringBoxChance.CategorySelected();
                break;
        }
    }
}
