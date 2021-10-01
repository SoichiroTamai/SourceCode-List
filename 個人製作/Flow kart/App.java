package com.example.myapplication;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.stream.JsonReader;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

/*
* App
* リソース管理などのシステム面全般
* シングルトン
*
*/
public class App
{
    // ゲームの実装------------------------------------------------>

    // 画面管理
    public enum e_Scene{Title, StageSelect, Pogramming, CodeWriting, Running, Result}
    public e_Scene scene = e_Scene.Title;          // 現在の画面

    // クラス取得 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    private MainActivity mainA = new MainActivity(); // クラスのメモリ確保(全て参照体の為deleteは必要ない,ガベージコレクター)
    public MainActivity GetMainActivity(){return  mainA;}

    // ゲームオブジェクト
    private GameObject gameObject = new GameObject();
    public GameObject GetGameObject(){return gameObject;}

    // タイトル
    private TitleScreen titleScreen = new TitleScreen();
//    public TitleScreen GetTitleScreen(){return titleScreen;}

    // ステージセレクト
    private StageSelectScreen stagesSelectScreen = new StageSelectScreen();

    // プログラミング画面
    private ProgrammingScreen programmingScreen = new ProgrammingScreen();
    //public ProgrammingScreen GetProgrammingScreen(){return programmingScreen;}

    // コード書き込み
    private CodeWritingScreen codeWritingScreen = new CodeWritingScreen();
//    public ProgrammingScreen GetProgrammingScreen(){return programmingScreen;}

    // 走行画面
    private RunningScreen runningScreen = new RunningScreen();
    public RunningScreen GetRunningScreen(){return runningScreen;}

    // リザルト画面
    private ResultScreen resultScreen = new ResultScreen();
    //public ResultScreen GetResultScreen(){return resultScreen;}

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    // Json  =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public  MapJsonData mapJsonData;
    private String      MapJsonFilename = "MapData.json";

    // Jsonファイル読み込み
    MapJsonData LoadJson(String filename)
    {
        InputStream inputStream = null;
        try{
            // ファイルを読み込み
            inputStream = view.getContext().getResources().getAssets().open(filename);
        } catch (IOException e) {
            e.printStackTrace();
        }
        // InputStreamRenderで中の文字列を取得
        InputStreamReader isr = new InputStreamReader(inputStream);
        // JsonRenderでJson形式に成形
        JsonReader jsr = new JsonReader(isr);
        // Gsonで読み込んだJsonファイルを自作クラスのデータ構造に変換する
        return new Gson().fromJson(jsr,MapJsonData.class);
    }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

    // 文字表示用
    //public Paint paint;

    // 特殊な事をしない限りはこの間を編集するだけのはず
    int se1 = 0;
    int se2 = 0;
    float StatusBar_Height = 0.0f;


    // 画面比率 (16:9 固定)
    private Vector2 DisplaySize = new Vector2(0.0f,0.0f);
    private Vector2 DisplayRatio = new Vector2(1.0f,1.0f);

    private float scale = 1.0f;

    // Getter =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public Vector2 GetDisplaySize(){
        return  DisplaySize;
    }

//    public float GetSbar_Hei(){
//        return StatusBar_Height;
//    }

    // Displayサイズを取得 =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public void GetDisplaySize(float _dpx,float _dpy) {
        DisplaySize.x = _dpx;
        DisplaySize.y = _dpy;

        // 画面比率計算(x:y)　※ y = 1.0f
        DisplayRatio.x = _dpx/_dpy;
    }


    // StatusBarの高さを求める =-=-=-=-=-=-=-=-=-=-=-=-
    public void AskForStatusBar(float _vsx,float _vsy) {

        int x=0,y=0;

        if(_vsx == DisplaySize.x){
            if(_vsy > DisplaySize.y) {
                StatusBar_Height = (_vsy - DisplaySize.y) * dsPar - 1.0f;
            }else{
                StatusBar_Height = (DisplaySize.y - _vsy) * dsPar - 1.0f;
            }
        }

        if(_vsx == DisplaySize.y){
            if(_vsy > DisplaySize.x) {
                StatusBar_Height = (_vsy - DisplaySize.x) * dsPar - 1.0f;
            }else{
                StatusBar_Height = (DisplaySize.x - _vsy) * dsPar - 1.0f;
            }
        }

        if(_vsy == DisplaySize.y){
            if(_vsx > DisplaySize.x) {
                StatusBar_Height = (_vsx - DisplaySize.x)/2 * dsPar - 1.0f;
            }else{
                StatusBar_Height = (DisplaySize.x - _vsx)/2 * dsPar - 1.0f;
            }
        }

        if(_vsy == DisplaySize.x){
            if(_vsx > DisplaySize.y) {
                StatusBar_Height = (_vsx - DisplaySize.y) * dsPar - 1.0f;
            }else{
                StatusBar_Height = (DisplaySize.y - _vsx) * dsPar - 1.0f;
            }
        }

//        if(DisplaySize.x < DisplaySize.y) {
//            //                 DisplaySize - ViewSize(コンテンツ領域)
//            StatusBar_Height = (DisplaySize.x - _vs) * dsPar;
//        }
//        else {
//            StatusBar_Height = (DisplaySize.y - _vs) * dsPar;
//        }
    }

    // 画像の固定比に調整 拡大率計算(1:y) ※ x = 1  基準値→背景の高さ =-=-=-=-
    public float GetScale(float a_Height) {
        scale = (DisplayRatio.x * DisplaySize.x ) / a_Height;
        return scale;
    }

    // アプリケーションが開始された時
    // 諸々の初期化は終わっているので、ここでロードをかけてもOK
    public void Start()
    {
        // 文字設定
//        //塗りつぶし設定、FILLで通常、STROKEで枠線描画、FILL_AND_STROKEで両方の描画（太字対応可能）
//        paint.setStyle(Paint.Style. FILL_AND_STROKE);
//        //文字の線の太さ（大きければ大きいほど太字になる、FILLだと反映されないので注意）
//        paint.setStrokeWidth(3);
//        //文字の大きさ
//        paint.setTextSize(50);
//        //文字の色
//        paint.setColor(Color.argb(255, 0, 0, 0));
//        //paint.setColor(Color.BLACK);でも可能Color.～で汎用的な色は揃っています

        // Json 読み込み
        mapJsonData = LoadJson(MapJsonFilename);

        // Json 組み込みテスト
//        Log.d("Map1_No",Integer.toString(mapJsonData.MapData.get(0).Map_No));
//        Log.d("Map2_PosX",Float.toString(mapJsonData.MapData.get(1).StartPosition.x));
//        Log.d("Map3_Time",Integer.toString(mapJsonData.MapData.get(2).TargetTime));

        // 初期値を代入(安全を考慮)
        Scene.SetMapData(1);

        // クラス(画面)初期化
        //gameObject.Start();
        titleScreen.Start();
        stagesSelectScreen.Start();
        programmingScreen.Start();
        codeWritingScreen.Start();
        runningScreen.Start();
        resultScreen.Start();

        soundManager.PlayBGM("bgm.mp3");
        se1 = soundManager.LoadSE("se1.mp3");
        se2 = soundManager.LoadSE("se2.mp3");
    }


    // 毎回呼び出される関数(30fps)
    Vector2 vp = new Vector2();
    Vector2 vm = new Vector2();
    public boolean Update()
    {
        // ゲームの更新
        vp.Add(vm);

        switch (scene){
            case Title:         titleScreen.Update();
                break;
            case StageSelect:   stagesSelectScreen.Update();
                break;
            case Pogramming:    programmingScreen.Update();
                break;
            case CodeWriting:   codeWritingScreen.Update();
                break;
            case Running:       runningScreen.Update();
                break;
            case Result:        resultScreen.Update();
                break;
        }

//        // タッチの処理
//        Pointer p = touchManager.GetTouch(); // ここでnullが帰る場合はタッチされていない
//        if(p==null){ return true; }
//
//        if(p.OnDown()) // 押された瞬間
//        {
//            vp.x = p.GetDownPos().x;
//            vp.y = p.GetDownPos().y;
//            vm.Clear();
//            soundManager.PlaySE(se1);
//        }
//
//        if(p.OnFlick()) // 離された＆それがフリックと判定された
//        {
//            vm = p.GetDtoU();
//        }
//
//        if(p.OnUp())
//
//            soundManager.PlaySE(se2);
//            Log.d("touch", "OnUP!!!!!");
//        }


        // 必ずゲーム更新の後に行う事
        touchManager.Update();
        return true;
    }


    // Androidから再描画命令を受けた時
    // ここ以外での描画は無視されます。
    // 早くシステムに返さないと行けないので、描画以外の余計なことはしない。
    // 30fpsで再描画以来はかけているが、呼び出される頻度は端末によります。
    // ここが安定して動いているとは思わないでください。
    public void Draw()
    {
        switch (scene){
            case Title:         titleScreen.Draw();
                break;
            case StageSelect:   stagesSelectScreen.Draw();
                break;
            case Pogramming:    programmingScreen.Draw();
                break;
            case CodeWriting:   codeWritingScreen.Draw();
                break;
            case Running:       runningScreen.Draw();
                break;
            case Result:       resultScreen.Draw();
                break;
        }

        //imageManage.Draw("ball.png", vp.x, vp.y);
    }

//    // ホームボタンなどを押して裏側へ回った時
//    public void Suspend()
//    {
//        soundManager.StopBGM();
//    }
//
//    // 再度アクティブになった時
//    public void Resume()
//    {
//        soundManager.PlayBGM("bgm.mp3");
//    }

    // <--------------------------------------------------ゲームの実装

    // アプリケーション大本
    private OriginalView view = null;
    public OriginalView GetView(){return view;}
    public void SetView(OriginalView _ov)
    {
        view = _ov;

        // viewがないと初期化出来ないもののインスタンス化
        touchManager = new TouchManager();
        Start();
    }

    // 解像度対応
    private float sdPar = 0; // システム座標→ディスプレイ座標変換用
    private float dsPar = 0; // ディスプレイ座標→システム座標変換用
    public float SD(){return sdPar;}
    public float DS(){return dsPar;}

    // システム画面サイズと実機画面サイズが出揃った段階で比率を計算
    public void SetSDPar(float _dp, float _sp)
    {
        sdPar = _dp/_sp;
        dsPar = _sp/_dp;
    }

    // リソース管理
    private ImageManager imageManage = new ImageManager();
    public ImageManager ImageMgr(){return imageManage;}
    private SoundManager soundManager = new SoundManager();
    public SoundManager SoundMgr(){return soundManager;}

    // タッチ管理
    private TouchManager touchManager = null;
    public TouchManager TouchMgr(){return touchManager;}

    //シングルトン実装
    private App() { }
    private static App app = new App();
    public static App Get()
    {
        return app;
    }
}
