using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStatus : MonoBehaviour
{
    //成功する確率
    public int Probability;
    public int probability;

    //成功の最大値
    public int MaxClearNum = 100;
    //成功の最小値
    public int MinClearNum = 0;

    //成功したかどうか
    private bool IsClear;

    // Start is called before the first frame update
    void Start()
    {
        //何パーセントで成功するかを割り出す
        Probability = Random.Range(MinClearNum, MaxClearNum);

        //そのパーセントで成功するかどうかを出す
        probability = Random.Range(0, 100);

        //さっき出した数より前に出した数の方が大きければ成功
        if(probability<Probability)
        {
            IsClear = true;
        }
        else
        {
            IsClear = false;
        }
    }

    public bool GetIsClear(){ return IsClear; }

    // Update is called once per frame
    void Update()
    {
        
    }
}
