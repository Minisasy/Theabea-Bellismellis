using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] int poolSize;
    [SerializeField] float spawnTimer = 1f;

    [SerializeField] float xPos;
    [SerializeField] float yPos;
    [SerializeField] float zPos1;
    [SerializeField] float zPos2;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }
    private void Start()
    {
        StartCoroutine(SpawnBall());
    }
    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(ball, transform);
            pool[i].SetActive(false);
        }
    }
    IEnumerator SpawnBall()
    {
        while (true)
        {
            EnableBall();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
    void EnableBall()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                Vector3 ballSpawn = new Vector3(xPos, yPos, Random.Range(zPos1, zPos2));
                pool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                pool[i].transform.position = ballSpawn;
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
