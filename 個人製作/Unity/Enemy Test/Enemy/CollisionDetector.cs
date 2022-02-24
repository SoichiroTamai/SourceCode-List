using System;
using UnityEngine;
using System.Collections.Generic;

namespace Enemy
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private NavMeshControl navMeshControl;
        
        // 複数感知用
        List<GameObject> triggerList = new List<GameObject>();
        List<GameObject> triggerExitList = new List<GameObject>();

        private bool isPlayer = false;

        void Update()
        {
            isPlayer = false;

            foreach (GameObject obj in triggerList)
            {
                if (obj.CompareTag("Player"))
                {
                    isPlayer = true;
                    EnemyDatas.instance.GetDistance(obj);
                    navMeshControl.OnDetectObject(obj);
                }
            }

            foreach (GameObject obj in triggerExitList)
            {
                if (obj.CompareTag("Player"))
                {
                    EnemyDatas.instance.animation.SetAnimation(animationState.Idol);
                }
            }

            triggerList.Clear();
            triggerExitList.Clear();
        }

        private void OnTriggerStay(Collider other)
        {
            triggerList.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            triggerExitList.Add(other.gameObject);
        }
    }
}