using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
public class ColorPalette : ScriptableObject
{
    public Texture2D source;
    public List<Color> palette = new List<Color>();
    public List<Color> newPalette = new List<Color>();
    public Texture2D cachedTexture;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Color Palette")]
    public static void CreateColorPalette()
    {
        if (Selection.activeObject is Texture2D)
        {
            var selectedTexture = Selection.activeObject as Texture2D;
            var assetPath = AssetDatabase.GetAssetPath(selectedTexture).Replace(".png", "-color-palette.asset");

            var newPalette = CustomAssetUtil.CreateAsset<ColorPalette>(assetPath);
            newPalette.source = selectedTexture;
            newPalette.ResetPalette();

            Debug.Log("Created a Palette: " + assetPath);
        }
        else
        {
            Debug.Log("Can't create a Palette");
        }
    }
#endif

    public void ResetPalette()
    {
        palette = BuildPalette(source);
        newPalette = new List<Color>(palette  );
    }

    public Color GetColor(Color color)
    {
        for (var i = 0; i < palette.Count; i++)
        {
            var tmpColor = palette[i];
            if (Mathf.Approximately(color.r, tmpColor.r) &&
                Mathf.Approximately(color.g, tmpColor.g) &&
                Mathf.Approximately(color.b, tmpColor.b) &&
                Mathf.Approximately(color.a, tmpColor.a))
            {
                return newPalette[i];
            }
        }

        return color;
    }

    private List<Color> BuildPalette(Texture2D texture)
    {
        var palette = new List<Color>();
        foreach (var color in texture.GetPixels())
        {
            if (!palette.Contains(color) && color.a == 1)
            {
                palette.Add(color);
            }
        }
        return palette;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor
{
    public ColorPalette colorPalette;

    void OnEnable()
    {
        colorPalette = target as ColorPalette;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Source Texture");
        colorPalette.source = EditorGUILayout.ObjectField(colorPalette.source, typeof(Texture2D), false) as Texture2D;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Current Color");
        GUILayout.Label("New Color");
        EditorGUILayout.EndHorizontal();

        for (var i = 0; i < colorPalette.palette.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ColorField(colorPalette.palette[i]);
            colorPalette.newPalette[i] = EditorGUILayout.ColorField(colorPalette.newPalette [i]);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Revert Palette"))
        {
            colorPalette.ResetPalette();
        }

        EditorUtility.SetDirty(colorPalette);
    }
}
#endif