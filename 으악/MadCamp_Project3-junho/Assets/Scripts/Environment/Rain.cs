using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public ParticleSystem rain;
    public Transform playerTransform;

    private bool onRain;

    private void Update()
    {
        if (onRain)
        {
            gameObject.transform.position = playerTransform.position;
        }
    }

    void SetRain(bool onRain)
    {
        this.onRain = onRain;
    }

    public void SetRain(float time)
    {
        StartCoroutine(IESetRain(time));
    }

    public IEnumerator IESetRain(float time)
    {
        SetRain(true);
        yield return new WaitForSeconds(time);
        SetRain(false);
    }

}