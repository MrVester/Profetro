using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelectableUnit : MonoBehaviour
{
    const float animationDuration = 0.1f;

    private Color selectColor = Color.green;
    private Color deselectColor = Color.white;

    private bool isRotating = false;

    /* public Material selectMat;
     public Material deselectMat;*/

    /* public Selectable(RotatePlayingArea rotatePlayingArea)
     {
         this.rotatePlayingArea = rotatePlayingArea;
     }*/

    private void Start()
    {
        RotatePlayingArea.onRotationStartEvent.AddListener(SetisRotationTrue);
        RotatePlayingArea.onRotationEndEvent.AddListener(SetisRotationFalse);
        
    }
    private void SetisRotationTrue()
    {
        isRotating = true;
    }
    private void SetisRotationFalse()
    {
 
        isRotating = false;
    }
    public void Select()
    {

        GetComponent<Renderer>().material.color = selectColor;

    }
    public void Deselect()
    {
        GetComponent<Renderer>().material.color = deselectColor;

    }
    public void CLickUnit(GameObject LastSelectableTile)
    {
        if (LastSelectableTile )
        {
            this.transform.SetParent(LastSelectableTile.transform);
            transform.position = new Vector3(LastSelectableTile.transform.position.x, LastSelectableTile.transform.position.y+0.5f, LastSelectableTile.transform.position.z);
        }
    }
    public bool IsAccessibleForPlayer(int PlayerID)
    {
        if(this.transform.parent!= null)
        {
            var tileArray = this.transform.root.GetComponent<TileManager>().GetAvailableTilesForPlayer(PlayerID);
            foreach (var tile in tileArray)
            {
                if (this.transform.parent.gameObject == tile)
                {
                    return true;
                }
            }
            return false;
        }
        return true;
        
    }


}
