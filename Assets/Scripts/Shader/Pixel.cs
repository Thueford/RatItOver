using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    public static Pixel self;

    [Header("Shader")]
    public Material MatPixel;

    

    [Header("Textures")]
    public RenderTexture bufferTex;

    private int lastWidth = Screen.width;
    private int lastHeight = Screen.height;


    private void Awake() => self = this;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
            
        //Recreate Textures if Window was resized
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            bufferTex = createTexture();
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }

        Graphics.Blit(source, bufferTex);
        Graphics.Blit(bufferTex, destination); //, MatPixel
    }

    private RenderTexture createTexture()
    {
        RenderTexture tex = new RenderTexture(Screen.width / 10, Screen.height / 10, 24);
        tex.Create();
        return tex;
    }
}
