using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class Agent : MonoBehaviour
{
    public List<Generator> generatorList = new List<Generator>();
    public Generator target;
    public AIPath ai;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        ai = GetComponent<AIPath>();
        
        SelectNextGenerator();
        
    }

    public void Complete(Generator generator)
    {
        if (generator)
        {
            print($"{name}: Completed {generator.name}");
            generator.Leave(this);
            generatorList.Remove(generator);
        }

        if (generatorList.Count == 0)
        {
            StartCoroutine(GoToExit());
            return;
        }
        target = null;
        
        SelectNextGenerator();
    }

    public void SelectNextGenerator()
    {
        if (target)
        {
            Complete(target);
            return;
        }
        
        if (generatorList.Count == 0)
        {

            StartCoroutine(GoToExit());
            return;
        }


        target = generatorList[0];
        
        
        
        print($"{name}: Queing up for {target.name}");
        target.QueUp(this);
    }

    IEnumerator GoToExit()
    {
        ai.destination = startPosition;
        Vector3 dif = transform.position - startPosition;
        
        print($"{name}: leaving building");
        while (dif.magnitude > 0.5f )
        {
            yield return new WaitForSeconds(0.5f);
            dif = transform.position - startPosition;
        }
        
        Destroy(gameObject);
    }


    public void GoTo(Vector3 position, UnityAction onComplete = null, UnityAction onCancel = null)
    {
        Debug.DrawLine(transform.position,position,Color.yellow,3f);
        ai.destination = position;
    }


    public void SetAI(bool flag)
    {
        ai.enabled = flag;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 lastPosition = transform.position;
        foreach (var generator in generatorList)
        {
            Vector3 nextPosition = generator.transform.position;
            Gizmos.DrawLine(lastPosition,nextPosition);
            lastPosition = nextPosition;
        }
    }
}
