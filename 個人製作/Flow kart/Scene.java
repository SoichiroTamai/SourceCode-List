package com.example.myapplication;

import android.graphics.Rect;

public class Scene extends GameObject {

    // ========================================================================================
    public static void SetMapData(int _no){
        Map_img     = "Share/StageMap/Map" + App.Get().mapJsonData.MapData.get(_no).Map_No + ".png";
        CAR_Pos_Abs = App.Get().mapJsonData.MapData.get(_no).StartPosition;
        TargetTime  = App.Get().mapJsonData.MapData.get(_no).TargetTime;
    }

    // 前の値を引き継ぐ ========================================================================
    // UI ==================================================
    // デバック用UI
    public Boolean DebugFlg = false;

    // 拡大率(画面調整用)
    public static float dsp = 0.0f;

    // 画面端(上限値)
    public static float EdgeOfScreenX = 0.0f;
    public static float EdgeOfScreenY = 0.0f;

    // 背景
    protected static Vector2 Background_Pos = new Vector2(0.0f, 0.0f);
    protected static Vector2 Background_Scale = new Vector2(0.0f, 0.0f);
    protected final float Background_W = 720.0f,Background_H = 405.0f;

    // ボタンサイズ
    protected float UIButtonSize_W = 65.0f, UIButtonSize_H = 30.0f;
    protected static final String BuckButton_img = "Share/Bu_Back.png";
    protected static final String PlayButton_img = "Share/Bu_Play.png";
    protected static final String FastForwardButton_ON_img = "RunningAsset/Bu_FastForward_ON.png";
    protected static final String FastForwardButton_OFF_img = "RunningAsset/Bu_FastForward_OFF.png";
    protected static Vector2 ButtonSize =  new Vector2 (0.0f,0.0f);


    // タイマー ==================================================================================================
    public long startTime           = 0;  // 開始時刻
    public long FMstartTime         = 0;  // 切り替え開始時刻(早送り)
    public long nowTime             = 0;  // 経過時刻
    public long FastForwardMagTime  = 0;  // 経過時刻(倍速)
    public long ReleseTime          = 0;  // 倍速が解除された時の加速分
    public long Time                = 0;
    public static long ClearTime    = 0; // クリア時間
    public static int  TargetTime   = 0; // 目標時間

    // 現在の時間を取得(ミリ秒＝1000で1秒)
    public void TimeReset() {
        startTime = System.currentTimeMillis();
        FastForwardMagTime = 0;
        nowTime = 0;
        ReleseTime = 0;
        Time= 0;
    }

    // 現在(呼び出し時)の時刻を取得
    public void setNowTime() {
        FMstartTime = System.currentTimeMillis();
    }

    // 倍速が解除された時
    public void WhenReleasingDoubleSpeed() {
        ReleseTime = FastForwardMagTime;
    }

    // 現在の時間から、最初に保存した時間を引き差分を現在の経過時間とする (等倍速)
    public void TimeUpdate() {
        nowTime = System.currentTimeMillis() - startTime + FastForwardMagTime;
    }

    // 減速
    public void TimeUpdate(float SlowSpeed) {
        Time = System.currentTimeMillis() - FMstartTime;
        FastForwardMagTime = (long) ((float)Time * SlowSpeed) - Time + ReleseTime;
        TimeUpdate();
    }

    // 加速
//    public void TimeUpdate(float FastForwardMag) {
//        Time = System.currentTimeMillis() - FMstartTime;
//        FastForwardMagTime = (Time * (long) FastForwardMag) - Time + ReleseTime;
//        TimeUpdate();
//        //nowTime = nowTime + FastForwardMagTime;
//    }

    // クリアタイム
    protected void ClearTime_Set(){ ClearTime = nowTime; }
    protected void ClearTime_Reset(){ ClearTime = 0; }

    // タイマー描画
    public void TimeDraw(float x, float y, String _str, float TextSize) {
        // 秒数
        int s = (int) nowTime / 1000;
        if (s < 60)
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(s) + "秒",0xff000000, TextSize);
        else
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(s / 60) + "分" + Integer.toString(s % 60) + "秒",0xff000000, TextSize);
    }

    // クリアタイム描画
    public void ClearTimeDraw(float x, float y, String _str, float TextSize) {
        // 秒数
        int s = (int) ClearTime / 1000;
        if (s < 60)
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(s) + "秒",0xff000000, TextSize);
        else
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(s / 60) + "分" + Integer.toString(s % 60) + "秒",0xff000000, TextSize);
    }

    // 時間を描画
    public void TimeDraw(float x, float y, String _str, float TextSize, int _Time) {
        // 秒数
        if (_Time < 60)
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(_Time) + "秒",0xff000000, TextSize);
        else
            App.Get().GetView().DrawString_SD(x,y,_str + Integer.toString(_Time / 60) + "分" + Integer.toString(_Time % 60) + "秒",0xff000000, TextSize);
    }

    // ====================================================================================================================================


    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    protected void SceneInit() {
        // 画面比率
        dsp = App.Get().DS();

        // 背景
        Background_Pos = new Vector2(App.Get().GetDisplaySize().x, App.Get().GetDisplaySize().y);
        Background_Scale = new Vector2(((Background_Pos.y*dsp)*1.778f)/Background_W,(Background_Pos.y*dsp)/Background_H);
        EdgeOfScreenX = (Background_Pos.y*dsp) * 1.778f;            // 横幅上限値
        EdgeOfScreenY = Background_Pos.y*dsp;                       // 縦幅上限値

        // 注視点が中央の時の座標
        Background_Pos = new Vector2(EdgeOfScreenX/2.0f,        // 横幅上限値/2 16 / 9 = 1.778f
                EdgeOfScreenY/2.0f -1.0f);  // 縦幅上限値/2  - sbh(今回は1.0f固定)
    }

    // 背景を描画
    protected void BackgroundDraw(String Background_img) {
        App.Get().ImageMgr().DrawLU(Background_img,0.0f, 0.0f, Background_Scale.x, Background_Scale.y,0.0f);
        //App.Get().ImageMgr().Draw(Background_img,Background_Pos.x, Background_Pos.y, Background_Scale.x, Background_Scale.y,0.0f);
    }
}
