using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class CRT : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    [Range(0, 1)]
    float noiseX;
    public float NoiseX { get { return noiseX; } set { noiseX = value; } }

    [SerializeField]
    [Range(0, 1)]
    float rgbNoise;
    public float RGBNoise { get { return rgbNoise; } set { noiseX = value; } }

    [SerializeField]
    [Range(0, 1)]
    float sinNoiseScale;
    public float SinNoiseScale { get { return sinNoiseScale; } set { sinNoiseScale = value; } }

    [SerializeField]
    [Range(0, 10)]
    float sinNoiseWidth;
    public float SinNoiseWidth { get { return sinNoiseWidth; } set { sinNoiseWidth = value; } }

    [SerializeField]
    float sinNoiseOffset;
    public float SineNoiseOffset { get { return sinNoiseOffset; } set { sinNoiseOffset = value; } }

    [SerializeField]
    Vector2 offset;
    public Vector2 Offset { get { return offset; } set { offset = value; } }

    [SerializeField]
    [Range(0, 2)]
    float scanLineTail = 1.5f;
    public float ScanLineTail { get { return scanLineTail; } set { scanLineTail = value; } }

    [SerializeField]
    [Range(-10, 10)]
    float scanLineSpeed = 10;
    public float ScanLineSpeed { get { return scanLineSpeed; } set { scanLineSpeed = value; } }

    void OnRenderImage(RenderTexture scr, RenderTexture dest)
    {
        material.SetFloat("_NoiseX", noiseX);
        material.SetFloat("_RGBNoise", rgbNoise);
        material.SetFloat("_SinNoiseScale", sinNoiseScale);
        material.SetFloat("_SinNoiseWidth", sinNoiseWidth);
        material.SetFloat("_SinNoiseOffset", sinNoiseOffset);
        material.SetFloat("_ScanLineTail", scanLineTail);
        material.SetFloat("_ScanLineSpeed", scanLineSpeed);
        material.SetVector("_Offset", offset);
        Graphics.Blit(scr, dest, material);
    }
}
