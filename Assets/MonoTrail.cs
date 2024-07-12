using UnityEngine;

public class MonoTrail : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material[] afterimageMaterials;
    public float afterimageDuration = 0.5f;
    public float afterimageInterval = 0.1f;

    private float lastAfterimageTime;

    void Update()
    {
        // 잔상 생성 주기를 체크합니다.
        if (Time.time > lastAfterimageTime + afterimageInterval)
        {
            //CreateAfterimage();
            lastAfterimageTime = Time.time;
        }
    }

    void CreateAfterimage()
    {
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh);

        // 각 서브 메시에 대해 잔상을 생성합니다.
        for (int i = 0; i < bakedMesh.subMeshCount; i++)
        {
            GameObject afterimage = new GameObject("Afterimage");
            afterimage.transform.position = transform.position;
            afterimage.transform.rotation = transform.rotation;

            MeshFilter meshFilter = afterimage.AddComponent<MeshFilter>();
            meshFilter.mesh = bakedMesh;

            MeshRenderer meshRenderer = afterimage.AddComponent<MeshRenderer>();
            meshRenderer.material = afterimageMaterials[i];

            meshRenderer.materials = new Material[] { afterimageMaterials[i] };
            meshRenderer.SetPropertyBlock(new MaterialPropertyBlock());

            // 해당 서브 메시만 렌더링하도록 설정합니다.
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.vertices = bakedMesh.vertices;
            meshFilter.mesh.normals = bakedMesh.normals;
            meshFilter.mesh.uv = bakedMesh.uv;
            meshFilter.mesh.triangles = bakedMesh.GetTriangles(i);
            meshFilter.mesh.RecalculateBounds();

            Destroy(afterimage, afterimageDuration);
        }
    }
}
