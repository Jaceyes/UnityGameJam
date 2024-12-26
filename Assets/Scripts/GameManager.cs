using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject[] gamePanel;
    [SerializeField] private ScoringBoxGrandTotal playerScore;
    [SerializeField] private ScoringBoxGrandTotal enemyScore;
    [SerializeField] private PlayerControler playerControler;

    private EnemyGameControler enemyGameControler;
    public GameObject currentEnemy;
    
    public bool IsInDiceGame = false;

    private void Start()
    {
        currentEnemy = GameObject.Find("Enemy");
        enemyGameControler = currentEnemy.GetComponent<EnemyGameControler>();
    }

    public void GameStart()
    {
        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(true);
        }
        IsInDiceGame = true;
        enemyGameControler.GenerateGame();
    }
    public void GameOver()
    {
        if(playerScore.score > enemyScore.score)
        {
            Debug.Log("Player Win");
            playerControler.GetGreatDice();
        }
        else if(playerScore.score < enemyScore.score)
        {
            Debug.Log("Enemy Win");
            playerControler.LoseGreatDice();
        }
        else
        {
            Debug.Log("Draw");
        }

        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(false);
        }
        IsInDiceGame = false;
        currentEnemy.layer = 6;
        Destroy(currentEnemy.GetComponent<CapsuleCollider2D>());
    }
}
