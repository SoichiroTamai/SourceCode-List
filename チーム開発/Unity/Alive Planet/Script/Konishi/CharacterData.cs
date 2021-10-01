using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    int hp = 10;
    float attackRange = 10.0f;
    private Bullets.BulletManager bulletManager;

    public void UpdateShot(GameObject targetObj,GameObject shooter)
    {
        //if(shooter.tag == "Enemy") { return; }
        bulletManager.Shot(targetObj, shooter);
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.Find("Bullet Manager").GetComponent<Bullets.BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
