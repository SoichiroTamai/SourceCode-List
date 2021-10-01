#include "../inc_Common.hlsli"
#include "inc_GenerateShadowMapShader.hlsli"

/*
	�E���_�����W�ϊ�����

	�EPos��PS�֍s����Screen���W�n�ɂȂ��Ă��܂��l���ς���
	�@VS�ł̎ˉe���W�����̂܂ܕۑ����Ă������߂�wvpPos�ɂ����

	 �E���W���ς����邩�ς����Ȃ����́A�Z�}���e�B�N�X�ɂ��
*/

// ���_�V�F�[�_
VSOutput main(
	float4 pos : POSITION, // �����F�Z�}���e�B�N�X
	float2 uv : TEXCOORD0)
{
	VSOutput Out;

	// ���C�g�J�����Ŏˉe�ϊ�
	Out.Pos = mul(pos, g_mW);
	Out.Pos = mul(Out.Pos, g_mLightVP);

	// �ˉe���W
	Out.wvpPos = Out.Pos;
	// UV���W
	Out.UV = uv;

	return Out;
}