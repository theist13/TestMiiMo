using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Normal,
    Bomb,
    Disco
}
public class Piece : MonoBehaviour
{
    public PieceType tileType; // Enum for different tile types
    public Color color;
    [SerializeField]private SpriteRenderer normalPiece;
    [SerializeField]private SpriteRenderer specialPiece;

    public void Init(PieceType type , Color clr)
    {
        tileType = type;
        color = clr;

        //normalPiece = GetComponent<SpriteRenderer>();
        normalPiece.color = color;
        //specialPiece = GetComponentInChildren<SpriteRenderer>();
        specialPiece.enabled = false;
    }

    public void SetPieceType(PieceType type , Color clr)
    {
        color = clr;
        tileType = type;
        switch (type)
        {
            case PieceType.Normal:
                normalPiece.color = clr;
                specialPiece.enabled = false;
                //Debug.Log("Set this to normal");
                break;
            case PieceType.Bomb:
                Debug.Log("Set this to bomb");
                normalPiece.color = clr;
                specialPiece.enabled = true;
                specialPiece.color = Color.white;
                break; 
            case PieceType.Disco:
                Debug.Log("Set this to disco");
                normalPiece.color = Color.black;
                specialPiece.enabled = true;
                specialPiece.color = clr;
                break;
        }
    }
    public void EnablePiece(bool isEnable)
    {
        normalPiece.enabled = isEnable;
        specialPiece.enabled = isEnable;
    }
}
