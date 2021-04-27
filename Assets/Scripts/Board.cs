using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject controller;
    private int xBoard = -1;
    private int yBoard = -1; 

    public void Activate(){

        controller = GameObject.FindGameObjectWithTag("GameController"); // Get the game controller

        SetCoords(); // Take the instantiated location and adjust transform
        
    }
    
    public void SetCoords() // correspond unity coordinates with in-game coordinates
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset. not doing anything?!
        x *= 1.27f;
        y *= 1.27f;

        //Add constants (pos 0,0)
        x += -4.5f;
        y += -4.4f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, 1.0f);
    }


    //public void OnMouseUp(){
        //Debug.Log("Chessboard clicked");
   // }
}
