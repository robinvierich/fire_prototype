using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PlayerActionType
{
    None,
    Move,
    SpreadFire
}

[System.Serializable]
public class PlayerAction
{
    public PlayerActionType type;
}

public class Player : MonoBehaviour {

    public Vector2Int gridPos = new Vector2Int(0,0);
    public TileGrid tileGrid = null;

    public readonly PlayerAction[] playerActions = new PlayerAction[2]
    {
        new PlayerAction {type = PlayerActionType.Move},
        new PlayerAction {type = PlayerActionType.SpreadFire}
    };

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(tileGrid, "tileGrid must be set!");
    }


    public PlayerActionType handleTileSelected(Tile tile)
    {
        if (tileGrid.isAdjacent(tile.gridPos, gridPos))
        {
            if (tile.CanBurn())
            {
                return PlayerActionType.SpreadFire;
            }
        }

        if (tile.isOnFire())
        {
            // #TODO if (isConnected)
            return PlayerActionType.Move;
        }

        return PlayerActionType.None;
    }

    // Update is called once per frame
    void Update () {

    }
}
