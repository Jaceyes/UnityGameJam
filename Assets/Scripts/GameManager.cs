using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private ScoreCardBox[] scoreCardBoxes;
    [SerializeField] private GameObject ResultPanel;
    [SerializeField] private TextMeshProUGUI ResultText;
    [SerializeField] private Button resultButton;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField]private GameObject draw;

    private EnemyGameControler enemyGameControler;
    public GameObject currentEnemy;
    private GameObject enemyAnimator;

    public bool IsInDiceGame = false;
    public bool isPlayerTurn = true;

    private void Start()
    {
        currentEnemy = GameObject.Find("Enemy");
        enemyAnimator = currentEnemy.transform.GetChild(0).gameObject;
        enemyGameControler = currentEnemy.GetComponent<EnemyGameControler>();
        resultButton.onClick.AddListener(GameOverOver);
    }

    public void GameStart()
    {
        if (IsInDiceGame)
        {
            return;
        }
        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(true);
        }
        IsInDiceGame = true;
        enemyGameControler.GenerateGame();
    }
    public void GameOver()
    {
        IsInDiceGame = false;
        ResultPanel.SetActive(true);
        if (playerScore.score > enemyScore.score)
        {
            Debug.Log("Player Win");
            playerControler.GetGreatDice();
            lose.SetActive(false);
            win.SetActive(true);
            draw.SetActive(false);
        }
        else if (playerScore.score < enemyScore.score)
        {
            playerControler.LoseGreatDice();
            lose.SetActive(true);
            win.SetActive(false);
            draw.SetActive(false);
        }
        else
        {
            Debug.Log("Draw");
            win.SetActive(false);
            lose.SetActive(false);
            draw.SetActive(true);
        }
        
        currentEnemy.layer = 6;
        Destroy(currentEnemy.transform.GetChild(0).GetComponent<CapsuleCollider2D>());
        Destroy(currentEnemy.transform.GetChild(0).GetComponent<Rigidbody2D>());
        ResultText.text = $"{playerScore.score}\n{enemyScore.score}";
    }
    public void GameOverOver(){
        ClearScore();
        ResultPanel.SetActive(false);
        for (int i = 0; i < gamePanel.Length; i++)
        {
            gamePanel[i].SetActive(false);
        }
    }
    private void ClearScore(){
        for(int i = 0; i < scoreCardBoxes.Length; i++){
            scoreCardBoxes[i].Initialize();
        }
    }
    public void InitEnemy()
    {
        Destroy(enemyAnimator);
        currentEnemy.transform.position = playerControler.transform.position + new Vector3(25, 0, 0);
        enemyAnimator = Instantiate(Resources.Load<GameObject>(System.IO.Path.Combine(GameData.enemyPrex, GameData.enemies[Random.Range(0, GameData.enemies.Length)])), currentEnemy.transform);
    }
}

public class GameData
{
    public static string enemyPrex = "Prefabs/Enemy/";
    public static string[] enemies = new string[] { "AlienFlying", "AlienWalking", "DanceGirl" };
}
