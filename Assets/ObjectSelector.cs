using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public GameObject mainCameraObject; // 메인 카메라 오브젝트
    public GameObject character; // 캐릭터 오브젝트
    public float selectionRadius = 10f; // 캐릭터 주변 오브젝트 선택 반경
    public LayerMask selectableLayer; // 선택 가능한 레이어
    private GameObject selectedObject; // 선택된 오브젝트
    private Camera mainCamera; // 메인 카메라 컴포넌트

    void Start()
    {
        // 메인 카메라 오브젝트에서 Camera 컴포넌트를 가져옵니다.
        mainCamera = mainCameraObject.GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("The main camera object does not have a Camera component.");
        }
    }

    void Update()
    {
        SelectObject();
    }

    void SelectObject()
    {
        // 캐릭터 주변의 모든 콜라이더를 찾습니다.
        Collider[] hitColliders = Physics.OverlapSphere(character.transform.position, selectionRadius, selectableLayer);

        // 가장 가까운 오브젝트를 찾기 위한 변수들
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 mousePosition = Input.mousePosition;

        foreach (var hitCollider in hitColliders)
        {
            // 스크린 포인트를 월드 포인트로 변환
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(hitCollider.transform.position);
            float distance = Vector3.Distance(screenPoint, mousePosition);

            //Debug.Log("screenPoint : " + screenPoint + "\t mousePosition : " + mousePosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hitCollider.gameObject;
            }
        }

        if (closestObject != null)
        {
            if (selectedObject != null)
            {
                // 이전에 선택된 오브젝트의 하이라이트를 해제합니다.
                //HighlightObject(selectedObject, false);
            }

            selectedObject = closestObject; // 가장 가까운 오브젝트를 선택
            //Debug.Log("Selected Object: " + selectedObject.name);

            // 선택된 오브젝트를 하이라이트합니다.
            //HighlightObject(selectedObject, true);
        }
        else
        {
            //Debug.Log("No object found in range.");
        }
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (highlight)
            {
                renderer.material.color = Color.yellow; // 하이라이트 색상
            }
            else
            {
                renderer.material.color = Color.white; // 기본 색상
            }
        }
    }

    void OnDrawGizmos()
    {
        if (character != null)
        {
            // OverlapSphere의 반경을 시각적으로 표시합니다.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(character.transform.position, selectionRadius);
        }
    }
}
