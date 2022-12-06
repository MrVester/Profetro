using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class RotatePlayingArea : MonoBehaviour
{
    public bool isRotating = false;
    public GameObject target;
    const float animationDuration = 0.5f;

    public static UnityEvent onRotationStartEvent = new UnityEvent();
    public static UnityEvent onRotationEndEvent = new UnityEvent();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RotateObject(45);
    }
    private void RotateObject(float angle)
    {
        if (!isRotating)
        {
            Vector3 tRot = target.transform.rotation.eulerAngles;
            Quaternion startrot = target.transform.rotation;
            Quaternion endrot = Quaternion.Euler(tRot + new Vector3(0, angle, 0));
            StartCoroutine(ObjectRotate(startrot, endrot));
        }
    }
    private IEnumerator ObjectRotate(Quaternion startrot, Quaternion endrot)
    {
        isRotating = true;
        onRotationStartEvent?.Invoke();
        float t = 0;
        while (t < 1)
        {
            target.transform.rotation = Quaternion.Slerp(startrot, endrot, t);
            t += Time.deltaTime / animationDuration;
            //transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
            yield return null;
        }
        target.transform.rotation = endrot;
        isRotating = false;
        onRotationEndEvent?.Invoke();
    }
}
