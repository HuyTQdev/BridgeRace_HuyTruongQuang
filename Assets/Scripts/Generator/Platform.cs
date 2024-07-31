using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Platform : MonoBehaviour
{
    List<EatableBrickScript> spaceforGenBrick;
    [SerializeField]List<EatableBrickScript> activeBrick;
    [SerializeField] Transform bricksTf;
    [SerializeField] Bridge[] bridges;
    [field: SerializeField] public int floor { get; private set; }
    [SerializeField] GameObject block;
    [SerializeField] int numBlockToPassLevel;
    List<Bridge> emptyBridges;
    Dictionary<DataColor, int> colorOnl;

    public List<Transform> CharacterTf { get; private set; }
    public void Init()
    {
        colorOnl = new Dictionary<DataColor, int>();
        emptyBridges = new List<Bridge>();
        spaceforGenBrick = new List<EatableBrickScript>();
        activeBrick = new List<EatableBrickScript>();
        for (int i = 0; i < bricksTf.childCount; i++)
        {
            spaceforGenBrick.Add(bricksTf.GetChild(i).GetComponent<EatableBrickScript>());
        }
        foreach (Bridge bridge in bridges) bridge.Init(this);
    }

    public void BrickGenerate(int num, DataColor dataColor, Collider collider)
    {
        for(int i = 0; i < num && spaceforGenBrick.Count > 0 ; i++)
        {
            int pos = UnityEngine.Random.Range(0, spaceforGenBrick.Count);
            spaceforGenBrick[pos].Generate(dataColor, collider, this);
            activeBrick.Add(spaceforGenBrick[pos]);
            spaceforGenBrick.Remove(spaceforGenBrick[pos]);
        }
        if(colorOnl.ContainsKey(dataColor))
        {
            colorOnl[dataColor] += num;
        }
    }


    public void OpenBlocked()
    {
        block.SetActive(true);
    }

    public void HideBrickAfterPass(DataColor color)
    {
        for(int i = 0; i < activeBrick.Count; i++)
        {
            if(activeBrick[i].DataColor != null && activeBrick[i].DataColor == color)
            {
                activeBrick[i].Hide();
                i--;
            }
        }
    }

    public void Register(Character character, Collider collider)
    {
        colorOnl.Add(character.DataColor, 0);
        BrickGenerate(numBlockToPassLevel + 5, character.DataColor, collider);
    }

    public void UnRegister(Character character)
    {
        HideBrickAfterPass(character.DataColor);

        if (colorOnl.ContainsKey(character.DataColor))
        {
            colorOnl.Remove(character.DataColor);
        }
    }

    public void BrickHide(EatableBrickScript brick)
    {
        spaceforGenBrick.Add(brick);
        if (activeBrick.Contains(brick))
        {
            activeBrick.Remove(brick);
            colorOnl[brick.DataColor] -= 1;
        }
    }

    public Bridge GetPriorBridgeByFloor(string colorName)
    {
        emptyBridges.Clear();
        foreach(Bridge bridge in bridges)
        {
            if(bridge.FirstBrick.DataColor == null)
            {
                emptyBridges.Add(bridge);
            }else if(bridge.FirstBrick.DataColor.name == colorName)
            {
                return bridge;
            }
        }
        if(emptyBridges.Count > 0)
        {
            return emptyBridges[(int)UnityEngine.Random.Range(0, emptyBridges.Count - .01f)];
        }
        else
        {
            return bridges[(int)UnityEngine.Random.Range(0, bridges.Length - .01f)];
        }
    }
    public void RespawnBlock()
    {
        DataColor atLeastColor = colorOnl.Aggregate((x, y) => x.Value >y.Value ? y : x).Key;
        int pos = UnityEngine.Random.Range(0, spaceforGenBrick.Count);
        spaceforGenBrick[pos].Generate(atLeastColor, 
            Character.GetColliderByDataColor(atLeastColor.name), this);
        activeBrick.Add(spaceforGenBrick[pos]);
        spaceforGenBrick.Remove(spaceforGenBrick[pos]);
        if (colorOnl.ContainsKey(atLeastColor))
        {
            colorOnl[atLeastColor] += 1;
        }
    }
}
