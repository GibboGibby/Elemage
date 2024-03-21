using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpellShape
{

    private bool reverseAllowed = false;

    public SpellShape(int[,] shape)
    {
        this.shape = shape;
    }

    public SpellShape(int[,] shape, bool reversible)
    {
        this.shape = shape;
        this.reverseAllowed = reversible;
    }

    public SpellShape(Position[] positions, int len)
    {
        for (int i = 0; i < len; i++)
        {
            Tuple<int, int> tuple = PositionValues[positions[i]];
            shape[tuple.Item1, tuple.Item2] = i + 1;
        }
    }

    public static string PositionAsString(Position pos)
    {
        switch (pos)
        {
            case Position.TopLeft:
                return "Top Left";
            case Position.MidLeft:
                return "Mid Left";
            case Position.MidRight:
                return "Mid Right";
            case Position.BottomLeft:
                return "Bottom Left";
            case Position.BottomRight:
                return "Bottom Right";
            case Position.TopRight:
                return "Top Right";
            case Position.TopMiddle:
                return "Top Middle";
            case Position.BottomMiddle:
                return "Bottom Middle";
            case Position.MidMiddle:
                return "Mid Middle";
            default:
                return "No Position Found";
        }
    }

    public enum Position { 
        TopLeft,
        TopMiddle,
        TopRight,
        MidLeft,
        MidMiddle,
        MidRight,
        BottomLeft,
        BottomMiddle,
        BottomRight
    }

    Dictionary<Position, Tuple<int, int>> PositionValues = new Dictionary<Position, Tuple<int, int>>() {
        { Position.TopLeft, new Tuple<int, int>(0,0) },
        { Position.TopMiddle, new Tuple<int, int>(0,1) },
        { Position.TopRight, new Tuple<int, int>(0,2) },
        { Position.MidLeft, new Tuple<int, int>(1,0) },
        { Position.MidMiddle, new Tuple<int, int>(1,1) },
        { Position.MidRight, new Tuple<int, int>(1,2) },
        { Position.BottomLeft, new Tuple<int, int>(2,0) },
        { Position.BottomMiddle, new Tuple<int, int>(2,1) },
        { Position.BottomRight, new Tuple<int, int>(2,2) },
    };
    // Start is called before the first frame update

    private int[,] shape =
    {
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 }
    };

    public string GetRowAsString(int x)
    {
        return shape[x, 0].ToString() + " " + shape[x,1].ToString() + " " + shape[x,2].ToString();
    }

    public void SetReversable(bool val)
    {
        reverseAllowed = val;
    }

    public static bool operator ==(SpellShape left, SpellShape right)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (left.reverseAllowed || right.reverseAllowed)
                {
                    if ((left.shape[i,j] == 0 && right.shape[i,j] != 0) || (left.shape[i,j] != 0 && right.shape[i,j] == 0))
                    {
                        return false;
                    }
                }
                else
                {
                    if (left.shape[i, j] != right.shape[i, j])
                    {
                        return false;
                    }
                }
                
            }
        }

        return true;
    }

    public static bool operator !=(SpellShape left, SpellShape right)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (left.shape[i, j] == right.shape[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
