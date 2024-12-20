using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] enemy;
    public Transform[] spawnPoint;

    private int rand;
    private int randPosition;
    public float startTimeBtwSpown;
    private float timeBtwSpawns;
    
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpown;
    }

    
    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            rand=Random.Range(0, enemy.Length);
            randPosition= Random.Range(0, spawnPoint.Length);
            Instantiate(enemy[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpown;
        }
        else {timeBtwSpawns-=Time.deltaTime; }
    }
}
