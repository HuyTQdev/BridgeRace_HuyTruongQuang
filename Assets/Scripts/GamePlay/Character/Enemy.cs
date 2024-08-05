using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { IDLE, COLLECT, BUILD}
public class Enemy: Character
{
    [SerializeField] NavMeshAgent agent;
    public List<Vector3> TarBlockPositions { get; private set; }
    StateMachine<Enemy> stateMachine;
    List<IState<Enemy> > states;
    [field: SerializeField] public string curState;
    public override void Generate(DataColor dataColor, Platform platform)
    {
        stateMachine = new StateMachine<Enemy>(this);
        TarBlockPositions = new List<Vector3>();

        states = new List<IState<Enemy>>();
        states.Add(new IIdleState());
        states.Add(new ICollectState());
        states.Add(new IBuildState());

        ChangeState(EnemyState.IDLE);

        Listen(dataColor.name);
        base.Generate(dataColor, platform);
    }
    private void Update()
    {
        stateMachine.Update();
        if (agent.velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
            _animator.SetFloat("velocity", 1);
        }
        else
            _animator.SetFloat("velocity", 0);
         curState = stateMachine.currentState.GetType().Name;
    }

    private void Listen(string dataColor)
    {
        EventManager.Instance.StartListening("GENBLOCK" + dataColor, AddBlockTf);
        EventManager.Instance.StartListening("ADDBLOCK" + dataColor, RemoveBlockTf);
        EventManager.Instance.StartListening("GENBLOCKGREY", AddBlockTf);
        EventManager.Instance.StartListening("ADDBLOCKGREY", RemoveBlockTf);
    }
    private void OnDisable()
    {
        if (!EventManager.CheckNull() && DataColor != null)
        {
            EventManager.Instance.StopListening("GENBLOCK" + DataColor.name, AddBlockTf);
            EventManager.Instance.StopListening("ADDBLOCK" + DataColor.name, RemoveBlockTf);
            EventManager.Instance.StopListening("GENBLOCKGREY", AddBlockTf);
            EventManager.Instance.StopListening("ADDBLOCKGREY", RemoveBlockTf);
        }
    }

    private void AddBlockTf(object[] parameters)
    {
        if(parameters.Length > 0 && parameters[0] is Vector3)
        {
            TarBlockPositions.Add((Vector3)parameters[0]);
            if(stateMachine.currentState == states[(int)EnemyState.IDLE])
            {
                ChangeState(EnemyState.COLLECT);
            }
        }
    }
    private void RemoveBlockTf(object[] parameters)
    {
        if (parameters.Length > 0 && parameters[0] is Vector3)
        {
            Vector3 tmp = (Vector3)parameters[0];
            if (TarBlockPositions.Contains(tmp)){
                TarBlockPositions.Remove(tmp);
            }
            CalculateToChange();
        }
    }
    public void SetDest(Vector3 targetPos)
    {
        agent.enabled = true;
        agent.SetDestination(targetPos);
    }
    public override void StopMoving()
    {
        base.StopMoving();
        agent.enabled = true;
        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }
        agent.enabled = false;
        
    }
    public override void Wake()
    {
        base.Wake();
        CalculateToChange();
    }

    public override void Pass()
    {
        TarBlockPositions.Clear();
        CalculateToChange();
        base.Pass();

    }


    public void CalculateToChange()
    {
        int i = 10;
        if(isEndGame)
        {
            return;
        }
        if (addBlockScript.CurNumBlock >= i)
        {
            ChangeState(EnemyState.BUILD);
        }
        else if (TarBlockPositions.Count == 0)
        {
            if (addBlockScript.CurNumBlock > 0)
            {
                ChangeState(EnemyState.BUILD);
            }
            else
            {
                ChangeState(EnemyState.IDLE);
            }
        }
        else
        {
            ChangeState(EnemyState.COLLECT);
        }
    }
    public void ChangeState(EnemyState enemyState)
    {
        stateMachine.ChangeState(states[(int)enemyState]);
    }
    public override void ChangePlatform(Platform platform)
    {
        base.ChangePlatform(platform);
        CalculateToChange();
    }

}
