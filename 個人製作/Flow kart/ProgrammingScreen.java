package com.example.myapplication;

import android.graphics.Rect;

public class ProgrammingScreen extends Scene {

    // フィールド ------------------------------->
    static final float icSize = 32.0f;          // アイコンサイズ
    static final float icSize2 = icSize/2;

    private int clickPosX;
    private int clickPosY;

    // 右下の座標
//    public Vector2 BottomRightCoordinates = new Vector2(0.0f, 0.0f);

    // 背景 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private String Background_img = "ProgrammingAsset/Background_P.png";
//    private Vector2 Background_Pos = new Vector2(0.0f, 0.0f);
//    private Vector2 Background_Scale = new Vector2(0.0f, 0.0f);
//
//    // 背景画像サイズ
//    private float Background_W = 720.0f, Background_H = 405.0f;

    // 端子 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private String Delete_img = "ProgrammingAsset/ic_delete.png";
    private Vector2 Delete_Pos = new Vector2(0.0f, 0.0f);

    private Vector2 Symbol_CURSOR_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 Order_CURSOR_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 AND_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 ANI_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 OUT_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 WIRE_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 BRANCH_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 BU_Pos = new Vector2(0.0f, 0.0f);
    private Vector2 BD_Pos = new Vector2(0.0f, 0.0f);
    private String  EDGE_img = "ProgrammingAsset/ic_edge.png";      // 右端に黒の線
    private Vector2 EDGE_Pos = new Vector2(0.0f, 0.0f);     // ラダー図の左端の座標
    private float EdgeLeftLineDrawPosX = 0.0f;                     // 画面上でのラインの表示位置　( EDGE_Pos.x + icSize2  )

    // 命令 (パーツ)
    private float   OrderPosX = 0.0f;
    private float   RelaySelect_W = 30.0f;               // リレー選択選択画面の幅
    private float   RelaySelect_H = 20.0f;              // リレー選択選択画面の高さ
    private boolean RelaySelectMode_flg = false;        // true = 内部リレーを選択中
//    float sensor1_PosY = 0.0f;
//    float sensor2_PosY = 0.0f;
//    float sensor3_PosY = 0.0f;
//    float sensor4_PosY = 0.0f;
//    float sensor5_PosY = 0.0f;
//    float sensor6_PosY = 0.0f;

    // sensor 一覧のY座標
    float ListPosY[] = new float[OrderListDrawNum];

    // textサイズ (sensor)
    float TextSizeX = 80.0f;
    float TextSizeY = 10.0f;

    // ラダー図用
    boolean LongPressPreventionFlg = false;    // 長押し防止フラグ
    static int WYT = 0;                        // タップした場所の格納先 ( タップ場所 = ProgramDate[WYT] ) ※添字に使用   　WYT　…　Where you tapped
    //static final int Ladder_NumPerLine = 7;   // ラダー図が1行で格納できる数 ( 端子数 )

    // ラダー図用 (2次元配列用)
    private class WhereTap{
        int row     = 0; // 行 (横)
        int column  = 0; // 列 (縦)
    }
    static WhereTap WhereTap;                  // タップした場所の格納先

    // 画像指定用
//    enum ic {None, Wire, Branch, Bd, Bu, And, Ani, Out}

    // 選択している情報
    private ic ImgState = ic.None;
    private OrderList OrderState = OrderList.None;

    // マップ
    private Vector2 Map_Pos = new Vector2(0.0f, 0.0f);

    // スクロール限界
    private float Scroll_UpperLimit = 0.0f;
    private float Scroll_LowerLimit = 0.0f;

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {

        // 画面比率の補正値 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
//        // 画面端(上限値)
//        float EdgeOfScreenY = (App.Get().GetDisplaySize().y * dsp);
//        float EdgeOfScreenX = ((EdgeOfScreenY * 16.0f) / 9.0f);

        // 初期位置 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        float ic_PosX = 33.0f;

//        Background_Pos = new Vector2(App.Get().GetDisplaySize().x, App.Get().GetDisplaySize().y);

        // 端子
        Delete_Pos = new Vector2(ic_PosX, 130.0f);
        AND_Pos = new Vector2(ic_PosX, 180.0f);
        ANI_Pos = new Vector2(ic_PosX, 230.0f);
        OUT_Pos = new Vector2(ic_PosX, 280.0f);
        WIRE_Pos = new Vector2(ic_PosX, 330.0f);

        // 命令(パーツ)
        //OrderPosX = 72.0f;
        OrderPosX = 97.0f;

        // センサーのY座標
        for (int i = 0; i < OrderListDrawNum; i++) {
            ListPosY[i] = 135.0f + (50.0f * i);
//            sensor2_PosY = 185.0f;
//            sensor3_PosY = 235.0f;
//            sensor4_PosY = 285.0f;
//            sensor5_PosY = 335.0f;
//            sensor6_PosY = 385.0f;
        }

        Symbol_CURSOR_Pos = new Vector2(ic_PosX, 130.0f);
        Order_CURSOR_Pos = new Vector2(OrderPosX, ListPosY[0]);

        // スクロール限界
        Scroll_UpperLimit = ListPosY[0];
        Scroll_LowerLimit = ListPosY[4]-10.0f;


        Map_Pos = new Vector2(EdgeOfScreenX * 0.88f, EdgeOfScreenY * 0.2f + 10.0f);
        CAR_Pos = new Vector2(EdgeOfScreenX * 0.88f, EdgeOfScreenY * 0.7f);

        // ラダー図配置場所( ラダー図の横の線 )
        EDGE_Pos = new Vector2(170.0f,50.0f);

        EdgeLeftLineDrawPosX = EDGE_Pos.x + icSize2;

        // 要素追加 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        DataInformation ad = new DataInformation();
        ProgramDate.add(0, ad);
        // 追加した要素を初期化
        Vector2 adV = new Vector2(EDGE_Pos.x + icSize, EDGE_Pos.y);
        ad = new DataInformation(ic.And, OrderList.起動ボタン, adV);
        ProgramDate.set(0, ad);

        // 初期化が二回実行する為に最後に実行した値を初期値(index:0)に設定 =-=-=-=-=-=-=
        if (ProgramDate.size() > 1) {
            ProgramDate.remove(ProgramDate.size() - 1);
        }
//        // 2次元 Ver ==================================================================-
//        for(int i = 0; i < Ladder_NumPerLine; i++){
//            Vector2 adV2 = new Vector2(EDGE_Pos.x + icSize, EDGE_Pos.y);
//            LineDate[i] = new DataInformation(ic.None, adV2, OrderList.None);
//        }
//        ProgramDates.add(LineDate);
//        if (ProgramDates.size() > 1) {
//            ProgramDates.remove(ProgramDates.size() - 1);
//        }
    }

    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update() {

        TouchProc();

        if(frame<=60)
            frame++;
        else{
            frame = 0;
        }
    }

    private void MaxRelayCount() {

        // 内部リレー使用個数の調整
        int MemoryMax = 0;

        for (int pd = 0; pd < ProgramDate.size(); pd++) {
            if (ProgramDate.get(pd).M_No > MemoryMax) {
                MemoryMax = ProgramDate.get(pd).M_No;
            }
        }
        useInternalRelayNum = MemoryMax+1;
    }

    // タッチの処理 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private void TouchProc() {

        Pointer p = App.Get().TouchMgr().GetTouch();

        // 早期リターン
        if (p == null) {
            LongPressPreventionFlg = false;
            return;
        }
        else
            LongPressPreventionFlg = true;

        clickPosX = p.GetNowPos().x;
        clickPosY = p.GetNowPos().y;


        // タップ位置が命令リスト内か？
        if( OriginalMath.Get().isInside_OrderList(p) ){

            // スクロール限限界以内か
            //if(Scroll_UpperLimit + 10 >= ListPosY[0] ) {

                //if(Scroll_UpperLimit >= ListPosY[0])
                for (int i = 0; i < OrderListDrawNum; i++)
                    ListPosY[i] += (float) (p.GetNowPos().y - p.GetDownPos().y) / 10.0f;
            //}

        }

        // ボタン
        if (p.OnUp()) {

            // もどるボタン
            if( OriginalMath.Get().IconClick_inside_rect( p,0.0f,UIButtonSize_W,0.0f,UIButtonSize_H) ) {
                App.Get().scene = App.e_Scene.StageSelect;
            }
            App.Get().ImageMgr().Draw(PlayButton_img, CAR_Pos.x , EdgeOfScreenY * 0.95f,0.67f,0.7f,0.0f);

//            // ヘルプ
//            if( OriginalMath.Get().IconClick_inside_rect(p, CAR_Pos.x - UIButtonSize_W,UIButtonSize_W * 2 + 1.0f,0.0f, UIButtonSize_H) ) {
//                DebugFlg = !DebugFlg;
//            }

            // 実行
            if( OriginalMath.Get().IconClick_inside_rect(p, CAR_Pos.x - (UIButtonSize_W / 2.0f),CAR_Pos.x + (UIButtonSize_W / 2.0f),
                                                            EdgeOfScreenY * 0.95f - (UIButtonSize_H / 2.0f),EdgeOfScreenY * 0.95f  + (UIButtonSize_H / 2.0f)) ) {
                Convert_to_2D_Array();
                App.Get().GetRunningScreen().Rest();
                App.Get().scene = App.e_Scene.Running;
            }
        }

        // リレー選択時
        if(RelaySelectMode_flg){
            RelaySelect(p);
            return;
        }

        // ラダー図内かどうか =============================================================================================================================================================================================================
        if( OriginalMath.Get().Inside_the_Ladder( p, EdgeLeftLineDrawPosX,EdgeLeftLineDrawPosX + (icSize * Ladder_NumPerLine),
                                                    EDGE_Pos.y - icSize2,EDGE_Pos.y + (icSize * 8) + (icSize2))          && !RelaySelectMode_flg)           // y … 8行分
        {
//            if (p.GetDownPos().x > EDGE_Pos.x + icSize / 2 && p.GetDownPos().y > EDGE_Pos.y - icSize / 2 &&
//                    p.GetDownPos().x < EDGE_Pos.x + (7 * icSize) + (icSize / 2) && p.GetDownPos().y < EDGE_Pos.y + (8 * icSize) + (icSize / 2)) {
            // ラダー図 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


            if (LongPressPreventionFlg) {

                //　タップされた場所を求める用のカウンター ( ProgramDate[TapLocations_cnt] … タップ位置 )
                int TapLocations_cnt = 0;

                // タップ場所は何番目に値するか？ ================================================================================================================================================================================================
                while (!OriginalMath.Get().Inside_the_Ladder(p,EdgeLeftLineDrawPosX + (icSize * (TapLocations_cnt % Ladder_NumPerLine)),  EdgeLeftLineDrawPosX + (icSize * (TapLocations_cnt % Ladder_NumPerLine + 1)),
                                                               EDGE_Pos.y - icSize2 + (icSize * (TapLocations_cnt / Ladder_NumPerLine)),EDGE_Pos.y - icSize2 + (icSize * (TapLocations_cnt / Ladder_NumPerLine + 1)) ))
                {
                    TapLocations_cnt++;
                }

                WYT = TapLocations_cnt;    // タップした場所の格納先を代入 ( タップ位置 = ProgramDate[WYT] ) ※ 値保持

                // 背景&要素の追加 ========================================================================================================================================================================================================================

                // クリック場所がデータサイズより大きかったら不足している配列分を確保  =-=-=-=-=-=-=-=-=-=-=-=-=
                for (int pd_cnt = ProgramDate.size(); ProgramDate.size() <= WYT; pd_cnt++) {

                    // 要素を追加
                    DataInformation ad = new DataInformation();

                    // 追加した要素を初期化 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    float x = EdgeLeftLineDrawPosX + (icSize * (pd_cnt % Ladder_NumPerLine)) + icSize2;
                    float y = EDGE_Pos.y + (icSize * (WYT / Ladder_NumPerLine));

                    Vector2 adV = new Vector2(x, y);
                    ad = new DataInformation(ic.None, OrderList.None, adV);

                    ProgramDate.add(ad);
                    ProgramDate.set(ProgramDate.size() - 1, ad);  // 末尾にセット
                }

                // タップ位置に何も配置されていない場合は要素を追加 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                if (ProgramDate.get(WYT).imgNo == ic.None) {
                    Vector2 adV = new Vector2(ProgramDate.get(WYT).Pos.x, ProgramDate.get(WYT).Pos.y);
                    DataInformation ad = new DataInformation(ImgState, OrderState, adV);
                    ProgramDate.set(WYT, ad);
                }

                //LongPressPreventionFlg = true;

                // タップ場所のデータを参照
                ImgState = ProgramDate.get(WYT).imgNo;
                OrderState = ProgramDate.get(WYT).order_id;

            }

            // 不要な要素を削除 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            for (int i = ProgramDate.size() - 1; i > WYT; i--) {
                if (ProgramDate.get(i).imgNo == ic.None)
                    ProgramDate.remove(i);
            }

        }
        // データ切り替え ================================================================================================================================================================================================
        // 端子 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // 端子欄内か？
        else if(OriginalMath.Click_inside_rect(p, 0.0f,63.0f, 86.0f ,360.f )) {

            // Dlete =-=-=-=-=-=-=-=-=-=-=-=
            if (OriginalMath.Get().IconClick_inside_rect(p, Delete_Pos, icSize)) {
                DataDelete();
//                ImgState = ic.None;
//
//                // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
//                if (WYT < 0) {
//                    DataInformation ad = new DataInformation(ic.None, ProgramDate.get(0).Pos, OrderList.None);
//                    ProgramDate.set(WYT, ad);
//                } else {
//                    DataInformation ad = new DataInformation(ic.None, ProgramDate.get(WYT).Pos,OrderList.None);
//                    ProgramDate.set(WYT, ad);
//                }
            }
            // AND =-=-=-=-=-=-=-=-=-=-=-=
            else if (OriginalMath.Get().IconClick_inside_rect(p, AND_Pos, icSize)) {
                ChangeTex(ic.And);
//                ImgState = ic.And;
//
//                // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
//                if (WYT < 0) {
//                    DataInformation ad = new DataInformation(ic.And, ProgramDate.get(0).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                } else {
//                    DataInformation ad = new DataInformation(ic.And, ProgramDate.get(WYT).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                }
            }
            // ANI =-=-=-=-=-=-=-=-=-=-=-=
            else if (OriginalMath.Get().IconClick_inside_rect(p, ANI_Pos, icSize)) {
                ChangeTex(ic.Ani);
//                ImgState = ic.Ani;
//
//                // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
//                if (WYT <= 0) {
//                    DataInformation ad = new DataInformation(ic.Ani, ProgramDate.get(0).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                } else {
//                    DataInformation ad = new DataInformation(ic.Ani, ProgramDate.get(WYT).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                }
            }
            // OUT =-=-=-=-=-=-=-=-=-=-=-=
            else if (OriginalMath.Get().IconClick_inside_rect(p, OUT_Pos, icSize)) {
                ChangeTex(ic.Out);
//                ImgState = ic.Out;
//
//                // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
//                if (WYT < 0) {
//                    DataInformation ad = new DataInformation(ic.Out, ProgramDate.get(0).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                } else {
//                    DataInformation ad = new DataInformation(ic.Out, ProgramDate.get(WYT).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                }
            }
            // WIRE =-=-=-=-=-=-=-=-=-=-=-=
            else if (OriginalMath.Get().IconClick_inside_rect(p, WIRE_Pos, icSize)) {
                Change_Wire(ic.Wire);
//                ImgState = ic.Wire;
//
//                // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
//                if (WYT < 0) {
//                    DataInformation ad = new DataInformation(ic.Wire, ProgramDate.get(0).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                } else {
//                    DataInformation ad = new DataInformation(ic.Wire, ProgramDate.get(WYT).Pos, OrderState);
//                    ProgramDate.set(WYT, ad);
//                }
            }
        }

        // パーツ(命令) =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        int ListNo = 0;
        // クリック判定 (パーツ欄内か？)
        if( OriginalMath.Click_inside_rect(p, 64.0f, 130.0f, 86.0f ,360.f ) && ImgState.ordinal() != ic.None.ordinal() ) {
            if (ImgState != ic.None || ImgState != ic.Wire) {
                for (OrderList ol : OrderList.values()) {
                    // 入力可能端子？ =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    if (Search_Within_Range(ImgState, ic.None, ic.InputEnd)) {
                        if (ImgState != ic.And && ol == OrderList.起動ボタン) {
                            continue;
                        }
                        // sensor1～6 =-=-=-=-=-=-=-=-=-=-=-=
                        if (ol.ordinal() > OrderList.None.ordinal() && ol.ordinal() < OrderList.InputEnd.ordinal() || ol.ordinal() == OrderList.M.ordinal()) {
                            //if (OriginalMath.Get().clickIcon_inside_rect(p, OrderPosX, OrderPosX + TextSizeX, sensorPosY[sensorNo] - TextSizeY,sensorPosY[sensorNo])) {
                            //float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(sensor.toString(),50.0f,0.0f) / 2.0f;
                            if (OriginalMath.Get().IconClick_inside_rect(p, OrderPosX - 30.0f, OrderPosX + 30.0f, ListPosY[ListNo] - OriginalMath.Get().GetTextHeight(ol.toString()), ListPosY[ListNo])) {

                                // 内部リレーの時
                                if(ol.ordinal() == OrderList.M.ordinal()) {
                                    // 1つ目は0固定
                                    if(useInternalRelayNum <= 0){
                                        useInternalRelayNum++;
                                        ChangeRelay(ol, 0);
                                    }
                                    else{
                                        ProgramDate.get(WYT).order_id = OrderList.M;
                                        RelaySelectMode_flg = true;
                                        //useInternalRelayNum++;
                                    }
                                }
                                else
                                    ChangeOrder(ol);
                                break;
                            }
                            ListNo++;
                        }
                    }
                    // 出力端子？ =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                    else if (Search_Within_Range(ImgState, ic.InputEnd, ic.OutputEnd)) {
                        // 移動命令 =-=-=-=-=-=-=-=-=-=-=-=
                        //if (ol.ordinal() != OrderList.InputEnd.ordinal() && ol.ordinal() < OrderList.OutputEnd.ordinal()){
                        if (ol.ordinal() > OrderList.InputEnd.ordinal() && ol.ordinal() < OrderList.OutputEnd.ordinal() || ol.ordinal() == OrderList.M.ordinal()) {
                            //if (OriginalMath.Get().clickIcon_inside_rect(p, OrderPosX, OrderPosX + TextSizeX, sensorPosY[sensorNo] - TextSizeY,sensorPosY[sensorNo])) {
                            //float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(sensor.toString(),50.0f,0.0f) / 2.0f;
//                            float top = ListPosY[ListNo] - OriginalMath.Get().GetTextHeight(ol.toString());
//                            if (OriginalMath.Get().IconClick_inside_rect(p, OrderPosX - 30.0f, OrderPosX + 30.0f, top, ListPosY[ListNo])) {
                            if (OriginalMath.Get().IconClick_inside_rect(p, OrderPosX - 30.0f, OrderPosX + 30.0f,
                                                                            ListPosY[ListNo] - OriginalMath.Get().GetTextHeight(ol.toString()), ListPosY[ListNo])) {
                                // 同じなら交換しなしなくても良い
                                if(ol.ordinal() == OrderState.ordinal())
                                    break;

                                // 内部リレーの時
                                if(ol.ordinal() == OrderList.M.ordinal()) {
                                    // 1つ目は0固定
                                    if(useInternalRelayNum <= 0){
                                        useInternalRelayNum++;
                                        ChangeRelay(ol, 0);
                                    }
                                    else{
                                        ProgramDate.get(WYT).order_id = OrderList.M;
                                        RelaySelectMode_flg = true;
                                    }
                                }
                                else
                                    ChangeOrder(ol);

                                break;
                            }
                            ListNo++;
                        }
                    } else
                        ChangeOrder(OrderList.None);
                }

            }
        }
    }

    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {

        BackgroundDraw(Background_img);

        // 端子(記号) =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        App.Get().ImageMgr().Draw(Delete_img, Delete_Pos.x, Delete_Pos.y);
        App.Get().ImageMgr().Draw(AND_img, AND_Pos.x, AND_Pos.y);
        App.Get().ImageMgr().Draw(ANI_img, ANI_Pos.x, ANI_Pos.y);
        App.Get().ImageMgr().Draw(OUT_img, OUT_Pos.x, OUT_Pos.y);
        App.Get().ImageMgr().Draw(WIRE_img, WIRE_Pos.x, WIRE_Pos.y);

        switch (ImgState) {
            case None:
                Symbol_CURSOR_Pos = Delete_Pos;
                break;
            case Wire:
                Symbol_CURSOR_Pos = WIRE_Pos;
                break;
            case Branch:
                Symbol_CURSOR_Pos = BRANCH_Pos;
                break;
            case Bd:
                Symbol_CURSOR_Pos = BD_Pos;
                break;
            case Bu:
                Symbol_CURSOR_Pos = BU_Pos;
                break;
            case And:
                Symbol_CURSOR_Pos = AND_Pos;
                break;
            case Ani:
                Symbol_CURSOR_Pos = ANI_Pos;
                break;
            case Out:
                Symbol_CURSOR_Pos = OUT_Pos;
                break;
        }

        DrawCursor(Symbol_CURSOR_Pos.x, Symbol_CURSOR_Pos.y);

        // 命令 ====================================================================================================================================================
        // リスト表示 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        if (ImgState != ic.None || ImgState.ordinal() > ic.OutputEnd.ordinal()) {
            int ListNo = 0;
            for (OrderList order : OrderList.values()) {

                // 入力 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                if (Search_Within_Range(ImgState, ic.None, ic.InputEnd)) {
                    if(ImgState != ic.And && order == OrderList.起動ボタン){ continue; }
                        // 表示したい物
                        if (Search_Within_Range(order, OrderList.None, OrderList.InputEnd) || OrderList.M == order ) {
                            DrawOrderList(order,ListNo);
//                            // 表示可能範囲内か
//                            if( OriginalMath.Get().isInside_OrderList(ListPosY[ListNo]) ) {
//                                // TextSizeを調整してから表示
//                                float mx = 0.0f;
//                                if (OrderList.M == sensor) {
//                                    mx = OriginalMath.Get().GetTextWidth_AutoTextSize("内部リレー", 50.0f, 0.0f) / 2.0f;
//                                } else{
//                                    mx = OriginalMath.Get().GetTextWidth_AutoTextSize(sensor.toString(), 50.0f, 0.0f) / 2.0f;
//                                }
//                                if(OrderList.M == sensor)
//                                    App.Get().GetView().DrawString_NTS(OrderPosX - mx, ListPosY[ListNo], "内部リレー", 0xaa000000);
//                                else
//                                App.Get().GetView().DrawString_NTS(OrderPosX - mx, ListPosY[ListNo], sensor.toString(), 0xaa000000);
//                            }
                        ListNo++;
                    }
                }

                // 出力 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
                if (Search_Within_Range(ImgState, ic.InputEnd, ic.OutputEnd)) {
                    // 表示したい物
                    if (Search_Within_Range(order, OrderList.InputEnd, OrderList.OutputEnd)  || OrderList.M == order  ) {
                        DrawOrderList(order,ListNo);
//                        if( OriginalMath.Get().isInside_OrderList(ListPosY[ListNo]) ) {
//                            // TextSizeを調整してから表示
//                            float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(sensor.toString(), 50.0f, 0.0f) / 2.0f;
//                            App.Get().GetView().DrawString_NTS(OrderPosX - mx, ListPosY[ListNo], sensor.toString(), 0xaa000000);
//                        }
                        ListNo++;
                    }
                }
            }
        }

        int M_CorrectionValue = 0;  // 自己保持回路の時の参照先の補正値 ( ※ センサー6.ordinal = 7 , M.ordinal = 18 を連番に修正)

        if ( OrderState.ordinal() != OrderList.None.ordinal() ) {
            // 入力命令一覧のカーソル座標格納 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            //if(OrderState.ordinal() != OrderList.None.ordinal()  &&  OrderState.ordinal() < OrderList.InputEnd.ordinal() || OrderState.ordinal() == OrderList.M.ordinal() ){
            if (ImgState.ordinal() != ImgState.None.ordinal() && ImgState.ordinal() < ImgState.InputEnd.ordinal() ||
                    ImgState.ordinal() > ImgState.OutputEnd.ordinal() && ImgState.ordinal() < ImgState.IOEnd.ordinal()) {

                if (OrderState.ordinal() == OrderList.M.ordinal())
                    M_CorrectionValue = OrderList.InputEnd.ordinal() + 2;

                if (ImgState == ic.And) {
                    Order_CURSOR_Pos.y = ListPosY[OrderState.ordinal() - M_CorrectionValue - 1] - (TextSizeY / 2.0f);
                    //Order_CURSOR_Pos.y = ListPosY[ OrderState.ordinal() - M_CorrectionValue - (OrderList.None.ordinal() + 1) ] - (TextSizeY / 2.0f);
                } else {
                    Order_CURSOR_Pos.y = ListPosY[OrderState.ordinal() - M_CorrectionValue - (OrderList.起動ボタン.ordinal() + 1)] - (TextSizeY / 2.0f);
                }
            }
            // 出力命令一覧のカーソル座標格納 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            // 表示可能だったら （ 出力端子か内部リレー ）
            //if(OrderState.ordinal() > OrderList.InputEnd.ordinal() &&  OrderState.ordinal() < OrderList.OutputEnd.ordinal() || OrderState.ordinal() == OrderList.M.ordinal()  ){
            if (ImgState.ordinal() > ImgState.InputEnd.ordinal() && ImgState.ordinal() < ImgState.IOEnd.ordinal() &&
                ImgState.ordinal() != ImgState.OutputEnd.ordinal() ) {

                if (OrderState.ordinal() == OrderList.M.ordinal())
                    M_CorrectionValue = 3;      // M - OutPutEnd

                Order_CURSOR_Pos.y = ListPosY[OrderState.ordinal() - M_CorrectionValue - (OrderList.InputEnd.ordinal() + 1)] - (TextSizeY / 2.0f);
            }

            // 文字用(命令用)カーソル
            // 命令が選択されているなら表示
            // orderカーソル =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            //if (OrderState != OrderList.None)
            if (OriginalMath.isInside_OrderList(Order_CURSOR_Pos.y)) {
                DrawCursor(Order_CURSOR_Pos.x, Order_CURSOR_Pos.y);
            }
        }
//        for (int i = 0; i < sensorNum; i++) {
//            if( OrderState != OrderList.None && OrderState.ordinal() < OrderList.IOEnd.ordinal() ){
//                DrawCursor( OrderPosX + (TextSizeX/2.0f - 16.0f), sensorPosY[i] - (TextSizeY / 2.0f) );
//        }

        // ラダー図側 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        // 端
        for (int e = 0; e < 9; e++) {
            App.Get().ImageMgr().Draw(EDGE_img, EDGE_Pos.x, EDGE_Pos.y + e * icSize);
            App.Get().ImageMgr().Draw(EDGE_img, EDGE_Pos.x + 8 * icSize, EDGE_Pos.y + e * icSize, 180.0f);
        }

        DrawLadderDiagram();
//        for (int adCon = 0; adCon < ProgramDate.size(); adCon++) {
//
////            // ラダー図に画像を格納
////            if (WYT < ProgramDate.size()) {
////
////                String f_img = new String();
////
////                switch (ProgramDate.get(adCon).imgNo) {
////                    case Wire:
////                        f_img = WIRE_img;
////                        break;
////                    case Branch:
////                        f_img = BRANCH_img;
////                        break;
////                    case Bd:
////                        f_img = BD_img;
////                        break;
////                    case Bu:
////                        f_img = BU_img;
////                        break;
////                    case And:
////                        f_img = AND_img;
////                        break;
////                    case Ani:
////                        f_img = ANI_img;
////                        break;
////                    case Out:
////                        f_img = OUT_img;
////                        break;
////                }
////
////                if (WYT < 0)
////                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(0).Pos.x, ProgramDate.get(0).Pos.y);
////                else
////                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(adCon).Pos.x, ProgramDate.get(adCon).Pos.y);
////
////                // 命令が格納できる端子なら命令の内容を描画 (※特殊ノードを除く)
////                if(ProgramDate.get(adCon).order_id != null) {
////                    //String  NowOrderName = OrderState.toString();
////                    String  GetOrderName = ProgramDate.get(adCon).order_id.toString();
////                    if(Search_Within_Range(ProgramDate.get(adCon).order_id, OrderList.None, OrderList.OutputEnd)){
////
////                        // 入力端子
////                        if(ProgramDate.get(adCon).order_id.ordinal() <= OrderList.OutputEnd.ordinal() && ProgramDate.get(adCon).order_id != OrderList.InputEnd ){
////                            float _TextSize = TextSizeMin(OrderList.None,OrderList.OutputEnd,35.0f,5.0f);
////                            float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(ProgramDate.get(adCon).order_id.toString(),30.0f,10.0f) / 2.0f - 5.0f;
////                            if (WYT < 0)
////                                App.Get().GetView().DrawString_SD( ProgramDate.get(0).Pos.x - mx ,ProgramDate.get(0).Pos.y - 10.0f , GetOrderName , 0xaa000000,_TextSize);
////                                //App.Get().GetView().DrawString_NTS( ProgramDate.get(0).Pos.x - mx ,ProgramDate.get(0).Pos.y - 10.0f , GetOrderName , 0xaa000000);
////                            else
////                                App.Get().GetView().DrawString_SD(ProgramDate.get(adCon).Pos.x - mx ,ProgramDate.get(adCon).Pos.y - 10.0f , GetOrderName, 0xaa000000,_TextSize);
////                                //App.Get().GetView().DrawString_NTS(ProgramDate.get(adCon).Pos.x - mx ,ProgramDate.get(adCon).Pos.y - 10.0f , GetOrderName, 0xaa000000);
////                        }
////                    }
////                }
////            }
//
//            // ラダー図のカーソル
////            if (WYT < ProgramDate.size()) {
////                if (WYT < 0) {
////                    if(frame%30 < 15 )
////                        DrawCursor(ProgramDate.get(0).Pos.x, ProgramDate.get(0).Pos.y);
////                } else {
////                    if(frame%30 < 15 )
////                        DrawCursor(ProgramDate.get(WYT).Pos.x, ProgramDate.get(WYT).Pos.y);
////                }
////            }
//        }

        // 画像 ==========================================================================================================
        // マップを描画
        App.Get().ImageMgr().Draw(Background_Map_img, Map_Pos.x, Map_Pos.y, 0.1f, 0.1f, 0.0f);
        App.Get().ImageMgr().Draw(Map_img, Map_Pos.x, Map_Pos.y, 0.1f, 0.1f, 0.0f);

        // 車を描画
        DrawUICar(0.8f);

        App.Get().ImageMgr().Draw(PlayButton_img, CAR_Pos.x , EdgeOfScreenY * 0.95f,0.67f,0.7f,0.0f);
        //App.Get().ImageMgr().DrawLU(PlayButton_img, 0.0f , 0.0f,0.67f,0.7f,0.0f);

        if(DebugFlg) {
            // ボタン反応位置確認用
            for (float w = 0.0f; w <= UIButtonSize_W; w++)
                App.Get().ImageMgr().Draw(Test_img, w, UIButtonSize_H);
            for (float h = 0.0f; h <= UIButtonSize_H; h++)
                App.Get().ImageMgr().Draw(Test_img, UIButtonSize_W, h);

            // 画面端確認用
            for (float w = 1.0f; w <= EdgeOfScreenX; w++)
                App.Get().ImageMgr().Draw(Test_img, w, EdgeOfScreenY - 1.0f);
            for (float h = 1.0f; h <= EdgeOfScreenY; h++)
                App.Get().ImageMgr().Draw(Test_img, EdgeOfScreenX - 1.0f, h);
        }

        // UI文字 ====================================================================================
        // 画面比率
        //                     　                        ↓描画したい文字列
        //App.Get().GetView().GetCanvas().drawText("CS1", 250.0f, 405.0f, App.Get().paint);
        //
        //                                                      ↑X　   ↑Y　   ↑文字の書式設定変数

        // DrawString → ※ 注視点左下
        //App.Get().GetView().DrawString(215.0f,405.0f,"SNSR1",0xaa000000);

        if(DebugFlg) {
            App.Get().GetView().DrawString_SD(360.0f, 20.0f,
                    "x = " + Integer.toString(clickPosX) + ", y = " + Integer.toString(clickPosY),
                    0xff000000);
        }

        App.Get().GetView().DrawString_SD(520.0f,20.0f,"【 マップ 】",0xff000000);

        DrawRectBackground(515.0f,Map_Pos.y + 58.0f,10.0f, 10.0f,255,0,0,255);
        App.Get().GetView().DrawString_SD(520.0f, Map_Pos.y + 62.0f," → スタート",0xff000000,25.0f);

        DrawRectBackground(585.0f,Map_Pos.y + 58.0f,10.0f, 10.0f,255,255,0,0);
        App.Get().GetView().DrawString_SD(590.0f, Map_Pos.y  + 62.0f," → ゴール",0xff000000, 25.0f);
        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    }

    private void DrawRectBackground(float x, float y, float w, float h, int a, int r,int g, int b){

        x *= App.Get().SD();
        y *= App.Get().SD();
        w *= App.Get().SD();
        h *= App.Get().SD();


        App.Get().GetView().GetPaint().setARGB( a, r, g, b);
        App.Get().GetView().GetCanvas().drawRect( x - w/2, y - h/2, x + w/2, y + h/2, App.Get().GetView().GetPaint());
        App.Get().GetView().GetPaint().setARGB( 255,255,255,255);

    }

    private void DrawOrderList(OrderList sensor, int ListNo)
    {
        // 表示可能範囲内か
        if( OriginalMath.Get().isInside_OrderList(ListPosY[ListNo]) ) {
            // TextSizeを調整してから表示
            float mx = 0.0f;
            if (OrderList.M == sensor) {
                mx = OriginalMath.Get().GetTextWidth_AutoTextSize("内部リレー", 50.0f, 0.0f) / 2.0f;
            } else{
                mx = OriginalMath.Get().GetTextWidth_AutoTextSize(sensor.toString(), 50.0f, 0.0f) / 2.0f;
            }
            if(OrderList.M == sensor)
                App.Get().GetView().DrawString_NTS(OrderPosX - mx, ListPosY[ListNo], "内部リレー", 0xaa000000);
            else
                App.Get().GetView().DrawString_NTS(OrderPosX - mx, ListPosY[ListNo], sensor.toString(), 0xaa000000);
        }
    }

    // ラダー図描画
    public void DrawLadderDiagram() {

        //useInternalRelayNum = 0;

        for (int adCon = 0; adCon < ProgramDate.size(); adCon++) {
            // ラダー図に画像を格納
            if (WYT < ProgramDate.size()) {

                String f_img = new String();

                switch (ProgramDate.get(adCon).imgNo) {
                    case Wire:
                        f_img = WIRE_img;
                        break;
                    case Branch:
                        f_img = BRANCH_img;
                        break;
                    case Bd:
                        f_img = BD_img;
                        break;
                    case Bu:
                        f_img = BU_img;
                        break;
                    case And:
                        f_img = AND_img;
                        break;
                    case Ani:
                        f_img = ANI_img;
                        break;
                    case Out:
                        f_img = OUT_img;
                        break;
                }

                if (WYT < 0)
                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(0).Pos.x, ProgramDate.get(0).Pos.y);
                else
                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(adCon).Pos.x, ProgramDate.get(adCon).Pos.y);

                // 命令が格納できる端子なら命令の内容を描画 (※特殊ノードを除く)
                if (ProgramDate.get(adCon).order_id != null) {
                    //String  NowOrderName = OrderState.toString();
                    String GetOrderName = ProgramDate.get(adCon).order_id.toString();
                    if (Search_Within_Range(ProgramDate.get(adCon).order_id, OrderList.None, OrderList.OutputEnd) || ProgramDate.get(adCon).order_id == OrderList.M) {

                        // 入力可能
                        if (ProgramDate.get(adCon).order_id.ordinal() <= OrderList.OutputEnd.ordinal() && ProgramDate.get(adCon).order_id != OrderList.InputEnd || ProgramDate.get(adCon).order_id == OrderList.M) {
                            float _TextSize = TextSizeMin(OrderList.None, OrderList.OutputEnd, 35.0f, 5.0f);
                            float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(ProgramDate.get(adCon).order_id.toString(), 30.0f, 10.0f) / 2.0f - 5.0f;
                            if (WYT < 0)
                                App.Get().GetView().DrawString_SD(ProgramDate.get(0).Pos.x - mx, ProgramDate.get(0).Pos.y - 10.0f, GetOrderName, 0xaa000000, _TextSize);
                                //App.Get().GetView().DrawString_NTS( ProgramDate.get(0).Pos.x - mx ,ProgramDate.get(0).Pos.y - 10.0f , GetOrderName , 0xaa000000);
                            else
                                // 内部リレーの時
                                if(ProgramDate.get(adCon).order_id == OrderList.M) {

                                    //選択中
                                    if (RelaySelectMode_flg) {
                                        //App.Get().GetView().GetPaint().setARGB(255,255,255,255);
                                        for(int i=0; i < useInternalRelayNum + 1; i++) {
                                            //App.Get().GetView().GetPaint().setARGB(255,255,255/ (i+1) ,255/(i+1));
                                            if(i%2==0)
                                                App.Get().GetView().GetPaint().setARGB(255,220,220,220);
                                            else
                                                App.Get().GetView().GetPaint().setARGB(255,255,255,255);

                                            DrawRelayRect(ProgramDate.get(WYT).Pos.x, ProgramDate.get(WYT).Pos.y + 20.0f, i);
                                            App.Get().GetView().DrawString_SD(ProgramDate.get(WYT).Pos.x - 8.0f, ProgramDate.get(WYT).Pos.y + 20.0f + 3.0f + RelaySelect_H*i,"M"+i,0xff000000, 30.0f);
//                                            App.Get().GetView().GetCanvas().drawRect(200.0f * App.Get().SD() - RelaySelect_W/2, 200.0f*App.Get().SD() + RelaySelect_W/2,
//                                                                                    (200.0f*App.Get().SD() - RelaySelect_H/2) + RelaySelect_H * i, (200.0f*App.Get().SD() + RelaySelect_H/2) + RelaySelect_H * i,
//                                                                                    App.Get().GetView().GetPaint())
                                        }
                                    }

                                    App.Get().GetView().GetPaint().setARGB(255,0,0,0);

                                    mx = OriginalMath.Get().GetTextWidth_AutoTextSize("M" + Integer.toString(ProgramDate.get(adCon).M_No), 30.0f, 10.0f) / 2.0f - 5.0f;
                                    App.Get().GetView().DrawString_SD(ProgramDate.get(adCon).Pos.x - mx, ProgramDate.get(adCon).Pos.y - 10.0f, "M" + ProgramDate.get(adCon).M_No, 0xaa000000, _TextSize);
                                    //useInternalRelayNum++;
                                }
                                else
                                    App.Get().GetView().DrawString_SD(ProgramDate.get(adCon).Pos.x - mx, ProgramDate.get(adCon).Pos.y - 10.0f, GetOrderName, 0xaa000000, _TextSize);
                            //App.Get().GetView().DrawString_NTS(ProgramDate.get(adCon).Pos.x - mx ,ProgramDate.get(adCon).Pos.y - 10.0f , GetOrderName, 0xaa000000);
                        }
                    }
                }
            }

            if (WYT < ProgramDate.size()) {
                if (WYT < 0) {
                    if(frame%30 < 15 )
                        DrawCursor(ProgramDate.get(0).Pos.x, ProgramDate.get(0).Pos.y);
                } else {
                    if(frame%30 < 15 )
                        DrawCursor(ProgramDate.get(WYT).Pos.x, ProgramDate.get(WYT).Pos.y);
                }
            }
        }
    }

    // 内部リレーの選択時の背景を描画
    private void DrawRelayRect(float _x, float _y, int no) {

        _x *= App.Get().SD();
        _y *= App.Get().SD();

        App.Get().GetView().GetCanvas().drawRect(_x - RelaySelect_W/2 * App.Get().SD(), _y - RelaySelect_H/2 * App.Get().SD() + RelaySelect_H *App.Get().SD() * no,
                                                _x + RelaySelect_W/2 * App.Get().SD(), _y + RelaySelect_H/2 * App.Get().SD() + RelaySelect_H * App.Get().SD()* no,
                                                      App.Get().GetView().GetPaint());
    }

    // ラダー図描画
    public void Draw_UILadderDiagram() {

        // 端
        for (int e = 0; e < 9; e++) {
            App.Get().ImageMgr().Draw(EDGE_img, EDGE_Pos.x, EDGE_Pos.y + e * icSize);
            App.Get().ImageMgr().Draw(EDGE_img, EDGE_Pos.x + 8 * icSize, EDGE_Pos.y + e * icSize, 180.0f);
        }

        for (int adCon = 0; adCon < ProgramDate.size(); adCon++) {
            // ラダー図に画像を格納
            if (WYT < ProgramDate.size()) {

                String f_img = new String();

                switch (ProgramDate.get(adCon).imgNo) {
                    case Wire:
                        f_img = WIRE_img;
                        break;
                    case Branch:
                        f_img = BRANCH_img;
                        break;
                    case Bd:
                        f_img = BD_img;
                        break;
                    case Bu:
                        f_img = BU_img;
                        break;
                    case And:
                        f_img = AND_img;
                        break;
                    case Ani:
                        f_img = ANI_img;
                        break;
                    case Out:
                        f_img = OUT_img;
                        break;
                }

                if (WYT < 0)
                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(0).Pos.x, ProgramDate.get(0).Pos.y);
                else
                    App.Get().ImageMgr().Draw(f_img, ProgramDate.get(adCon).Pos.x, ProgramDate.get(adCon).Pos.y);

                // 命令が格納できる端子なら命令の内容を描画 (※特殊ノードを除く)
                if (ProgramDate.get(adCon).order_id != null) {
                    //String  NowOrderName = OrderState.toString();
                    String GetOrderName = ProgramDate.get(adCon).order_id.toString();
                    if (Search_Within_Range(ProgramDate.get(adCon).order_id, OrderList.None, OrderList.OutputEnd)) {

                        // 入力端子
                        if (ProgramDate.get(adCon).order_id.ordinal() <= OrderList.OutputEnd.ordinal() && ProgramDate.get(adCon).order_id != OrderList.InputEnd) {
                            float _TextSize = TextSizeMin(OrderList.None, OrderList.OutputEnd, 35.0f, 5.0f);
                            float mx = OriginalMath.Get().GetTextWidth_AutoTextSize(ProgramDate.get(adCon).order_id.toString(), 30.0f, 10.0f) / 2.0f - 5.0f;
                            if (WYT < 0)
                                App.Get().GetView().DrawString_SD(ProgramDate.get(0).Pos.x - mx, ProgramDate.get(0).Pos.y - 10.0f, GetOrderName, 0xaa000000, _TextSize);
                                //App.Get().GetView().DrawString_NTS( ProgramDate.get(0).Pos.x - mx ,ProgramDate.get(0).Pos.y - 10.0f , GetOrderName , 0xaa000000);
                            else
                                App.Get().GetView().DrawString_SD(ProgramDate.get(adCon).Pos.x - mx, ProgramDate.get(adCon).Pos.y - 10.0f, GetOrderName, 0xaa000000, _TextSize);
                            //App.Get().GetView().DrawString_NTS(ProgramDate.get(adCon).Pos.x - mx ,ProgramDate.get(adCon).Pos.y - 10.0f , GetOrderName, 0xaa000000);
                        }
                    }
                }
            }
        }
    }

    // ツール関連関数 ===========================================================================================
    // 範囲内か検索
    private boolean Search_Within_Range(ic _ic, ic start, ic End){
        if(_ic.ordinal() > start.ordinal() && _ic.ordinal() < End.ordinal()) {
            return true;
        }
        return false;
    }

    public boolean Search_Within_Range(OrderList order_id, OrderList start, OrderList End){
        if(order_id.ordinal() > start.ordinal() && order_id.ordinal() < End.ordinal()) {
            return true;
        }
        return false;
    }

    // 画像切り替え
    private void ChangeTex(ic _ic) {

        ImgState = _ic;

        // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
        if (WYT < 0) {
            DataInformation ad = new DataInformation(_ic, ProgramDate.get(0).order_id, ProgramDate.get(0).Pos);
            ProgramDate.set(WYT, ad);
        } else {
            DataInformation ad = new DataInformation(_ic, ProgramDate.get(WYT).order_id, ProgramDate.get(WYT).Pos);
            ProgramDate.set(WYT, ad);
        }
    }

    // 線に交換
    private void Change_Wire(ic _ic) {

        ImgState = _ic;
        OrderState = OrderList.None;

        // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
        if (WYT < 0) {
            DataInformation ad = new DataInformation(_ic, OrderList.None, ProgramDate.get(0).Pos);
            ProgramDate.set(WYT, ad);
        } else {
            DataInformation ad = new DataInformation(_ic, OrderList.None, ProgramDate.get(WYT).Pos);
            ProgramDate.set(WYT, ad);
        }
    }

    // 命令を交換
    private void ChangeOrder( OrderList order_id ){

        OrderState = order_id;

        // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
        if (WYT < 0) {
            DataInformation ad = new DataInformation(ProgramDate.get(0).imgNo, order_id, ProgramDate.get(0).Pos);
            ProgramDate.set(WYT, ad);
        } else {
            DataInformation ad = new DataInformation(ProgramDate.get(WYT).imgNo, order_id, ProgramDate.get(WYT).Pos);
            ProgramDate.set(WYT, ad);
        }
    }

    // 内部リレー用
    private void ChangeRelay( OrderList order_id , int _M_No){

        OrderState = order_id;

        // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
        if (WYT < 0) {
            DataInformation ad = new DataInformation(ProgramDate.get(0).imgNo, order_id, ProgramDate.get(0).Pos, _M_No);
            ProgramDate.set(WYT, ad);
        } else {
            DataInformation ad = new DataInformation(ProgramDate.get(WYT).imgNo, order_id, ProgramDate.get(WYT).Pos, _M_No);
            ProgramDate.set(WYT, ad);
        }
    }

    // リレー選択画面
    private void RelaySelect(Pointer _p){

        if (LongPressPreventionFlg) {
            for (int i = 0; i < useInternalRelayNum + 1; i++) {

                // 一覧をクリックしたら
                if (OriginalMath.Click_inside_rect(_p, ProgramDate.get(WYT).Pos.x - RelaySelect_W / 2, ProgramDate.get(WYT).Pos.x + RelaySelect_W / 2,
                        (ProgramDate.get(WYT).Pos.y - RelaySelect_H / 2) + RelaySelect_H * i + 20.0f, (ProgramDate.get(WYT).Pos.y + RelaySelect_H / 2) + RelaySelect_H * i + 20.0f)) {
                    // タップ箇所の番号を入れる
                    ProgramDate.get(WYT).SetMNumber(i);
                    ProgramDate.get(WYT).order_id = OrderList.M;

                    // 新しいリレーを選択したら使用個数を追加
                    if (useInternalRelayNum <= i)
                        MaxRelayCount();
                    //useInternalRelayNum++;

                    RelaySelectMode_flg = false;
                } else {
                    RelaySelectMode_flg = false;
                }
            }
        }
    }

    // データ削除
    private void DataDelete (){

        // 選択初期化
        ImgState    = ic.None;
        OrderState  = OrderList.None;

        // 端子一覧をタップした時に選択中の配置場所の画像を切り替え
        if (WYT < 0) {
            DataInformation ad = new DataInformation(ic.None, OrderList.None, ProgramDate.get(0).Pos);
            ProgramDate.set(WYT, ad);
        } else {
            DataInformation ad = new DataInformation(ic.None, OrderList.None, ProgramDate.get(WYT).Pos);
            ProgramDate.set(WYT, ad);
        }
    }

    // プログラムデータを2次元配列化
    private void Convert_to_2D_Array(){

        // 行数
        int column = ProgramDate.size() / (Ladder_NumPerLine+1);

        // 1行のみの場合
        if(column==0){
            for(int j=0; j < ProgramDate.size(); j++ ){
                // 配列メモリ確保
                //Program2DArray = new DataInformation[0][j];
                Program2DArray[0][j] = ProgramDate.get(j);
            }
        }
        // 2行以上
        else {
            for(int i=0; i <= column; i++ ){
                // 最後の行
                if(column<=i) {
                    // 右端までデータが格納されているか
                    if (ProgramDate.size() % Ladder_NumPerLine == 0) {
                        for (int j = 0; j < Ladder_NumPerLine; j++)
                            Program2DArray[i][j] = ProgramDate.get(j + (i * Ladder_NumPerLine));
                    } else{
                        for (int j = 0; j < ProgramDate.size() % Ladder_NumPerLine; j++)
                            Program2DArray[i][j] = ProgramDate.get(j + (i * Ladder_NumPerLine));
                    }
                }else{
                    for (int j = 0; j < Ladder_NumPerLine; j++) {
                        Program2DArray[i][j] = ProgramDate.get(j+(i*Ladder_NumPerLine));
                    }
                }
            }
        }
    }

    // Textサイズの最小値を求める (Textサイズを均一にする為)
    public float TextSizeMin(OrderList FirstOrder_id, OrderList LastOrder_id, float TextWidth, float minTestWidth){

        boolean flg = false;
        float   TextSize = 0.0f;
        float   ReTextSize = 0.0f;

        // OrderList ループ
        for (OrderList ol: OrderList.values()) {

            // 参照対象かを判定
            if (Search_Within_Range( ol, FirstOrder_id, LastOrder_id)) {

                // 2回目以降は現時点での最小値から開始
                if (flg) {
                    ReTextSize = OriginalMath.Get().GetAdjustTextSize(ol.toString(), TextWidth, TextSize, minTestWidth);

                    // 現在の値より TextSize が小さい時は入れ替える
                    if( TextSize > ReTextSize) {
                        TextSize = ReTextSize;
                    }
                }
                // 1度目は初期値なしでサイズ取得
                else {
                    TextSize = OriginalMath.Get().GetAdjustTextSize(ol.toString(), TextWidth, minTestWidth);
                    flg = true;
                }
            }
        }

        return TextSize;
    }
}