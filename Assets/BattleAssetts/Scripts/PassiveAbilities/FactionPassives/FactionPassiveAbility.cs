using System.Collections;
using UnityEngine;

namespace Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives
{
    public abstract class FactionPassiveAbility : PassiveAbility
    {
        // the team tag to the party the faction belongs to
        private string tag;

        public FactionPassiveAbility(string tag, string abilityName) : base(null, abilityName)
        {
            this.Tag = tag;
        }

        public string Tag { get => tag; set => tag = value; }
    }
}