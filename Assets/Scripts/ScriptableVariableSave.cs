using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Save Variable")]
public class ScriptableVariableSave : ScriptableObject
{
    public int controllerSelected;
    public int keyboardSelected;
    public float timeLeftRed;
    public float sprintRechargeRed;
    public float timeLeftBlue;
    public float sprintRechargeBlue;
}
