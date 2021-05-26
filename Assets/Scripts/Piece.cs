using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // References to objects in our Unity Scene
    public GameObject controller;
    public GameObject moveDot;

    // Location on the board
    int matrixX;
    int matrixY;

    // Position for this piece on the Board
    // The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1; 

    public bool attacking = false;

    private string player; // Variable for keeping track of the player it belongs to player A or player B

    //References to all the possible Sprites that this piece could be
    public Sprite A_normal, A_plus;
    public Sprite B_normal, B_plus;

    // Coordinates of piece being attacked
    private int attakX;
    private int attakY;

    public void Activate(){ // logic from unity chess tutorial

        controller = GameObject.FindGameObjectWithTag("GameController"); // Get the game controller

        SetCoords();

        switch(this.name){
            case "A_normal": this.GetComponent<SpriteRenderer>().sprite = A_normal; player = "A"; break;
            case "B_normal": this.GetComponent<SpriteRenderer>().sprite = B_normal; player = "B"; break;
            case "A_plus": this.GetComponent<SpriteRenderer>().sprite = A_plus; player = "A"; break;
            case "B_plus": this.GetComponent<SpriteRenderer>().sprite = B_plus; player = "B"; break;
        }
    }

    public void SetCoords() // logic from unity chess tutorial. correspond unity coordinates with in-game coordinates
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

    private void OnMouseUp(){ // from unity chess tutorial
        if (controller.GetComponent<Game>().GetCurrentPlayer() == player){
            DestroyMoveDots();
            GenerateMoveDots();
        }
    }

    public void GenerateMoveDots(){ 
        switch(this.name){
            case "A_normal": Move("A"); break;
            case "B_normal": Move("B"); break;
            case "A_plus": PlusMove("A"); break;
            case "B_plus": PlusMove("B"); break;
        }
    }

    public void DestroyMoveDots(){ // from unity chess tutorial
        GameObject[] moveDots = GameObject.FindGameObjectsWithTag("MoveDot");
        for (int i = 0; i < moveDots.Length; i++)
        {
            Destroy(moveDots[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    private bool isAttacking(int xIncrement, int yIncrement){
        Game sc = controller.GetComponent<Game>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        if (sc.isOnBoard(x,y)){
            string s = sc.getPieceAtXY(x,y);
            Debug.Log(s);

            if (s == null || 
            (this.player == "A" && (s == "A_normal" || s == "A_plus"))|| // don't attack one's own players
            (this.player == "B" && (s == "B_normal" || s == "B_plus"))){ 
                Debug.Log("Is NOT Attacking");
                return false;
            }
            Debug.Log("Is Attacking");
            this.attacking = true;
            return true;
        }
        this.attacking = false;
        return false;
    }

    private void Move(string player){

        if (this.player == "A"){
                if (isAttacking(1,1)){ // right 1, up 1
                    this.attakX = 1;
                    this.attakY = 1;
                    MovePlate(2,2, true);
                }
                else{
                    MovePlate(1,1, false);
                }
                if (isAttacking(-1,1)){  // left 1, up 1
                    this.attakX = -1;
                    this.attakY = 1;
                    MovePlate(-2,2, true);
                }
                else{
                    MovePlate(-1,1, false);
                }
        }
        else if (this.player == "B"){
            if (isAttacking(1,-1)){ // right 1, down 1
                    this.attakX = 1;
                    this.attakY = -1;
                    MovePlate(2,-2, true);
                }
                else{
                    MovePlate(1,-1, false);
                }
                if (isAttacking(-1,-1)){ // left 1, down 1
                    this.attakX = -1;
                    this.attakY = -1;
                    MovePlate(-2,-2, true);
                }
                else{
                     MovePlate(-1,-1, false);
                }
        }
    
    }

    private void PlusMove(string player){ // Plus pieces can move and capture on all 4 diagonals

        if (this.player == "A"){
                if (isAttacking(1,1)){ // right 1, up 1
                    this.attakX = 1;
                    this.attakY = 1;
                    MovePlate(2,2, true);
                }
                else{
                    MovePlate(1,1, false);
                }
                if (isAttacking(-1,1)){  // left 1, up 1
                    this.attakX = -1;
                    this.attakY = 1;
                    MovePlate(-2,2, true);
                }
                else{
                    MovePlate(-1,1, false);
                }
                if (isAttacking(1,-1)){ // right 1, down 1
                        this.attakX = 1;
                        this.attakY = -1;
                        MovePlate(2,-2, true);
                    }
                    else{
                        MovePlate(1,-1, false);
                    }
                    if (isAttacking(-1,-1)){ // left 1, down 1
                        this.attakX = -1;
                        this.attakY = -1;
                        MovePlate(-2,-2, true);
                    }
                    else{
                        MovePlate(-1,-1, false);
                    }
                
        }
        else if (this.player == "B"){
            if (isAttacking(1,-1)){ // right 1, down 1
                    this.attakX = 1;
                    this.attakY = -1;
                    MovePlate(2,-2, true);
                }
                else{
                    MovePlate(1,-1, false);
                }
                if (isAttacking(-1,-1)){ // left 1, down 1
                    this.attakX = -1;
                    this.attakY = -1;
                    MovePlate(-2,-2, true);
                }
                else{
                    MovePlate(-1,-1, false);
                }
                if (isAttacking(1,1)){ // right 1, up 1
                    this.attakX = 1;
                    this.attakY = 1;
                    MovePlate(2,2, true);
                }
                else{
                    MovePlate(1,1, false);
                }
                if (isAttacking(-1,1)){  // left 1, up 1
                    this.attakX = -1;
                    this.attakY = 1;
                    MovePlate(-2,2, true);
                }
                else{
                    MovePlate(-1,1, false);
                }
            
        }
    
    }


    private void MovePlate(int xIncrement, int yIncrement, bool attak){ // from unity chess tutorial except for attak boolean
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        if (attak){
            this.attakX = xBoard + attakX;
            this.attakY = yBoard + attakY;
        }

        if (sc.isOnBoard(x,y)){
            if (sc.GetPosition(x,y)==null){
                MoveDotsSpawn(x,y, attak);
            }
        }
        else{
            Debug.Log("Dot cannot be generated");
        }
    }

    private void MoveDotsSpawn(int matrixX, int matrixY, bool attak){ // from unity chess tutorial

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
        sqScript.attack = attak;
        if(attak){
            sqScript.setAttackedX(attakX);
            sqScript.setAttackedY(attakY);
        }
        sqScript.SetCurrentPlayer(this.player);
        sqScript.SetReference(gameObject);
        sqScript.SetCoords(matrixX, matrixY);
        
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

    public string GetPlayer(){ // for capturing
        return this.player;
    }

}
