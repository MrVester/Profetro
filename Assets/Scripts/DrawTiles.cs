using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTiles : MonoBehaviour
{
    public TileManager tiles;
    public int PlayerID;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var tileArray = tiles.GetAvailableTilesForPlayer(PlayerID);

            foreach (var tile in tileArray)
            {
                tile.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
