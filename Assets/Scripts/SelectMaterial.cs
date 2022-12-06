using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMaterial : MonoBehaviour
{
    [SerializeField]
    private Material selectable;
    [SerializeField]
    private Material standart;
    [SerializeField]
    private Material playable;


    public Material GetSelectable()
    {
        return selectable;
    }
    public Material GetStandart()
    {
        return standart;
    }
    public Material GetPlayable()
    {
        return playable;
    }
}
