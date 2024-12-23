using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameManager() { }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            //保证对象的唯一性
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
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
    [SerializeField] private Button rollBtn;
    [SerializeField] private Transform gameBoard;
    [SerializeField] private Transform fixedBoard;
    [SerializeField] private TextMeshProUGUI[] scoreText;
    [SerializeField] private TextMeshProUGUI remainCountText;
    [HideInInspector] public bool IsInDiceGame = false;

    #region 分数
    private int acescore = 0;
    private int twoscore = 0;
    private int threescore = 0;
    private int fourscore = 0;
    private int fivescore = 0;
    private int sixscore = 0;
    private int totalScore = 0;
    private int bonusScore = 0;
    private int total = 0;
    private int threeOfKindScore = 0;
    private int fourOfKindScore = 0;
    private int fullHouseScore = 0;
    private int smallStraightScore = 0;
    private int largeStraightScore = 0;
    private int yahtzeeScore = 0;
    private int chanceScore = 0;
    private int grandTotalScore = 0;

    #endregion

    #region DiceGame

    private int ramainChance = 3;

    private bool isPlayerTurn = true;

    private bool canFixScore = false;

    private bool[] socreFixed = new bool[17];

    private Dice[] dices = new Dice[5];
    private GameObject[] diceGameobjects = new GameObject[5];

    #endregion

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int index = i;
            dices[i] = new Dice();
            diceGameobjects[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Dice"), gameBoard.transform);
            diceGameobjects[i].GetComponentInChildren<TextMeshProUGUI>().text = dices[i].GetUpNumber().ToString();
            diceGameobjects[i].GetComponent<Button>().onClick.AddListener(() => { SetFixed(index); });
        }
        for(int i=0;i<scoreText.Length;i++)
        {
            int index = i;
            scoreText[i].GetComponent<Button>()?.onClick.AddListener(() => FixScore(index));
        }
        rollBtn.onClick.AddListener(RollDice);
    }

    public void SetDiceGame(bool isStart)
    {
        IsInDiceGame = isStart;
        gameCanvas.SetActive(isStart);
        for (int i = 0; i < 5; i++)
        {
            dices[i].SetFixed(false);
            diceGameobjects[i].transform.position = gameBoard.GetChild(i).position;
        }
    }

    public void StartGame()
    {
        SetDiceGame(true);
    }


    private void RollDice()
    {
        if (!isPlayerTurn || ramainChance <= 0)
            return;
        canFixScore = true;
        ramainChance--;
        UpdateChanceText();
        for (int i = 0; i < dices.Length; i++)
        {
            if (dices[i].GetFixed())
                continue;
            int diceNumber = dices[i].Roll();
            diceGameobjects[i].GetComponentInChildren<TextMeshProUGUI>().text = diceNumber.ToString();
        }
        UpdateScoreText();
    }
    private void UpdateChanceText()
    {
        remainCountText.text = ramainChance.ToString();
    }

    private void SetFixed(int index)
    {
        dices[index].SetFixed(!dices[index].GetFixed());
        if (dices[index].GetFixed())
            diceGameobjects[index].transform.SetParent(fixedBoard);
        else
            diceGameobjects[index].transform.SetParent(gameBoard);
    }

    private void NextTurn(){
        for(int i=0;i<5;i++){
            dices[i].SetFixed(false);
            diceGameobjects[i].transform.SetParent(gameBoard);
        }
        ramainChance = 3;
        UpdateChanceText();
    }

    #region Score
    private void FixScore(int index){
        if(socreFixed[index] || !canFixScore){
            return;
        }
        UpdateScoreText();
        socreFixed[index] = true;
        scoreText[index].color = Color.black;
        canFixScore = false;
        NextTurn();
    }
    private void ComputeScore()
    {
        acescore = socreFixed[0]?acescore:GetScoreUpperSection(1);
        twoscore = socreFixed[1]?twoscore:GetScoreUpperSection(2);
        threescore = socreFixed[2]?threescore:GetScoreUpperSection(3);
        fourscore = socreFixed[3]?fourscore:GetScoreUpperSection(4);
        fivescore = socreFixed[4]?fivescore:GetScoreUpperSection(5);
        sixscore = socreFixed[5]?sixscore:GetScoreUpperSection(6);
        totalScore =acescore + twoscore + threescore + fourscore + fivescore + sixscore;
        bonusScore = totalScore >= 63 ? 35 : 0;
        total = totalScore + bonusScore;
        threeOfKindScore = socreFixed[9]?threeOfKindScore:GetScoreNofKind(3);
        fourOfKindScore =socreFixed[10]?fourOfKindScore: GetScoreNofKind(4);
        fullHouseScore = socreFixed[11]?fullHouseScore:GetScoreFullHouse();
        smallStraightScore = socreFixed[12]?smallStraightScore:GetScoreSmallStraight();
        largeStraightScore = socreFixed[13]?largeStraightScore:GetScoreLargeStraight();
        yahtzeeScore = socreFixed[14]?yahtzeeScore:GetScoreYahtzee();
        chanceScore = socreFixed[15]?chanceScore:GetScoreChance();
        grandTotalScore = total + threeOfKindScore + fourOfKindScore + fullHouseScore + smallStraightScore + largeStraightScore + yahtzeeScore + chanceScore;
    }
    private void UpdateScoreText()
    {
        ComputeScore();
        scoreText[0].text = acescore.ToString();
        scoreText[1].text = twoscore.ToString();
        scoreText[2].text = threescore.ToString();
        scoreText[3].text = fourscore.ToString();
        scoreText[4].text = fivescore.ToString();
        scoreText[5].text = sixscore.ToString();
        scoreText[6].text = totalScore.ToString();
        scoreText[7].text = bonusScore.ToString();
        scoreText[8].text = total.ToString();
        scoreText[9].text = threeOfKindScore.ToString();
        scoreText[10].text = fourOfKindScore.ToString();
        scoreText[11].text = fullHouseScore.ToString();
        scoreText[12].text = smallStraightScore.ToString();
        scoreText[13].text = largeStraightScore.ToString();
        scoreText[14].text = yahtzeeScore.ToString();
        scoreText[15].text = chanceScore.ToString();
        scoreText[16].text = grandTotalScore.ToString();
    }

    private int GetScoreUpperSection(int number)
    {
        int score = 0;
        for (int i = 0; i < dices.Length; i++)
        {
            if (dices[i].GetUpNumber() == number)
            {
                score += number;
            }
        }
        return score;
    }
    private int GetScoreNofKind(int n)
    {
        int score = 0;
        int[] count = new int[6];
        for (int i = 0; i < dices.Length; i++)
        {
            count[dices[i].GetUpNumber() - 1]++;
        }
        for (int i = 0; i < 6; i++)
        {
            if (count[i] >= n)
            {
                for (int j = 0; j < dices.Length; j++)
                {
                    score += dices[j].GetUpNumber();
                }
                break;
            }
        }
        return score;
    }
    private int GetScoreFullHouse()
    {
        int score = 0;
        int[] count = new int[6];
        for (int i = 0; i < dices.Length; i++)
        {
            count[dices[i].GetUpNumber() - 1]++;
        }
        bool has2 = false;
        bool has3 = false;
        for (int i = 0; i < 6; i++)
        {
            if (count[i] == 2)
            {
                has2 = true;
            }
            if (count[i] == 3)
            {
                has3 = true;
            }
        }
        if (has2 && has3)
        {
            score = 25;
        }
        return score;
    }
    private int GetScoreSmallStraight()
    {
        int score = 0;
        int[] count = new int[6];
        for (int i = 0; i < dices.Length; i++)
        {
            count[dices[i].GetUpNumber() - 1]++;
        }
        for (int i = 0; i < 3; i++)
        {
            if (count[i] > 0 && count[i + 1] > 0 && count[i + 2] > 0 && count[i + 3] > 0)
            {
                score = 15;
                break;
            }
        }
        return score;
    }
    private int GetScoreLargeStraight()
    {
        int score = 0;
        int[] count = new int[6];
        for (int i = 0; i < dices.Length; i++)
        {
            count[dices[i].GetUpNumber() - 1]++;
        }
        for (int i = 0; i < 2; i++)
        {
            if (count[i] > 0 && count[i + 1] > 0 && count[i + 2] > 0 && count[i + 3] > 0 && count[i + 4] > 0)
            {
                score = 30;
                break;
            }
        }
        return score;
    }
    private int GetScoreYahtzee()
    {
        int score = 0;
        int[] count = new int[6];
        for (int i = 0; i < dices.Length; i++)
        {
            count[dices[i].GetUpNumber() - 1]++;
        }
        for (int i = 0; i < 6; i++)
        {
            if (count[i] == 5)
            {
                score = 50;
                break;
            }
        }
        return score;
    }
    private int GetScoreChance()
    {
        int score = 0;
        for (int i = 0; i < dices.Length; i++)
        {
            score += dices[i].GetUpNumber();
        }
        return score;
    }
    #endregion
}
