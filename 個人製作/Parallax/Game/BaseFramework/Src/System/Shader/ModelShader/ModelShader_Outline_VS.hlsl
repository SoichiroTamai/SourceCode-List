// �J�����⃉�C�g�̃f�[�^
#include "../inc_Common.hlsli"
// ���ʃf�[�^
#include "inc_ModelShader.hlsli"


//==================================
// ���_�V�F�[�_�[
//==================================
VSOutput main(
	float4 pos		: POSITION,		// �\�����W
	float2 uv		: TEXCOORD0,	// UV���W
	float3 normal	: NORMAL,		// �@��
	float4 color	: COLOR,		// ���_�F
	float3 tangent	: TANGENT		// �ڐ�
)
{
	VSOutput Out;

	// ���_���W��@�������ɁA�������炷
	pos.xyz = pos.xyz + normal * 0.03;

	// 3D���_���W��2D�֕ϊ�
	Out.Pos = mul(pos, g_mW);		// ���[���h�s��ŕϊ����Ă���A
	Out.Pos = mul(Out.Pos, g_mV);	// �J�����̋t�s��(�r���[�s��)�ŕϊ����āA
	Out.Pos = mul(Out.Pos, g_mP);	// �ˉe�s��ŕϊ�

	return Out;
}