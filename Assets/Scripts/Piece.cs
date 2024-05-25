using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Bobm,
    Disco
}
public class Piece : MonoBehaviour
{
    public TileType tileType; // Enum for different tile types
    public Color color;

    public void Init(TileType type , Color clr)
    {
        tileType = type;
        color = clr;

        GetComponent<SpriteRenderer>().color = color;
    }
}
