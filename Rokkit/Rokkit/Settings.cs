using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rokkit
{
    public class Settings
    {
        static public Vector2 Gravity = new Vector2(0, 9.8f);
        // static public Vector2 Gravity = new Vector2(0, 0);
        // static public float StepSpeed = 0.17f;
        static public float StepSpeed = 0.10f;
        static public Keys Restart = Keys.R;
    }
}
