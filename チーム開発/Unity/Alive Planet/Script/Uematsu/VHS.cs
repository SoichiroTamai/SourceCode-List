using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHS : MonoBehaviour
{
    public Material VHS_Mat;
    [SerializeField, Range(0, 1)] float _bleeding = 0.8f;
    [SerializeField, Range(0, 1)] float _fringing = 1.0f;
    [SerializeField, Range(0, 1)] float _scanline = 0.125f;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= 1.0f)
        {
            if (t > Random.Range(3.0f, 8.0f)) t = 0.0f;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        VHS_Mat.SetFloat("_src", 0.5f);
        var bleedWidth = 0.04f * _bleeding;  // width of bleeding
        var bleedStep = 2.5f / src.width; // max interval of taps
        var bleedTaps = Mathf.CeilToInt(bleedWidth / bleedStep);
        var bleedDelta = bleedWidth / bleedTaps;
        var fringeWidth = 0.0025f * _fringing; // width of fringing

        VHS_Mat.SetInt("_Width", src.width);
        VHS_Mat.SetInt("_Height", src.height);
        VHS_Mat.SetInt("_BleedTaps", bleedTaps);
        VHS_Mat.SetFloat("_BleedDelta", bleedDelta);
        VHS_Mat.SetFloat("_FringeDelta", fringeWidth);
        VHS_Mat.SetFloat("_Scanline", _scanline);
        VHS_Mat.SetFloat("_NoiseY", 1.0f - t);


        Graphics.Blit(src, dest, VHS_Mat);
    }
}
