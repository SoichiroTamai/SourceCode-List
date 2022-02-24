using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Bite : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player")) { return; }
            if (EnemyDatas.instance.animation.m_nowState != animationState.A_Bite) { return; }

            Debug.Log("“–‚½‚Á‚½");
        }
    }
}