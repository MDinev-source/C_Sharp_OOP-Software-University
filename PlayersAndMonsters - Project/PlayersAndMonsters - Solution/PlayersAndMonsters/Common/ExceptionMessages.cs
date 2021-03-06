using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Common
{
    public static class ExceptionMessages
    {
        public const string InvalidUserName =
          "Player's username cannot be null or an empty string.";
        public const string DamagePointCannoBeNull = "Damage points cannot be less than zero";
        public const string InvalidHealth =
         "Player's health bonus cannot be less than zero.";
        public const string InvalidCardName =
         "Card's name cannot be null or an empty string.";
        public const string InvalidDamagePoints =
         "Card's damage points cannot be less than zero.";
        public const string InvalidHealthPoints =
          "Card's HP cannot be less than zero.";
        public const string PlayerIsDead =
        "Player is dead!";
        public const string InvalidCard =
       "Card cannot be null!";
        public const string RepoContainsCard
           = "Card {0} already exists!";
        public const string InvalidPlayer =
      "Player cannot be null!";
        public const string RepoContainsPlayer
           = "Player {0} already exists!";
    }
}
