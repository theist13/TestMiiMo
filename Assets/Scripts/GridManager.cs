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
    public bool foundAnyMatch;

    public void ResetMatchTile()
    {
        foundAnyMatch = false;
        amoutToRemove = 0;
    }

    public bool CheckIfAnyMatchFound()
    {
        return foundAnyMatch;
    }

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
                //tiles[x, y].color = colors[Random.RandomRange(0, colors.Length)];
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
                newPiece.Init(TileType.Normal , colors[Random.Range(0,colors.Length)]);
                tiles[x, y].AddPeice(newPiece);
                go.transform.position = tiles[x, y].transform.position;
            }
        }
    }
    public void ShiftTilesDown()
    {
        //Piece[] TempPieces;

        foreach (var tile in tiles)
        {
            //tile.GetAbovePieceTile();
            if (tile.isRemove)
            {
                //Debug.Log($"{tile.x},{tile.y} is empty");
                tile.GetAbovePieceTile();
            }
        }

        Debug.Log("ShiftTilesDown");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y].isRemove)
                {
                    // Find the first non-removed tile above the current tile
                    for (int k = y; k > 0; k--)
                    {
                        //if (!tiles[x, k + 1].isRemove)
                        //{
                        //    Piece temp = tiles[x, k].piece;
                        //    tiles[x, k].piece = tiles[x, k - 1].piece;
                        //    tiles[x, k - 1].piece = temp;

                        //    // Update their positions (if necessary)
                        //    StartCoroutine(Lerp(tiles[x, k].piece.transform, tiles[x, k].transform.position, 1));
                        //    StartCoroutine(Lerp(tiles[x , k - 1].piece.transform, tiles[x, k - 1].transform.position, 1));

                        //   // tiles[x, k].piece.GetComponent<Renderer>().enabled = true;
                        //   // tiles[x, k - 1].piece.GetComponent<Renderer>().enabled = true;
                        //}
                    }

                }
            }
        }
    }

    IEnumerator Lerp(Transform tilePos, Vector3 end, float duration)
    {
        float time = 0;
        Vector3 start = tilePos.transform.position;
        while (time < duration)
        {
            tilePos.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        tilePos.position = end;
    }
}
