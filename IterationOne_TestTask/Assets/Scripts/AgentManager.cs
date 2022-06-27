using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;

public class AgentManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public Transform agentStart;
    public List<Generator> generators = new List<Generator>();


    public Button buttonSpawn;
    public Slider sliderSpawn;
    public float spawnCooldown = 1;
    

    private void Start()
    {
        Generator[] generators =  FindObjectsOfType<Generator>();

        this.generators = new List<Generator>(generators);


        if (buttonSpawn)
        {
            buttonSpawn.onClick.RemoveAllListeners();
            buttonSpawn.onClick.AddListener(AddAgent);
        }
    }

    public void AddAgent()
    {
        GameObject agentObj = Instantiate(agentPrefab, agentStart.position, Quaternion.identity);
        Agent agent = agentObj.GetComponent<Agent>();
        Shuffle(generators);
        agent.generatorList = GetRandomGenerators();


        if (sliderSpawn && buttonSpawn)
        {
            buttonSpawn.interactable = false;
            sliderSpawn.value = 0;
            sliderSpawn.DOValue(1, 1).OnComplete(() =>
            {
                buttonSpawn.interactable = true;
                sliderSpawn.value = 0;
            });
            

        }
        
    }

    public List<Generator> GetRandomGenerators()
    {
        return new List<Generator>(Shuffle(generators));
    }

    public List<Generator> Shuffle(List<Generator> alpha )
    {
        for (int i = 0; i < alpha.Count; i++) {
            var temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }

        return alpha;
    }

}
