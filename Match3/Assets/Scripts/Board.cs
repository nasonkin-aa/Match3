using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    private int Xs, Ys;
    private Tile TileGo;
    private List<Sprite> TileSprite = new List<Sprite>();
    private void Awake()
    {
        Instance = this;
    }

    public Tile[,] SetValue(int Xs, int Ys, Tile TileGo, List<Sprite> TileSprite)
    {
        this.Xs = Xs;
        this.Ys = Ys;
        this.TileGo = TileGo;
        this.TileSprite = TileSprite;

        return CreateBoard();
    }

    private Tile[,] CreateBoard()
    {
        Tile[,] TileArray = new Tile[Xs, Ys];
        float Xpos = transform.position.x;
        float Ypos = transform.position.y;
        Vector2 TileSize = TileGo.SpriteRenderer.bounds.size;

        Sprite CashSprite = null;
        
        for (int x = 0; x < Xs; x++)
        {
            for( int y = 0; y <Ys; y++)
            {
                Tile NewTile = Instantiate(TileGo, transform.position, Quaternion.identity);
                NewTile.transform.position = new Vector3(Xpos + (TileSize.x * x), Ypos + (TileSize.y * y), 0);
                NewTile.transform.parent = transform;
                TileArray[x, y] = NewTile;

                List<Sprite> TempStrite = new List<Sprite>();
                TempStrite.AddRange(TileSprite);

                TempStrite.Remove(CashSprite);
                if(x > 0)
                {
                    TempStrite.Remove(TileArray[x - 1, y].SpriteRenderer.sprite);

                }

                NewTile.SpriteRenderer.sprite = TempStrite[Random.Range(0, TempStrite.Count)];
                CashSprite = NewTile.SpriteRenderer.sprite;
            }
        }
        return TileArray;
    }
}
