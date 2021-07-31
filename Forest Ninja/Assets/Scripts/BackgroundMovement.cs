using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    Transform t;
    float cameraPos;
    float playerPos;
    [SerializeField] float speed = 1;
    public Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        t = GetComponent<Transform>();
        cameraPos = t.position.x;
        playerPos = player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - t.position.x - 50 > 0)
        {
            cameraPos += 57.6f * 2;
        }
        t.position = new Vector3(cameraPos+(player.position.x-playerPos)*speed, t.position.y, t.position.z);
    }
}
