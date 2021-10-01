using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    //スクロールが始まったかのフラグ
    private bool ScrollStartFlg = false;
    //スクロールの起点となる座標
    private Vector2 scrollSrartPos = new Vector2();
    //左右の移動制限
    public static float SCROLL_END_LEFT = 10;
    public static float SCROLL_END_RIGHT = -10;
    //スクロール距離
    public static float SCROLL_DISTANCE_CORRECTION = 0.001f;

    //タッチポジションの初期化
    private Vector2 ClickPosition = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //レイを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (ScrollStartFlg==false&&hit2d)
            {
                //タッチ位置にオブジェクトがあったら動かない
                //スクロールとオブジェクトタッチの処理を分ける
                return;
            }
            else
            {
                //タッチした場所に何もなければスクロール開始
                ScrollStartFlg = true;
                //X軸方向に動いていなければ
                if(scrollSrartPos.x==0.0f)
                {
                    //スクロール開始位置を取得(マウスのワールド座標)
                    scrollSrartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    //クリックした位置を格納
                    Vector2 ClickMovePos = ClickPosition;

                    if (scrollSrartPos.x != ClickMovePos.x) 
                    {
                        //直前のタッチ位置との差を格納する
                        float diffPos = SCROLL_DISTANCE_CORRECTION * (ClickMovePos.x - scrollSrartPos.x);

                        //カメラの座標
                        Vector2 Pos = this.transform.position;
                        //タッチ位置との差分カメラ座標を動かす
                        Pos.x -= diffPos;

                        //スクロールが制限を超過する場合処理を止める
                        //if(Pos.x>SCROLL_END_RIGHT||Pos.x<SCROLL_END_LEFT)
                        //{
                        //    return;
                        //}

                        //カメラの座標を計算した分移動
                        this.transform.position = Pos;
                        //スクロール開始位置を変更し、次のスクロールに備える
                        scrollSrartPos = ClickMovePos;
                    }
                }
            }
        }
        else
        {
            //タッチを離したらフラグを落とし、スクロール開始位置も初期化する
            ScrollStartFlg = false;
            scrollSrartPos = new Vector2();
        }
    }
}
