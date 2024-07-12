using UnityEngine;

public class SkinnedMeshTransparency : MonoBehaviour
{
    public float transparency = 0.5f; // 투명도 값 (0 = 완전히 투명, 1 = 완전히 불투명)
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Material[] materials;

    void Start()
    {
        // SkinnedMeshRenderer를 가져옵니다.
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // 메쉬에 사용되는 메테리얼 배열을 가져옵니다.
        materials = skinnedMeshRenderer.materials;

        // 메테리얼의 쉐이더가 Transparent를 지원하는지 확인합니다.
        foreach (var material in materials)
        {
            if (material.shader.name != "Standard")
            {
                Debug.LogWarning("This script only works with the Standard Shader.");
            }
        }
    }

    void Update()
    {
        // 매 프레임마다 투명도를 업데이트합니다.
        //SetTransparency(transparency);
    }

    public void SetTransparency(float alpha)
    {
        foreach (var material in materials)
        {
            // 현재 메테리얼의 색상을 가져옵니다.
            Color color = material.color;

            // 알파 값을 설정합니다.
            color.a = Mathf.Clamp01(alpha);

            // 메테리얼의 색상을 업데이트합니다.
            material.color = color;

            // 메테리얼의 렌더링 모드를 설정합니다.
            if (alpha < 1.0f)
            {
                material.SetFloat("_Mode", 3); // Transparent 모드
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
            }
            else
            {
                material.SetFloat("_Mode", 0); // Opaque 모드
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
            }
        }
    }
}
