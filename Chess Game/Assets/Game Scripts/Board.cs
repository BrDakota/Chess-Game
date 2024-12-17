using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public GameObject[,] tilesArr = new GameObject[8, 8];

    public GameObject quitBtn;
    public GameObject resetBtn;
    public GameObject winnerText;

    // key: king[0], queen[1], rook[2], bishop[3], knight[4], pawn[5]
    public GameObject[] blackPieces = new GameObject[6];
    public GameObject[] whitePieces = new GameObject[6];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] playerBlack = new GameObject[16];

    public string currentPlayer = "white";
    public string winner = "";
    public bool gameOver = false;

    private string[] charArr = {"A", "B", "C", "D", "E", "F", "G", "H"};

    // Start is called before the first frame update
    void Start()
    {
        // This loop fills the tiles array with all the correct tiles
        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                tilesArr[i, j] = GameObject.Find($"{charArr[i]}{j + 1}");
            }
        }

        // This creates all the pieces on the board
        playerBlack = new GameObject[] {Instantiate(blackPieces[2], GetTilePosition(7, 0), quaternion.identity), Instantiate(blackPieces[4], GetTilePosition(7, 1), quaternion.identity), 
        Instantiate(blackPieces[3], GetTilePosition(7, 2), quaternion.identity), Instantiate(blackPieces[1], GetTilePosition(7, 3), quaternion.identity), Instantiate(blackPieces[0], GetTilePosition(7, 4), quaternion.identity),
        Instantiate(blackPieces[3], GetTilePosition(7, 5), quaternion.identity), Instantiate(blackPieces[4], GetTilePosition(7, 6), quaternion.identity), Instantiate(blackPieces[2], GetTilePosition(7, 7), quaternion.identity),
        Instantiate(blackPieces[5], GetTilePosition(6, 0), quaternion.identity), Instantiate(blackPieces[5], GetTilePosition(6, 1), quaternion.identity), Instantiate(blackPieces[5], GetTilePosition(6, 2), quaternion.identity), 
        Instantiate(blackPieces[5], GetTilePosition(6, 3), quaternion.identity), Instantiate(blackPieces[5], GetTilePosition(6, 4), quaternion.identity), Instantiate(blackPieces[5], GetTilePosition(6, 5), quaternion.identity), 
        Instantiate(blackPieces[5], GetTilePosition(6, 6), quaternion.identity), Instantiate(blackPieces[5], GetTilePosition(6, 7), quaternion.identity)};
        playerWhite = new GameObject[] {Instantiate(whitePieces[2], GetTilePosition(0, 0), quaternion.identity), Instantiate(whitePieces[4], GetTilePosition(0, 1), quaternion.identity), 
        Instantiate(whitePieces[3], GetTilePosition(0, 2), quaternion.identity), Instantiate(whitePieces[1], GetTilePosition(0, 3), quaternion.identity), Instantiate(whitePieces[0], GetTilePosition(0, 4), quaternion.identity),
        Instantiate(whitePieces[3], GetTilePosition(0, 5), quaternion.identity), Instantiate(whitePieces[4], GetTilePosition(0, 6), quaternion.identity), Instantiate(whitePieces[2], GetTilePosition(0, 7), quaternion.identity),
        Instantiate(whitePieces[5], GetTilePosition(1, 0), quaternion.identity), Instantiate(whitePieces[5], GetTilePosition(1, 1), quaternion.identity), Instantiate(whitePieces[5], GetTilePosition(1, 2), quaternion.identity), 
        Instantiate(whitePieces[5], GetTilePosition(1, 3), quaternion.identity), Instantiate(whitePieces[5], GetTilePosition(1, 4), quaternion.identity), Instantiate(whitePieces[5], GetTilePosition(1, 5), quaternion.identity), 
        Instantiate(whitePieces[5], GetTilePosition(1, 6), quaternion.identity), Instantiate(whitePieces[5], GetTilePosition(1, 7), quaternion.identity)};
        
        // This sets the piece connected to each tile
        for (int i = 0; i < 2; i++){
            for(int j = 0; j < tilesArr.GetLength(0); j++){
                if(i == 0){
                    tilesArr[i, j].GetComponent<Tiles>().piece = playerWhite[j];
                }else{
                    tilesArr[i, j].GetComponent<Tiles>().piece = playerWhite[j + 8];
                }
            }
        }
        for (int i = 7; i > 5; i--){
            for(int j = 0; j < tilesArr.GetLength(0); j++){
                if(i == 7){
                    tilesArr[i, j].GetComponent<Tiles>().piece = playerBlack[j];
                }else{
                    tilesArr[i, j].GetComponent<Tiles>().piece = playerBlack[j + 8];
                }
            }
        }

        // Sets the position values of the piece class
        for(int i = 0; i < playerBlack.Length / 2; i++){
            playerBlack[i].GetComponent<Pieces>().posX = 7;
            playerBlack[i + 8].GetComponent<Pieces>().posX = 6;
            playerBlack[i].GetComponent<Pieces>().posY = i;
            playerBlack[i + 8].GetComponent<Pieces>().posY = i;

            playerWhite[i].GetComponent<Pieces>().posX = 0;
            playerWhite[i + 8].GetComponent<Pieces>().posX = 1;
            playerWhite[i].GetComponent<Pieces>().posY = i;
            playerWhite[i + 8].GetComponent<Pieces>().posY = i;
        }

        resetBtn = GameObject.FindGameObjectWithTag("ResetBtn");
        resetBtn.SetActive(false);
        quitBtn = GameObject.FindGameObjectWithTag("QuitBtn");
        quitBtn.SetActive(false);
        winnerText = GameObject.FindGameObjectWithTag("WinnerTxt");
        winnerText.GetComponent<TMP_Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == "black")
        {
            gameOver = true;
            resetBtn.SetActive(true);
            quitBtn.SetActive(true);
            winnerText.GetComponent<TMP_Text>().text = "Black has won!";

        }
        else if(winner == "white")
        {
            gameOver = true;
            resetBtn.SetActive(true);
            quitBtn.SetActive(true);
            winnerText.GetComponent<TMP_Text>().text = "White has won!";
        }
    }

    public UnityEngine.Vector2 GetTilePosition(int x, int y){
        return tilesArr[x, y].transform.position;
    }
    public void SetPositionEmpty(int x, int y){
        tilesArr[x, y].GetComponent<Tiles>().piece = null;
    }
    public void SetPosition(int x, int y, GameObject pieceT){
        tilesArr[x, y].GetComponent<Tiles>().piece = pieceT;
    }
    public bool PositionOnBoard(int x, int y){
        if(x < 0 || y < 0 || x >= tilesArr.GetLength(1) || y >= tilesArr.GetLength(0)) return false;
        return true;
    }
    
    public GameObject GetPiece(int x, int y){
        return tilesArr[x, y].GetComponent<Tiles>().piece;
    }

    public string SwitchTurn()
    {
        if(currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }

        return currentPlayer;
    }

    public void Reset()
    {
        SceneManager.LoadScene("Chess Board");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
