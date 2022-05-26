using UnityEngine;
public class Hlpr_ResizeSpriteFullScreen : MonoBehaviour
{
    private const float _yRatio = 0.8f;
    void Start()
    {
        Resize();
    }
    void Resize()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        transform.localScale = Vector3.one;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float cameraOrthoFullSize = Camera.main.orthographicSize * 2f;
        float worldScreenHeight = cameraOrthoFullSize;
        float worldScreenWidth = cameraOrthoFullSize * Camera.main.aspect;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;

        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height * _yRatio;
        transform.localScale = yHeight;
        ;
    }
}
