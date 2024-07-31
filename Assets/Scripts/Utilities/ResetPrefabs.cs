using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefabs : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("CurGem", 0);
        PlayerPrefs.SetInt("CurLevel", 0);
    }

}
