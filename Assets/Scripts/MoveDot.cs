using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDot : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    // coordinates of piece being attacked
    int attackedX;
    int attackedY;

    //false: movement, true: attacking
    public bool attack;

    private void OnMouseUp(){
        //string debugg = this.name + " clicked";
        Debug.Log("Dot Clicked");

        controller = GameObject.FindGameObjectWithTag("GameController");

        //attack = controller.GetComponent<Piece>().attacking;

        if (attack){
            // destroy opponent poiece
            GameObject cp = controller.GetComponent<Game>().GetPosition(attackedX, attackedY); // position after jumping
            Destroy(cp);
        }
            
        //}

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Piece>().GetXBoard(), 
            reference.GetComponent<Piece>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<Piece>().SetXBoard(matrixX);
        reference.GetComponent<Piece>().SetYBoard(matrixY);
        reference.GetComponent<Piece>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        // switch player
        controller.GetComponent<Game>().changeCurrentPlayer();

        // delete old dots
        reference.GetComponent<Piece>().DestroyMoveDots();
    }

        public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void setAttackedX(int x){
        this.attackedX = x;
    }

    public void setAttackedY(int y){
        this.attackedY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
