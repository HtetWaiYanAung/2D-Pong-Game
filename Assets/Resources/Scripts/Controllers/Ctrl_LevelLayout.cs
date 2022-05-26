using System.Collections.Generic;
using UnityEngine;

public class Ctrl_LevelLayout : MonoBehaviour
{
    public So_LevelData Level;
    private List<Ctrl_Brick> _bricks = new List<Ctrl_Brick>();

    public List<Ctrl_Brick> GenerateBricks()
    {
        for (int i = 0; i < _bricks.Count; i++)
        {
            Destroy(_bricks[i]);
        }
        _bricks.Clear();
        for (int i = 0; i < Level.x.Count; i++)
        {
            Vector2 pos = new Vector2(Level.x[i], Level.y[i]);
            GameObject go = Instantiate(Level.BrickColorList[i].GenerateBrickType(), pos, Quaternion.identity);
            go.transform.SetParent(gameObject.transform, false);
            _bricks.Add(go.GetComponent<Ctrl_Brick>());
        }
        return _bricks;
    }

}
public static class BrickColorExtension
{
    public static Color GenerateBrickColor(this E_BrickType brickColor)
    {
        switch (brickColor)
        {
            case E_BrickType.ToughBrick:
                return Color.red;
            case E_BrickType.NormalBrick:
                return Color.green;
            case E_BrickType.PowerUpBrick:
                return Color.blue;
            case E_BrickType.PassThroughBrick:
                return Color.yellow;
            default:
                return Color.white;
        }
        
    }
    public static GameObject GenerateBrickType(this E_BrickType brickType)
    {
        switch (brickType)
        {
            case E_BrickType.ToughBrick:
                return Brick("Pfb_BrickTough");
            case E_BrickType.NormalBrick:
                return Brick("Pfb_BrickNormal");
            case E_BrickType.PowerUpBrick:
                return Brick("Pfb_BrickPowerUp");
            case E_BrickType.PassThroughBrick:
                return Brick("Pfb_BrickPassThrough");
            default:
                return Brick("Pfb_BrickNormal");

        }
    }
    static GameObject Brick(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/{name}");
    }
}
