package com.example.myapplication;


public class RunningScreen extends Scene {

    // 背景 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private String Background_img = "RunningAsset/Background_R.png";

    // 移動量
    private Vector2 MovePos = new Vector2(0.0f, 0.0f);  // 移動座標
    private Vector2 MoveSum = new Vector2(0.0f, 0.0f);  // 総移動量
    private float MoveRot   = 0.0f;     // 回転量
    private float MoveSpeed = 0.0f;     // 移動速度
    private float car_Rot   = 0.0f;     // 車の回転度数

    private Vector2 MapPos = new Vector2(0.0f, 0.0f);

    // 条件文(判定箇所)
    private Vector2 conditional = new Vector2(0.0f, 0.0f);

    //private int a = 0xff000000;

    int colorPosX = 0;
    int colorPosY = 0;

    // 実行中か
    boolean isAlive     = false;
    int     sensorNo    = 0;
    int     NowSensorNo = 0;

    // マップ上での車の座標
    //Vector2 CarMapPos = new Vector2(0.0f,0.0f);

    // 早送り
    private boolean SlowModeFlg         = false;    // スローモードか
    private float   Car_SlowSpeedMag    = 0.0f;     // 何倍速か
    private float   Car_1xSpeed         = 0.0f;     // 初速度 (デフォルト)

    private boolean ClickFlg = false;

    private boolean GOALflg = false;

    float MapHeight = 0.0f;

    // 現在読み取り中
    // FastForwardのデータ
    DataInformation nowDIF = new DataInformation();

    //private Vector2 ButtonSize =  new Vector2 (0.0f,0.0f);
//    float colorPosScaleX = 0.0f;
//    float colorPosScaleY = 0.0f;

//    File file = new File(Environment.getExternalStorageDirectory() + "ScreenShot.png");
//    File file = new File(ScreenShot_img);
//    Bitmap bmp = null;
//    int color = 0;
    private static final int TouchColorPosY(Pointer p){ return (int) OriginalMath.Get().GetTextureSize(Map_img).y - ((int)EdgeOfScreenY - p.GetNowPos().y); };

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {

        //CAR_Pos = new Vector2(112.5f,EdgeOfScreenY * 0.7f);
        ButtonSize =  new Vector2 (EdgeOfScreenX - OriginalMath.Get().GetTextureSize(BuckButton_img).x, EdgeOfScreenY - OriginalMath.Get().GetTextureSize(BuckButton_img).y);

        //CAR_Pos          = new Vector2(112.5f,253.0f);
        //CAR_Pos_Abs          = new Vector2(112.5f,893.0f);    // マップ上での車の座標 (旧Map)
        //CAR_Pos_Abs          = new Vector2(490f,880.0f);      // マップ上での車の座標
        CarPos_Abs_to_Rel();

        Car_1xSpeed      = 2.5f;
        Car_SlowSpeedMag = 0.2f;
        MoveSpeed        = Car_1xSpeed;

        MapHeight = OriginalMath.Get().GetTextureSize(Map_img).y;
        TimeReset();

        //InputDataReset();

        //CarMapPos = new Vector2(CAR_Pos.x, MapHeight - CAR_Pos.y);
        //file.getParentFile().mkdir();
    }

    public void InputDataReset(){
        for (int i=0; i < sensorNum; i++) {
            InputData id = new InputData();
            switch (i){
                case 0: id.order_id = OrderList.センサー1; break;
                case 1: id.order_id = OrderList.センサー2; break;
                case 2: id.order_id = OrderList.センサー3; break;
                case 3: id.order_id = OrderList.センサー4; break;
                case 4: id.order_id = OrderList.センサー5; break;
                case 5: id.order_id = OrderList.センサー6; break;
                default: id.order_id = OrderList.None;   break;
            }

            if(i==0)
                InputDatas.add(0,id);
            else
                InputDatas.add(InputDatas.size(),id);
        }
    }

    // リセット ///////////////////////////////////////////////////////////////////////////////////////
    public void Rest(){

        MapPos  = new Vector2(0.0f, 0.0f);
        MoveSum = new Vector2(0.0f,0.0f);
        car_Rot = 0.0f;

        CarPos_Abs_to_Rel();

        if(!CAR_Pos_Abs_Fixflg) {
            CAR_Pos_Abs_Fixflg = true;
            ResetPosX =  EdgeOfScreenX/2 - CAR_Pos.x;
            ResetPosY =  EdgeOfScreenY/2 - CAR_Pos.y;
        }

        //CarPos_Abs_to_Rel(ResetPosX, ResetPosY);

//
        SlowModeFlg = false;
        ClickFlg    = false;
        GOALflg     = false;

        TimeReset();
    }

    // 電気が来ているか？
    boolean isElectrictyComing = false;

    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update(){

        TouchProc();

        // ON(ライン上) のセンサーを検索
        for (int i=0; i<sensorNum; i++){
            if(isSensor(sensor_Pos[i]))
                isSensorFlg[i] = true;
            else
                isSensorFlg[i] = false;
        }

        // スローモード
        if (SlowModeFlg){
            MoveSpeed = Car_1xSpeed * Car_SlowSpeedMag;
            TimeUpdate(Car_SlowSpeedMag);
        }
        else {
            MoveSpeed = Car_1xSpeed;
            TimeUpdate();
        }

        MovePos = new Vector2(0.0f,0.0f);
        MoveRot = 0.0f;

        // プログラム内を参照
        //for (int pd = 0;  pd >= 0 && pd < ProgramDate.size(); pd++){
        for (DataInformation pd : ProgramDate) {

            // 入力端子なら
            if(pd.order_id.ordinal() > OrderList.起動ボタン.ordinal() && pd.order_id.ordinal() < OrderList.InputEnd.ordinal())
                sensorNo = pd.order_id.ordinal()-OrderList.起動ボタン.ordinal() - 1;

            // 各端子ごとの処理
            switch (pd.imgNo) {
//                case Branch:
//                    break;
//                case Bd:
//                    break;
//                case Bu:
//                    break;
                case None:
                    isAlive = false;
                    break;

                case And:
                    //conditional.y = 200.0f;     // 仮) y座標が 200.0f になるまで稼働
                    if (pd.order_id == OrderList.起動ボタン){ isAlive = true; break; }
                    nowDIF = pd;
                    NowSensorNo = sensorNo;
                    isAlive = isSensor(sensor_Pos[sensorNo]);
                    break;

                case Ani:
                    nowDIF = pd;
                    NowSensorNo = sensorNo;
                    isAlive = !isSensor(sensor_Pos[sensorNo]);
                    break;

                case Out:
                    switch (pd.order_id){
                        case 前進:    MovePos.y =  MoveSpeed;
                            break;
                        case 後進:    MovePos.y = -MoveSpeed;
                            break;
                        case 左移動:  MovePos.x =  MoveSpeed;
                            break;
                        case 右移動:  MovePos.x = -MoveSpeed;
                            break;
                        case 右旋回:  MoveRot  =  MoveSpeed;
                            break;
                        case 左旋回:  MoveRot  = -MoveSpeed;
                            break;
                        case M:
                    }
                    break;
            }
        }

        // 入力時移動量加算
        if(isAlive){
            //CAR_Pos.Add(Move);
            MapPos.Add(MovePos);
            MoveSum.Add(OriginalMath.Get().GetInverseMat(MovePos));
            //CarMapPos.Add(MovePos);
            car_Rot += MoveRot;

            if(car_Rot > 360)
                car_Rot = 0;
            else  if(car_Rot < 0.0f)
                car_Rot = 360.0f - car_Rot;

//            for (int i = 0; i < sensorNum; i++ )
//                sensor_Pos[i].Add(MovePos);
        }

        // ゴール
        if(isGOAL()){
            ClearTime_Set();
            Rest();
            App.Get().scene = App.e_Scene.Result;
        }


//        if(a <= 0xffff0000)
//            a += 0x00110000;
//        else
//            a = 0xff000000;
    }

    private boolean isSensor( Vector2 InputdevicePos ) {
        // ライン上(黒線)に居るか？
        //if (GetPixelColor(Map_img,(int)InputdevicePos.x,(int) OriginalMath.Get().GetTextureSize(Map_img).y - ((int)EdgeOfScreenY - (int)InputdevicePos.y)) != 0xff000000)     // 車を移動させる場合
        //if (GetPixelColor(Map_img,(int)InputdevicePos.x,(int) MapHeight - ((int)EdgeOfScreenY - (int)InputdevicePos.y)) - (int)(MapHeight - MapPos.y)  != 0xff000000)   // マップを移動させる

        // ライン上か？
        if (GetPixelColor(Map_img,(int)(InputdevicePos.x + MoveSum.x),(int)(MapHeight - ( EdgeOfScreenY - InputdevicePos.y) + MoveSum.y)) == 0xff000000)
            return true;
        else
            return false;
    }

    private boolean isGOAL( ){
        if(GetPixelColor(Map_img,(int)(CAR_Pos.x + MoveSum.x),(int)(MapHeight - ( EdgeOfScreenY - CAR_Pos.y) + MoveSum.y)) == 0xffff0000)
            return true;
        else
            return false;
    }

    private void TouchProc() {
        // タッチ処理
        Pointer p = App.Get().TouchMgr().GetTouch();

        if (p == null) {
            ClickFlg = false;
            return;
        } //早期

        colorPosX = p.GetNowPos().x;
        colorPosY = (int)((float)TouchColorPosY(p) + MoveSum.y);

        // 早送り
        if(OriginalMath.Get().Click_inside_rect(p,0.0f,OriginalMath.Get().GetTextureSize(FastForwardButton_ON_img).x*0.8f,
                                                    0.0f, OriginalMath.Get().GetTextureSize(FastForwardButton_ON_img).y*0.8f) && ClickFlg == false) {
            ClickFlg = true;

            if(!SlowModeFlg)
                setNowTime();
            else
                WhenReleasingDoubleSpeed();

            SlowModeFlg = !SlowModeFlg;
        }

        //color = getViewCapture(App.Get().GetView()).getPixel(colorPosX,colorPosY);

        //bmp = App.Get().GetView()

        //color = bmp.getColor(colorPosX,colorPosY).toArgb();

//        colorPosScaleX = (float)colorPosX * (Background_Scale.x)/Background_W;
//        colorPosScaleY = (float)colorPosY * (Background_Scale.y)/Background_H;
//
//        colorPosX = (int)((float)colorPosX * colorPosScaleX);
//        colorPosY = (int)((float)colorPosY * colorPosScaleY);

        // 戻る
        if(OriginalMath.Get().IconClick_inside_rect(p, ButtonSize.x, EdgeOfScreenX, ButtonSize.y, EdgeOfScreenY)) {
                Rest();
                App.Get().scene = App.e_Scene.Pogramming;
        }
//        if(p.OnUp())
//            if (p.GetUpPos().x >= EdgeOfScreenX - MathNest.Get().GetTextureSize(BukcButton_img).x && p.GetUpPos().x <= EdgeOfScreenX &&
//                p.GetUpPos().y >= EdgeOfScreenY - MathNest.Get().GetTextureSize(BukcButton_img).y && p.GetUpPos().y <= EdgeOfScreenY) {
//                App.Get().scene = App.e_Scene.Pogramming;
//            }
    }


    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {
        BackgroundDraw(Background_img);
        App.Get().ImageMgr().DrawMap(Background_Map_img, MapPos.x + ResetPosX, MapPos.y + ResetPosY, 1.0f, 1.0f, 0.0f);
        App.Get().ImageMgr().DrawMap(Map_img, MapPos.x + ResetPosX, MapPos.y + ResetPosY, 1.0f, 1.0f, 0.0f);
        //App.Get().ImageMgr().Draw(CAR_img, CAR_Pos.x, CAR_Pos.y, 0.8f, 0.8f, 0.0f);

        //DrawCar(0.8f, car_Rot, MoveRot);
        DrawCar(0.8f, car_Rot, MoveRot, isSensorFlg);

        if(DebugFlg) {
            App.Get().GetView().GetPaint().setARGB(255, 255, 255, 255);
            App.Get().GetView().GetCanvas().drawRect(0.0f, 0.0f, 500.0f, 70.0f, App.Get().GetView().GetPaint());
            App.Get().GetView().GetCanvas().drawRect(1300.0f, 0.0f, 1950.0f, 70.0f, App.Get().GetView().GetPaint());

            App.Get().GetView().DrawString(20.0f, 50.0f, "car ( " + Float.toString(CAR_Pos.x + MoveSum.x) + ", " + Float.toString(CAR_Pos.y + MoveSum.y) + ")", 0xff000000);
        }

        //saveCapture(App.Get().GetView(),file);

//        App.Get().GetView().DrawString(1000.0f,300.0f,
//                "x = " + Integer.toString(colorPosX) + ", y =" + Integer.toString(colorPosY) + " : " +
//                        Integer.toHexString(color),
//                0xff000000);

        //saveCapture(file);

        //App.Get().ImageMgr().DrawLU(ScreenShot_img,100.0f,100.0f,0.5f,0.5f,0.0f);

        // sensor表示
//        for (int i = 0; i < sensorNum; i++ )
//            App.Get().ImageMgr().Draw(Test_img,sensor_Pos[i].x - 2.5f,sensor_Pos[i].y- 2.5f,5.0f,5.0f,0.0f);

        // 戻る
        App.Get().ImageMgr().DrawLU(BuckButton_img,EdgeOfScreenX - OriginalMath.Get().GetTextureSize(BuckButton_img).x,EdgeOfScreenY - OriginalMath.Get().GetTextureSize(BuckButton_img).y,1.0f,1.0f,0.0f);

        if(!DebugFlg) {
            // 時間を用背景
            App.Get().GetView().GetPaint().setARGB(255, 255, 255, 255);
            App.Get().GetView().GetCanvas().drawRect(1485.0f, 0.0f, 1950.0f, 17.0f * App.Get().SD(), App.Get().GetView().GetPaint());

            // スローモードUI表示
            if(SlowModeFlg)
                App.Get().ImageMgr().DrawLU(FastForwardButton_ON_img,0.0f,0.0f,0.8f,0.8f,0.0f);
            else
                App.Get().ImageMgr().DrawLU(FastForwardButton_OFF_img,0.0f,0.0f,0.8f,0.8f,0.0f);

            // 時間を表示
            TimeDraw(EdgeOfScreenX * 0.785f, 15.0f, "経過時間 ", 50.0f);
        }

        // デバッグ用
        if(DebugFlg){
            App.Get().GetView().GetPaint().setARGB(255,0,0,0);

            // タップ位置のマップの座標 & 色
            App.Get().GetView().DrawString(1315.0f,50.0f,
                                        "x = " + Integer.toString(colorPosX) + ", y = " + Integer.toString(colorPosY) + " : " + Integer.toHexString(GetPixelColor(Map_img,colorPosX,colorPosY)),
                                        0xff000000);

            if( nowDIF.order_id != null && nowDIF.order_id.ordinal() > OrderList.起動ボタン.ordinal()) {
                App.Get().GetView().GetPaint().setARGB(255,255,255,255);
                App.Get().GetView().GetCanvas().drawRect(0.0f,(EdgeOfScreenY - 20.0f)*App.Get().SD(),315.0f*App.Get().SD(), EdgeOfScreenY*App.Get().SD(),App.Get().GetView().GetPaint());
                App.Get().GetView().GetPaint().setARGB(255,0,0,0);
                App.Get().GetView().DrawString(0.0f, (EdgeOfScreenY-5.0f)*App.Get().SD(),
                        nowDIF.order_id.toString() + " ( " + Float.toString(sensor_Pos[NowSensorNo].x + MoveSum.x) + ", " +
                                                                   Float.toString(MapHeight - (EdgeOfScreenY - sensor_Pos[NowSensorNo].y) + MoveSum.y) + " ) : " +
                                Integer.toHexString(GetPixelColor(Map_img, (int)(sensor_Pos[NowSensorNo].x + MoveSum.x),(int)(MapHeight - (EdgeOfScreenY - sensor_Pos[NowSensorNo].y) + MoveSum.y))),
                        0xff000000);
            }
            //TimeDraw(EdgeOfScreenX * 0.75f, 30.0f);
        }

//        App.Get().GetView().DrawString(1000.0f,300.0f,
//                "x = " + Integer.toString(colorPosX) + ", y =" + Integer.toString(colorPosY) + " : " + Integer.toHexString(App.Get().GetView().getBitmapFromView().getPixel(colorPosX,colorPosY)),
//                0xff000000);
    }

    // 処理関数
    // マップ上での絶対値から画面上での相対座標に変換(車)
    private void CarPos_Abs_to_Rel () {
        CAR_Pos = new Vector2(CAR_Pos_Abs.x,0.0f);
        CAR_Pos.y  =  EdgeOfScreenY - ( 1000.0f - CAR_Pos_Abs.y - MoveSum.y);       // 1000.0f → マップの高さ
    }

//    // スクリーンショットを取得して保存する
//    public void saveCapture(File file) {
//        Bitmap capture = App.Get().GetView().getBitmapFromView();
//        FileOutputStream fos = null;
//        try {
//            fos = new FileOutputStream(file, false);
//            // 画像のフォーマットと画質と出力先を指定して保存
//            capture.compress(Bitmap.CompressFormat.PNG, 100, fos);
//            fos.flush();
//        } catch (Exception e) {
//            e.printStackTrace();
//        } finally {
//            if (fos == null) return;
//            try {
//                fos.close();
//            } catch (Exception ie) {
//                fos = null;
//            }
//        }
//    }

//    public void saveCapture2(Bitmap mBitmap){
//        // sdcardフォルダを指定
//        File root = Environment.getExternalStorageDirectory();
//
//        // 日付でファイル名を作成　
//        Date mDate = new Date();
//        SimpleDateFormat fileName = new SimpleDateFormat("yyyyMMdd_HHmmss");
//
//        // 保存処理開始
//        FileOutputStream fos = null;
//        try {
//            fos = new FileOutputStream(new File(root, fileName.format(mDate) + ".jpg"));
//        } catch (FileNotFoundException e) {
//            e.printStackTrace();
//        }
//
//        // jpegで保存
//        mBitmap.compress(Bitmap.CompressFormat.JPEG, 100, fos);
//
//        // 保存処理終了
//        try {
//            fos.close();
//        } catch (IOException e) {
//            e.printStackTrace();
//        }
//    }

//    // スクリーンショットを取得する
//    public Bitmap getViewCapture(View view) {
//        view.setDrawingCacheEnabled(true);
//        Bitmap cache = );
//        if(cache == null) return null;
//        Bitmap screen_shot = Bitmap.createBitmap(cache);
//        view.setDrawingCacheEnabled(false);
//        return screen_shot;
//    }
}