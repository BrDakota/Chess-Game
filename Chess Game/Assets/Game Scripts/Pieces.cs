using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    GameObject board;
    public GameObject movePlate;

    public string name = new string("");
    public string team;
    public int posX;
    public int posY;

    // Start is called before the first frame update
    public void Start()
    {
        board = GameObject.FindGameObjectWithTag("Board");
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

            if(hit.collider != null && hit.collider.transform == this.transform)
            {
                DestroyMovePlates();

                IntiateMovePlates();
            }
        }
    }

    public void DestroyMovePlates(){
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++){
            Destroy(movePlates[i]);
        }
    }

    public void IntiateMovePlates(){
        switch(this.name){
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case"black_knight":
            case"white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(-1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(posX - 1, posY);
                break;
            case "white_pawn":
                PawnMovePlate(posX + 1, posY);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement){
        Board sc = board.GetComponent<Board>();

        int x = posX + xIncrement;
        int y = posY + yIncrement;

        while(sc.PositionOnBoard(x, y) && sc.GetPiece(x, y) == null){
            MovePlateAttackSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if(sc.PositionOnBoard(x, y) && sc.GetPiece(x, y).GetComponent<Pieces>().team != team){
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate(){
        PointMovePlate(posX + 1, posY + 2);
        PointMovePlate(posX - 1, posY + 2);
        PointMovePlate(posX + 2, posY + 1);
        PointMovePlate(posX + 2, posY - 1);
        PointMovePlate(posX + 1, posY - 2);
        PointMovePlate(posX - 1, posY - 2);
        PointMovePlate(posX - 2, posY + 1);
        PointMovePlate(posX - 2, posY - 1);
    }

    public void SurroundMovePlate(){
        PointMovePlate(posX, posY + 1);
        PointMovePlate(posX, posY - 1);
        PointMovePlate(posX - 1, posY - 1);
        PointMovePlate(posX - 1, posY + 0);
        PointMovePlate(posX - 1, posY + 1);
        PointMovePlate(posX + 1, posY - 1);
        PointMovePlate(posX + 1, posY + 0);
        PointMovePlate(posX + 1, posY + 1);
    }

    public void PointMovePlate(int x, int y){
        Board sc = board.GetComponent<Board>();
        if(sc.PositionOnBoard(x, y)){
            GameObject cp = sc.GetPiece(x, y);

            if(cp == null){
                MovePlateSpawn(x, y);
            }else if(cp.GetComponent<Pieces>().team != team){
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y){
        Board sc = board.GetComponent<Board>();
        if(sc.PositionOnBoard(x, y)){
            if(sc.GetPiece(x, y) == null){
                MovePlateSpawn(x, y);
            }

            if(sc.PositionOnBoard(x, y + 1) && sc.GetPiece(x, y + 1) != null && sc.GetPiece(x, y + 1).GetComponent<Pieces>().team != team){
                MovePlateAttackSpawn(x, y + 1);
            }
            if(sc.PositionOnBoard(x, y - 1) && sc.GetPiece(x, y - 1) != null && sc.GetPiece(x, y - 1).GetComponent<Pieces>().team != team){
                MovePlateAttackSpawn(x, y - 1);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY){
        Board sc = board.GetComponent<Board>();

        GameObject mp = Instantiate(movePlate, sc.GetTilePosition(matrixX, matrixY), quaternion.identity);
        
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY){
        Board sc = board.GetComponent<Board>();

        GameObject mp = Instantiate(movePlate, sc.GetTilePosition(matrixX, matrixY), quaternion.identity);
        
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
