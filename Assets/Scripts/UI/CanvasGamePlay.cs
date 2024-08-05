using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    class CanvasGamePlay: UICanvas
    {
        [SerializeField] Joystick joystick;
        public Joystick GetJoystick()
        {
            return joystick;
        }
    }
}
