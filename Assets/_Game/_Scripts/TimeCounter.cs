using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{

    [SerializeField] private float currentTime = 0f;
    private bool counting = true;
    [SerializeField] private TextMeshProUGUI counterUI;

    private Coroutine coroutine;

    private void Start()
    {
        coroutine = StartCoroutine(Counter());
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        string counterText = "<b>TEMPO:	" + time.ToString(@"m\:ss");
        counterUI.text = counterText;
    }

    IEnumerator Counter()
    {
        if (counting)
        {
            yield return new WaitForSeconds(1f);
            currentTime++;
        }
        yield return coroutine = StartCoroutine(Counter());
    }
}