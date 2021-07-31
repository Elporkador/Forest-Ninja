using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spikes;
    public GameObject smallPost;
    public GameObject[] patterns;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        while (Player.inMenu == true)
        {
            if (Player.inMenu == false)
            {
                InvokeRepeating("spawnSpike", 0f, 0.5f);
                InvokeRepeating("spawnPost", 0f, 0.2f);
                InvokeRepeating("spawnPattern", 3f, 7f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnSpike()
    {
        if (Random.Range(0, 4) == 0)
            Instantiate(spikes, new Vector2(player.transform.position.x + 50, -4), spikes.transform.rotation);
    }

    void spawnPost()
    {
        if (Random.Range(0, 7) == 0)
            Instantiate(smallPost, new Vector2(player.transform.position.x + 50, Random.Range(-3, 0)), smallPost.transform.rotation);
    }

    void spawnPattern()
    {
        Instantiate(patterns[Random.Range(0, patterns.Length)], new Vector2(player.transform.position.x + 30, 0), player.transform.rotation);
    }


}
