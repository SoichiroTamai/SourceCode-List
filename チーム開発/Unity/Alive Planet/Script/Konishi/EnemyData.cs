using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bullets;

public class EnemyData : CharacterData
{
    private EnemyManager enemyManager;
    CharacterData charaData;
    public GameObject targetObj;

    Plane plane = new Plane();
    float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        charaData = GameObject.Find("CharacterData").GetComponent<CharacterData>();
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();

        plane.SetNormalAndPosition(Vector3.back, transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetObj) { return; }
        charaData.UpdateShot(targetObj, this.gameObject);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            //Plane�Ƃ̌�_�����߂āA�L�����N�^�[��������
            var lookPoint = ray.GetPoint(distance); ;
            transform.LookAt(transform.localPosition + Vector3.forward, targetObj.transform.position - transform.localPosition);
            Debug.Log("���@�Ɍ���");

        }
        //transform.LookAt(targetObj.transform.position);
    }
}
