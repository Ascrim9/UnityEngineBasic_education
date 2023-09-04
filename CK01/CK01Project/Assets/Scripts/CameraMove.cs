using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public RectTransform uiElement; // �̵���ų UI ��� (RawImage ��)

    public float movementAmount = 10.0f; // UI ����� �̵� ��
    public float movementSpeed = 5.0f; // UI ����� �̵� �ӵ�

    private Vector3 originalPosition; // UI ����� ���� ��ġ ����

    void Start()
    {
        originalPosition = uiElement.localPosition;
    }

    void LateUpdate()
    {
        // ���콺 �Է��� �޾Ƽ� UI ��� �̵���Ŵ
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // UI ����� ���ο� ��ġ ���
        Vector3 targetPosition = originalPosition + new Vector3(mouseX, -mouseY, 0) * movementAmount;

        // Y �� �̵� ���� (y-5�̻��� ����������)
        targetPosition.y = Mathf.Max(targetPosition.y, originalPosition.y - 5.0f);

        // ���ο� ��ġ�� �ε巴�� �̵�
        uiElement.localPosition = Vector3.Lerp(uiElement.localPosition, targetPosition, Time.fixedDeltaTime * movementSpeed);
    }
}