using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteSwapper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ColorPalette[] palettes;

    private Texture2D texture;
    private MaterialPropertyBlock block;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (palettes.Length > 0)
        {
            SwapColors(palettes[Random.Range(0, palettes.Length)]);
        }
    }

    private void SwapColors(ColorPalette palette)
    {
        if (palette.cachedTexture == null)
        {
            texture = spriteRenderer.sprite.texture;

            var cloneTexture = new Texture2D(texture.width, texture.height)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point
            };

            var colors = texture.GetPixels();
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = palette.GetColor(colors[i]);
            }

            cloneTexture.SetPixels(colors);
            cloneTexture.Apply();

            palette.cachedTexture = cloneTexture;
        }

        block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", palette.cachedTexture);
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        spriteRenderer.SetPropertyBlock(block);
    }
}
