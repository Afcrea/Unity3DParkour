using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public GameObject mainCameraObject; // ���� ī�޶� ������Ʈ
    public GameObject character; // ĳ���� ������Ʈ
    public float selectionRadius = 10f; // ĳ���� �ֺ� ������Ʈ ���� �ݰ�
    public LayerMask selectableLayer; // ���� ������ ���̾�
    private GameObject selectedObject; // ���õ� ������Ʈ
    private Camera mainCamera; // ���� ī�޶� ������Ʈ

    void Start()
    {
        // ���� ī�޶� ������Ʈ���� Camera ������Ʈ�� �����ɴϴ�.
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
        // ĳ���� �ֺ��� ��� �ݶ��̴��� ã���ϴ�.
        Collider[] hitColliders = Physics.OverlapSphere(character.transform.position, selectionRadius, selectableLayer);

        // ���� ����� ������Ʈ�� ã�� ���� ������
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 mousePosition = Input.mousePosition;

        foreach (var hitCollider in hitColliders)
        {
            // ��ũ�� ����Ʈ�� ���� ����Ʈ�� ��ȯ
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
                // ������ ���õ� ������Ʈ�� ���̶���Ʈ�� �����մϴ�.
                //HighlightObject(selectedObject, false);
            }

            selectedObject = closestObject; // ���� ����� ������Ʈ�� ����
            //Debug.Log("Selected Object: " + selectedObject.name);

            // ���õ� ������Ʈ�� ���̶���Ʈ�մϴ�.
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
                renderer.material.color = Color.yellow; // ���̶���Ʈ ����
            }
            else
            {
                renderer.material.color = Color.white; // �⺻ ����
            }
        }
    }

    void OnDrawGizmos()
    {
        if (character != null)
        {
            // OverlapSphere�� �ݰ��� �ð������� ǥ���մϴ�.
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(character.transform.position, selectionRadius);
        }
    }
}
