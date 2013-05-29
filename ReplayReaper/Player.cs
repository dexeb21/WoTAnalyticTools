using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReplayReaper
{
    public class Player
    {
        private UInt64  _ID;
        private string  _vehicleType;
        private bool    _isAlive;
        private string  _name;
        private string  _clanAbbrev;
        private int     _team;
        private string  _events;
        private bool    _isTeamKiller;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        public Player()
        {

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public UInt64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string VehicleType
        {
            get
            {
                return _vehicleType;
            }
            set
            {
                _vehicleType = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public bool isAlive
        {
            get
            {
                return _isAlive;
            }
            set
            {
                _isAlive = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string ClanAbbrev
        {
            get
            {
                return _clanAbbrev;
            }
            set
            {
                _clanAbbrev = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public int Team
        {
            get
            {
                return _team;
            }
            set
            {
                _team = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public bool isTeamKiller
        {
            get
            {
                return _isTeamKiller;
            }
            set
            {
                _isTeamKiller = value;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}
