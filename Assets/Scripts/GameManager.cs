using System.Collections;
using System.Collections.Generic;
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
    public GameObject currentEnemy;
    
    public bool IsInDiceGame = false;

    private void Start()
    {
        currentEnemy = GameObject.Find("Enemy");
    }

    public void GameStart()
    {
        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(true);
        }
        IsInDiceGame = true;
    }
    public void GameOver()
    {
        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(false);
        }
        IsInDiceGame = false;
        currentEnemy.layer = 6;
        Destroy(currentEnemy.GetComponent<CapsuleCollider2D>());
    }
}
