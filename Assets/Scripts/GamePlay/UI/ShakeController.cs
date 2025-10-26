using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ShakeController : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] float shakeTime;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Shake()
    {
        // Debug.Log("hello1");
        StartCoroutine(ShakeCoroutine());
    }
    IEnumerator ShakeCoroutine()
    {
        Vector2 beforePos = rectTransform.anchoredPosition;
        float st = shakeTime;
        // Debug.Log(st);
        while (st > 0)
        {
            // Debug.Log("hello3");
            rectTransform.anchoredPosition += new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            st -= Time.deltaTime*2;
            yield return null;
            rectTransform.anchoredPosition = beforePos;
            yield return null;
            // Debug.Log("hello");
        }
        rectTransform.anchoredPosition = beforePos;
    }
}
