using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject peicePrefab;
    public Tile[,] tiles;
    private Color[] colors;

    public int normalAmoutToRemove;
    private int bombAmountToRemove;
    private int discoAmoutToRemove;

    public Action<int> OnAddScore;

    public bool isExcuteTileShift;

    public PieceType specialTypeToadd;
    public Color clickPieceColor;
    public void ResetMatchTile()
    {
        isExcuteTileShift = false;
        normalAmoutToRemove = 0;
        bombAmountToRemove = 0;
        discoAmoutToRemove = 0;
    }

    public bool CheckIfAnyMatchFound()
    {
        return isExcuteTileShift;
    }
    public Color[] Colors { get { return colors; } }
    public void Init()
    {
        tiles = new Tile[width, height];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        colors = new Color[] { Color.red , Color.green ,Color.blue , Color.yellow };
        GameObject gridHolder = new GameObject("Grid Holder");
        gridHolder.transform.parent = transform;
        for (int x = 0; x < width; x++)
        {
            GameObject go = new GameObject($"Row {x}");
            go.transform.parent = gridHolder.transform;
            for (int y = 0; y < height; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity);
                newTile.name = $"{x},{y}";
                newTile.transform.parent = go.transform;
                tiles[x, y] = newTile.GetComponent<Tile>();
                tiles[x, y].InitTile(this);
                tiles[x, y].x = x;
                tiles[x, y].y = y;
            }
        }
        FindAllNeighbor();
        FillBoardWithPeice();
    }
    private void FindAllNeighbor()
    {
        foreach (var tile in tiles)
        {
            tile.FindNeighbor();
        }
    }
    private void FillBoardWithPeice()
    {
        GameObject pieceHolder = new GameObject("Piece Holder");
        pieceHolder.transform.parent = transform;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject go = Instantiate(peicePrefab);
                go.transform.parent = pieceHolder.transform;
                Piece newPiece = go.GetComponent<Piece>();
                newPiece.Init(PieceType.Normal, colors[UnityEngine.Random.Range(0, colors.Length)]);
                tiles[x, y].AddPeice(newPiece);
                go.transform.position = tiles[x, y].transform.position;
            }
        }
    }

    public void RemovePieceByBomb(int x , int y)
    {
        foreach (var tile in tiles)
        {
            if(tile.x == x || tile.y == y)
            {
                tile.RemovePiece();
                bombAmountToRemove++;
            }
        }
        ShiftTilesDown();
    }
    public void RemovePieceByDisco(Color clr)
    {
        foreach (var tile in tiles)
        {
            if (tile.piece.color == clr)
            {
                tile.RemovePiece();
                discoAmoutToRemove++;
            }
        }
        ShiftTilesDown();
    }

    public void ShiftTilesDown()
    {
        specialTypeToadd  = PieceType.Normal;

        //Adding special piece
        if(normalAmoutToRemove >= 6 && normalAmoutToRemove < 10)
        {
            specialTypeToadd = PieceType.Bomb;
            Debug.Log("bomb");
        }
        else if(normalAmoutToRemove >= 10)
        {
            specialTypeToadd = PieceType.Disco;
            Debug.Log("Disco");
        }

        int score = normalAmoutToRemove * 100;
        score += bombAmountToRemove * 200;
        score += discoAmoutToRemove * 200;

        OnAddScore?.Invoke(score);
        foreach (var tile in tiles)
        {
            if (tile.isRemove)
            {
                tile.GetAbovePieceTile();
            }
        }
    }
}
