using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelData", order = 1)]
public class So_LevelData : ScriptableObject
{
    public List<float> x = new List<float>();
    public List<float> y = new List<float>();
    public List<E_BrickType> BrickColorList = new List<E_BrickType>();
}
