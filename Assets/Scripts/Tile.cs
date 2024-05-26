using MiscUtil.Xml.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class Tile : MonoBehaviour
{
    private GridManager gridManager;
    public int x;
    public int y;
    public Piece piece;
    public Tile[] neighbors;
    public bool isRemove;

    public void InitTile(GridManager grid)
    {
        gridManager = grid;
        isRemove = false;
    }
    public void AddPeice(Piece newPiece)
    {
        piece = newPiece;
    }
    public void OnMouseUp()
    {
        if(piece.tileType == TileType.Normal)
        {
           // Debug.Log($"{x},{y}");
            gridManager.ResetMatchTile();
            DestroyPieceInTile();
        }
        else
        {
            Debug.Log($"something wrong : {x},{y}");
        }
    }
    public void FindNeighbor()
    {
        // Left
        if (x - 1 >= 0)
        {
            neighbors[0] = gridManager.tiles[x - 1, y];
        }

        // Right
        if (x + 1 <= gridManager.width - 1)
        {
            neighbors[1] = gridManager.tiles[x + 1, y];
        }

        // Down
        if (y - 1 >= 0)
        {
            neighbors[2] = gridManager.tiles[x , y - 1];
        }

        // Top
        if (y + 1 <= gridManager.height - 1)
        {
            neighbors[3] = gridManager.tiles[x, y + 1];
        }
    }
    public void DestroyPieceInTile()
    {
        bool foundSameColorNeighbor = false;

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] == null) continue;
            if (neighbors[i].piece.color == piece.color)
            {
                if (!neighbors[i].isRemove)
                {
                    foundSameColorNeighbor = true;
                    gridManager.amoutToRemove++;
                    neighbors[i].isRemove = true;
                    neighbors[i].DestroyPieceInTile();
                    neighbors[i].piece.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        if (!foundSameColorNeighbor && !gridManager.isExcuteTileShift && gridManager.amoutToRemove > 1)
        {
            Debug.Log("Finish Remove");
            gridManager.isExcuteTileShift = true;
            StartCoroutine(DelayToShiftTileDown());
        }

    }

    public void GetAbovePieceTile()
    {
        float moveTime = 0.5f;
        while(isRemove)
        {
            if (neighbors[3])
            {
                if (!neighbors[3].isRemove)
                {
                    Piece temp = piece;
                    piece = neighbors[3].piece;
                    neighbors[3].piece = temp;

                    neighbors[3].isRemove = true;
                    piece.transform.DOMove(transform.position, moveTime);

                    neighbors[3].piece.transform.position = new Vector3(x, (gridManager.height + (neighbors[3].y)) , 0);
                    neighbors[3].piece.GetComponent<SpriteRenderer>().enabled = true;
                    neighbors[3].piece.color = gridManager.Colors[Random.Range(0,gridManager.Colors.Length)];
                    neighbors[3].piece.GetComponent<SpriteRenderer>().color = neighbors[3].piece.color;
                    neighbors[3].piece.transform.DOMove(neighbors[3].transform.position, moveTime);
                    isRemove = false;
                }
                neighbors[3].GetAbovePieceTile();
            }
            else
            {
                piece.transform.position = new Vector3(x, gridManager.height + y, 0);
                piece.GetComponent<SpriteRenderer>().enabled = true;
                piece.color = gridManager.Colors[Random.Range(0, gridManager.Colors.Length)];
                piece.GetComponent<SpriteRenderer>().color = piece.color;
                piece.transform.DOMove(transform.position, moveTime);

                isRemove = false;
            }
        }
    }
    IEnumerator DelayToShiftTileDown()
    {
        yield return new WaitForSeconds(0.1f);
        gridManager.ShiftTilesDown();
    }
}
