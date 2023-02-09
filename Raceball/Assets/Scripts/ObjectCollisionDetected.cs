using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCollisionDetected : MonoBehaviour
{
    private Color originalColor = Color.clear;
    [SerializeField] public Color hitColor = Color.red;
    [SerializeField] public float decaySpeed = 1f;
    private float currentDecay = 0f;

    void Start()
    {
        if (originalColor.Equals(Color.clear))
        {
            originalColor = GetComponent<MeshRenderer>().material.color;
        }
    }

    void Update()
    {
        if (currentDecay > 0)
        {
            currentDecay -= Time.deltaTime * decaySpeed;

            float r = originalColor.r + hitColor.r * 2 * currentDecay;
            float g = originalColor.g + hitColor.g * 2 * currentDecay;
            float b = originalColor.b + hitColor.b * 2 *  currentDecay;

            var currentColor = new Color(r, g, b);
            SetColor(currentColor);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.name);
        this.currentDecay = 1.5f;
    }

    void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
