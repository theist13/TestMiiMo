using MiscUtil.Xml.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            Debug.Log($"{x},{y}");
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
                    gridManager.foundAnyMatch = true;
                    neighbors[i].isRemove = true;
                    neighbors[i].DestroyPieceInTile();
                    neighbors[i].piece.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                    gridManager.foundAnyMatch = false;
            }
            else
                gridManager.foundAnyMatch = false;
        }

        if (!foundSameColorNeighbor && !gridManager.CheckIfAnyMatchFound() && gridManager.amoutToRemove > 0)
        {
            // gridManager.ShiftTilesDown();
            StartCoroutine(test());
        }
    }
    public void GetAbovePieceTile()
    {
        if(isRemove)
        {
            if (neighbors[3] && !neighbors[3].isRemove)
            {
                Piece temp = piece;
                piece = neighbors[3].piece;
                neighbors[3].piece = temp;

                neighbors[3].isRemove = true;

                //piece.transform.position = transform.position;
                //neighbors[3].piece.transform.position = neighbors[3].transform.position;
                StartCoroutine(Lerp(piece.transform, transform.position, 1));

                neighbors[3].piece.transform.position = new Vector3(x, gridManager.height + 1, 0);
                neighbors[3].piece.GetComponent<SpriteRenderer>().enabled = true;
                neighbors[3].piece.GetComponent<SpriteRenderer>().color = Color.black;
                StartCoroutine(Lerp(neighbors[3].piece.transform, neighbors[3].transform.position, 1));

                isRemove = false;

                neighbors[3].GetAbovePieceTile();
            }
            //Debug.Log($"{x},{y} is empty");
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
    IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
        gridManager.ShiftTilesDown();
    }
}
