using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRemove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(selfDestroy());
    }

    private IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
