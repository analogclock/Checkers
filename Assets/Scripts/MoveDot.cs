using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDot : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The piece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    // coordinates of piece being attacked
    int attackedX;
    int attackedY;

    //false: movement, true: attacking
    public bool attack;

    private string player;

    private void OnMouseUp(){
        //string debugg = this.name + " clicked";
        Debug.Log("Dot Clicked");

        controller = GameObject.FindGameObjectWithTag("GameController");

        //attack = controller.GetComponent<Piece>().attacking;

        if (attack){
            // destroy opponent poiece
            GameObject cp = controller.GetComponent<Game>().GetPosition(attackedX, attackedY); // position after jumping
            controller.GetComponent<Game>().SetPositionEmpty(attackedX, attackedY);
            Destroy(cp);
            if (this.player == "A"){ // opponent has one fewer pieces
                controller.GetComponent<Game>().DecreasePiecesB();
            }
            else {
                controller.GetComponent<Game>().DecreasePiecesA();
            }
        }

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Piece>().GetXBoard(), 
            reference.GetComponent<Piece>().GetYBoard());

        // if piece has reached opponent's side, delete old piece & reference, and change to plus piece
        if(this.player == "A" && this.matrixY == 7){//  || (this.player == "B" && this.matrixY == 0)
            Destroy(GetReference());
            GameObject p = controller.GetComponent<Game>().Create("A_plus", this.matrixX, this.matrixY); // create plus piece where original piece was
            SetReference(p);
        }
        else if(this.player == "B" && this.matrixY == 0){//  || (this.player == "B" && this.matrixY == 0)
            Destroy(GetReference());
            GameObject p = controller.GetComponent<Game>().Create("B_plus", this.matrixX, this.matrixY); // create plus piece where original piece was
            SetReference(p);
        }
        else{
            //Move reference chess piece to this position
            reference.GetComponent<Piece>().SetXBoard(matrixX);
            reference.GetComponent<Piece>().SetYBoard(matrixY);
            reference.GetComponent<Piece>().SetCoords();
        }

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        // switch player
    
        controller.GetComponent<Game>().changeCurrentPlayer();

        // delete old dots
        reference.GetComponent<Piece>().DestroyMoveDots();

    } // end of mouseup

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

    public void SetCurrentPlayer(string p){
        this.player = p;
    }
}
