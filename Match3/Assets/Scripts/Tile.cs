using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public bool IsSelected;
    public bool IsEmpty
    {
        get
        {
            return SpriteRenderer.sprite == null ? true : false;
        }
    }

}
