using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hlpr_ResizeSpriteFullScreen : MonoBehaviour
{

    // Start is called before the first frame update
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
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        transform.localScale *= 0.8f;
;    }
}
