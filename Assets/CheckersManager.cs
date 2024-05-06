using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersManager : MonoBehaviour
{
    [HideInInspector] public static CheckersManager instance = null;

    [SerializeField] private int boardWidth = 8;
    [SerializeField] private int boardHeight = 8;
    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private Material blackMaterial;

    private CheckersTile[,] board;

    private CheckersPiece selectedPiece = null;
    private Coroutine pieceMovementCoroutine = null;

    private const float _tileSize = 1;
    private const float _moveAnimationLength = .2f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateCheckersBoard();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMouseClick();
        }
    }

    private void CreateCheckersBoard()
    {
        board = new CheckersTile[boardWidth, boardHeight];

        bool nextTileIsDefault = true;

        GameObject tempTile;

        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                tempTile = Instantiate(tilePrefab);
                tempTile.transform.position += new Vector3(_tileSize * j, 0, _tileSize * i);
                if (!nextTileIsDefault) tempTile.GetComponent<Renderer>().material =  blackMaterial;
                //Alternate color each tile
                nextTileIsDefault = !nextTileIsDefault;
                board[i, j] = tempTile.GetComponent<CheckersTile>();
            }
            //If the height is even, then we need to alternate color tile for next column
            if (boardHeight % 2 == 0) nextTileIsDefault = !nextTileIsDefault;
        }
    }

    public void SelectPiece(CheckersPiece select)
    {
        if (selectedPiece != null) selectedPiece.DeselectPiece();
        selectedPiece = select;
    }

    public void MoveCheckerPiece(CheckersTile targetTile)
    {
        if (selectedPiece != null && pieceMovementCoroutine == null)
        {
            pieceMovementCoroutine = StartCoroutine(MovePieceRoutine(selectedPiece.gameObject, targetTile.CheckersPieceTarget.position));
            selectedPiece.DeselectPiece();
            selectedPiece = null;
        }
    }

    private IEnumerator MovePieceRoutine(GameObject pieceToMove, Vector3 targetPosition)
    {
        float timer = 0;
        Vector3 startPos = pieceToMove.transform.position;

        while (timer < _moveAnimationLength)
        {
            timer += Time.deltaTime;
            pieceToMove.transform.position = Vector3.Lerp(startPos, targetPosition, timer / _moveAnimationLength);
            yield return null;
        }
        pieceToMove.transform.position = targetPosition;

        pieceMovementCoroutine = null;
    }

    private void HandleMouseClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
            hit.transform.gameObject.GetComponent<IClickableObject>()?.OnClicked();     
        }
    }
}
