using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pacman_c_sharp
{
    class DelayOffTimer
    {
        private DateTime startTime = DateTime.UtcNow;

        public void Start()
        {
           startTime = DateTime.UtcNow;
        }
        public bool Check(int millisecDelayOff)
        {
            var delay = new TimeSpan(0, 0, 0, 0, millisecDelayOff);
        
            TimeSpan duration = DateTime.UtcNow - startTime;

            return duration.Duration() < delay;
        }
    }
}
