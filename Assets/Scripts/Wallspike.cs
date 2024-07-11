using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Wallspike : MonoBehaviour
{
    public GameObject spikePrefab;
    [SerializeField] private bool dir = true;
    [SerializeField] private int amount = 12;
    private PlayerMovement p;
    private float pscore;
    public List<GameObject> spikes;


    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("PlayerControl").GetComponent<PlayerMovement>();
        if (dir)
        {
            for (int i = 0; i < amount; i++)
            {
                spikes.Add( Instantiate(spikePrefab, new Vector3(transform.position.x, transform.position.y - i, 0), Quaternion.Euler(0, 0, -90)));
                
            }
        }

        else
        {
            for (int i = 0; i < amount; i++)
            {
                spikes.Add( Instantiate(spikePrefab, new Vector3(transform.position.x, transform.position.y - i, 0), Quaternion.Euler(0, 0, 90)));
            }
        }

        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(p.score != pscore)
        {
            Shuffle();
            pscore = p.score;
        }

    }

    public void Shuffle() {
        for (int i = 0; i < amount; i++)
        {
            spikes[i].SetActive(RandomBool());
        }
    }

    bool RandomBool()
    {
        int i = UnityEngine.Random.Range(0, 2);

        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
