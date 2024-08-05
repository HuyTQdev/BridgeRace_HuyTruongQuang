using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

public class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] List<DataColor> dataColors;
    [SerializeField] Character player;

    [SerializeField] int numBlock;
    [SerializeField] Transform[] characterTfs;
    [SerializeField] int numPlayer;
    [SerializeField] List<Platform> platforms;
    DataColor playerColor;

    private void Start()
    {
        Addressables.LoadAssetAsync<DataColorList>("DataColorList").Completed += (op) =>
        {
            dataColors = op.Result.dataColors;
            StartGenerate();

        };
    }
    private void StartGenerate()
    {
        playerColor = dataColors[PlayerPrefs.HasKey("Color") ? PlayerPrefs.GetInt("Color") : 0];
        foreach (Platform platform in platforms)
        {
            platform.Init();
        }


        for (int i = 0; i < numPlayer; i++)
        {
            if (dataColors[i] != playerColor)
            {
                ObjectPool.Instance.GetObject("Enemy", characterTfs[i].position)
                    .GetComponent<Enemy>().Generate(dataColors[i], platforms[0]);
            }else
            {
                player.transform.position = characterTfs[i].position;
                player.Generate(playerColor, platforms[0]);
            }
        }

    }
}
