package com.example.myapplication;

public class ResultScreen extends Scene {

    // 背景 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private String Background_img = "TitleAsset/Background_T.png";

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {

    }

    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update(){

        Pointer p = App.Get().TouchMgr().GetTouch();
        if (p == null) {
            return;
        } //早期

        if(p.OnUp()){

            ClearTime_Reset();

            // シーン切り替え ( 戻る )
            if(OriginalMath.Get().IconClick_inside_rect(p, ButtonSize.x, EdgeOfScreenX, ButtonSize.y, EdgeOfScreenY)) {
                App.Get().scene = App.e_Scene.Pogramming;
            }
        }
    }

    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {
        BackgroundDraw(Background_img);

        App.Get().GetView().DrawString_SD(EdgeOfScreenX * 0.13f, EdgeOfScreenY * 0.25f, "クリアおめでとうございます！", 0xff000000,100.0f);
        ClearTimeDraw(EdgeOfScreenX * 0.22f, EdgeOfScreenY * 0.45f, "クリアタイム　", 100.0f);
        TimeDraw(EdgeOfScreenX * 0.22f, EdgeOfScreenY * 0.65f,"目標タイム　　",100.0f , TargetTime);
        //App.Get().GetView().DrawString_SD(EdgeOfScreenX * 0.2f, EdgeOfScreenY * 0.6f, "目標時間　　3分　0秒", 0xff000000,100.0f);

        // 戻る
        App.Get().ImageMgr().DrawLU(BuckButton_img,EdgeOfScreenX - OriginalMath.Get().GetTextureSize(BuckButton_img).x,EdgeOfScreenY - OriginalMath.Get().GetTextureSize(BuckButton_img).y,1.0f,1.0f,0.0f);

    }
}
