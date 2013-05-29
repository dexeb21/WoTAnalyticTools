using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheReaper
{
    public class VehicleData
    {
        private IList<object> _PersonalResult;
        private int _size;

        public VehicleData(IList<object> personalresult)
        {
            _PersonalResult = personalresult;
            _size = personalresult.Count;
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Оставшееся HP
        /// RAW[0];
        /// </summary>
        public int p00_healt { get { return (int)_PersonalResult[0]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Заработанно серебра
        /// RAW[1];
        /// </summary>
        public int p01_credits { get { return (int)_PersonalResult[1]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Заработанно опыта
        /// RAW[2];
        /// </summary>
        public int p02_XP { get { return (int)_PersonalResult[2]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Произведено выстрелов
        /// RAW[3];
        /// </summary>
        public int p03_shots { get { return (int)_PersonalResult[3]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// прямых попаданий
        /// RAW[4];
        /// </summary>
        public int p04_hits { get { return (int)_PersonalResult[4]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p05 { get { return (int)_PersonalResult[5]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// фугасных повреждений
        /// RAW[6];
        /// </summary>
        public int p06_he_hits { get { return (int)_PersonalResult[6]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// пробитий
        /// RAW[7];
        /// </summary>
        public int p07_pierced { get { return (int)_PersonalResult[7]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Нанесенный урон
        /// RAW[8];
        /// </summary>
        public int p08_damageDealt { get { return (int)_PersonalResult[8]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Урон по разведданным
        /// RAW[9];
        /// </summary>
        public int p09_damageAssisted { get { return (int)_PersonalResult[9]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// полученный урон
        /// RAW[10];
        /// </summary>
        public int p10_damageReceived { get { return (int)_PersonalResult[10]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Получено попаданий
        /// RAW[11];
        /// </summary>
        public int p11_shotsReceived { get { return (int)_PersonalResult[11]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Обнаружено противников RAW[12]
        /// </summary>
        public int p12_spotted { get { return (int)_PersonalResult[12]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Повреждено противников RAW[13]
        /// </summary>
        public int p13_damaged { get { return (int)_PersonalResult[13]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Уничтожено противников RAW[14]
        /// </summary>
        public int p14_kills { get { return (int)_PersonalResult[14]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p15 { get { return (int)_PersonalResult[15]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p16 { get { return (int)_PersonalResult[16]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Тимкиллер
        /// RAW[17];
        /// </summary>
        public bool p17_isTeamKiller { get { return (bool)_PersonalResult[17]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p18 { get { return (int)_PersonalResult[18]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p19 { get { return (int)_PersonalResult[19]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Пройдено RAW[20]
        /// </summary>
        public int p20_mileage { get { return (int)_PersonalResult[20]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Время в бою до уничтожения RAW[21]
        /// </summary>
        public int p21_lifeTime { get { return (int)_PersonalResult[21]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ID Уничтожившего танк RAW[22]
        /// </summary>
        public int p22_killerID { get { return (int)_PersonalResult[22]; } }
        //////////////////////////////////////////////////////////////////////////
        public object p23 { get { return (object)_PersonalResult[23]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ID Потенциальный полученный урон RAW[24]
        /// </summary>
        public int p24_potentialDamageReceived { get { return (int)_PersonalResult[24]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ID Потенциальный полученный урон RAW[24]
        /// </summary>
        public int p25_repair { get { return (int)_PersonalResult[25]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// repair или autoRepairCost Ремонт техники
        /// RAW[25];
        /// </summary>
        public int p26_freeXP { get { return (int)_PersonalResult[26]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Детали
        /// RAW[27];
        /// </summary>
        public List<DetailsInfo> p27_details
        {
            get
            {
                int size = ((string)_PersonalResult[27]).Length;
                byte[] bytes = new byte[size];

                int j = 0;
                foreach (char ch in (string)_PersonalResult[27])
                {
                    bytes[j] = Convert.ToByte(ch);
                    j++;
                }

                // 22 = магическое число Count = x(4+2*9) 4 и 2 колво байт, 9 колво данных
                int chunk_count = size / 22;
                DetailsInfo DI;
                List<DetailsInfo> Result = new List<DetailsInfo>();

                for (int k = 0; k < chunk_count; k++)
                {
                    DI = new DetailsInfo();
                    DI.RAWArray[0] = BitConverter.ToInt32(bytes, 4 * k);

                    for (int l = 0; l < 9; l++)
                    {
                        DI.RAWArray[l + 1] = BitConverter.ToInt16(bytes, 4 * chunk_count + 2 * l + 18 * k);
                    }
                    Result.Add(DI);
                }
                return Result;
            }
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// accountDBID
        /// RAW[28];
        /// </summary>
        public int p28_accountDBID { get { return (int)_PersonalResult[28]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Команда или респ
        /// RAW[29];
        /// </summary>
        public int p29_team { get { return (int)_PersonalResult[29]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ID Танка
        /// RAW[30];
        /// </summary>
        public int p30_typeCompDescr { get { return (int)_PersonalResult[30]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p31 { get { return (int)_PersonalResult[31]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p32 { get { return (int)_PersonalResult[32]; } }
        /**/////////////////////////////////////////////////////////////////////////
        public int p33 { get { return (_size > 33) ? -1 : (int)_PersonalResult[33]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p34 { get { return (_size > 33) ? -1 : (int)_PersonalResult[34]; } }
        /*/////////////////////////////////////////////////////////////////////////
        public int p35 { get { return (int)_PersonalResult[35]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Заработанно серебра (еще один)
        /// RAW[36];
        /// </summary>
        public int p36_tmenXP { get { return (int)_PersonalResult[36]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p37 { get { return (int)_PersonalResult[37]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p38 { get { return (int)_PersonalResult[38]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p39 { get { return (int)_PersonalResult[39]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p40 { get { return (int)_PersonalResult[40]; } }
        //////////////////////////////////////////////////////////////////////////
        public int p41 { get { return (int)_PersonalResult[41]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// repair или autoRepairCost Автоматический ремонт техники
        /// RAW[42];
        /// </summary>
        public int p42_autoRepairCost { get { return (int)_PersonalResult[42]; } }
        //////////////////////////////////////////////////////////////////////////
        public object p43 { get { return (object)_PersonalResult[43]; } }
        //////////////////////////////////////////////////////////////////////////
        public object p44 { get { return (object)_PersonalResult[44]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Премиум вккаунт
        /// RAW[45];
        /// </summary>
        public bool p45_isPremium { get { return (bool)_PersonalResult[45]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Премиум коэфф на кредиты
        /// RAW[46];
        /// </summary>
        public int p46_premiumCreditsFactor10 { get { return (int)_PersonalResult[46]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Премиум коэфф на опыт
        /// RAW[47];
        /// </summary>
        public int p47_premiumXPFactor10 { get { return (int)_PersonalResult[47]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Коэфф за первую победу
        /// RAW[45];
        /// </summary>
        public int p48_dailyXPFactor10 { get { return (int)_PersonalResult[48]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Похоже на базовый коэфф без ПА для опыта и серебра
        /// RAW[49];
        /// </summary>
        public int p49_aogasFactor10 { get { return (int)_PersonalResult[49]; } }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// markOfMastery Хз что это.
        /// RAW[50];
        /// </summary>
        public int p50_markOfMastery { get { return (int)_PersonalResult[50]; } }
        //////////////////////////////////////////////////////////////////////////
        public object p51 { get { return (object)_PersonalResult[51]; } }*/
    }
}
