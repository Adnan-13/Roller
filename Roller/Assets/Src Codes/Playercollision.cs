using UnityEngine;


public class Playercollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().levelFailed();
        }
    }
}
