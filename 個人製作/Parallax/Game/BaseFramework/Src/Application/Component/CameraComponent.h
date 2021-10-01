#pragma once

#include "../Game/GameObject.h"

/*
	【 プロジェクション座標変換参考サイト 】
	https://yttm-work.jp/gmpg/gmpg_0004.html
*/

// ======================================
// カメラコンポーネントクラス
// ======================================

class CameraComponent
{
public:
	// コンストラクター　オーナーの設定と射影行列
	CameraComponent(GameObject& owner);
	~CameraComponent();

	// オフセット行列取得
	inline Matrix& OffsetMatrix() { return m_mOffset; }

	// カメラ行列取得
	inline const Matrix& GetCameratMatrix() { return m_mCam; }

	// ビュー行列取得
	inline const Matrix& GetViewtMatrix() {return m_mView;}
	
	// 射影行列取得
	inline const Matrix& GetProjtMatrix() { return m_mProj; }

	// カメラ行列・ビュー行列設定（ 行列 m と 行列 Offset が合成され、最終てきなカメラ行列になる）
	void SetCameraMatrix(const Matrix& m);

	// カメラ情報（ビュー・射影行列など）をシェーダーにセット
	void SetToShader();

	// カメラから見た指定された座標にレイを飛ばす用の情報作成
	void CreateRayInfoFormPlanPos(RayInfo& rDstInfo, const Math::Vector2 planePos);
	
	// ↓追加分 --------------------------------------------------------------------------------------

	// 視野角を取得
	float GetViewingAngle() const { return m_viewingAngle; }
	
	// クリップ値を取得
	float GetNear() const { return m_near; }
	float GetFar() const { return m_far; }

protected:

	// オフセット行列
	Matrix	m_mOffset;
	// カメラ行列
	Matrix	m_mCam;
	// ビュー行列
	Matrix	m_mView;
	// 射影行列
	Matrix	m_mProj;

	GameObject& m_owner;

	// 視野角 (画角)
	float m_viewingAngle;

	// クリップ値 (視錐台のZ軸方向の映る範囲)
	float m_near;	// 前面クリップ面 (最近接)
	float m_far;	// 後方クリップ面 (最遠隔)
};