using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    [SerializeField] public float bounceSpeed = 0.5f;
    [SerializeField] public float maxBounce = 1.0f;
    private float currentBounce = 0;
    private bool isGoingUp = true;

    // Update is called once per frame
    void Update()
    {
        if (isGoingUp)
        {
            currentBounce += Time.deltaTime;
            this.transform.Translate(new Vector3(0, bounceSpeed * Time.deltaTime, 0));
        }
        else
        {
            currentBounce -= Time.deltaTime;
            this.transform.Translate(new Vector3(0, -bounceSpeed * Time.deltaTime, 0));
        }

        if (this.currentBounce > maxBounce)
        {
            this.currentBounce = maxBounce;
            this.isGoingUp = false;
        }
        if (this.currentBounce < 0)
        {
            this.currentBounce = 0;
            this.isGoingUp = true;
        }
    }
}
