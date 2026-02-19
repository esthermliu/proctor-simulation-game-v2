using UnityEngine;
using System.Runtime.InteropServices;

public static class FirebaseProxy
{
    [DllImport("__Internal")]
    private static extern void LogDocumentToFirebase(string collectionName, string jsonData);

    public static void LogDocument(string collectionName, object jsonData)
    {
        LogDocumentToFirebase(collectionName, JsonUtility.ToJson(jsonData));
    }
}