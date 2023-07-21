using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IconStorage", menuName = "IconStorage", order = 0)]
[Serializable]
public class UnitIconStorage : ScriptableObject
{
    [SerializeField] public UnitIcon[] UnitIcons;
}