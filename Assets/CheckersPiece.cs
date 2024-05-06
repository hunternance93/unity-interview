using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersPiece : MonoBehaviour, IClickableObject
{
    private Renderer _renderer;
    private Color startingColor;
    private Color selectionColor = Color.yellow;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        startingColor = _renderer.material.color;
    }

    public void OnClicked()
    {
        SelectPiece();
    }

    public void SelectPiece()
    {
        if (IsValidSelection())
        {
            CheckersManager.instance.SelectPiece(this);
            _renderer.material.color = selectionColor;
        }
    }

    public void DeselectPiece()
    {
        _renderer.material.color = startingColor;
    }

    public bool IsValidSelection()
    {
        //TODO: In a real Checkers game, we would have to check if this is our own piece, if it is our turn, etc
        return true;
    }
}
