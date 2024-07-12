using UnityEngine;

public class SkinnedMeshTransparency : MonoBehaviour
{
    public float transparency = 0.5f; // ���� �� (0 = ������ ����, 1 = ������ ������)
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Material[] materials;

    void Start()
    {
        // SkinnedMeshRenderer�� �����ɴϴ�.
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // �޽��� ���Ǵ� ���׸��� �迭�� �����ɴϴ�.
        materials = skinnedMeshRenderer.materials;

        // ���׸����� ���̴��� Transparent�� �����ϴ��� Ȯ���մϴ�.
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
        // �� �����Ӹ��� ������ ������Ʈ�մϴ�.
        //SetTransparency(transparency);
    }

    public void SetTransparency(float alpha)
    {
        foreach (var material in materials)
        {
            // ���� ���׸����� ������ �����ɴϴ�.
            Color color = material.color;

            // ���� ���� �����մϴ�.
            color.a = Mathf.Clamp01(alpha);

            // ���׸����� ������ ������Ʈ�մϴ�.
            material.color = color;

            // ���׸����� ������ ��带 �����մϴ�.
            if (alpha < 1.0f)
            {
                material.SetFloat("_Mode", 3); // Transparent ���
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
                material.SetFloat("_Mode", 0); // Opaque ���
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
