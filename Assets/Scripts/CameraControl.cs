using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraControl : MonoBehaviour
{
    const float PI = 3.14159f;
    const float animationDuration = 0.5f;

    public Texture2D moveCursorTexture;

    [Range(0f, 1f)]
    public float camTarAngle = 0.5f;
    [Range(2f, 10f)]
    public float radius = 5;

    public float scrollScale = 1;

    public GameObject target;

    public float circularAngle;

    private int modenum = 0;
    public bool isCamLerp = false;
    Vector3 viewCamPos;
    Vector3 boardCamPos;

    private delegate void CameraMode();
    private CameraMode cammode;


    private Vector3 deltaMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 previousMousePosition;
    private Vector3 mousePositionSum;



    private void OnEnable()
    {
        cammode = ViewMode;
    }

    private void Update()
    {
        SetCursor();
        if (!isCamLerp)
        {
            cammode();
            ChangeAngleAndRad();
        }


        if (Input.GetKeyDown("space") && !isCamLerp)
        {
            SwitchCameraMode();

        }

        SetMousePosition();

    }

    private void SetCursor()
    {
        if (Input.GetMouseButton(2) && !isCamLerp)
        {
            //Vector2 moveCursorCenter = new Vector2(moveCursorTexture.Size().x / 2, moveCursorTexture.Size().y / 2);
            Cursor.SetCursor(moveCursorTexture, new Vector2(16, 16), CursorMode.ForceSoftware);
        }
        else
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void ChangeAngleAndRad()
    {
        camTarAngle = mousePositionSum.y;
        radius -= Input.mouseScrollDelta.y * scrollScale;
        radius = Mathf.Clamp(radius, 3, 10);
    }

    private void SetMousePosition()
    {
        previousMousePosition = currentMousePosition;
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(2) && !isCamLerp)
        {

            deltaMousePosition = currentMousePosition - previousMousePosition;
            mousePositionSum += deltaMousePosition * Time.deltaTime * 0.25f;
            mousePositionSum.y = Mathf.Clamp01(mousePositionSum.y);

        }
        Debug.Log(mousePositionSum);
    }

    private void SwitchCameraMode()
    {

        switch (modenum)
        {
            case 0:
                boardCamPos = new Vector3(target.transform.position.x, target.transform.position.y + radius, target.transform.position.z);

                StartCoroutine(CameraLerp(transform.position, boardCamPos));

                cammode = BoardMode;

                modenum++;
                break;
            case 1:
                //angle
                circularAngle = mousePositionSum.x * 2 * PI;
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
        circularAngle = mousePositionSum.x * 2 * PI;
        //camera position on a circle
        viewCamPos = new Vector3(target.transform.position.x + Mathf.Cos(circularAngle) * radius, radius * Mathf.Tan(camTarAngle), target.transform.position.z + Mathf.Sin(circularAngle) * radius);
        transform.position = viewCamPos;
        //gameobject looks at our target
        transform.LookAt(target.transform, Vector3.up);
    }

    private void BoardMode()
    {
        boardCamPos = new Vector3(target.transform.position.x, target.transform.position.y + radius, target.transform.position.z);
        transform.position = boardCamPos;

        transform.rotation = Quaternion.Euler(90, -mousePositionSum.x * 360, 90);
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
