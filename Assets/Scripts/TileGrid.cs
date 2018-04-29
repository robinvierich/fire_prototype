using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TileGrid : MonoBehaviour {

    public int width = 32;
    public int height = 32;
    public int padding = 1;

    public GameObject tilePrefab;

    private GameObject[] tiles;

    private Bounds tileBounds;

    private Dictionary<GameObject, Tile> quadToTileCache = new Dictionary<GameObject, Tile>();


    private int getTileIndex(int x, int y)
    {
        return width * y + x;
    }

    private GameObject getTile(int x, int y)
    {
        return tiles[getTileIndex(x, y)];
    }

    public Vector3 gridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3((tileBounds.size.x + padding) * gridPos.x, (tileBounds.size.y + padding) * gridPos.y, 0);
    }

    public bool isAdjacent(Vector2Int gridPos1, Vector2Int gridPos2)
    {
        return Vector2Int.Distance(gridPos1, gridPos2) == 1;
    }

    public GameObject[] getAdjacentTiles(Vector2Int gridPos)
    {
        GameObject[] tiles = { null, null, null, null };
        int currIdx = 0;

        if (gridPos.x > 0)
        {
            tiles[currIdx++] = getTile(gridPos.x - 1, gridPos.y);
        }

        if (gridPos.x < width - 1)
        {
            tiles[currIdx++] = getTile(gridPos.x + 1, gridPos.y);
        }

        if (gridPos.y > 0)
        {
            tiles[currIdx++] = getTile(gridPos.y - 1, gridPos.y);
        }

        if (gridPos.x < width - 1)
        {
            tiles[currIdx++] = getTile(gridPos.y + 1, gridPos.y);
        }

        return tiles;
    }

    public Tile getTileFromQuad(GameObject quad)
    {
        Tile tile = null;
        quadToTileCache.TryGetValue(quad, out tile);
        return tile;
    }

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(tilePrefab, "tilePrefab must be set!");
        Assert.IsNotNull(tilePrefab.GetComponent<Tile>(), "tilePrefab must have a Tile script component!");

        tileBounds = tilePrefab.GetComponent<Tile>().GetBounds();

        tiles = new GameObject[width * height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                Vector3 pos = gridToWorldPosition(gridPos);
                GameObject tile =  Instantiate(tilePrefab, pos, Quaternion.identity, this.transform);
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.gridPos = gridPos;
                tiles[getTileIndex(x, y)] = tile;
                quadToTileCache[tileScript.quad] = tileScript;
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
