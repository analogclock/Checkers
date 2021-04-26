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
    private GameObject[] playerA = new GameObject[1]; // dummy for now
    private GameObject[] playerB = new GameObject[1];

    private string currentPlayer = "A"; // current turn

    private bool gameOver = false; // Game Ending

    // create pieces and set their positions
    void Start()
    {
        playerA = new GameObject[]{Create("A_normal", 0,0)};
        playerB = new GameObject[]{Create("B_normal", 0,7)};

        for (int i = 0; i<playerA.Length; i++){
            setPosition(playerA[i]);
            setPosition(playerB[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(piece, new Vector3(0, 0, 1), Quaternion.identity);
        Piece p = obj.GetComponent<Piece>();
        p.name = name; // name is a built in variable in unity
        p.SetXBoard(x);
        p.SetYBoard(y);
        p.Activate();
        return obj;
    }

    public void setPosition(GameObject obj)
    {
        Piece p = obj.GetComponent<Piece>();

        //Overwrites either empty space or whatever was there
        positions[p.GetXBoard(), p.GetYBoard()] = obj;
    }
}
