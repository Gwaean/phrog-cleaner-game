using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float animDuration = 0.2f;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void FadeOutAnim()
    {
        StartCoroutine(FillingAnim());
    }
    IEnumerator FillingAnim()
    {
        float elapsedTime = 0f;
        float startValue = sprite.color.a;

        while (elapsedTime < animDuration)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(startValue, 0f /*targetValue*/, elapsedTime / animDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        gameObject.SetActive(false);
    }
}