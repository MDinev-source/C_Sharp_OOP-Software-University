namespace PlayersAndMonsters.Models.BattleFields
{
    using System;
    using System.Linq;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (attackPlayer.GetType().Name == "Beginner")
            {
                attackPlayer.Health += 40;
                attackPlayer.CardRepository.Cards.ToList().ForEach(x => x.DamagePoints += 30);
            }

            if (enemyPlayer.GetType().Name == "Beginner")
            {
                enemyPlayer.Health += 40;
                enemyPlayer.CardRepository.Cards.ToList().ForEach(x => x.DamagePoints += 30);
            }

            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);

            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);


            while (true)
            {
                int totalAtackDamage = attackPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);

                if (enemyPlayer.Health - totalAtackDamage < 0)
                {
                    enemyPlayer.Health = 0;
                }
                else
                {
                    enemyPlayer.Health -= totalAtackDamage;
                }

                if (enemyPlayer.IsDead)
                {
                    break;
                }

                int totalEnemyDamage = enemyPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);

                if (attackPlayer.Health - totalEnemyDamage < 0)
                {
                    attackPlayer.Health = 0;
                }
                else
                {
                    attackPlayer.Health -= totalEnemyDamage;
                }

                if (attackPlayer.IsDead)
                {
                    break;
                }
            }
        }
    }
}
