using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enum", menuName = "Enums/Create new", order = 1)]
public class EnumDataSO : ScriptableObject
{
    public string enumName;
    public List<string> enumValues;
    
}