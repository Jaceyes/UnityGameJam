using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{
    [SerializeField]private DiceActionApi diceActionApi;
    [SerializeField]private Die[] dies;
    // Start is called before the first frame update
    void Start()
    {
        int[] numbers = {1,2,3,4,5};
        GetComponent<Button>().onClick.AddListener(() => {
            // diceActionApi.RollDice(dies,numbers);
            // diceActionApi.DiesToRoll(dies,numbers);
            // diceActionApi.DiesToHolder(dies,new int[]{6});
            diceActionApi.CategorySelected(2);
        });
    }
}
