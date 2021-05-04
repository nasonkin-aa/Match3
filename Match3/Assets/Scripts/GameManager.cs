using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardSettings
{
    public int Xs, Ys;
    public Tile TileGo;
    public List<Sprite> TileSprite;
}

public class GameManager : MonoBehaviour
{
    [Header("Game board parameters")]
    public BoardSettings boardSettings;
    // Start is called before the first frame update
    void Start()
    {
            BoardController.Instance.SetValue(Board.Instance.SetValue(boardSettings.Xs, boardSettings.Ys, boardSettings.TileGo, boardSettings.TileSprite),
            boardSettings.Xs, boardSettings.Ys,
            boardSettings.TileSprite);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
