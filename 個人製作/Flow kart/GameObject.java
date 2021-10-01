package com.example.myapplication;
import android.graphics.Color;
import android.graphics.DrawFilter;
import android.graphics.Rect;

import java.util.ArrayList;

public class GameObject {

    // 前の値を引き継ぐ ========================================================================
    // 確認用
    protected static final String Test_img = "System/Test.png";
    protected static final String Test2_img = "System/Test_Blue.png";

    // マップ
    protected static String Map_img = "Share/StageMap/Map1.png";
    protected static final String Background_Map_img = "Share/Background_Map.png";

    // 車
    protected static final String CAR_img = "Share/car.png";
    protected static final String Sensor_img = "Share/sensor.png";

    int frame = 0;

    public static final int GetPixelColor( String _pas, int x, int y ) { return App.Get().ImageMgr().Get(_pas).getPixel(x,y);}

    // センサーの個数
    protected static final int sensorNum = 6;
    protected static final int OrderListDrawNum = OrderList.OrderList_End.ordinal();


    class InputData{
        OrderList   order_id;
        boolean     InternalRelay;

        InputData(){
            order_id        = OrderList.None;
            InternalRelay   = false;
        };
    }

    public ArrayList<InputData> InputDatas;
    public boolean isSensor[] = new boolean[sensorNum];

    // ========================================================================================
    // ラダー図
    protected String  AND_img = "ProgrammingAsset/ic_and.png";
    protected String  ANI_img = "ProgrammingAsset/ic_ani.png";
    protected String  OUT_img = "ProgrammingAsset/ic_out.png";
    protected String  WIRE_img = "ProgrammingAsset/ic_wire.png";
    protected String  BRANCH_img = "ProgrammingAsset/ic_branch.png";
    protected String  BU_img = "ProgrammingAsset/ic_bu.png";
    protected String  BD_img = "ProgrammingAsset/ic_bd.png";

    // 画像指定用
    public enum ic {None, And, Ani, InputEnd,
                    Out, OutputEnd ,
                    Timer, Counter, IOEnd,
                    Wire, Branch, Bd, Bu , End}
    // 命令リスト
    //public enum OrderList {None,SNSR1,SNSR2,SNSR3,SNSR4,SNSR5,SNSR6, InputEnd,  // 入力 → パーツ名
    public enum OrderList {None, 起動ボタン, センサー1, センサー2, センサー3, センサー4, センサー5, センサー6, InputEnd,  // 入力 → パーツ名    ※ SNSR → センサー
                            前進, 後進, 左移動, 右移動, 左旋回, 右旋回, OutputEnd,                                // 出力 → 行動パターン )
                            Timer, Counter, M,                                                                // 入出力可能+(特殊なノード)
                            OrderList_End };

    // 内部リレー (M)
    //public static boolean InternalRelay[] = new boolean[100];
    public int useInternalRelayNum = 0;     // 使用している内部リレーの個数

    // ラダー図格納用　( プログラムデータ情報 )
    protected static class DataInformation {
        protected ic        imgNo;
        protected Vector2   Pos;
        protected OrderList order_id;
        protected int       M_No;

        protected DataInformation() { }

        protected DataInformation(ic ai, OrderList ao, Vector2 av) {
            imgNo    = ai;
            order_id = ao;
            Pos      = av;
        }

        protected DataInformation(ic ai, OrderList ao, Vector2 av, int am) {
            imgNo       = ai;
            order_id    = ao;
            Pos         = av;
            M_No        = am;
        }

        // 内部リレーの番号用
        protected void  SetMNumber( int _no ){ M_No = _no; }
        protected int   GetMNumber() { return M_No; }

    }

    // ラダー図が1行で格納できる数 ( 端子数 )
    protected static final int Ladder_NumPerLine = 7;

    // どのセンサーがONか
    protected static boolean isSensorFlg[] = new boolean[sensorNum];

    // プログラムデータ
    public static ArrayList<DataInformation> ProgramDate = new ArrayList<>();

    // 2次元ver
    public DataInformation[] LineDate = new DataInformation[Ladder_NumPerLine];
    public static ArrayList<DataInformation[]> ProgramDates = new ArrayList<>();
    public static DataInformation[][] Program2DArray = new DataInformation[8][Ladder_NumPerLine];
    public int column = 0; // 行数

    protected String CURSOR_img = "ProgrammingAsset/ic_cursor.png";
    protected void DrawCursor(float x, float y){ App.Get().ImageMgr().Draw(CURSOR_img, x, y); }

     //車 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    protected Vector2          CAR_Pos     = new Vector2(0.0f, 0.0f);  // 画面上での座標
    protected static Vector2   CAR_Pos_Abs = new Vector2(0.0f, 0.0f);  // マップ上での座標
    protected Vector2[]        sensor_Pos = new Vector2[sensorNum];
    protected static boolean   CAR_Pos_Abs_Fixflg = false;                     // 初期位置の修正
    protected static float     ResetPosX = 0.0f;
    protected static float     ResetPosY = 0.0f;

    public void DrawUICar(float car_Scale){

        // sensor
        sensor_Pos[0] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y - 60.0f);
        sensor_Pos[1] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y - 60.0f);
        sensor_Pos[2] = new Vector2(CAR_Pos.x - 30.0f,CAR_Pos.y);
        sensor_Pos[3] = new Vector2(CAR_Pos.x + 30.0f,CAR_Pos.y);
        sensor_Pos[4] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y + 60.0f);
        sensor_Pos[5] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y + 60.0f);

        // 車表示
        App.Get().ImageMgr().Draw(CAR_img, CAR_Pos.x, CAR_Pos.y, car_Scale, car_Scale, 0.0f);

        // sensor表示
        for (int i = 0; i < sensorNum; i++ ){
            App.Get().ImageMgr().Draw(Test_img,sensor_Pos[i].x - 2.5f,sensor_Pos[i].y - 2.5f,car_Scale * 6.25f, car_Scale * 6.25f,0.0f);

            //センサー番号
            switch (i){
                case 0:
                case 1:
                    App.Get().GetView().DrawString_SD(sensor_Pos[i].x - 5.0f, sensor_Pos[i].y - 5.0f , Integer.toString(i+1) ,0xff000000);
                    break;
                case 2: App.Get().GetView().DrawString_SD(sensor_Pos[i].x - 15.0f, sensor_Pos[i].y + 5.0f, Integer.toString(i+1) ,0xff000000);
                    break;
                case 3: App.Get().GetView().DrawString_SD(sensor_Pos[i].x + 5.0f, sensor_Pos[i].y + 5.0f, Integer.toString(i+1) ,0xff000000);
                    break;
                case 4:
                case 5:
                    App.Get().GetView().DrawString_SD(sensor_Pos[i].x - 5.0f, sensor_Pos[i].y + 13.0f , Integer.toString(i+1) ,0xff000000);
                    break;

            }
        }

        // UI
        App.Get().GetView().DrawString_SD(CAR_Pos.x - 80.0f, CAR_Pos.y - 85.0f, "【 センサーの位置 】" ,0xff000000);
    }

    public void DrawCar(float car_Scale, float car_Rot,float MoveRot){

        // sensor
        if(car_Rot == 0.0f ) {
            sensor_Pos[0] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y - 60.0f);
            sensor_Pos[1] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y - 60.0f);
            sensor_Pos[2] = new Vector2(CAR_Pos.x - 30.0f,CAR_Pos.y);
            sensor_Pos[3] = new Vector2(CAR_Pos.x + 30.0f,CAR_Pos.y);
            sensor_Pos[4] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y + 60.0f);
            sensor_Pos[5] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y + 60.0f);
        }
        else {
            for (int i = 0; i< 6; i++){

                float AfterPosX = 0.0f;
                float AfterPosY = 0.0f;

                // センサーポスｘ = （車の中心点からのX座標） * Math.cos(alpha) - （車の中心点からのY座標） * Math.sin(alpha)＋車の中心点X
                // センサーポスｙ = （車の中心点からのX座標） * Math.sin(alpha) + （車の中心点からのY座標） * Math.cos(alpha)＋車の中心点Y

                AfterPosX = (sensor_Pos[i].x - CAR_Pos.x) * (float)Math.cos(Math.toRadians(MoveRot)) -
                            (sensor_Pos[i].y - CAR_Pos.y) * (float)Math.sin(Math.toRadians(MoveRot)) + CAR_Pos.x;

                AfterPosY = (sensor_Pos[i].x - CAR_Pos.x) * (float)Math.sin(Math.toRadians(MoveRot)) +
                            (sensor_Pos[i].y - CAR_Pos.y) * (float)Math.cos(Math.toRadians(MoveRot)) + CAR_Pos.y;

                sensor_Pos[i].x = AfterPosX;
                sensor_Pos[i].y = AfterPosY;
            }
        }

        // 車表示
        App.Get().ImageMgr().Draw(CAR_img, CAR_Pos.x, CAR_Pos.y, car_Scale, car_Scale, car_Rot);

        // sensor表示
        for (int i = 0; i < sensorNum; i++ )
            App.Get().ImageMgr().Draw(Sensor_img,sensor_Pos[i].x,sensor_Pos[i].y,1.0f, 1.0f, car_Rot);      // 常時
            //DrawFlash(Sensor_img,sensor_Pos[i].x,sensor_Pos[i].y,1.0f, 1.0f, car_Rot);                            // 点滅
            //App.Get().ImageMgr().Draw(Test_img,sensor_Pos[i].x - 2.5f,sensor_Pos[i].y- 2.5f,car_Scale * 6.25f, car_Scale * 6.25f, car_Rot);
    }

    public void DrawCar(float car_Scale, float car_Rot,float MoveRot, boolean _isSensor[]){

        // sensor
        if(car_Rot == 0.0f ) {
            sensor_Pos[0] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y - 60.0f);
            sensor_Pos[1] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y - 60.0f);
            sensor_Pos[2] = new Vector2(CAR_Pos.x - 30.0f,CAR_Pos.y);
            sensor_Pos[3] = new Vector2(CAR_Pos.x + 30.0f,CAR_Pos.y);
            sensor_Pos[4] = new Vector2(CAR_Pos.x - 10.0f,CAR_Pos.y + 60.0f);
            sensor_Pos[5] = new Vector2(CAR_Pos.x + 10.0f,CAR_Pos.y + 60.0f);
        }
        else {
            for (int i = 0; i< 6; i++){

                float AfterPosX = 0.0f;
                float AfterPosY = 0.0f;

                // センサーポスｘ = （車の中心点からのX座標） * Math.cos(alpha) - （車の中心点からのY座標） * Math.sin(alpha)＋車の中心点X
                // センサーポスｙ = （車の中心点からのX座標） * Math.sin(alpha) + （車の中心点からのY座標） * Math.cos(alpha)＋車の中心点Y

                AfterPosX = (sensor_Pos[i].x - CAR_Pos.x) * (float)Math.cos(Math.toRadians(MoveRot)) -
                        (sensor_Pos[i].y - CAR_Pos.y) * (float)Math.sin(Math.toRadians(MoveRot)) + CAR_Pos.x;

                AfterPosY = (sensor_Pos[i].x - CAR_Pos.x) * (float)Math.sin(Math.toRadians(MoveRot)) +
                        (sensor_Pos[i].y - CAR_Pos.y) * (float)Math.cos(Math.toRadians(MoveRot)) + CAR_Pos.y;

                sensor_Pos[i].x = AfterPosX;
                sensor_Pos[i].y = AfterPosY;
            }
        }

        // 車表示
        App.Get().ImageMgr().Draw(CAR_img, CAR_Pos.x + ResetPosX, CAR_Pos.y + ResetPosY, car_Scale, car_Scale, car_Rot);

        // sensor表示
        for (int i = 0; i < sensorNum; i++ )
            isDraw(Sensor_img,sensor_Pos[i].x + ResetPosX,sensor_Pos[i].y + ResetPosY,1.0f, 1.0f, car_Rot,_isSensor[i]);
    }

     // 点滅処理 ======================================================================================
    public Boolean AlphaFlg    = false;     // true … 加算, false ... 減算
    public int     ChangeAlpha = 0;         // Alpha値の変化量
    int ColorFrame = 0;


    public void UpdateAlpha(){
        if (ColorFrame>256)
            AlphaFlg = true;

        if(AlphaFlg) {
            ColorFrame -= ChangeAlpha;
            if (ColorFrame <= 0)
                AlphaFlg = false;
        }
        else
            ColorFrame += ChangeAlpha;
    }

    public void DrawFlash(String _imageName, float _x, float _y, float _sx, float _sy, float _rot){
        App.Get().ImageMgr().SetPaintAlpha(ColorFrame);
        App.Get().ImageMgr().Draw(_imageName, _x, _y, _sx,_sy, _rot);
        App.Get().ImageMgr().SetPaintAlpha(255);
    }

    // False の時は不透明描画
    public void isDraw(String _imageName, float _x, float _y, float _sx, float _sy, float _rot, boolean _is){

        if(!_is)
            App.Get().ImageMgr().SetPaintAlpha(80);

        App.Get().ImageMgr().Draw(_imageName, _x, _y, _sx,_sy, _rot);
        App.Get().ImageMgr().SetPaintAlpha(255);
    }
}
