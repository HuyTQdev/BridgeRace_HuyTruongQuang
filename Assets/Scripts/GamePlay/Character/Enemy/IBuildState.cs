using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class IBuildState : IState<Enemy>
{
    Bridge targetBridge;
    public void OnEnter(Enemy t)
    {
        targetBridge = t.platform.GetPriorBridgeByFloor(t.DataColor.name);
        t.SetDest(targetBridge.StartPosition);
    }

    public void OnExcute(Enemy t)
    {
        //Debug.Log(t.DataColor.name + ": " + Vector3.Distance(t.transform.position, targetBridge.StartPosition));
        if(Vector3.Distance(t.transform.position, targetBridge.StartPosition + 
            Vector3.up *(t.transform.position.y - targetBridge.StartPosition.y)) < .1f)
        {
            t.SetDest(targetBridge.GatePosition + Vector3.forward);
        }
        if(t.addBlockScript.CurNumBlock == 0)
        {
            t.CalculateToChange();
        }
    }

    public void OnExit(Enemy t)
    {
    }
}
