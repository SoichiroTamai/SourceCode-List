using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class NavMeshControl : MonoBehaviour
    {
        private NavMeshAgent m_agent;
        //public float m_moveDstance;   

        void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
        }

        // 検知オブジェクトがPlayerなら追いかける
        public void OnDetectObject(Collider other)
        {
            //if (other.CompareTag("Player"))
            //{
                Move(other.transform);
            //}
        }

        public void OnDetectObject(GameObject other)
        {
            // 検知オブジェクトがPlayerなら追いかける
            //if (other.CompareTag("Player"))
            //{
                Move(other.transform);
            //}
        }

        void Rot(Transform _target)
        {
            // 次に目指すべき位置を取得
            //var nextPoint = m_agent.steeringTarget;
            var nextPoint = _target.position;
            Vector3 targetDir = nextPoint - transform.position;

            // その方向に向けて旋回する(120度/秒)
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime);

            //// 自分の向きと次の位置の角度差が30度以上の場合、その場で旋回
            //float angle = Vector3.Angle(targetDir, transform.forward);
            //if (angle < 30f)
            //{
            //    transform.position += transform.forward * 5.0f * Time.deltaTime;
            //    // もしもの場合の補正
            //    if (Vector3.Distance(nextPoint, transform.position) < 0.5f) transform.position = nextPoint;
            //}
        }

        void Move(Transform _target)
        {
            //float dir = Vector3.Distance(this.transform.position, _target.position);

            //if (EnemyDatas.instance.m_dir > 25.0f && EnemyDatas.instance.m_dir < m_moveDstance) { return; }

            Rot(_target);

            if (EnemyDatas.instance.animation.m_nowState != animationState.Walk) { return; }

            Debug.Log("追従：" + EnemyDatas.instance.m_dir);

            // targetに向かって移動
            m_agent.SetDestination(_target.position);
            m_agent.nextPosition = transform.position;
        }
    }
}