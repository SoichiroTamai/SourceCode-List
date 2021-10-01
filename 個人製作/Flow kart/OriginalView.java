package com.example.myapplication;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.fonts.Font;
import android.os.Handler;
import android.util.Log;
import android.util.StringBuilderPrinter;
import android.view.View;

import org.w3c.dom.Text;

/*
*OriginalView
*
* AndroidView、メインループ管理クラス
*
*
*/

public class OriginalView extends View {
    public OriginalView(Context context) {
        super(context);
    }

    // スレッドスタート
    public void start()
    {
        // アプリケーション本体にView情報を渡す
        App.Get().SetView(this);

        Thread trd = new Thread(new Runnable(){
            public void run() {
                while(loop) {

                    long prev = System.currentTimeMillis();

                    // ゲーム根幹処理
                    mHandler.post(new Runnable() {
                        public void run() {
                            loop = Update(); // 自作のループ処理開始
                        }
                    });

                    // メインスレッドに再描画を任せる
                    mHandler.post(new Runnable() {
                        public void run() {postInvalidate();} // 再描画依頼
                    });

                    // FPS制御
                    long ptTime = 32-(System.currentTimeMillis()-prev);
                    try {
                        if(ptTime<0) {
                            Thread.sleep(1);
                        }else{
                            Thread.sleep(ptTime);
                            //Log.d("sleep", "ptTime : " + ptTime);
                        }
                    } catch (Exception e) {
                    }
                }
            }
        });
        trd.start();
    }

    // メインループ
    private boolean Update()
    {
        return App.Get().Update();
    }

    // Androidから呼び出される描画関数
    @Override
    protected void onDraw(Canvas c) {
        super.onDraw(c);
        canvas = c; // メインキャンバスを覚える

        isDraw = true;
        App.Get().Draw();
        isDraw = false;
    }

    // 文字列描画
    public void DrawString(float _x, float _y, String _str, int _color)
    {
        if(canvas == null){return;}

        paint.setColor(_color);
        paint.setTextSize(50);

        canvas.drawText(_str, _x, _y, paint);
    }

    // ディスプレイ座標に修正ver
    public void DrawString_SD(float _x, float _y, String _str, int _color)
    {
        _x *= App.Get().SD();
        _y *= App.Get().SD();

        if(canvas == null){return;}

        paint.setColor(_color);
        paint.setTextSize(50);

        canvas.drawText(_str, _x, _y, paint);
    }

    // TextSize調整なしver
    public void DrawString_NTS(float _x, float _y, String _str, int _color)
    {
        _x *= App.Get().SD();
        _y *= App.Get().SD();

        if(canvas == null){return;}

        paint.setColor(_color);

        float Psize = paint.getTextSize();

        paint.setTextSize(Psize);

        canvas.drawText(_str, _x, _y, paint);
    }

    // ディスプレイ座標に修正ver
    public void DrawString_SD(float _x, float _y, String _str, int _color, float TextSize)
    {
        _x *= App.Get().SD();
        _y *= App.Get().SD();

        if(canvas == null){return;}

        paint.setColor(_color);
        paint.setTextSize(TextSize);

        canvas.drawText(_str, _x, _y, paint);
    }

    // 指定した横幅にテキストサイズが収まるように調整してから表示(ディスプレイ座標に修正ver)
    public void DrawString_AutoTextReSize(float _x, float _y, String _str, int _color, float TextWidth, float minTestWidth)
    {
        _x *= App.Get().SD();
        _y *= App.Get().SD();

        if(canvas == null){return;}

        paint.setColor(_color);
        paint.setTextSize(OriginalMath.Get().GetAdjustTextSize(_str, TextWidth, minTestWidth));

        canvas.drawText(_str, _x, _y, paint);
    }

    public void SetAutoTextSize( String _str, float TextWidth, float minTestWidth){
        paint.setTextSize(OriginalMath.Get().GetAdjustTextSize(_str, TextWidth, minTestWidth));
    }

    public Paint GetPaint() { return paint; }

    private boolean loop = true;                // 終了検知
    private Handler mHandler = new Handler();   // スレッドハンドル

    private Canvas canvas = null;               // 描画先
    public Canvas GetCanvas(){return canvas;}

    private Paint paint = new Paint();          // 描画情報
    private int frameCnt = 0;                   // 処理フレームカウント用

    // 描画して良いかどうか
    private boolean isDraw = false;
    public boolean IsDraw(){return isDraw;}

//    public Bitmap getBitmapFromView()
//    {
//        Bitmap bitmap = Bitmap.createBitmap(this.getWidth(), this.getHeight(), Bitmap.Config.ARGB_8888);
////        Canvas canvas = new Canvas(bitmap);
////        this.draw(canvas);
//        return bitmap;
//    }
}