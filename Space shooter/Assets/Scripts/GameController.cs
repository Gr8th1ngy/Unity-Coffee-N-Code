﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private float maxZValue;
    private float maxXvalue;

    private void Start() {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        maxZValue = screenBounds.z * 1.2f;
        maxXvalue = screenBounds.z * 0.9f;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves() 
    {
        yield return new WaitForSeconds(startWait);

        while(true) 
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];

                Vector3 spawnPosition = new Vector3(Random.Range(-maxXvalue, maxXvalue), 0f, maxZValue);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
        }
    }
}
