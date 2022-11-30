using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelectableUnit : MonoBehaviour
{
    const float animationDuration = 0.1f;
    private const float Offset = 0.4f;
    private Color selectColor = Color.green;
    private Color deselectColor = Color.white;
    private bool isUnitOnTile = false;
    private bool isRotating = false;


    private bool IsUnitOmTIle
    {
        get { return isUnitOnTile; }
        set { isUnitOnTile = value; }
    }
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
        if (this.transform.parent ) 
        {
            if (this.transform.parent.GetComponent<SelectableTile>())
            {
                this.transform.parent.GetComponent<SelectableTile>().IsTileTaken = false;
            }
             
        }
            
            SelectableTile SelectableTile = LastSelectableTile.GetComponent<SelectableTile>();
            SelectableTile.IsTileTaken = true;
            this.transform.SetParent(LastSelectableTile.transform);
            transform.position = new Vector3(LastSelectableTile.transform.position.x, LastSelectableTile.transform.position.y+ Offset, LastSelectableTile.transform.position.z);
            SelectableTile.DropTile();
            transform.rotation = Quaternion.Euler(-90, 0, Mathf.Atan2(-transform.position.x, -transform.position.z) * Mathf.Rad2Deg - 180);
            isUnitOnTile = true;

    }
    public bool IsAccessibleForPlayerTile(int PlayerID , GameObject LastSelectedTile)
    {
        
            var tileArray = LastSelectedTile.transform.root.GetComponent<TileManager>().GetAvailableTilesForPlayer(PlayerID);
            foreach (var tile in tileArray)
            {
                if (LastSelectedTile == tile)
                {
                    return true;
                }
            }
            return false;
        
        
    }
    public bool IsAccessibleForPlayerUnit(int PlayerID)
    {
        if (IsUnitOmTIle)
        {


            var tileArray = this.transform.root.GetComponent<TileManager>().GetAvailableTilesForPlayer(PlayerID);
            foreach (var tile in tileArray)
            {
                if (this.transform.parent == tile.transform)
                {
                    return true;
                }
            }
            return false;
        }
        return true;

    }


}
