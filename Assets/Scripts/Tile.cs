using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class BurnStateOptions
{
    public BurnState burnState;
    public Texture tileTexture;
}

public class Tile : MonoBehaviour
{
    public BurnState burnState = BurnState.UNBURNT;
    public GameObject quad = null;
    public BurnStateOptions[] burnStateOptions = new BurnStateOptions[(int)BurnState.Count]
    {
        new BurnStateOptions { burnState = BurnState.UNBURNT, tileTexture = null },
        new BurnStateOptions { burnState = BurnState.ON_FIRE, tileTexture = null },
        new BurnStateOptions { burnState = BurnState.BURNT, tileTexture = null },
    };

    private Dictionary<BurnState, Texture> burnStateTextureCache = new Dictionary<BurnState, Texture>();

    public Bounds GetBounds()
    {
        return quad.GetComponent<MeshRenderer>().bounds;
    }

    public bool CanBurn()
    {
        return burnState == BurnState.UNBURNT;
    }

    public void StartBurn()
    {
        Assert.IsTrue(CanBurn(), "Cannot start burn! Burn state is " + burnState.ToString());
        burnState = BurnState.ON_FIRE;
    }

    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(quad, "Quad must be set!");
        Assert.IsNotNull(quad.GetComponent<MeshRenderer>(), "Quad must have a MeshRenderer!");
        Assert.IsNotNull(quad.GetComponent<MeshFilter>(), "Quad must have a MeshFilter!");

        foreach (BurnStateOptions burnStateOption in burnStateOptions)
        {
            Assert.IsNotNull(burnStateOption.tileTexture, "each burn state requires a tile texture to be set! Missing texture for " + burnStateOption.burnState);
            burnStateTextureCache[burnStateOption.burnState] = burnStateOption.tileTexture;
        }
    }


    // Update is called once per frame
    void Update()
    {
        quad.GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", burnStateTextureCache[burnState]);

        //switch(burnState)
        //{
        //case BurnState.UNBURNT:
        //    tileMeshRenderer.materials[0].SetTexture("_MainTex", burnStateTextureCache[burnState]);
        //    break;
        //case BurnState.ON_FIRE:
        //    break;
        //case BurnState.BURNT:
        //    break;
        //default:
        //    throw new System.Exception("Invalid burn state!");
        //}

    }
}
