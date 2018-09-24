using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeFade : MonoBehaviour {

    public static IEnumerator FadeIn(GameObject panel, float aTime)
    {
        float alpha = panel.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1, t));
            panel.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }
}
