using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("�� ����")]
    [SerializeField] private GameObject[] mapPrefabs; // 3���� �� ������ �迭
    [SerializeField] private int mapSize = 50; // �� �� ���׸�Ʈ�� ũ��
    [SerializeField] private int initialMapCount = 3; // �ʱ⿡ ������ �� ���׸�Ʈ ��
    [SerializeField] private int maxMapSegments = 10; // �ִ�� ������ �� ���׸�Ʈ ��
    [SerializeField] private float playerLookAheadDistance = 100f; // �÷��̾� �տ� ���� ������ �Ÿ�
    [SerializeField] private bool useSequentialPattern = true; // ���������� �� �������� ������� ����

    [Header("����")]
    [SerializeField] private Transform playerTransform; // �÷��̾��� Transform ����

    private List<GameObject> activeMapSegments = new List<GameObject>(); // Ȱ��ȭ�� �� ���׸�Ʈ ���� ����Ʈ
    private float furthestMapZ = 0f; // ���� �ָ� ������ ���� Z ��ġ
    private int currentPrefabIndex = 0; // ���� ����� ������ �ε���

    private void Start()
    {
        // �� ������ Ȯ��
        if (mapPrefabs == null || mapPrefabs.Length == 0)
        {
            Debug.LogError("�� �������� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // �ʱ� �� ���׸�Ʈ ����
        for (int i = 0; i < initialMapCount; i++)
        {
            CreateMapSegment();
        }

        // �÷��̾ �������� �ʾҴٸ� ã��
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>()?.transform;
            if (playerTransform == null)
            {
                Debug.LogWarning("�÷��̾� Transform�� ã�� �� �����ϴ�. ���� ī�޶� ������ ����մϴ�.");
                playerTransform = Camera.main.transform;
            }
        }
    }

    private void Update()
    {
        // �÷��̾� �տ� �� ���� �� ���׸�Ʈ�� �ʿ����� Ȯ��
        if (playerTransform != null && furthestMapZ - playerTransform.position.z < playerLookAheadDistance)
        {
            CreateMapSegment();
            RemoveDistantMapSegments();
        }
    }

    private void CreateMapSegment()
    {
        if (mapPrefabs == null || mapPrefabs.Length == 0)
        {
            Debug.LogError("�� �������� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ����� �� ������ ����
        GameObject prefabToUse;

        if (useSequentialPattern)
        {
            // ���������� �� ������ ��� (0, 1, 2, 0, 1, 2, ...)
            prefabToUse = mapPrefabs[currentPrefabIndex];

            // ���� �ε����� �̵� (��ȯ��)
            currentPrefabIndex = (currentPrefabIndex + 1) % mapPrefabs.Length;
        }
        else
        {
            // �����ϰ� �� ������ ����
            prefabToUse = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
        }

        // �� �� ���׸�Ʈ�� ��ġ ���
        Vector3 position = new Vector3(0, 0, furthestMapZ);

        // �� ������ �ν��Ͻ�ȭ
        GameObject newMap = Instantiate(prefabToUse, position, Quaternion.identity);
        newMap.transform.parent = transform; // ������ ���� �� ������Ʈ�� �ڽ����� ����
        newMap.name = "�ʼ��׸�Ʈ_" + activeMapSegments.Count + "_" + currentPrefabIndex;

        // Ȱ�� ���׸�Ʈ ����Ʈ�� �߰�
        activeMapSegments.Add(newMap);

        // ���� �� Z ��ġ ������Ʈ
        furthestMapZ += mapSize;
    }

    private void RemoveDistantMapSegments()
    {
        // �ִ� �������� ���� ���׸�Ʈ�� �ִٸ� ���� ������ ���� ����
        if (activeMapSegments.Count > maxMapSegments)
        {
            // ���� ������ �� ���׸�Ʈ ��������
            GameObject oldestSegment = activeMapSegments[0];

            // ����Ʈ���� ����
            activeMapSegments.RemoveAt(0);

            // ���� ������Ʈ �ı�
            Destroy(oldestSegment);
        }
    }

#if UNITY_EDITOR
    // �����Ϳ��� �� ũ�⸦ �ð�ȭ�ϴ� ���� �޼���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(0, 0, i * mapSize);
            Gizmos.DrawWireCube(pos + new Vector3(0, 0, mapSize / 2), new Vector3(mapSize, 1, mapSize));
        }
    }
#endif
}