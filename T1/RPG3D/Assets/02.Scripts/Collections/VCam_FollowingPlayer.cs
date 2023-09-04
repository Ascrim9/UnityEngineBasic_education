using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace _02.Scripts.Collections
{
    public class VCamFollowingPlayer : MonoBehaviour
    {
        private CinemachineVirtualCamera _vCam;
        
        private Transform _followTarget;
        private Transform _followTargetRoot;

        [FormerlySerializedAs("_rotateSpeedY")] [SerializeField] private float rotateSpeedY;
        [FormerlySerializedAs("_rotateSpeedX")] [SerializeField] private float rotateSpeedX;
        [FormerlySerializedAs("_angleXMax")] [SerializeField] private float angleXMax;
        [FormerlySerializedAs("_angleXMin")] [SerializeField] private float angleXMin;
        [FormerlySerializedAs("_fovMin")] [SerializeField] private float fovMin = 3.0f;
        [FormerlySerializedAs("_fovMax")] [SerializeField] private float fovMax = 3.0f;

        [FormerlySerializedAs("_scrollThreshold")] [SerializeField] private float scrollThreshold;
        [FormerlySerializedAs("_scrollSpeed")] [SerializeField] private float scrollSpeed;

        private void Awake()
        {
            _vCam = GetComponent<CinemachineVirtualCamera>();
            _followTarget = _vCam.Follow;
            _followTargetRoot = _followTarget.root;
        }
        
        private void LateUpdate()
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            _followTargetRoot.Rotate(Vector3.up, mouseX * rotateSpeedY * Time.deltaTime, Space.World);
            _followTarget.Rotate(Vector3.left, mouseY * rotateSpeedX * Time.deltaTime, Space.Self);
            _followTarget.eulerAngles = new Vector3(ClampAngle(_followTarget.eulerAngles.x, angleXMax, angleXMax),
                _followTarget.eulerAngles.y,
                    0.0f);
            
            if (Mathf.Abs(Input.mouseScrollDelta.y) > scrollThreshold)
            {
                _vCam.m_Lens.FieldOfView -= Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime;


                if (_vCam.m_Lens.FieldOfView < fovMin)
                {
                    _vCam.m_Lens.FieldOfView = fovMin;
                }
                else if (_vCam.m_Lens.FieldOfView > fovMax)
                {
                    _vCam.m_Lens.FieldOfView = fovMax;
                }


            }
        }

        
        //유니티에서의 음수 각도는 360 도를 더한 오일러 각도임
        private float ClampAngle(float angle, float min, float max)
        {
            angle = (angle + 360.0f) % 360.0f;

            switch (angle > 180.0f)
            {
                case true:
                    angle -= 360.0f;
                    break;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}
