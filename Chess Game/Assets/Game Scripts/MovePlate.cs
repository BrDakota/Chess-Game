using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject board;
    GameObject reference = null;
    GameObject reference2 = null;

    int matrixY;
    int matrixX;

    //false: movement, true: attacking
    public bool attack = false;
    //false: normal movement, true: castling
    public bool castle = false;

    public void Start()
    {
        if (castle)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void Update(){
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

            if(hit.collider != null && hit.collider.transform == this.transform)
            {
                board = GameObject.FindGameObjectWithTag("Board");

                if(attack){
                    GameObject cp = board.GetComponent<Board>().GetPiece(matrixX, matrixY);

                    if (cp.GetComponent<Pieces>().name == "white_king") board.GetComponent<Board>().winner = "black";
                    if (cp.GetComponent<Pieces>().name == "black_king") board.GetComponent<Board>().winner = "white";

                    Destroy(cp);
                }

                if (castle)
                {
                    board.GetComponent<Board>().SetPositionEmpty(reference2.GetComponent<Pieces>().posX, reference2.GetComponent<Pieces>().posY);
                    reference2.GetComponent<Pieces>().posX = matrixX;
                    if(reference2.GetComponent<Pieces>().posY == 0)
                    {
                        reference2.GetComponent<Pieces>().posY = matrixY + 1;
                        reference2.transform.position = board.GetComponent<Board>().tilesArr[matrixX, matrixY + 1].transform.position;

                        board.GetComponent<Board>().SetPosition(matrixX, matrixY + 1, reference2);
                        reference2.GetComponent<Pieces>().turnCounter++;
                    }
                    else
                    {
                        reference2.GetComponent<Pieces>().posY = matrixY - 1;
                        reference2.transform.position = board.GetComponent<Board>().tilesArr[matrixX, matrixY - 1].transform.position;

                        board.GetComponent<Board>().SetPosition(matrixX, matrixY - 1, reference2);
                        reference2.GetComponent<Pieces>().turnCounter++;
                    }
                }

                board.GetComponent<Board>().SetPositionEmpty(reference.GetComponent<Pieces>().posX, reference.GetComponent<Pieces>().posY); // Tile no longer contains a piece and is set null
                reference.GetComponent<Pieces>().posX =  matrixX;
                reference.GetComponent<Pieces>().posY = matrixY;
                reference.transform.position = board.GetComponent<Board>().tilesArr[matrixX, matrixY].transform.position;

                board.GetComponent<Board>().SetPosition(matrixX, matrixY, reference);
                reference.GetComponent<Pieces>().DestroyMovePlates();
                board.GetComponent<Board>().SwitchTurn();
                reference.GetComponent<Pieces>().turnCounter++;
            }
        }
    }

    public void SetReference(GameObject obj){
        reference = obj;
    }

    public void SetReference2(GameObject obj)
    {
        reference2 = obj;
    }

    public GameObject GetReference(){
        return reference;
    }

    public void SetCoords(int x, int y){
        matrixX = x;
        matrixY = y;
    }
}
