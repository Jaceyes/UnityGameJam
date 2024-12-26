using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{
    [SerializeField]private DiceActionApi diceActionApi;
    [SerializeField]private Die[] dies;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        int[] numbers = {1,2,3,4,5};
        GetComponent<Button>().onClick.AddListener(() => {
            GameGenerate gameGenerate = new GameGenerate();
            for(int i = 0; i < gameGenerate.rounds.Count; i++){
                print("Round " + i + " : " + gameGenerate.rounds[i].pattern);
                for(int j = 0; j < gameGenerate.rounds[i].dicesNum.Length; j++){
                    print(gameGenerate.rounds[i].dicesNum[j]);
                }
                print("------------------\n");
            }
        });
    }
}
