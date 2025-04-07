using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("�̵� ����")]
    [Tooltip("��ֹ��� �̵� �ӵ�")]
    public float moveSpeed = 5f;

    [Tooltip("��ֹ��� �̵��� �� �ִ� �ִ� �Ÿ� (����)")]
    public float leftBound = -5f;

    [Tooltip("��ֹ��� �̵��� �� �ִ� �ִ� �Ÿ� (������)")]
    public float rightBound = 5f;

    [Tooltip("ó�� �̵� ���� (true: ������, false: ����)")]
    public bool moveRight = true;

    private Vector3 startPosition;

    void Start()
    {
        // ���� ��ġ ����
        startPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ ���
        float currentX = transform.position.x - startPosition.x;

        // ���� ��ȯ Ȯ��
        if (currentX >= rightBound)
        {
            moveRight = false;
        }
        else if (currentX <= leftBound)
        {
            moveRight = true;
        }

        // �̵� ���⿡ ���� �̵�
        float direction = moveRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }

    // �ð������� �̵� ���� ǥ�� (�����Ϳ����� ����)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 leftPoint = transform.position;
        leftPoint.x = transform.position.x + leftBound;

        Vector3 rightPoint = transform.position;
        rightPoint.x = transform.position.x + rightBound;

        Gizmos.DrawSphere(leftPoint, 0.3f);
        Gizmos.DrawSphere(rightPoint, 0.3f);
        Gizmos.DrawLine(leftPoint, rightPoint);
    }
}