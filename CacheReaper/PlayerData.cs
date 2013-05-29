using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheReaper
{
    public class PlayerData
    {
        private IList<object> _player;
        public PlayerData(IList<object> data)
        {
            _player = data;
        }
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string name { get { return (string)_player[0]; } }
        /// <summary>
        /// ID клана
        /// </summary>
        public int clanDBID { get { return (int)_player[1]; } }
        /// <summary>
        /// Абревиатура клана
        /// </summary>
        public string clanAbbver { get { return (string)_player[2]; } }
        /// <summary>
        /// prebattleID
        /// </summary>
        public int prebattleID { get { return (int)_player[3]; } }
        /// <summary>
        /// Команда/респ
        /// </summary>
        public int team { get { return (int)_player[4]; } }
    }
}
