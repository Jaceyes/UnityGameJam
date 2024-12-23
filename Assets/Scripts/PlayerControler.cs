using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]private float moveSpeed = 5f;

    private float rightBorder;
    private Animator animator;

    private void Start(){
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if(GameManager.Instance.IsInDiceGame){
            return;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse0)){
            BackgroundMove();
            animator.SetBool("isWalking", true);
            // GameManager.Instance.StartGame();
        }else{
            animator.SetBool("isWalking", false);
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
}
