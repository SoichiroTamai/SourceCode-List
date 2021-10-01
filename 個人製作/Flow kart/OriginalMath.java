package com.example.myapplication;
import android.graphics.Paint;
import android.graphics.Rect;

import org.w3c.dom.Text;

// 算術クラス ( Math + α )
public class OriginalMath {
    private OriginalMath() { }
    private static OriginalMath originalMath = new OriginalMath();
    public static final OriginalMath Get() {
        return originalMath;
    }

    // 逆行列を取得
    public Vector2 GetInverseMat( Vector2 v ){
        v.x *= -1.0f;
        v.y *= -1.0f;
        return v;
    }

    // 2つの座標の直線距離を求める
    public static float GetHypotenuse(Vector2 v1, Vector2 v2) {
        float hypotenuse = (float)Math.sqrt((Math.pow(v1.x - v2.x, 2) + Math.pow(v1.y - v2.y, 2)));
        return hypotenuse;
    }

    // 四角形の中かどうかを判定( Determine_if_inside_rect )
    public boolean IconClick_inside_rect(Vector2 Pos, Rect region) {
        if (Pos.x >= region.left && Pos.x <= region.right &&
                Pos.y >= region.top && Pos.y <= region.bottom) {
            return true;
        }
        return false;
    }

    // クリック座標が四角形の中かどうかを判定
    public boolean IconClick_inside_rect(Pointer p, float left, float right, float top, float bottom) {

        if ((float)p.GetUpPos().x >= left && (float)p.GetUpPos().x <= right &&
            (float)p.GetUpPos().y >= top  && (float)p.GetUpPos().y <= bottom) {
            return true;
        }
        return false;
    }

    // クリック座標が四角形の中かどうかを判定
    public static boolean Click_inside_rect(Pointer p, float left, float right, float top, float bottom) {

        if ((float)p.GetDownPos().x >= left && (float)p.GetDownPos().x <= right &&
            (float)p.GetDownPos().y >= top  && (float)p.GetDownPos().y <= bottom) {
            return true;
        }
        return false;
    }

    public boolean isInside_OrderList(Pointer p) {

        if ((float)p.GetDownPos().x >= 65.0f && (float)p.GetDownPos().x <= 128.0f &&
                (float)p.GetDownPos().y >= 88.0f  && (float)p.GetDownPos().y <= Scene.EdgeOfScreenY) {
            return true;
        }
        return false;
    }

    public static boolean isInside_OrderList(float y){
        if ( y >= 88.0f + 16.0f && y <= Scene.EdgeOfScreenY - 16.0f)
            return true;

        return false;
    }

    // クリック座標が四角形の中かどうかを判定
    public boolean Click_inside_rect(Pointer p, Rect _rect) {

        if (p.GetDownPos().x >= _rect.left && p.GetDownPos().x <= _rect.right &&
            p.GetDownPos().y >= _rect.top  && p.GetDownPos().y <= _rect.bottom) {
            return true;
        }
        return false;
    }

    // 端子アイコン用の判定
    public boolean IconClick_inside_rect(Pointer p, Vector2 v, float icSize) {

        if ((float) p.GetDownPos().x >= v.x - icSize / 2 && (float) p.GetDownPos().x <= v.x + icSize / 2 &&
            (float) p.GetDownPos().y >= v.y - icSize / 2 && (float) p.GetDownPos().y <= v.y + icSize / 2) {
                return true;
        }
        return false;
    }

    // ラダー図内か
    public boolean Inside_the_Ladder(Pointer p, float left, float right, float top, float bottom) {

        if ((float) p.GetDownPos().x >= left && (float) p.GetDownPos().x < right &&
            (float) p.GetDownPos().y >= top  && (float) p.GetDownPos().y < bottom)
        {
            return true;
        }
        return false;
    }

    // 画像サイズ取得
    public Vector2 GetTextureSize(String _pass) {
        Vector2 TexSize = new Vector2(App.Get().ImageMgr().Get(_pass).getWidth(), App.Get().ImageMgr().Get(_pass).getHeight());
        return TexSize ;
    }

    // Textサイズ取得 ( 左右に余白のある横幅 )
    public float GetTextWidth(String _str) {
        float TextWidth = 0.0f;
        TextWidth = App.Get().GetView().GetPaint().measureText(_str) / App.Get().SD();

        return TextWidth;
    }

    // Textサイズ取得   ※ TextSizeの調整あり
    public float GetTextWidth_AutoTextSize(String _str, float TextWidth, float minSize) {
        App.Get().GetView().GetPaint().setTextSize(GetAdjustTextSize(_str,TextWidth,minSize));
        App.Get().GetView().SetAutoTextSize(_str,TextWidth,minSize);
        TextWidth = App.Get().GetView().GetPaint().measureText(_str) / App.Get().SD();

        return TextWidth;
    }

    public float GetTextWidth(String _str, Paint paint ) {
        float TextWidth = paint.measureText(_str);
//        Rect TextSize = GetTextRect(_str);
//        float TextWidth = TextSize.right - TextSize.left;
        //TextWidth *= App.Get().SD();
        return TextWidth;
    }


    // Textの高さを取得
    public float GetTextHeight(String _str) {
        Rect TextSize = GetTextRect(_str);
        float TextHeight = TextSize.bottom - TextSize.top;
        TextHeight *= App.Get().DS();
        return TextHeight;
    }

    // Textの高さを取得
    public float GetTextHeight(String _str, Paint paint) {
        Rect TextSize = GetTextRect(_str);
        float TextHeight = TextSize.bottom - TextSize.top;
        return TextHeight;
    }

    // Textサイズ取得 ( テキストがぴったり入る矩形サイズ )
    public Rect GetTextRect(String _str) {
        Rect bounds = new Rect();
        App.Get().GetView().GetPaint().getTextBounds(_str,0,_str.length(),bounds);
        return bounds;
    }

    // Textサイズ取得 ( テキストがぴったり入る矩形サイズ )
    public Rect GetTextRect(String _str, Paint paint ) {
        Rect bounds = new Rect();
        App.Get().GetView().GetPaint().getTextBounds(_str,0,_str.length(),bounds);

        // ディスプレイ座標サイズに修正
//        bounds.top *= App.Get().SD();
//        bounds.bottom *= App.Get().SD();
//        bounds.right *= App.Get().SD();
//        bounds.left *= App.Get().SD();

        return bounds;
    }

    /**
     * 指定された幅に収まるようなテキストサイズを取得。
     *
     * - 全体的にピクセル単位 (sp ではない) なので注意！
     *
     * @param text          入れたいテキスト
     * @param width         収めたい幅 (px)
     * @param initTextSize  テキストサイズ初期値 (この値からはじめて徐々に小さくしていく) (px)
     * @param minTextSize   最小テキストサイズ (このサイズよりは小さくしない) (px)
     *
     * @return  収まるテキストサイズ (px) ※　paint.setTextSize() に使用
     *
     * 【 参考サイト 】 https://gist.github.com/kyuuki/5704195
     */

    // テキストサイズを１行に収まるように調整 ( 初期値あり )
    public float GetAdjustTextSize(String text, float width, float initTextSize, float minTextSize) {
        Paint paint = App.Get().GetView().GetPaint();

        width *= App.Get().SD();
        minTextSize *= App.Get().SD();

        // テキストサイズ (この値を徐々に小さくしていく)
        float textSize = initTextSize;

        // Paint にテキストを書いてテキスト横幅取得
        paint.setTextSize(textSize);
        float textWidth = paint.measureText(text);

        /*
         * 横幅に収まるまでループ
         */
        while (width < textWidth) {
            textSize -= 1;

            // 最小サイズ以下になったら終了
            if (minTextSize > 0 && minTextSize >= textSize) {
                textSize = minTextSize;
                break;
            }

            paint.setTextSize(textSize);
            textWidth = paint.measureText(text);
        }

        return textSize;
    }


    // テキストサイズを１行に収まるように調整 ( 初期値なし )
    public float GetAdjustTextSize(String text, float width, float minTextSize) {
        Paint paint = App.Get().GetView().GetPaint();

        width *= App.Get().SD();
        minTextSize *= App.Get().SD();

        // テキストサイズ (この値を徐々に小さくしていく)
        float textSize = 50.0f;
        //float textSize = GetTextWidth(text,width,minTextSize);

        // Paint にテキストを書いてテキスト横幅取得
        paint.setTextSize(textSize);
        float textWidth = paint.measureText(text);

        /*
         * 横幅に収まるまでループ
         */
        while (width < textWidth) {
            textSize -= 1;

            // 最小サイズ以下になったら終了
            if (minTextSize > 0 && minTextSize >= textSize) {
                textSize = minTextSize;
                break;
            }

            paint.setTextSize(textSize);
            textWidth = paint.measureText(text);
        }

        return textSize;
    }
}
