using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]private Transform background1;
    [SerializeField]private Transform[] background1Array;
    [SerializeField]private Transform background2;
    [SerializeField]private Transform[] background2Array;
    [SerializeField]private Transform background3;
    [SerializeField]private Transform[] background3Array;
    [SerializeField]private Transform background4;
    [SerializeField]private Transform[] background4Array;
    [SerializeField]private Transform background5;
    [SerializeField]private Transform[] background5Array;
    [SerializeField]private Transform ground;

    [SerializeField]private float moveSpeed = 5f;

    private void Update(){
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Mouse0)){
            BackgroundMove();
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
        
    }
}
