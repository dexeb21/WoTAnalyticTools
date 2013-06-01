using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheReaper
{
    public class CommonData
    {
        private IList<object> _common;
        public CommonData(IList<object> data)
        {
            _common = data;
        }
        /// <summary>
        /// ID Типа арены
        /// </summary>
        public int arenaTypeID { get { return (int)_common[0]; } }
        /// <summary>
        /// Время создания арены
        /// </summary>
        public int arenaCreateTime { get { return (int)_common[1]; } }
        /// <summary>
        /// Команда победителей
        /// </summary>
        public int winnerTeam { get { return (int)_common[2]; } }
        /// <summary>
        /// ???
        /// </summary>
        public int finishReason { get { return (int)_common[3]; } }
        /// <summary>
        /// Продолжительность боя
        /// </summary>
        public double duration { get { return (double)_common[4]; } }
        public int p05 { get { return (int)_common[5]; } }
        public int p06 { get { return (int)_common[6]; } }
        /// <summary>
        /// Блокировка танка
        /// </summary>
        public int vehLockMode { get { return (int)_common[7]; } }
    }
}
