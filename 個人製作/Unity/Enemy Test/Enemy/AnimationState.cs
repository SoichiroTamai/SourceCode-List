using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum animationState
    {
        Idol = 0,
        Hit = 1,
        Sleep = 2,
        Die = 3,

        Walk = 5,
        Ran = 6,

        A_Bite   = 10, // 噛みつき攻撃 
        A_Tackle = 11, // 飛びつき攻撃 
        A_Breath = 12, // ブレス攻撃 
        A_Magic  = 13  // 妖術 
    }

    public class AnimationState : MonoBehaviour
    {
        private Animator m_animator;
        private string m_stateName;

        public animationState m_nowState;

        private void Reset()
        {
            m_animator = GetComponent<Animator>();
            m_stateName = "State";
        }

        // アニメーションの再生速度
        void AnimationSpeed(float _speed)
        {
            ///m_animator.SetFloat("MovingSpeed", Speed);
            m_animator.speed = _speed;
        }

        // 再生するアニメーションを設定
        public void SetAnimation(animationState _state)
        {
            if(m_nowState == _state) { return; }

            m_animator.SetInteger(m_stateName, (int)_state);
            m_nowState = _state;
        }

        // アニメーションイベント
        void AnimatnEnd()
        {
            //Debug.Log("Animation が終わった");
            m_animator.SetInteger(m_stateName, (int)animationState.Idol);
            m_nowState = animationState.Idol;
        }

        // 開始時の初期化
        void Start()
        {
            Reset();

            // 待機状態に変更
            SetAnimation((int)animationState.Idol);
        }

        // 更新
        void Update()
        {
            // 停止確認用
            if (Input.GetKey(KeyCode.Space))
            {
                AnimationSpeed(0.0f);
            }
            else
            {
                AnimationSpeed(1.0f);
            }

            if (isDirRange(0.0f, 10.0f))
            {
                // 近接
                EnemyDatas.instance.animation.SetAnimation(animationState.A_Bite);
            }
            else if(isDirRange(10.0f, 15.0f))
            {
                // 中距離
                EnemyDatas.instance.animation.SetAnimation(animationState.A_Tackle);
            }
            else if (isDirRange(15.0f, 30.0f))
            {
                // 遠距離
                EnemyDatas.instance.animation.SetAnimation(animationState.A_Magic);
            }
            else if (isDirRange(30.0f, 40.0f))
            {
                // 追従
                EnemyDatas.instance.animation.SetAnimation(animationState.Walk);
            }
            else
            {
                // 待機
                EnemyDatas.instance.animation.SetAnimation(animationState.Idol);
            }

        }

        bool isDirRange(float min, float max )
        {
            if (EnemyDatas.instance.m_dir > min && EnemyDatas.instance.m_dir < max)
            { return true; }

            return false;
        }
    }
}