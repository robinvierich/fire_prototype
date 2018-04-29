using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour {

    public Camera gameCamera = null;
    public TileGrid tileGrid = null;
    public Player player = null;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(gameCamera, "gameCamera must be set!");
        Assert.IsNotNull(tileGrid, "tileGrid must be set!");
        Assert.IsNotNull(player, "player must be set!");
    }

    void MoveToGridPosition(GameObject obj, Vector2Int gridPos, float z)
    {
        Vector3 worldPos = tileGrid.gridToWorldPosition(gridPos);
        worldPos.z = z;
        obj.transform.position = worldPos;
    }

    // Update is called once per frame
    void Update () {
        MoveToGridPosition(player.gameObject, player.gridPos, -1);

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
                    PlayerActionType playerAction = player.handleTileSelected(hitTile);

                    switch(playerAction)
                    {
                    case PlayerActionType.None:
                        break;

                    case PlayerActionType.Move:
                        player.gridPos = hitTile.gridPos;
                        break;

                    case PlayerActionType.SpreadFire:
                        player.gridPos = hitTile.gridPos;
                        hitTile.StartBurn();
                        break;
                    }
                }
            }
        }
    }
}
