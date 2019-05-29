using UnityEditor;
using UnityEngine;

public class CustomAssetUtil
{
#if UNITY_EDITOR
    public static T CreateAsset<T>(string path) where T : ScriptableObject
    {
        var asset = ScriptableObject.CreateInstance<T>();
        var newPath = AssetDatabase.GenerateUniqueAssetPath(path);
        AssetDatabase.CreateAsset(asset, newPath);
        AssetDatabase.SaveAssets();
        return asset;
    }
#endif
}
