using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawTiles : MonoBehaviour
{
    List<GameObject>  tileArray;
    List<GameObject> LassTileArray;
    [SerializeField]
    GameObject RotateArea;
    public int PlayerID;

    void Update()
    {
        if (RotateArea.GetComponent<RotatePlayingArea>().isRotating)
        {
            LassTileArray = this.GetComponent<TileManager>().GetAvailableTilesForPlayer(PlayerID);

            foreach (var tile in tileArray)
            {

                tile.GetComponent<Renderer>().material = this.GetComponent<SelectMaterial>().GetStandart();
            }
        }
        else
        {
            tileArray = this.GetComponent<TileManager>().GetAvailableTilesForPlayer(PlayerID);

            foreach (var tile in tileArray)
            {
                if(!tile.GetComponent<SelectableTile>().IsTileSelected)
                {
                    tile.GetComponent<Renderer>().material = this.GetComponent<SelectMaterial>().GetPlayable();
                }
                
            }
            LassTileArray = tileArray;
        }
    
    }
}
