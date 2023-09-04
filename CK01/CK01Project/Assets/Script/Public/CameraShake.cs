using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float originalAmplitude;

    private void Start()
    {
       
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
 
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                originalAmplitude = noise.m_AmplitudeGain;
            }
        }
    }

    public void Shake(float duration, float amplitude, float frequency)
    {
        StartCoroutine(ShakeCamera(duration, amplitude, frequency));
    }

    private IEnumerator ShakeCamera(float duration, float amplitude, float frequency)
    {
        if (virtualCamera != null)
        {
            
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                noise.m_AmplitudeGain = amplitude;
                noise.m_FrequencyGain = frequency;
            }
        }

        yield return new WaitForSeconds(duration);

  
        if (virtualCamera != null)
        {
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise != null)
            {
                noise.m_AmplitudeGain = originalAmplitude;
                noise.m_FrequencyGain = 0f;
            }
        }
    }
}
