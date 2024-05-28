using UnityEngine;

public enum PieceType
{
    Normal,
    Bomb,
    Disco
}
public class Piece : MonoBehaviour
{
    public PieceType tileType; 
    public Color color;
    [SerializeField]private SpriteRenderer normalPiece;
    [SerializeField]private SpriteRenderer specialPiece;

    public void Init(PieceType type , Color clr)
    {
        tileType = type;
        color = clr;

        normalPiece.color = color;
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
                break;
            case PieceType.Bomb:
                normalPiece.color = clr;
                specialPiece.enabled = true;
                specialPiece.color = Color.white;
                break; 
            case PieceType.Disco:
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
