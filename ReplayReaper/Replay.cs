using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayReaper
{
    public class WoTReplay
    {
        private Players _players;
        private ReplayInfo _info;
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public WoTReplay()
        {
            _players = new Players();
            _info = new ReplayInfo();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public Players Players
        {
            get
            {
                return _players;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public ReplayInfo Info
        {
            get
            {
                return _info;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}