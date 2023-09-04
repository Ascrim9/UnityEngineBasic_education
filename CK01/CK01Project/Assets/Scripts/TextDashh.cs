using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDashh : MonoBehaviour
{
    public float letterDelay = 0.1f;
    private TMP_Text textMeshPro;
    private string fullText;
    private int currentCharacter = 0;
    private bool isRevealing = false;

    private void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
        fullText = textMeshPro.text;
        textMeshPro.text = string.Empty;
    }

    public void StartReveal()
    {
        if (!isRevealing)
        {
            StartCoroutine(RevealText());
        }
    }

    private IEnumerator RevealText()
    {
        isRevealing = true;
        currentCharacter = 0;
        textMeshPro.text = string.Empty;

        while (currentCharacter < fullText.Length)
        {
            textMeshPro.text += fullText[currentCharacter];
            currentCharacter++;
            yield return new WaitForSeconds(letterDelay);
        }

        isRevealing = false;
    }
}