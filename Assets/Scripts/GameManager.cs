using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameManager(){}

    private static GameManager instance;
    public static GameManager Instance {
        get {
            //保证对象的唯一性
            if (instance == null){
                instance = FindObjectOfType<GameManager>();
                if(instance == null){
                    GameObject go = new GameObject("GameManager");//创建游戏对象
                    instance = go.AddComponent<GameManager>();//挂载脚本到游戏对象
                }
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    [Header("组件")]
    [SerializeField] private GameObject gameCanvas;
    [HideInInspector] public bool IsInDiceGame = false;

    public void StartDiceGame(){
        IsInDiceGame = true;
        gameCanvas.SetActive(true);
    }

    public void EndDiceGame(){
        IsInDiceGame = false;
        gameCanvas.SetActive(false);
    }
}
