using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generator : MonoBehaviour
{

    public List<Agent> que = new List<Agent>();
    public Agent currentAgent;

    public float duration = 3f;

    public float minDistance = 2;
    public float maxSeekTime = 20;
    public int deltaMoney = 1;
    
    public Vector3 queDirection = Vector3.forward;
    public float queStepSize = 2;

    public void QueUp(Agent agent)
    {
        if(!agent)
            return;
        
        if(que.Contains(agent))
            return;

        que.Add(agent);
        agent.GoTo(GetLastQuePos());

        if (currentAgent == null)
            NextAgent();
    }

    public void HandleAgent()
    {
        
    }

    public void Leave(Agent agent)
    {
        if(currentAgent != agent)
            return;

        currentAgent = null;
    }

    private void FixedUpdate()
    {
        if(currentAgent)
            HandleAgent();
    }

    public void NextAgent()
    {
        if(que.Count == 0)
            return;

        
        StartCoroutine(AgentAction(que[0]));
        que.RemoveAt(0);
    }

    public Vector3 GetLastQuePos()
    {
        return transform.position + queDirection.normalized * que.Count * queStepSize;
    }

    public void RefreshQue()
    {
        for (int i = 0; i < que.Count; i++)
        {
            Vector3 position =  transform.position + queDirection.normalized * i * queStepSize;
            que[i].GoTo(position);
        }
    }

    public IEnumerator AgentAction(Agent agent)
    {
        // Go to Generator
        if (currentAgent && agent != currentAgent)
        {
            Leave(agent);
        }
        
        
        currentAgent = agent;

        RefreshQue();
        Vector3 dif = transform.position - agent.transform.position;

        float startSeekTime = Time.time;
        
        print($"{agent.name} Seeking {name}");
        while (dif.magnitude > minDistance && Time.time - startSeekTime < maxSeekTime)
        {
            yield return new WaitForSeconds(0.5f);
            dif = transform.position - agent.transform.position;
        }

        agent.SetAI(false);
        Vector3 agentsPrevPosition = agent.transform.position;
        agent.transform.DOMove(transform.position, 0.5f);

        
        print($"{agent.name} doing {name}");
        yield return new WaitForSeconds(duration);
        
        GeneratorManager.instance.DeltaMoney(deltaMoney);
        
        
        agent.transform.DOMove(agentsPrevPosition, 0.5f);
        agent.SetAI(true);
        agent.Complete(this);
        
        

        currentAgent = null;
        NextAgent();
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,minDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position,queDirection.normalized * queStepSize);
    }
}
