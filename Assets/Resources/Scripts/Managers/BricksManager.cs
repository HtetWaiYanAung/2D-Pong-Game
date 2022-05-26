using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    public int rows;
    public int columns;

    public float spacing = 0.1f;

    private List<Ctrl_Brick> bricks = new List<Ctrl_Brick>();
    public So_BrickPos Level;

    private void Start()
    {
        //GenerateBircks();
    }
    public void GenerateBircks()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            Destroy(bricks[i]);
        }
        bricks.Clear();
        for (int i = 0; i < Level.x.Count; i++)
        {
            Vector2 pos = new Vector2(Level.x[i], Level.y[i]);
            GameObject go = Instantiate(Level.BrickColorList[i].GenerateBrickType(), pos, Quaternion.identity);
            go.transform.SetParent(gameObject.transform, false);
            bricks.Add(go.GetComponent<Ctrl_Brick>());
        }
    }

    public List<Ctrl_Brick> GenerateBricks()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            Destroy(bricks[i]);
        }
        bricks.Clear();
        for (int i = 0; i < Level.x.Count; i++)
        {
            Vector2 pos = new Vector2(Level.x[i], Level.y[i]);
            GameObject go = Instantiate(Level.BrickColorList[i].GenerateBrickType(), pos, Quaternion.identity);
            go.transform.SetParent(gameObject.transform, false);
            bricks.Add(go.GetComponent<Ctrl_Brick>());
        }
        return bricks;
    }

}
public static class BrickColorExtension
{
    public static Color GenerateBrickColor(this BrickType brickColor)
    {
        switch (brickColor)
        {
            case BrickType.Red:
                return Color.red;
            case BrickType.Green:
                return Color.green;
            case BrickType.Blue:
                return Color.blue;
            case BrickType.Yellow:
                return Color.yellow;
        }
        return Color.white;
    }
    public static GameObject GenerateBrickType(this BrickType brickColor)
    {
        switch (brickColor)
        {
            case BrickType.Red:
                return Brick("Pfb_BrickTough");
            case BrickType.Green:
                return Brick("Pfb_BrickNormal");
            case BrickType.Blue:
                return Brick("Pfb_BrickPowerUp");
            case BrickType.Yellow:
                return Brick("Pfb_BrickPassThrough");
        }
        return Brick("Pfb_BrickNormal");
    }
    static GameObject Brick(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/{name}");
    }
}
