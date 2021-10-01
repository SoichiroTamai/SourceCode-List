package com.example.myapplication;

public class TitleScreen extends Scene {

    //protected final Vector2 ciPar = new Vector2(2.75f,3.2f);    // 文字 → 画像

    // 背景 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private String Background_img = "TitleAsset/Background_T.png";
    private String Logo_img = "TitleAsset/Flowkart.png";
    private String TapStart_img = "TitleAsset/TapStart.png";

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {
        // シーン、ゲームオブジェクトの初期化
        SceneInit();

        // Alpha値の変化量
        ChangeAlpha = 10;
    }

    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update(){

        UpdateAlpha();

        Pointer p = App.Get().TouchMgr().GetTouch();
        if (p == null) {
            return;
        } //早期

        if(p.OnUp())
            // シーン切り替え
            App.Get().scene = App.e_Scene.StageSelect;
    }

    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {

        BackgroundDraw(Background_img);
//        App.Get().GetView().DrawString(EdgeOfScreenX * App.Get().SD(), EdgeOfScreenY * App.Get().SD(), "＋", 0xff000000);
//        App.Get().GetView().DrawString(Background_Pos.x * App.Get().SD(), Background_Pos.y * App.Get().SD(), "■", 0xff000000);
//        App.Get().GetView().DrawString(275.0f, 320.0f, "＋", 0xff000000);
//        App.Get().ImageMgr().Draw(Test_img, 100.0f, 100.0f, 1.0f, 1.0f, 0.0f);
//
//        App.Get().GetView().DrawString(50.0f * ciPar.x, 50.0f * ciPar.y, "＋", 0xff0000ff);
//        App.Get().ImageMgr().Draw(Test_img, 50.0f, 50.0f, 1.0f, 1.0f, 0.0f);

//        for (float i = 0.0f; i <= 300.0f; i++){
//            App.Get().GetView().DrawString(300.0f, i, "・", 0xff0000ff);
//            App.Get().GetView().DrawString(i,300.0f, "・", 0xff0000ff);
//        }

//        float w = 1895.0f;
//        float h = 1100.0f;

//        float scaleX;
//
//        float w = 0.0f * App.Get().SD();
//        float h = 0.0f * App.Get().SD();

        //App.Get().ImageMgr().Draw(LogoCycle_img,EdgeOfScreenX/2.0f,EdgeOfScreenY /2.0f,2.0f,1.8f,0.0f);
        // タイトル
        App.Get().ImageMgr().Draw(Logo_img,EdgeOfScreenX/2.0f,EdgeOfScreenY * 0.3f,1.2f,1.2f,0.0f);

        // TapStart ( 点滅処理 )
        DrawFlash(TapStart_img,EdgeOfScreenX/2.0f,EdgeOfScreenY * 0.7f,0.7f,0.7f,0.0f);

        if(DebugFlg) {
            //App.Get().GetView().GetCanvas().drawLine(0.0f, 0.0f, EdgeOfScreenX * App.Get().SD(), EdgeOfScreenY * App.Get().SD(), App.Get().GetView().GetPaint());
        }

//        for (float i = 0.0f; i <= w; i++)
//            App.Get().GetView().DrawString(i,h, "・", 0xff0000ff);
//
//        for (float i = 0.0f; i <= h; i++)
//            App.Get().GetView().DrawString(w, i, "・", 0xff0000ff);

//            for (float i=0.0f;i<=300.0f;i++){
//            App.Get().ImageMgr().Draw(Test_img,300.0f, i, 1.0f, 1.0f,0.0f);
//            App.Get().ImageMgr().Draw(Test_img,i,300.0f, 1.0f, 1.0f,0.0f);
//        }
    }
}
