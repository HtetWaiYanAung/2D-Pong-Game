
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pfb_BrickPosEditor : MonoBehaviour
{
    public int rows;
    public int columns;

    public float spacing = 0.1f;

    [SerializeField] private GameObject brickPrefab;
    private List<GameObject> bricks = new List<GameObject>();
    [SerializeField] private So_BrickPos so_BrickPos;
    [SerializeField] private BrickType brickType;

    private void Start()
    {
        ResetLevel();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Ctrl_Brick ctrl_Brick = hit.transform.GetComponent<Ctrl_Brick>();
                hit.transform.GetComponent<SpriteRenderer>().color = brickType.GenerateBrickColor();
                so_BrickPos.x.Add(hit.transform.localPosition.x);
                so_BrickPos.y.Add(hit.transform.localPosition.y);
                so_BrickPos.BrickColorList.Add(brickType);
                EditorUtility.SetDirty(this);
            }
        }
    }
    public void ResetLevel()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            Destroy(bricks[i]);
        }
        bricks.Clear();
        float StartX = -(rows / 2f) * spacing;
        float StartY = (columns / 2f) * spacing + spacing / 2f;
        for (int x = 1; x <= rows; x++)
        {
            for (int y = 1; y <= columns; y++)
            {
                Vector2 pos = new Vector2(-StartY + y * spacing, -StartX + -x * spacing);
                GameObject go = Instantiate(brickPrefab, pos, Quaternion.identity);
                go.transform.SetParent(gameObject.transform, false);
                bricks.Add(go);
            }
        }

        if (Camera.main.aspect <= 0.5f)
        {
            transform.localScale = Vector3.one * Camera.main.aspect * 0.75f * 2f;
        }
        else
        {
            transform.localScale = Vector3.one * 0.75f;
        }

    }
}

