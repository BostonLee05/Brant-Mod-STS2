using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using Brant.Cards;
using Brant.Character;
using Brant.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Brant.Relics;

[Pool(typeof(BrantRelicPool))]
public class Bravo : BrantRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override int DisplayAmount => this.Owner?.PlayerCombatState != null ? ERSystem.GetGauge(this.Owner.PlayerCombatState) : 0;

    public override Task BeforeCombatStart()
    {
        if (this.Owner?.PlayerCombatState != null)
        {
            ERSystem.SetToTheHorizonActive(this.Owner.PlayerCombatState, false);
            ERSystem.AttackCounterField.Set(this.Owner.PlayerCombatState, 0);
            ERSystem.SetGauge(this.Owner.PlayerCombatState, 0);
        }
        return base.BeforeCombatStart();
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.PlayerCombatState != null)
        {
            ERSystem.SetToTheHorizonActive(player.PlayerCombatState, false);
        }
        return base.AfterPlayerTurnStart(choiceContext, player);
    }

    public override Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player && this.Owner?.PlayerCombatState != null)
        {
            ERSystem.SetToTheHorizonActive(this.Owner.PlayerCombatState, false);
        }
        return base.BeforeSideTurnEnd(choiceContext, side, participants);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var combatState = this.Owner.PlayerCombatState;
        if (combatState == null) return;

        bool isStrike = cardPlay.Card.Tags.Contains(CardTag.Strike);

        // Every 2 basic attack heals (5hp), Bravo fills 1 gauge
        if (isStrike)
        {
            int counter = ERSystem.AttackCounterField.Get(combatState);
            counter++;
            ERSystem.AttackCounterField.Set(combatState, counter);

            if (counter >= 2)
            {
                ERSystem.AttackCounterField.Set(combatState, 0);
                this.Flash();
                await CreatureCmd.Heal(this.Owner.Creature, 5M);
                await ERSystem.AddGauge(this.Owner, 1);
            }
        }

        // To the Horizon: "This turn, each strike cards fills 1 gauge"
        if (isStrike && ERSystem.IsToTheHorizonActive(combatState))
        {
            this.Flash();
            await ERSystem.AddGauge(this.Owner, 1);
        }
    }
}
