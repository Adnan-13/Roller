using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform playerTransform;

    [SerializeField]
    Vector3 offset = new Vector3(0f, 1f, 3f);

    void Update ()
    {
        transform.position = playerTransform.position + offset;
	}
}
