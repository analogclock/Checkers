using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject piece;

    // Matrices needed, positions of each of the GameObjects
    // Also separate arrays for the players in order to easily keep track of them all
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerA = new GameObject[8]; 
    private GameObject[] playerB = new GameObject[8];
    private string[,] stringPositions = new string[8,8]; // this variable and it's implementaitons are all me

    private string currentPlayer = "A"; // current turn

    private bool gameOver = false; // Game Ending

    // how many pieces does each player have
    private int piecesA = 8;
    private int piecesB = 8;

    void Start() // create pieces and set their positions. Idea and structure from Unity Chess Tutorial
    {
        playerA = new GameObject[]{
        Create("A_normal", 0,0), Create("A_normal", 1,1), 
        Create("A_normal", 2,0), Create("A_normal", 3,1), 
        Create("A_normal", 4,0), Create("A_normal", 5,1),
        Create("A_normal", 6,0), Create("A_normal", 7,1)};
        playerB = new GameObject[]{
        Create("B_normal", 0,6), Create("B_normal", 1,7),
        Create("B_normal", 2,6), Create("B_normal", 3,7),
        Create("B_normal", 4,6), Create("B_normal", 5,7),
        Create("B_normal", 6,6), Create("B_normal", 7,7)};

        for (int i = 0; i<playerA.Length; i++){
            SetPosition(playerA[i]);
            SetPosition(playerB[i]);
        }
    }

    public GameObject Create(string name, int x, int y) // from Unity Chess tutorial
    {
        GameObject obj = Instantiate(piece, new Vector3(0, 0, -1), Quaternion.identity);
        Piece p = obj.GetComponent<Piece>();
        stringPositions[x,y] = name;
        p.name = name; // name is a built in variable in unity
        p.SetXBoard(x);
        p.SetYBoard(y);
        p.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj) // from unity chess
    {
        Piece p = obj.GetComponent<Piece>();

        //Overwrites either empty space or whatever was there
        if (isOnBoard(p.GetXBoard(), p.GetYBoard())){
            positions[p.GetXBoard(), p.GetYBoard()] = obj;
            stringPositions[p.GetXBoard(), p.GetYBoard()] = p.name; // me
        }
        
    }

    public void SetPositionEmpty(int x, int y) // from unity chess
    {
        positions[x, y] = null;
        stringPositions[x,y] = null; // me
        
        
    }
    // The following 4 methods are all my work 
    public string getPieceAtXY(int x, int y){
        return this.stringPositions[x,y];
    }

    public void eitherPlayerLost(){
        if (this.piecesA == 0 || this.piecesB == 0){
            this.gameOver=true;
            print("Game Over!");
            //SceneManager.UnloadSceneAsync();
            Application.Quit();
        }
    }

    public void DecreasePiecesA(){
        this.piecesA = this.piecesA - 1;
        print("A: " + this.piecesA);
        print("B: " + this.piecesB);
        eitherPlayerLost();
    }

    public void DecreasePiecesB(){
        this.piecesB = this.piecesB - 1;
        print("A: " + this.piecesA);
        print("B: " + this.piecesB);
        eitherPlayerLost();
    }

    // Getter and Setter methods from Unity Chess Tutorial

    public GameObject GetPosition(int x, int y)
    {
        //Debug.Log(positions[x,y]);
        string str = stringPositions[x,y];
        Debug.Log(str);
        return positions[x, y];
    }

    public string GetCurrentPlayer(){
        return this.currentPlayer;
    }

    public void changeCurrentPlayer(){
        if (this.currentPlayer == "A"){
            this.currentPlayer = "B";
        }
        else if (this.currentPlayer == "B"){
            this.currentPlayer = "A";
        }
    }

    public bool isOnBoard(int x, int y){
        bool xCoord = x>=0 && x<=7;
        bool yCoord = y>=0 && y<=7;
        return xCoord && yCoord;
    }

}
