using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class GridCell : MonoBehaviour
{
    public SpriteRenderer cellSprite;
    public SpriteRenderer xSprite;

    [HideInInspector]
    public bool isFull;

    private int posX;
    private int posY;
    private Vector3 initXScale;
    private Color initXColor;

    private void Start()
    {
        initXColor = xSprite.color;
        initXScale = xSprite.transform.localScale;
    }

    public void FillCell()
    {
        if (isFull)
            return;
        isFull = true;

        // Tween Anims
        xSprite.transform.DOKill();

        xSprite.transform.localScale = Vector3.zero;
        xSprite.color = Color.white;
        xSprite.gameObject.SetActive(true);

        xSprite.transform.DOScale(initXScale, 0.2f).SetEase(Ease.OutBack);
        xSprite.DOColor(initXColor, 0.2f).SetEase(Ease.InOutSine);
    }

    public void ResetCell()
    {
        StartCoroutine(waitAndResetCell(0.2f));
    }

    private IEnumerator waitAndResetCell(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        isFull = false;

        // Tween Anims
        xSprite.transform.DOKill();
        xSprite.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            xSprite.gameObject.SetActive(false);
        });
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
