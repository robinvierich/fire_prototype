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


    private Vector3 gridToWorldPosition(int x, int y)
    {
        return new Vector3((tileBounds.size.x + padding) * x, (tileBounds.size.y + padding) * y, 0);
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
                Vector3 pos = gridToWorldPosition(x, y);
                GameObject tile =  Instantiate(tilePrefab, pos, Quaternion.identity, this.transform);
                Tile tileScript = tile.GetComponent<Tile>();
                tiles[getTileIndex(x, y)] = tile;
                quadToTileCache[tileScript.quad] = tileScript;
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
