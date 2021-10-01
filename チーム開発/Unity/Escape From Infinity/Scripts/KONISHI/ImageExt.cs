using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExt
{
    // image‚Ì•s“§–¾“x‚ğİ’è
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        c.a = alpha;
        image.color = c;
    }
}
