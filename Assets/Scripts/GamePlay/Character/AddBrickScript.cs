using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockScript : MonoBehaviour
{
    [SerializeField] Transform firstBlock;
    [SerializeField] Vector3 blockDistance;
    public int CurNumBlock { get; private set; }
    Stack<GameObject> blocks;
    public DataColor DataColor { get; private set; }
    GameObject go;
    private void Awake()
    {
        CurNumBlock = 0;
        blocks = new Stack<GameObject>();
    }
    public void Generate(DataColor dataColor)
    {
        this.DataColor = dataColor;
        EventManager.Instance.StartListening("ADDBLOCK" + DataColor.name, Add);
        //for (int i = 0; i < 35; i++) Add(null);
    }


    private void OnDisable()
    {
        if (!EventManager.CheckNull() && DataColor != null)
        {
            EventManager.Instance.StopListening("ADDBLOCK" + DataColor.name, Add);
        }
    }



    public void Add(object[] parameters)
    {
        go = ObjectPool.Instance.GetObject("StackableBrick");
        go.transform.rotation = firstBlock.transform.rotation;
        go.transform.SetParent(firstBlock.parent);
        go.GetComponent<MeshRenderer>().material = DataColor.brickMat;
        go.transform.localPosition = firstBlock.transform.localPosition + blockDistance * CurNumBlock;
        blocks.Push(go);
        CurNumBlock += 1;
    }

    public void Minus()
    {
        go = blocks.Peek();
        blocks.Pop();
        go.SetActive(false);
        CurNumBlock -= 1;
    }

    public void BrickSplash()
    {
        while(blocks.Count > 0)
        {
            go = ObjectPool.Instance.GetObject("EatableBrick");
            go.GetComponent<EatableBrickScript>().Splash(blocks.Peek().transform);
            Minus();
        }
    }

    public void EndGame()
    {
        while (blocks.Count > 0)
        {
            Minus();
        }
    }
}