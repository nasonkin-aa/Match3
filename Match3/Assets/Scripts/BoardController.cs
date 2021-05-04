using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;

    private int Xs, Ys;
    private List<Sprite> TileSprite = new List<Sprite>();
    private Tile[,] TileArray;

    private Tile OldSelectTile;
    private Tile CacheOldSelectTile;
    private Vector2[] DirRay = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private bool IsFindMatch = false;
    private bool IsShift = false;
    private bool IsSearchEmptyTile = false;
    //private bool Check = true;

    public void SetValue(Tile[,] TileArray,int Xs, int Ys, List<Sprite> TileSprite)
    {
        this.TileArray = TileArray;
        this.Xs = Xs;
        this.Ys = Ys;
        this.TileSprite = TileSprite;
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSearchEmptyTile)
        {
            SearchEmtyTile();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (ray != false)
            {
                CheckSelectTile(ray.collider.gameObject.GetComponent<Tile>());
            }       
        }
       
        
    }
    private void SelectTile(Tile tile)
    {
        tile.IsSelected = true;
        tile.SpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        OldSelectTile = tile;
        CacheOldSelectTile = tile;//
    }

    private void DeselecTile(Tile tile)
    {
        tile.IsSelected = false;
        tile.SpriteRenderer.color = new Color(1, 1, 1);
        CacheOldSelectTile = OldSelectTile;//
        OldSelectTile = null;
    }

    private void CheckSelectTile(Tile tile)
    {
        if (tile.IsEmpty || IsShift)
        {
            return;
        }
        if (tile.IsSelected)
        {
            DeselecTile(tile);
        }
        else
        {
            if (!tile.IsSelected && OldSelectTile == null)
            {
                SelectTile(tile);
            }
            else
            {
                if (AdjacentTiles().Contains(tile))
                {
                    SwapTwoTiles(tile);
                    FindAllMatch(tile);
                    DeselecTile(OldSelectTile);
                }
                else
                {
                    DeselecTile(OldSelectTile);
                    SelectTile(tile);
                }
            }
        }
    }

    IEnumerator Delay(Tile tile)
    {

        yield return new WaitForSeconds(0.2f);
        //Debug.Log("lol");
        if (CacheOldSelectTile != null) 
        {
            Sprite CashSprite = tile.SpriteRenderer.sprite;
            tile.SpriteRenderer.sprite = CacheOldSelectTile.SpriteRenderer.sprite;
            CacheOldSelectTile.SpriteRenderer.sprite = CashSprite;
        }
            DeselecTile(tile);
    }
    private List<Tile> FindMatch(Tile tile, Vector2 dir)
    {
        List<Tile> CashFindTile = new List<Tile>();
        RaycastHit2D Hit = Physics2D.Raycast(tile.transform.position, dir);
        while(Hit.collider != null && Hit.collider.GetComponent<Tile>().SpriteRenderer.sprite == tile.SpriteRenderer.sprite)
        {
            CashFindTile.Add(Hit.collider.gameObject.GetComponent<Tile>());
            Hit = Physics2D.Raycast(Hit.collider.gameObject.transform.position, dir);
        }
        return CashFindTile;
    }


    private void SwapTwoTiles(Tile tile)
    {

        if (OldSelectTile.SpriteRenderer.sprite == tile.SpriteRenderer.sprite)
        {
            return;
        }
        Sprite CashSprite = OldSelectTile.SpriteRenderer.sprite;
        OldSelectTile.SpriteRenderer.sprite = tile.SpriteRenderer.sprite;
        tile.SpriteRenderer.sprite = CashSprite;

            StartCoroutine(Delay(tile));
    }

    private void DeleteSprite(Tile tile, Vector2[] dirArray)
    {
     
        List<Tile> CashFindSprite = new List<Tile>();
        for (int i = 0; i < dirArray.Length; i++)
        {
            CashFindSprite.AddRange(FindMatch(tile, dirArray[i]));
        }
        if (CashFindSprite.Count >= 2)
        {

            for (int i = 0; i < CashFindSprite.Count;i++)
            {
               
                CashFindSprite[i].SpriteRenderer.sprite = null;
            }
            IsFindMatch = true;
        }
    }

    private void FindAllMatch(Tile tile)
    {
        if (tile.IsEmpty)
        {
            return;
        }
        DeleteSprite(tile, new Vector2[2] { Vector2.up, Vector2.down });
        DeleteSprite(tile, new Vector2[2] { Vector2.left, Vector2.right });

        if (IsFindMatch)
        {
            IsFindMatch = false;
            tile.SpriteRenderer.sprite = null;
            IsSearchEmptyTile = true;
        }
    }


    private List<Tile> AdjacentTiles()
    {
        List<Tile> CashTile = new List<Tile>();
        for(int i = 0; i < DirRay.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(OldSelectTile.transform.position,DirRay[i]);
            if (hit.collider != null)
            {
                CashTile.Add(hit.collider.gameObject.GetComponent<Tile>());
            }
        }
        return CashTile;
    }

    private void SearchEmtyTile()
    {
        for (int x = 0; x < Xs; x++)
        {
            for (int y = Ys - 1; y > -1; y--)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    ShiftTileDown(x, y);

                }
            }
        }
        IsSearchEmptyTile = false;
        for (int x = 0; x < Xs; x++)
        {
            for (int y = 0; y < Ys; y++)
            {
                FindAllMatch(TileArray[x, y]);

            }
        }
      
    }

    private void ShiftTileDown(int Xpos, int Ypos)
    {
    
        IsShift = true;
        List<SpriteRenderer> CashRenderer = new List<SpriteRenderer>();
        int Count = 0;
        for (int y = Ypos; y < Ys; y++)
        {
            Tile tile = TileArray[Xpos, y];
            if (tile.IsEmpty)
            {
                Count++;
            }
            CashRenderer.Add(tile.SpriteRenderer);

        }
       
        for (int i = 0; i < Count; i++)
        {
            
            //Check = false;
            UI.Insctance.AddScore(10);
            SetNewSprite(Xpos, CashRenderer);
        }
        OldSelectTile = null;
        CacheOldSelectTile = null;
      //  Check = false;
        IsShift = false;
    }
    private void SetNewSprite(int Xpos, List<SpriteRenderer> renderers)
    {
        for (int y = 0; y < renderers.Count - 1; y++)
        {
            renderers[y].sprite = renderers[y + 1].sprite;
            renderers[y + 1].sprite = GetNewSprite(Xpos, Ys - 1);
        }
    }
    private Sprite GetNewSprite(int Xpos, int Ypos)
    {
        List<Sprite> CashSprite = new List<Sprite>();
        CashSprite.AddRange(TileSprite);

        if (Xpos > 0)
        {
            CashSprite.Remove(TileArray[Xpos - 1, Ypos].SpriteRenderer.sprite);
        }
        if (Xpos < Xs - 1)
        {
            CashSprite.Remove(TileArray[Xpos + 1, Ypos].SpriteRenderer.sprite);
        }
        if (Ypos > 0)
        {
            CashSprite.Remove(TileArray[Xpos, Ypos - 1].SpriteRenderer.sprite);
        }
        return CashSprite[Random.Range(0, CashSprite.Count)];

    }

}
