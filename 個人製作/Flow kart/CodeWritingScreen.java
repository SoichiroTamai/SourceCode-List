package com.example.myapplication;

public class CodeWritingScreen extends Scene {

    // 初期化 ///////////////////////////////////////////////////////////////////////////////////////
    public void Start() {
    }

    // 更新 /////////////////////////////////////////////////////////////////////////////////////////
    public void Update(){
        Pointer p = App.Get().TouchMgr().GetTouch();
        if (p == null) { return; } //早期

        if(p.OnUp()) {
            App.Get().GetRunningScreen().Start();
            App.Get().scene = App.e_Scene.Running;
        }
    }

    // 描画 ////////////////////////////////////////////////////////////////////////////////////////
    public void Draw() {
        App.Get().GetView().DrawString(150.0f,100.0f,"書き込み中 ．．．",0xff000000);
        //App.Get().GetView().GetCanvas().drawText("書き込み中 ．．．", 150.0f, 100.0f, App.Get().paint);
    }
}
