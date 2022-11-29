using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Selectable : MonoBehaviour
{
    const float animationDuration = 0.1f;

    private Color selectColor = Color.green;
    private Color deselectColor = Color.white;

    private bool isTileSelected = false;
    private bool isTileLerp = false;
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
        RotatePlayingArea.onRotationChangedEvent.AddListener(SetisRotationFalse);
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
    public void CLickTile()
    {
        //Если объект не поворачивается
        if (isRotating) return;
        //Если выбран
        if (!isTileLerp && !isTileSelected)

        {
            StartCoroutine(TileLerp(transform.position, transform.position + new Vector3(0, 0.1f, 0)));
            isTileSelected = true;
        }
        else
            //Если не выбран
            if (!isTileLerp && isTileSelected)

        {
            StartCoroutine(TileLerp(transform.position, transform.position - new Vector3(0, 0.1f, 0)));
            isTileSelected = false;
        }
    }
    private IEnumerator TileLerp(Vector3 startpoint, Vector3 endpoint)
    {
        isTileLerp = true;
        float t = 0;
        while (t < 1)
        {

            transform.position = Vector3.Slerp(startpoint, endpoint, t);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
        transform.position = endpoint;
        isTileLerp = false;
    }


}
