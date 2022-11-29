using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetSelectableChilds : MonoBehaviour
{

    void Start()
    {
        Transform[] father = GetComponentsInChildren<Transform>();

        foreach (var child in father)
        {
            if (child.name != "_Border" && child.name != "_Center")
            {
                child.gameObject.AddComponent<MeshCollider>();
                child.gameObject.AddComponent<Selectable>();
            }
        }
    }

}
