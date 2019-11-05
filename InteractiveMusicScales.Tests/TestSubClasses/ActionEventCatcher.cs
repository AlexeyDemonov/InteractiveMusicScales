using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveMusicScales.Tests
{
    class ActionEventCatcher
    {
        public bool EventWasTriggered { get; private set; } = false;

        public void CatchEvent()
        {
            EventWasTriggered = true;
        }
    }
}
