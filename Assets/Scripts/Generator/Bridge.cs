using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] Transform gate;
    [SerializeField] Transform startTf;
    [SerializeField] Vector3 stepSize;
    [SerializeField] int numBrick;
    [SerializeField] GameObject virtualBlocks;
    public BuildableBrickScript FirstBrick { get; private set; }
    public BuildableBrickScript LastBrick { get; private set; }
    public Vector3 GatePosition { get; private set; }
    public Vector3 StartPosition { get; private set; }
    public void Init(Platform platform)
    {
        virtualBlocks.SetActive(false);
        GatePosition = gate.position;
        StartPosition = startTf.position - Vector3.forward;
        for(int i = 0; i < numBrick; i++)
        {
            if (i == 0)
            {
                FirstBrick =
                ObjectPool.Instance.GetObject("BuildableBrick",
                    startTf.position + i * stepSize.z * Vector3.forward + i * stepSize.y * Vector3.up)
                    .GetComponent<BuildableBrickScript>();
                FirstBrick.Generate(platform, stepSize);
            }
            else if (i == numBrick - 1)
            {
                LastBrick = 
                ObjectPool.Instance.GetObject("BuildableBrick",
                    startTf.position + i * stepSize.z * Vector3.forward + i * stepSize.y * Vector3.up)
                    .GetComponent<BuildableBrickScript>();
                LastBrick.Generate(platform, stepSize);
            }
            else
            {
                ObjectPool.Instance.GetObject("BuildableBrick",
                    startTf.position + i * stepSize.z * Vector3.forward + i * stepSize.y * Vector3.up)
                    .GetComponent<BuildableBrickScript>().Generate(platform, stepSize);
            }
        }
    }

}
