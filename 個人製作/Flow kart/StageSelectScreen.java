package com.example.myapplication;

public class StageSelectScreen extends Scene {

    // 背景 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    protected String    Background_img      = "StageSelectAsset/Background_S.png";
    protected String    MapButton_Back_img  = "StageSelectAsset/MapButton_Back.png";
    protected Vector2   MapButton_Pos       = new Vector2(0.0f,0.0f);           // 1つ目の座標
    private   Vector2   MapSelectBuckSize   = new Vector2(0.0f,0.0f);

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {
        MapButton_Pos     = new Vector2(160.0f,160.0f);
        MapSelectBuckSize = new Vector2( OriginalMath.Get().GetTextureSize(MapButton_Back_img).x, OriginalMath.Get().GetTextureSize(MapButton_Back_img).y);
    }
    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update(){
        Pointer p = App.Get().TouchMgr().GetTouch();
        if (p == null) {
            return;
        } //早期

        if(p.OnUp()) {
            // シーン切り替え ( 戻る )
            if(OriginalMath.Get().IconClick_inside_rect(p, 0.0f, MapSelectBuckSize.x,0.0f, MapSelectBuckSize.y )) {
                App.Get().scene = App.e_Scene.Title;
            }

            // ステージ
            for (int i = 0; i < App.Get().mapJsonData.MapData.size(); i++) {
                if(OriginalMath.Get().IconClick_inside_rect(p, MapButton_Pos.x + 150.0f * i - MapSelectBuckSize.x / 2,MapButton_Pos.x + 150.0f * i + MapSelectBuckSize.x / 2,
                                                               MapButton_Pos.y - MapSelectBuckSize.y / 2,MapButton_Pos.y + MapSelectBuckSize.y / 2)) {
                    SetMapData(i);
                    App.Get().scene = App.e_Scene.Pogramming;
                }
            }
        }
    }

    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {
        BackgroundDraw(Background_img);

        // 戻る
        App.Get().ImageMgr().DrawLU(BuckButton_img, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f);
        App.Get().GetView().DrawString_SD(EdgeOfScreenX * 0.33f, EdgeOfScreenY * 0.15f, "ステージ選択", 0xff000000,100.0f);

        for (int i = 0; i < App.Get().mapJsonData.MapData.size(); i++) {
            App.Get().ImageMgr().Draw(MapButton_Back_img,MapButton_Pos.x + 150.0f * i, MapButton_Pos.y );
            App.Get().GetView().DrawString_SD(MapButton_Pos.x + 150.0f * i - 10.0f,MapButton_Pos.y + 10.0f, App.Get().mapJsonData.MapData.get(i).Map_No, 0xff000000,100.0f);
        }
    }
}