using UnityEngine;

namespace MalbersAnimations.Reactions
{
    [System.Serializable]
    [AddTypeMenu("Malbers/Stats")]

    public class StatReaction : Reaction
    {
        public StatModifier modifier;

        public override System.Type ReactionType => typeof(Stats);

        protected override bool _TryReact(Component reactor)
        {
            var stats = reactor as Stats;
            return modifier.ModifyStat(stats);
        }
    }
}
