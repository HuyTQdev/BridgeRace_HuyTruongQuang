using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/DataColor")]
[System.Serializable]
public class DataColor : ScriptableObject
{
    [field: SerializeField] public Material charMat { get; private set; }
    [field: SerializeField] public Material brickMat{get; private set;}
    [field: SerializeField] public string name { get; private set; }

    [field: SerializeField] public Color renderColor { get; private set; }
}
