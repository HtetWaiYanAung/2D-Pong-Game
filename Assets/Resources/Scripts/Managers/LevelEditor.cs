using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public float Spacing = 0.1f;

    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private So_LevelData _soLevelData;
    [SerializeField] private E_BrickType _brickType;
    private List<GameObject> _bricks = new List<GameObject>();

    private void Start()
    {
        ResetLevel();
        for (int i = 0; i < _soLevelData.BrickColorList.Count; i++)
        {
            if (_soLevelData.BrickColorList[i] == E_BrickType.PassThroughBrick)
            {
                _soLevelData.BrickColorList[i] = E_BrickType.PowerUpBrick;
            }
        }
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
                hit.transform.GetComponent<SpriteRenderer>().color = _brickType.GenerateBrickColor();
                _soLevelData.x.Add(hit.transform.localPosition.x);
                _soLevelData.y.Add(hit.transform.localPosition.y);
                _soLevelData.BrickColorList.Add(_brickType);
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }
    }
    public void ResetLevel()
    {
        for (int i = 0; i < _bricks.Count; i++)
        {
            Destroy(_bricks[i]);
        }
        _bricks.Clear();
        float StartX = -(Rows / 2f) * Spacing;
        float StartY = (Columns / 2f) * Spacing + Spacing / 2f;
        for (int x = 1; x <= Rows; x++)
        {
            for (int y = 1; y <= Columns; y++)
            {
                Vector2 pos = new Vector2(-StartY + y * Spacing, -StartX + -x * Spacing);
                GameObject go = Instantiate(_brickPrefab, pos, Quaternion.identity);
                go.transform.SetParent(gameObject.transform, false);
                _bricks.Add(go);
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

