using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[,] tilesArr = new GameObject[8, 8];
    public GameObject[] piecesArr;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
