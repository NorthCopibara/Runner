using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLighting : MonoBehaviour
{
    [SerializeField] private GameObject lighting;

    private void Start()
    {
        Spawn();
    }

    private void Spawn() 
    {
        if (RandomSpawn() < 200) 
        {
            lighting.SetActive(true);
        }
        StartCoroutine(NextSpawn());
    }

    private float RandomSpawn()
    {
        float time = Random.Range(0, 1000);
        return time;
    }

    IEnumerator NextSpawn() 
    {
        yield return new WaitForSeconds(8);
        if (lighting.active == true)
            StartCoroutine(NextSpawn());
        else
            Spawn();
    }
}
