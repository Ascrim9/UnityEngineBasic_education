using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public RectTransform uiElement; // 이동시킬 UI 요소 (RawImage 등)

    public float movementAmount = 10.0f; // UI 요소의 이동 양
    public float movementSpeed = 5.0f; // UI 요소의 이동 속도

    private Vector3 originalPosition; // UI 요소의 원래 위치 저장

    void Start()
    {
        originalPosition = uiElement.localPosition;
    }

    void LateUpdate()
    {
        // 마우스 입력을 받아서 UI 요소 이동시킴
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // UI 요소의 새로운 위치 계산
        Vector3 targetPosition = originalPosition + new Vector3(mouseX, -mouseY, 0) * movementAmount;

        // Y 축 이동 제한 (y-5이상은 못내려간다)
        targetPosition.y = Mathf.Max(targetPosition.y, originalPosition.y - 5.0f);

        // 새로운 위치로 부드럽게 이동
        uiElement.localPosition = Vector3.Lerp(uiElement.localPosition, targetPosition, Time.fixedDeltaTime * movementSpeed);
    }
}