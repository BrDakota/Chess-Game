using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject board;
    GameObject reference = null;

    int matrixY;
    int matrixX;

    //false: movement, true: attacking
    public bool attack = false;

    public void Update(){
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

            if(hit.collider != null && hit.collider.transform == this.transform)
            {
                board = GameObject.FindGameObjectWithTag("Board");

                if(attack){
                    GameObject cp = board.GetComponent<Board>().GetPiece(matrixX, matrixY);

                    Destroy(cp);
                }

                board.GetComponent<Board>().SetPositionEmpty(reference.GetComponent<Pieces>().posX, reference.GetComponent<Pieces>().posY); // Tile no longer contains a piece and is set null
                reference.GetComponent<Pieces>().posX =  matrixX;
                reference.GetComponent<Pieces>().posY = matrixY;
                reference.transform.position = board.GetComponent<Board>().tilesArr[matrixX, matrixY].transform.position;

                board.GetComponent<Board>().SetPosition(matrixX, matrixY, reference);
                reference.GetComponent<Pieces>().DestroyMovePlates();
                board.GetComponent<Board>().SwitchTurn();
            }
        }
    }

    public void SetReference(GameObject obj){
        reference = obj;
    }

    public GameObject GetReference(){
        return reference;
    }

    public void SetCoords(int x, int y){
        matrixX = x;
        matrixY = y;
    }
}
