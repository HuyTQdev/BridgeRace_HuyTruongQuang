using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableBrickScript : MonoBehaviour
{
    [SerializeField] GameObject blockObj;
    [SerializeField] MeshRenderer bridgeMesh;
    Platform platform;
    DataColor charColor;
    public DataColor DataColor{ get; private set; }

    public void Generate(Platform platform, Vector3 stepSize)
    {
        DataColor = null;
        this.platform = platform;
        transform.localScale = stepSize;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            if (other.TryGetComponent<AddBlockScript>(out AddBlockScript addBlockScript)){
                charColor = addBlockScript.DataColor;
                if (DataColor != null && DataColor.name == charColor.name) { }
                else if (addBlockScript.CurNumBlock > 0)
                {
                    /*if (DataColor != null)
                    {
                        bridge.MinusColor(DataColor.name);
                    }*/
                    addBlockScript.Minus();
                    blockObj.SetActive(false);

                    DataColor = charColor;
                    //bridge.AddColor(DataColor.name);
                    bridgeMesh.enabled = true;
                    bridgeMesh.material = DataColor.brickMat;
                    platform.RespawnBlock();
                }
                else
                {
                    blockObj.SetActive(true);
                }
            }
        }
    }

}
