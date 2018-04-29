using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour {

    public Camera gameCamera = null;
    public TileGrid tileGrid = null;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(gameCamera, "gameCamera must be set!");
        Assert.IsNotNull(tileGrid, "tileGrid must be set!");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray mouseRay = gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out hit))
            {
                GameObject hitObj = hit.transform.gameObject;
                Tile hitTile = hitObj.GetComponent<Tile>();

                if (!hitTile)
                {
                    hitTile = tileGrid.getTileFromQuad(hitObj);
                }

                if (hitTile)
                {
                    if (hitTile.CanBurn())
                    {
                        hitTile.StartBurn();
                    }
                }


            }

        }
    }
}
