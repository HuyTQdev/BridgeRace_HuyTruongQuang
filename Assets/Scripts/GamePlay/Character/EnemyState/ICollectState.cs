using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ICollectState : IState<Enemy>
{
    int n;
    public void OnEnter(Enemy t)
    {
        if (t.TarBlockPositions.Count == 0) t.CalculateToChange();
        else
        {
            n = (int)UnityEngine.Random.Range(0, t.TarBlockPositions.Count - .01f);
            t.SetDest(t.TarBlockPositions[n]);
        }
    }

    public void OnExcute(Enemy t)
    {
    }

    public void OnExit(Enemy t)
    {
    }
}
