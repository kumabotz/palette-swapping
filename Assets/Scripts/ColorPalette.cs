using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ColorPalette : ScriptableObject
{
    public Texture2D Source;
    public List<Color> Palette = new List<Color>();
    public List<Color> NewPalette = new List<Color>();

    [MenuItem("Assets/Create/Color Palette")]
    public static void CreateColorPalette()
    {
        if (Selection.activeObject is Texture2D)
        {
            var selectedTexture = Selection.activeObject as Texture2D;
            var assetPath = AssetDatabase.GetAssetPath(selectedTexture).Replace(".png", "-color-palette.asset");

            var newPalette = CustomAssetUtil.CreateAsset<ColorPalette>(assetPath);
            newPalette.Source = selectedTexture;
            newPalette.ResetPalette();

            Debug.Log("Created a Palette: " + assetPath);
        }
        else
        {
            Debug.Log("Can't create a Palette");
        }
    }

    public void ResetPalette()
    {
        Palette = BuildPalette(Source);
        NewPalette = new List<Color>(Palette  );
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
