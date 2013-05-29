using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayReaper
{
    public class Players
    {
        List<Player> _player_list;
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public Players()
        {
            _player_list = new List<Player>();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public void AddPlayer(ref Player player)
        {
            _player_list.Add(player);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public Player this[int index]
        {
            get
            {
                return _player_list[index];
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public int Count
        {
            get
            {
                return _player_list.Count;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}
