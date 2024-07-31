using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] List<DataColor> dataColors;
    [SerializeField] Character player;

    [SerializeField] int numBlock;
    [SerializeField] Transform[] characterTfs;
    [SerializeField] int numPlayer;
    [SerializeField] List<Platform> platforms;
    [SerializeField] DataColor playerColor;

    private void Start()
    {
        StartGenerate();
    }
    private void StartGenerate()
    {
        foreach (Platform platform in platforms)
        {
            platform.Init();
        }

        player.transform.position = characterTfs[0].position;
        player.Generate(playerColor, platforms[0]);

        for (int i = 0; i < numPlayer - 1; i++)
        {
            if (dataColors[i] != playerColor)
            {
                ObjectPool.Instance.GetObject("Enemy", characterTfs[i].position)
                    .GetComponent<Enemy>().Generate(dataColors[i], platforms[0]);
            }else
            {
                numPlayer += 1;
            }
        }

    }
}
