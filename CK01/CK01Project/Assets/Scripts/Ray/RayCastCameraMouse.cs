using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class RayCastCameraMouse : MonoBehaviour
{

    [SerializeField] private PlayableDirector PlayTimeline;

    private Camera mainCamera;
    public float raycastDistance = 100f;

    public TMP_Text textMesh;

    private bool _isCorou = true;
    private int _isCount = 0;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Assuming you're using Fire1 input to shoot the raycast (you can change it to your preferred input).
        {
            ShootRaycastFromCamera();
        }
    }

    void ShootRaycastFromCamera()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // If the raycast hits something, you can do something with the hit information here.
            Debug.Log("Raycast hit at: " + hit.point);

            Debug.Log("Hit Green " + hit.collider.gameObject.name);

            if(hit.collider.gameObject.name == "Dark")
            {
                PlayTimeline.Play();
            }

            if(hit.collider.gameObject.name == "BadRoom")
            {
                if(_isCorou)
                {
                    _isCorou = false;
                    switch (_isCount)
                    {
                        case 0:
                            StartCoroutine(TextPopUp("�̰� ���� ���� �Ⱦ��ϴ� ħ��.."));
                            break;
                        case 1:
                            StartCoroutine(TextPopUp("���� �� ħ�밡 �Ⱦ�.. �ȴٰ�.."));
                            break;
                        default:
                            StartCoroutine(TextPopUp("�׸� ���� �;�.."));
                            break;
                    }

                    _isCount++;
                }
            }
        }
        else
        {
            // If the raycast doesn't hit anything, you can handle that here.
            Debug.Log("Raycast didn't hit anything.");
        }
    }

    IEnumerator TextPopUp(string text)
    {
        TextMeshConvert(text);

        yield return new WaitForSeconds(3.0f);

        _isCorou = true;

        TextMeshConvert("");
    }


    private void TextMeshConvert(string text)
    {
        textMesh.text = text;

        
    }
}
