public class NewUser
{
    public string email;
    public string password;
    public bool returnSecureToken;
    public NewUser(string e, string p, bool rST)
    {
        email = e;
        password = p;
        returnSecureToken = rST;
    }
    public NewUser() {
    }
}
