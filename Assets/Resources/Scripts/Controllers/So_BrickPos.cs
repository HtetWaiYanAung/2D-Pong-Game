using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BrickPos", order = 1)]
public class So_BrickPos : ScriptableObject
{
    public List<float> x = new List<float>();
    public List<float> y = new List<float>();
    public List<BrickType> BrickColorList = new List<BrickType>();
    private void OnValidate()
    {
        EditorUtility.SetDirty(this);
    }
}
