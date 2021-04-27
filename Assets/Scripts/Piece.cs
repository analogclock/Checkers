using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject moveDot;
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //Position for this piece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1; 

    private string player; // Variable for keeping track of the player it belongs to player A or player B

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
        x *= 1.27f;
        y *= 1.27f;

        //Add constants (pos 0,0)
        x += -4.5f;
        y += -4.4f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    private void OnMouseUp(){
        string debugg = this.name + " clicked";
        Debug.Log(debugg);

        DestroyMoveDots();
        GenerateMoveDots();
    }

    public void GenerateMoveDots(){ // encodes rules of movemnt sort of
        switch(this.name){
            case "A_normal": normalMoves("A"); break;
            case "B_normal": normalMoves("B"); break;
        }
    }

    public void DestroyMoveDots(){
        //Destroy old dots
        GameObject[] moveDots = GameObject.FindGameObjectsWithTag("MoveDot");
        for (int i = 0; i < moveDots.Length; i++)
        {
            Destroy(moveDots[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    private void normalMoves(string player){
        bool attacking = false; // maybe make a method to determine if is attacking
        if (player == "A"){
            if (!attacking){
                normalMovePlate(1,1);
                normalMovePlate(-1,1);
            }
            else if (attacking){
                normalMovePlate(2,2);
                normalMovePlate(-2,2);
            }
        }
        else if (player == "B"){
            if (!attacking){
                normalMovePlate(1,-1); 
                normalMovePlate(-1,-1);
            }
            else if (attacking){
                normalMovePlate(2,-2);
                normalMovePlate(-2,-2);
            }
        }
    
    }

    private void normalMovePlate(int xIncrement, int yIncrement){ // unity chess
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        if (sc.GetPosition(x,y)==null && sc.isOnBoard(x,y)){
            MoveDotsSpawn(x,y);
        }
    }

    private void plusMovePlate(string player){

    }

    private void MoveDotsSpawn(int matrixX, int matrixY){ // Unity Chess
    ///*
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 1.27f;
        y *= 1.27f;

        //Add constants (pos 0,0)
        x += -4.5f;
        y += -4.4f;

        //Set actual unity values
        GameObject sq = Instantiate(moveDot, new Vector3(x, y, -1.0f), Quaternion.identity);

        MoveDot sqScript = sq.GetComponent<MoveDot>();
        sqScript.SetReference(gameObject);
        sqScript.SetCoords(matrixX, matrixY);
       // */
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
