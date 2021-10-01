using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFrontScript : MonoBehaviour
{
    private SpriteRenderer sprite;

    private float f_View = 0.0f;

    private bool b_compleate;

    public bool Flg;

    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Flg = false;

        sprite = GetComponent<SpriteRenderer>();

        sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        b_compleate = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3();

        vec.x = mainCamera.transform.position.x;
        vec.y = mainCamera.transform.position.y;

        transform.position = vec;

        if (Flg)
        {
            f_View += 0.001f;

            sprite.color = new Color(1.0f, 1.0f, 1.0f, f_View);
            if (f_View > 1)
            {
                b_compleate = true;
            }
        }
    }

    public bool GetCompFlg()
    {
        return b_compleate;
    }
}
