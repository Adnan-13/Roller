using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    Rigidbody body;

    [SerializeField]
    public static float forwardForce = 100f;
    [SerializeField]
    float xMoveForce = 100f;

    bool steer = false;

    float xMove;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        takeInput();

        if(transform.position.y < -1)
        {
            FindObjectOfType<GameManager>().levelFailed();
        }
    }

    void FixedUpdate ()
    {
        body.AddForce(0f, 0f, forwardForce * Time.deltaTime, ForceMode.VelocityChange);

        if(steer)
        {
            body.AddForce(xMove * xMoveForce * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
        }
	}

    void takeInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");

        if(xMove != 0)
        {
            steer = true;
        }

        else
        {
            steer = false;
        }
    }

    
}
