using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject moveDot;

    //Position for this piece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for keeping track of the player it belongs to player A or player B
    private string player;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite A_normal, A_plus;
    public Sprite B_normal, B_plus;

    public void Activate(){

        controller = GameObject.FindGameObjectWithTag("GameController"); // Get the game controller

        SetCoords(); // Take the instantiated location and adjust transform

        // switch on sprite name
        switch(this.name){
            case "A_normal": this.GetComponent<SpriteRenderer>().sprite = A_normal; player = "A"; break;
            case "B_normal": this.GetComponent<SpriteRenderer>().sprite = B_normal; player = "B"; break;
            case "A_plus": this.GetComponent<SpriteRenderer>().sprite = A_plus; player = "A"; break;
            case "B_plus": this.GetComponent<SpriteRenderer>().sprite = B_plus; player = "B"; break;
        }
    }

    public void SetCoords() // correspond unity coordinates with in-game coordinates
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset. not doing anything?!
        x *= 1.0f;
        y *= 1.27f;

        //Add constants (pos 0,0)
        x += -4.5f;
        y += -4.4f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, 1.0f);
    }

    // Getter and Setter methods from Unity Chess Tutorial
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }
}
