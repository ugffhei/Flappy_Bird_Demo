using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    //The game object that will be spawned 
    public GameObject pipeSet;
    //The collector for newly created pipe 
    public GameObject pipeCollector;
    //Used to tell where the bullet will spawned 
    public Transform[] spawnPoints;

    //What the time resets to after we finish counting down to
    public float TIMER = 10f;
    //What the timer is at the moment 
    private float timeLeft = 10f;

    void Start()
    {
        timeLeft = TIMER;
        pipeCollector = transform.Find("PipeCollector").gameObject;
    }

    public void UpdatePipeSpawner()
    {

        //Count down the time 
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Vector3 pos = new Vector3((float) spawnPoints[0].position.x,
                Random.Range((float) spawnPoints[0].position.y, 
                (float) spawnPoints[1].position.y), 0);
            var var = Instantiate(pipeSet, pos, Quaternion.identity);
            var.transform.SetParent(pipeCollector.transform);
            //Reset the timer 
            timeLeft = TIMER;
        }
    }
}
