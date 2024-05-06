using UnityEngine;

public class CheckersTile : MonoBehaviour, IClickableObject
{
    //This is where the checkers piece will move when selected to move here.
    public Transform CheckersPieceTarget;

    //private CheckersPiece occupyingPiece get 

    public bool IsValidMove()
    {
        //TODO: If you made a full checkers game, you could check the validity of the space here (only diagonals, not occupied etc).
        return true;
    }

    public void OnClicked()
    {
        if (IsValidMove())
        {
            CheckersManager.instance.MoveCheckerPiece(this);
        }
    }
}
