using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject peicePrefab;
    public Tile[,] tiles;
    private Color[] colors;

    public int amoutToRemove;
    public bool isExcuteTileShift;

    public PieceType specialTypeToadd;
    public Color clickPieceColor;
    public void ResetMatchTile()
    {
        isExcuteTileShift = false;
        amoutToRemove = 0;
    }

    public bool CheckIfAnyMatchFound()
    {
        return isExcuteTileShift;
    }
    public Color[] Colors { get { return colors; } }
    void Start()
    {
        tiles = new Tile[width, height];
        InitializeBoard();
    }

    void Update()
    {
        
    }
    private void InitializeBoard()
    {
        //SetMainCameraPosition
        Camera.main.transform.position = new Vector3(width / 2, height / 2, -10);
        Camera.main.orthographicSize = (width + height) / 2;

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
                newPiece.Init(PieceType.Normal , colors[Random.Range(0,colors.Length)]);
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
            }
        }
        ShiftTilesDown();
    }

    public void ShiftTilesDown()
    {
        //Piece[] TempPieces;
        specialTypeToadd  = PieceType.Normal;
        if(amoutToRemove >= 6 && amoutToRemove < 10)
        {
            specialTypeToadd = PieceType.Bomb;
            Debug.Log("bomb");
        }
        else if(amoutToRemove >= 10)
        {
            specialTypeToadd = PieceType.Disco;
            Debug.Log("Disco");
        }

        foreach (var tile in tiles)
        {
            if (tile.isRemove)
            {
                tile.GetAbovePieceTile();
            }
        }
    }
}
