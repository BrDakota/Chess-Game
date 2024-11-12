using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public bool isOccupied = false;
    
    private string occupyingColor;

    public void setOccupancy(bool tf, string color){
        isOccupied = tf;
        occupyingColor = color;
    }

    public string getColor(){
        return occupyingColor;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
