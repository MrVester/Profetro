using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    private int PlayerID;
    private SelectableTile CurrentTile;
    private SelectableUnit CurrentUnit;
    private Ray ray;
    RaycastHit hit;
    GameObject LastSelectedTile;
    GameObject LastSelectedbUnit;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        TileSelectLogic();
        UnitSelectLogic();
    }

    private void UnitSelectLogic()
    {
        if (Physics.Raycast(ray, out hit))
        {
            SelectableUnit selectable = hit.collider.gameObject.GetComponent<SelectableUnit>();
            if (selectable && selectable.IsAccessibleForPlayer(PlayerID))
            {
                if (Input.GetMouseButtonDown(0) && LastSelectedTile.transform.childCount == 0)
                {
                    selectable.CLickUnit(LastSelectedTile);
                }

                if (CurrentUnit && CurrentUnit != selectable)
                {
                    CurrentUnit.Deselect();
                }
                CurrentUnit = selectable;
                selectable.Select();
            }
            else
            if (CurrentUnit)
            {
                CurrentUnit.Deselect();
                CurrentUnit = null;
            }
        }
        else
        if (CurrentUnit)
        {
            CurrentUnit.Deselect();
            CurrentUnit = null;
        }
    }

    private void TileSelectLogic()
    {
        if (Physics.Raycast(ray, out hit))
        {
            SelectableTile selectable = hit.collider.gameObject.GetComponent<SelectableTile>();
            if (selectable && selectable.IsAccessibleForPlayer(selectable.gameObject, PlayerID ) )
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if (LastSelectedTile != selectable.gameObject && LastSelectedTile)
                    {
                        LastSelectedTile.GetComponent<SelectableTile>().DropTile();
                        LastSelectedTile = selectable.CLickTile();
                    }
                    else
                    {
                        LastSelectedTile = selectable.CLickTile();
                    }
                    
                }

                if (CurrentTile && CurrentTile != selectable)
                {
                    CurrentTile.Deselect();
                }
                CurrentTile = selectable;
                selectable.Select();
                
            }
            else
            if (CurrentTile)
            {
                
                CurrentTile.Deselect();
                CurrentTile = null;
            }
        }
        else
        if (CurrentTile)
        {
            CurrentTile.Deselect();
            CurrentTile = null;
        }
    }
}
