package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.pm.ActivityInfo;
import android.graphics.Point;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.Display;
import android.view.View;
import android.widget.LinearLayout;

public class MainActivity extends AppCompatActivity {

    // ViewSize(コンテンツ領域サイズ取得用)
    private float dp;
    private int screenWidth, screenHeight;
    private OriginalView v;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // 横画面にするか縦画面にするか
        //noinspection AndroidLintSourceLockedOrientationActivity
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE); // 横画面固定
        //setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT); // 縦画面固定
        // 横画面にしたい場合は引数に「ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE」を指定
        // ちなみに上のコメントは、画面固定の警告を無視するものです。

        // 自分で作成したビューの設定
        //OriginalView v = new OriginalView(this);
        v = new OriginalView(this);
        setContentView(v);  // 使用するview(ウィンドウ)の設定

        // 描画設定
        App.Get().SetView(v);

        // 画面サイズ取得
        Display display = getWindowManager().getDefaultDisplay();
        Point point = new Point();
        display.getSize(point);

         // 解像度対応のため、画面サイズと基準になるサイズ(内部で使用する横幅)を設定
        // _spの値が画面上部の幅(横画面にした場合、長いほうが基準になるので値を増やす)
        App.Get().SetSDPar(point.x, 600);
        //App.Get().SetSDPar(point.x, 1280);

        App.Get().GetDisplaySize(point.x,point.y);

        // ゲーム用にウィンドウ表示部の設定
        int uiOptions =
                View.SYSTEM_UI_FLAG_HIDE_NAVIGATION     // 通知バー(上の時間とか)を消す
                |View.SYSTEM_UI_FLAG_FULLSCREEN         // ナビゲーションバー(下の操作ボタン)を消す
                |View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY;  // アプリ優先モード(上下のスワイプが無い限りフルスクリーンを続行し、しばらくしたらフルスクリーンに戻る)
        getWindow().getDecorView().setSystemUiVisibility(uiOptions); // 現在の画面(このアプリ)に設定

//        // ゲームの開始
//        v.start();

        // ViewSize(コンテンツ領域サイズ取得用)に一時切り替え
        setContentView(R.layout.activity_main);
        dp = getResources().getDisplayMetrics().density;
    }

    // ViewSize(コンテンツ領域サイズ取得)
    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);

        LinearLayout layout = findViewById(R.id.activity_main);
        int layoutWidth = layout.getWidth();
        int layoutHeight = layout.getHeight();

        // StatusBarの高さを求める
        App.Get().AskForStatusBar((float) layoutWidth,(float) layoutHeight);

        // 使用するview(ウィンドウ)を戻す
        setContentView(v);

        // ゲームの開始
        v.start();
    }
}
