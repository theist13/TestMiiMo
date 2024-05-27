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
        switch (piece.tileType)
        {
            case PieceType.Normal:
                gridManager.ResetMatchTile();
                gridManager.clickPieceColor = piece.color;
                RemovePieceInTileAndCheckNeighbor();
                break;
            case PieceType.Bomb:
                gridManager.RemovePieceByBomb(x, y);
                break;
            case PieceType.Disco:
                gridManager.RemovePieceByDisco(piece.color);
                break;
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
    public void RemovePiece()
    {
        isRemove = true;
        piece.EnablePiece(false);
    }
    public void RemovePieceInTileAndCheckNeighbor()
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
                    gridManager.normalAmoutToRemove++;
                    neighbors[i].RemovePiece();
                    neighbors[i].RemovePieceInTileAndCheckNeighbor();
                }
            }
        }

        if (!foundSameColorNeighbor && !gridManager.isExcuteTileShift)
        {
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
                    neighbors[3].piece.EnablePiece(true);
                    neighbors[3].piece.color = gridManager.Colors[Random.Range(0,gridManager.Colors.Length)];
                    neighbors[3].piece.SetPieceType(neighbors[3].piece.tileType, neighbors[3].piece.color);
                    neighbors[3].piece.transform.DOMove(neighbors[3].transform.position, moveTime);
                    isRemove = false;
                }
                neighbors[3].GetAbovePieceTile();
            }
            else
            {

                piece.transform.position = new Vector3(x, gridManager.height + y, 0);
                piece.EnablePiece(true);


                if(gridManager.specialTypeToadd == PieceType.Normal)
                {
                    piece.SetPieceType(PieceType.Normal, gridManager.Colors[Random.Range(0, gridManager.Colors.Length)]);
                }
                if (gridManager.specialTypeToadd == PieceType.Bomb)
                {
                    //For random special piece position
                    int rand = Random.Range(0, gridManager.normalAmoutToRemove);
                    if(rand == 0)
                    {
                        piece.SetPieceType(PieceType.Bomb, gridManager.Colors[Random.Range(0, gridManager.Colors.Length)]);
                        gridManager.specialTypeToadd = PieceType.Normal;
                    }
                    else
                    {
                        piece.SetPieceType(PieceType.Normal, gridManager.Colors[Random.Range(0, gridManager.Colors.Length)]);
                    }
                }
                if (gridManager.specialTypeToadd == PieceType.Disco)
                {
                    //For random special piece position
                    int rand = Random.Range(0, gridManager.normalAmoutToRemove);
                    if(rand == 0)
                    {
                        piece.SetPieceType(PieceType.Disco, gridManager.clickPieceColor);
                        gridManager.specialTypeToadd = PieceType.Normal;
                    }
                    else
                    {
                        piece.SetPieceType(PieceType.Normal, gridManager.Colors[Random.Range(0, gridManager.Colors.Length)]);
                    }
                }

                gridManager.normalAmoutToRemove--;

                piece.transform.DOMove(transform.position, moveTime);

                isRemove = false;

                //Debug.Log($"Create new piece");
            }
        }
    }
    IEnumerator DelayToShiftTileDown()
    {
        yield return new WaitForSeconds(0.1f);
        gridManager.ShiftTilesDown();
    }
}
