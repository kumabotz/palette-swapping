using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ColorPalette : ScriptableObject
{
    [MenuItem("Assets/Create/Color Palette")]
    public static void CreateColorPalette()
    {
        if (Selection.activeObject is Texture2D)
        {
            var selectedTexture = Selection.activeObject as Texture2D;
            var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);
            selectionPath = selectionPath.Replace(".png", "-color-palette.asset");
            Debug.Log("Creating a Palette: " + selectionPath);
        }
        else
        {
            Debug.Log("Can't create a Palette");
        }
    }
}
