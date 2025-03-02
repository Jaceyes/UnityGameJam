using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]private Transform background1;
    [SerializeField]private List<Transform> background1Array;
    [SerializeField]private Transform background2;
    [SerializeField]private List<Transform> background2Array;
    [SerializeField]private Transform background3;
    [SerializeField]private List<Transform> background3Array;
    [SerializeField]private Transform background4;
    [SerializeField]private List<Transform> background4Array;
    [SerializeField]private Transform background5;
    [SerializeField]private List<Transform> background5Array;
    [SerializeField]private Transform ground;
    [SerializeField]private LayerMask WhatIsEnemy;

    [SerializeField] private TextMeshProUGUI GreatDiceNumText;
    [SerializeField]private Transform firstGround;
    [SerializeField]private Transform secondGround;
    private Transform enemy;
    private Transform tempGround;

    [SerializeField]private float moveSpeed = 5f;

    [SerializeField]private int GreatDiceNum = 0;

    private float rightBorder;
    private Animator animator;

    private void Start(){
        enemy = GameObject.Find("Enemy").transform;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        animator = GetComponent<Animator>();
        UpdateGreatDiceNum();
    }

    private void Update(){
        if(GameManager.Instance.IsInDiceGame){
            return;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse0)){
            BackgroundMove();
            animator.SetBool("isWalking", true);
        }else{
            animator.SetBool("isWalking", false);
        }
        if(Physics2D.Raycast(transform.position, Vector2.right, 2, WhatIsEnemy)){
            animator.SetBool("isWalking", false);
            GameManager.Instance.GameStart();
        }
    }

    private void BackgroundMove(){
        ground.position += new Vector3(-moveSpeed*Time.deltaTime, 0, 0);
        background1.position += new Vector3(-moveSpeed*Time.deltaTime*0.8f, 0, 0);
        background2.position += new Vector3(-moveSpeed*Time.deltaTime*0.6f, 0, 0);
        background3.position += new Vector3(-moveSpeed*Time.deltaTime*0.4f, 0, 0);
        background4.position += new Vector3(-moveSpeed*Time.deltaTime*0.2f, 0, 0);
        background5.position += new Vector3(-moveSpeed*Time.deltaTime*0.05f, 0, 0);
        BackgroundControl();
    }

    private void BackgroundControl(){
        if(background1Array[background1Array.Count-2].position.x<= transform.position.x){
            AddBackground(background1Array);
        }
        if(background2Array[background2Array.Count-2].position.x<= transform.position.x){
            AddBackground(background2Array);
        }
        if(background3Array[background3Array.Count-2].position.x<= transform.position.x){
            AddBackground(background3Array);
        }
        if(background4Array[background4Array.Count-2].position.x<= transform.position.x){
            AddBackground(background4Array);
        }
        if(background5Array[background5Array.Count-2].position.x<= transform.position.x){
            AddBackground(background5Array);
        }
        if(firstGround.position.x <= -35){
            firstGround.position = secondGround.position + Vector3.right*35;
            tempGround = firstGround;
            firstGround = secondGround;
            secondGround = tempGround;
        }
        if(enemy.position.x <= -30){
            GameManager.Instance.InitEnemy();
        }
    }

    private void AddBackground(List<Transform> list){
        if (list.Count > 1)
        {
            Transform firstElement = list[0];
            firstElement.position = new Vector3(list[list.Count - 1].position.x + list[0].GetComponent<SpriteRenderer>().bounds.size.x, list[0].position.y, list[0].position.z);
            list.RemoveAt(0);
            list.Add(firstElement);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right*2);
    }
    public void GetGreatDice(){
        GreatDiceNum++;
        UpdateGreatDiceNum();
    }
    public void LoseGreatDice(){
        GreatDiceNum = GreatDiceNum>0?GreatDiceNum-1:0;
        UpdateGreatDiceNum();
    }
    private void UpdateGreatDiceNum(){
        GreatDiceNum = PlayerPrefs.GetInt("GreatDiceNum", 0);
        GreatDiceNumText.text = GreatDiceNum.ToString();
        PlayerPrefs.SetInt("GreatDiceNum", GreatDiceNum);
    }
}
