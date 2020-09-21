
using System;

[Serializable]
public class User
{
    public string name;
    public float score;
    public string localId;

    public User(string name, float score)
    {
        this.name = name;
        this.score = score;
        localId = "";
    }

    public void setLocalId(string localID)
    {
        localId = localID;
    }
}