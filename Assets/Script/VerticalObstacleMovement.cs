using UnityEngine;

public class VerticalObstacleMovement : MonoBehaviour
{
    [Header("�̵� ����")]
    [Tooltip("��ֹ��� �̵� �ӵ�")]
    public float moveSpeed = 3f;

    [Tooltip("��ֹ��� �̵��� �� �ִ� �ִ� �Ÿ� (�Ʒ�)")]
    public float bottomBound = -3f;

    [Tooltip("��ֹ��� �̵��� �� �ִ� �ִ� �Ÿ� (��)")]
    public float topBound = 3f;

    [Tooltip("ó�� �̵� ���� (true: ��, false: �Ʒ�)")]
    public bool moveUp = true;

    private Vector3 startPosition;

    void Start()
    {
        // ���� ��ġ ����
        startPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ ���
        float currentY = transform.position.y - startPosition.y;

        // ���� ��ȯ Ȯ��
        if (currentY >= topBound)
        {
            moveUp = false;
        }
        else if (currentY <= bottomBound)
        {
            moveUp = true;
        }

        // �̵� ���⿡ ���� �̵�
        float direction = moveUp ? 1f : -1f;
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);
    }

    // �ð������� �̵� ���� ǥ�� (�����Ϳ����� ����)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 bottomPoint = transform.position;
        bottomPoint.y = transform.position.y + bottomBound;

        Vector3 topPoint = transform.position;
        topPoint.y = transform.position.y + topBound;

        Gizmos.DrawSphere(bottomPoint, 0.3f);
        Gizmos.DrawSphere(topPoint, 0.3f);
        Gizmos.DrawLine(bottomPoint, topPoint);
    }
}