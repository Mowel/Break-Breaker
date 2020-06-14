using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] public int points;
    [SerializeField] public int hitsToBreak;
    [SerializeField] private Sprite hitSprite;

    public void BreakBrick()
    {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
    
}
