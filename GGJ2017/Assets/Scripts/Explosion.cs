using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float delay = 0.7f;

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());
    }
}
