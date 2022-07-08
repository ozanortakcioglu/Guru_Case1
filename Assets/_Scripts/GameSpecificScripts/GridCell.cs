using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridCell : MonoBehaviour
{

    public SpriteRenderer cellSprite;
    public SpriteRenderer xSprite;

    [HideInInspector]
    public bool isFull;

    private int posX;
    private int posY;

    public void FillCell()
    {
        if (isFull)
            return;
        isFull = true;
        //some anims
    }

    public void ResetCell()
    {
        isFull = false;
        //some anims
    }

    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
