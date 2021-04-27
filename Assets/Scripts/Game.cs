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
    private GameObject[,] positions = new GameObject[8, 8]; // internal game positions (not unity coordinates)
    private GameObject[] playerA = new GameObject[8]; // dummy for now
    private GameObject[] playerB = new GameObject[8];

    private string currentPlayer = "A"; // current turn

    private bool gameOver = false; // Game Ending

    // create pieces and set their positions. Idea and structure from Unity Chess Tutorial
    void Start()
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

    public GameObject Create(string name, int x, int y) // A lot from Unity Chess tutorial
    {
        GameObject obj = Instantiate(piece, new Vector3(0, 0, -1), Quaternion.identity);
        Piece p = obj.GetComponent<Piece>();
        p.name = name; // name is a built in variable in unity
        p.SetXBoard(x);
        p.SetYBoard(y);
        p.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj) // unity chess
    {
        Piece p = obj.GetComponent<Piece>();

        //Overwrites either empty space or whatever was there
        positions[p.GetXBoard(), p.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y) // unity chess
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool isOnBoard(int x, int y){
        bool xCoord = x>=0 && x<=7;
        bool yCoord = y>=0 && y<=7;
        return xCoord && yCoord;
    }
}
