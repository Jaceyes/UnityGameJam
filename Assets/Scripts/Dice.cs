using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    private string diceName;
    private int[] diceNumber = new int[6];
    private int UpNumberIndex;
    private bool isFixed;
    public Dice(){
        for(int i = 0; i < 6; i++){
            diceNumber[i] = i+1;
        }
        UpNumberIndex = 0;
        isFixed = false;
    }
    public int GetUpNumber(){
        return diceNumber[UpNumberIndex];
    }
    public bool GetFixed(){
        return isFixed;
    }
    public void SetFixed(bool isFixed){
        this.isFixed = isFixed;
    }
    public int Roll(){
        if(isFixed){
            return diceNumber[UpNumberIndex];
        }
        UpNumberIndex = Random.Range(0, 6);
        return diceNumber[UpNumberIndex];
    }
}
