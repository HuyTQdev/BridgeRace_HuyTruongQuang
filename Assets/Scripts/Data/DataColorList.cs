using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/DataColorList")]
[System.Serializable]
public class DataColorList : ScriptableObject
{
    [field: SerializeField] public List<DataColor> dataColors { get; private set; }
}
