using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyDatas : MonoBehaviour
    {
        public static EnemyDatas instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public new AnimationState animation;

        public float m_dir;

        private void Reset()
        {
            animation = GetComponent<AnimationState>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GetDistance(GameObject _target)
        {
            m_dir = Vector3.Distance(this.transform.position, _target.transform.position);
        }
    }
}