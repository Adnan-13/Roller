 using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

public class DatabaseConnect : MonoBehaviour
{
    public static User[] users;

    public TextMeshProUGUI login_email;
    public TextMeshProUGUI login_password;
    
    public TextMeshProUGUI register_username;
    public TextMeshProUGUI register_email;
    public TextMeshProUGUI register_password;

    public static string localId;
    public static string idToken;


    public TextMeshProUGUI userName;
    public Text score_text;

    public Text score;

    public string AuthKey = "AIzaSyD5Wnm58hu9oaU56jvhjOXzXsQSVYKWCXE";

    

    public void register()
    {
        NewUser newUser = new NewUser(register_email.text, register_password.text, true);

        string newUserData = JsonUtility.ToJson(newUser);

        //string username = register_username.text;
        //string password = register_password.text;

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = "https://identitytoolkit.googleapis.com/v1/accounts:signUp", 
            Params = new Dictionary<string, string>
            {
                { "key", AuthKey }
            },

            Body = newUser,
            
            EnableDebug = true

        };

        RestClient.Post<SignResponse>(currentRequest).Then(
            response =>
            {
                Debug.Log("Response:");

                idToken = response.idToken;
                localId = response.localId;

                postNewUserToDatabase();
                
                Debug.Log("Signed up: localId: " + response.localId + "\nidToken: " + response.idToken);

                SceneManager.LoadScene("LoginMenu");

            }).Catch(error =>
            {
                Debug.Log(error);

                

               // EditorUtility.DisplayDialog("Registration Failed", "Email already exists or Invalid email and/password", "OK");
            });
    }

    public void login()
    {
        NewUser newUser = new NewUser(login_email.text, login_password.text, true);

        string newUserData = JsonUtility.ToJson(newUser);

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword",
            Params = new Dictionary<string, string>
            {
                { "key", AuthKey }
            },

            Body = newUser,

            EnableDebug = true

        };

        RestClient.Post<SignResponse>(currentRequest).Then(
            response =>
            {
                Debug.Log("Login Successful!!");

                Debug.Log("Response:");
                idToken = response.idToken;
                localId = response.localId;

                getUserFromDatabase();

            }).Catch(error =>
            {
                Debug.Log(error);
                //EditorUtility.DisplayDialog("Login Failed", "Please check your Email and Password", "OK");
            });
    }


    public void postNewUserToDatabase()
    {
        User user = new User(userName.text, 0);

        user.setLocalId(localId);

        RestClient.Put("https://roller-c9a0f.firebaseio.com/Users/" + localId + ".json", user).Then(response =>
        {
            Debug.Log("New User Posted");
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    public static void postToDatabase()
    {
        User user = GameManager.currentUser;

        RestClient.Put("https://roller-c9a0f.firebaseio.com/Users/" + GameManager.currentUser.localId + ".json", user).Then(response =>
        {
            Debug.Log("Posted");
        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }

    public void getDataFromDatabase()
    {
        RestClient.Get<User>("https://roller-c9a0f.firebaseio.com/Users/" + localId + ".json").Then(response =>
        {
            
        }).Catch(error =>
        {
            Debug.Log(error);
        });
            
    }

    public void getUserFromDatabase()
    {
        RestClient.Get<User>("https://roller-c9a0f.firebaseio.com/Users/" + localId + ".json").Then(response =>
        {
            GameManager.currentUser = response;

            Debug.Log("Got: " + GameManager.currentUser.name);

            SceneManager.LoadScene("Menu");

        }).Catch(error =>
        {
            Debug.Log(error);
        });

    }

    public static void getAllUsersFromDatabase()
    {
        RestClient.Get("https://roller-c9a0f.firebaseio.com/Users/.json").Then(response =>
        {
            var responseJson = response.Text;

            Dictionary<string, User> value = JsonConvert.DeserializeObject<Dictionary<string, User>>(responseJson);

            users = new User[value.Count];

            value.Values.CopyTo(users, 0);

            var list = from u in users
                    orderby u.score descending
                    select u;
                    

            string playerNameObject = "Player";
            string scoreObject = "Score";

            int count = 1;

            Debug.Log("Total Players: " + DatabaseConnect.users.Length);

            foreach (User user in list)
            {
                Debug.Log(user.name + ": " + user.score);

                if (count > 5) break;

                string pNO = playerNameObject + count;
                string sO = scoreObject + count;

                GameObject.Find(pNO).GetComponent<TextMeshProUGUI>().text = user.name;
                GameObject.Find(sO).GetComponent<TextMeshProUGUI>().text = user.score.ToString("0");

                count++;
            }

        }).Catch(error =>
        {
            Debug.Log(error);
        });

    }
}
