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

    public void RollDice(Die[] dies,int[] number){
        rollDice.StartRollDice();
        for(int i = 0; i < dies.Length; i++){
            SetDice(dies[i],number[i]);
        }
    }
    public void DiesToHolder(Die[] dies,int[] numbers){
        for(int i = 0; i < dies.Length; i++){
            isMoved[i] = false;
        }
        for(int i = 0; i < numbers.Length; i++){
            for(int j=0;j<dies.Length;j++){
                if(dies[j].number == numbers[i] && !isMoved[j]){
                    DieToHolder(dies[j]);
                    isMoved[j] = true;
                }
            }
        }
    }
    public void DiesToRoll(Die[] dies,int[] numbers){
        for(int i = 0; i < dies.Length; i++){
            isMoved[i] = false;
        }
        for(int i = 0; i < numbers.Length; i++){
            for(int j=0;j<dies.Length;j++){
                if(dies[j].number == numbers[i] && !isMoved[j]){
                    DieToRoll(dies[j]);
                    isMoved[j] = true;
                }
            }
        }
    }

    private void DieToHolder(Die die){
        if(die.DieInHolder() || die.DieInRollHolder())
            return;
        die.MoveDie(diceToHolder.transform.position, 0.3f);
        die.MoveDieIntoHolder();
    }
    private void DieToRoll(Die die){
        if(die.DieInHolder() || die.DieInRollHolder())
            return;
        die.MoveDie(diceToRoll.transform.position, 0.3f);
        die.MoveDieIntoRollHolder();
    }
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
