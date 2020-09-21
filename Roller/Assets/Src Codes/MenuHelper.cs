using TMPro;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerNameText = null;

    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = GameManager.currentUser.name;
    }

    public void postUserToDatabase()
    {
        DatabaseConnect.postToDatabase();
    }

    public void loadUsers()
    {
        DatabaseConnect.getAllUsersFromDatabase();
    }

}
