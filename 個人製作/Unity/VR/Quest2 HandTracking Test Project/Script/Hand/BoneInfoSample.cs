using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoneInfoSample : MonoBehaviour
{
    OVRHand _hand;
    OVRSkeleton _skeleton;
    List<GameObject> _spheres = new List<GameObject>();

    void Start()
    {
        Debug.Log("BoneInfoSample_Start()");
        _hand = this.gameObject.GetComponent<OVRHand>();
        _skeleton = this.gameObject.GetComponent<OVRSkeleton>();
    }

    void Update()
    {
        if (_skeleton.Bones.Count != 0 &&_spheres.Count == 0)
        {
            CreateSphere();
        }
        else
        {
            //  トラックが外れたらSphereを消す
            foreach (var sphere in _spheres)
            {
                //if (sphere.activeSelf == _hand.IsTracked) { continue; }

                sphere.SetActive(_hand.IsTracked);
            
                //Debug.Log("hand.IsTracked()：" + _hand.IsTracked);

            }
        }
    }

    void CreateSphere()
    {

        //sif (_spheres[0].activeSelf) { return; }

        Debug.Log("CreateSphere()");

        var boneColor = new Dictionary<string, Color>()
        {
            { "Start",Color.black},     //  スタート位置
            { "Thumb",Color.red},       //  親指
            { "Index",Color.green},     //  人差し指    
            { "Middle",Color.blue},     //  中指
            { "Ring", Color.cyan},      //  薬指
            { "Pinky",Color.magenta},   //  小指
            { "Forearm",Color.yellow},  //  前腕部
        };

        foreach (var bone in _skeleton.Bones)
        {
            //  Sphereを生成しボーンに割り当てる
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = bone.Transform.position;
            sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            sphere.transform.parent = bone.Transform;

            //  指単位で色を変える
            var color = boneColor.FirstOrDefault(x => bone.Id.ToString().Contains(x.Key));
            sphere.GetComponent<Renderer>().material.color = color.Value;

            _spheres.Add(sphere);
        }
    }
}
