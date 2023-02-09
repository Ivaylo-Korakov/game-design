using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Verify player is specified
        if (player == null)
        {
            Debug.LogError("Player not specified for camera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.transform.position);

            // Calculate the distance between camera and player
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 4) { 
                transform.Translate(Vector3.forward * Time.deltaTime * 10);
            }
        }
    }
}
