using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraModSwitcher : MonoBehaviour
{
    const float PI = 3.14159f;
    const float animationDuration = 0.5f;

    [Range(0f, 1f)]
    public float camTarAngle = 0.5f;
    [Range(2f, 10f)]
    public float radius = 10;

    public GameObject target;
    public Slider slider;

    public float circularAngle;

    private int modenum = 0;
    public bool isCamLerp = false;
    Vector3 viewCamPos;
    Vector3 boardCamPos;

    private delegate void CameraMode();
    private CameraMode cammode;



    private void Start()
    {
        cammode = ViewMode;
    }

    private void Update()
    {
        if (!isCamLerp)
        {
            cammode();
        }


        if (Input.GetKeyDown("space") && !isCamLerp)
        {
            SwitchCameraMode();
        }


    }

    public void SwitchCameraMode()
    {

        switch (modenum)
        {
            case 0:
                boardCamPos = new Vector3(target.transform.position.x, target.transform.position.y + 10, target.transform.position.z);

                StartCoroutine(CameraLerp(transform.position, boardCamPos));

                cammode = BoardMode;

                modenum++;
                break;
            case 1:
                //angle
                circularAngle = slider.value * 2 * PI;
                //camera position on a circle
                viewCamPos = new Vector3(target.transform.position.x + Mathf.Cos(circularAngle) * radius, radius * Mathf.Tan(camTarAngle), target.transform.position.z + Mathf.Sin(circularAngle) * radius);

                StartCoroutine(CameraLerp(transform.position, viewCamPos));

                cammode = ViewMode;

                modenum = 0;
                break;
        }

    }

    private void ViewMode()
    {
        //angle
        circularAngle = slider.value * 2 * PI;
        //camera position on a circle
        viewCamPos = new Vector3(target.transform.position.x + Mathf.Cos(circularAngle) * radius, radius * Mathf.Tan(camTarAngle), target.transform.position.z + Mathf.Sin(circularAngle) * radius);
        transform.position = viewCamPos;
        //gameobject looks at our target
        transform.LookAt(target.transform, Vector3.up);
    }

    private void BoardMode()
    {
        boardCamPos = new Vector3(target.transform.position.x, target.transform.position.y + 10, target.transform.position.z);
        transform.position = boardCamPos;
        // transform.position = Vector3.Lerp(transform.position, boardCamPos, interpolationRatio);
        transform.rotation = Quaternion.Euler(90, -slider.value * 360, 90);
    }

    private IEnumerator CameraLerp(Vector3 startpoint, Vector3 endpoint)
    {
        isCamLerp = true;
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startpoint, endpoint, t);
            transform.LookAt(target.transform, Vector3.up);
            t += Time.deltaTime / animationDuration;
            yield return null;
        }
        isCamLerp = false;
    }

}
