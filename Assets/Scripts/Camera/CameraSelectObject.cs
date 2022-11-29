using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSelectObject : MonoBehaviour
{

    [SerializeField]
    private Selectable CurrentSelectable;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
            if (selectable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    selectable.CLickTile();
                }

                if (CurrentSelectable && CurrentSelectable != selectable)
                {
                    CurrentSelectable.Deselect();
                }
                CurrentSelectable = selectable;
                selectable.Select();
            }
            else
            if (CurrentSelectable)
            {
                CurrentSelectable.Deselect();
                CurrentSelectable = null;
            }
        }
        else
        if (CurrentSelectable)
        {
            CurrentSelectable.Deselect();
            CurrentSelectable = null;
        }
    }
}
