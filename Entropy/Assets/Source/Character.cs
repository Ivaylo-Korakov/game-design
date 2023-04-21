using UnityEngine;

namespace Assets.Source
{
    public class Character : MonoBehaviour
    {

        public GameObject Player;

        // Use this for initialization
        void Start()
        {

        }

        // UpdateTerrainChuk is called once per frame
        void Update()
        {
            Vector3 transform = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            Player.transform.position += transform;

            Debug.Log(Input.GetAxis("Horizontal"));
            Debug.Log(Player.transform.position);
        }
    }
}
