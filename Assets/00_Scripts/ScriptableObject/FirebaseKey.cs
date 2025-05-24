using UnityEngine;

[CreateAssetMenu(fileName = "FirebaseKey", menuName = "Scriptable Objects/FirebaseKey")]
public class FirebaseKey : ScriptableObject
{
    public string apiKey;
    public string appId;
    public string projectId;
    public string databaseUrl;
}
