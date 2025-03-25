using Game.Enemies;

namespace Game.Statistics
{
    public class GameStatistic
    {
        public EnemyStatistic[] Enemies;
    
        public GameStatistic()
        {
            Enemies = new EnemyStatistic[0];
        }
    }
}