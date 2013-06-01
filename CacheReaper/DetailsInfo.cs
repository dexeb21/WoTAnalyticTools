using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheReaper
{
    public class DetailsInfo
    {
        public int[] RAWArray = new int[10];
        public int ID { get { return RAWArray[0]; } }
        /// <summary>
        /// Вы обнаружили этого противника
        /// </summary>
        public int spotted { get { return RAWArray[1]; } }
        /// <summary>
        /// Вы уничтожили этого противника
        /// </summary>
        public int killed { get { return RAWArray[2]; } }
        /// <summary>
        /// Кол-во выстрелов
        /// </summary>
        public int hits { get { return RAWArray[3]; } }
        /// <summary>
        /// Кол-во фугасных повреждений
        /// </summary>
        public int he_hits { get { return RAWArray[4]; } }
        /// <summary>
        /// Кол-во пробитий
        /// </summary>
        public int pierced { get { return RAWArray[5]; } }
        /// <summary>
        /// Урона нанесено
        /// </summary>
        public int damageDealt { get { return RAWArray[6]; } }
        /// <summary>
        /// Урон по вашим разведданным
        /// </summary>
        public int damageAssisted { get { return RAWArray[7]; } }
        /// <summary>
        /// Критический повреждений нанесено
        /// </summary>
        public int crits { get { return RAWArray[8]; } }
        /// <summary>
        /// Повреждения от огня
        /// </summary>
        public int fire { get { return RAWArray[9]; } }
    }
}
