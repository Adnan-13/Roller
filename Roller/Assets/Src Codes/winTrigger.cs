using UnityEngine;

public class winTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider playerColider)
    {
        if(playerColider.tag == "Player")
        {
            FindObjectOfType<GameManager>().levelPassed();
        }
    }

}
