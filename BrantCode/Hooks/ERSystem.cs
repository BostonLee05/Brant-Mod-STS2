using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brant.Cards;

namespace Brant.Hooks;

public class ERSystem : CustomSingletonModel
{
    public ERSystem() : base(HookType.Combat) { }

    public const int DefaultER = 100;
    public const int MaxER = 250;
    public const int MaxGauge = 4;

    public static readonly SpireField<PlayerCombatState, int> ERField = new(() => DefaultER);
    public static readonly SpireField<PlayerCombatState, int> MaxERField = new(() => MaxER);
    public static readonly SpireField<PlayerCombatState, int> GaugeField = new(() => 0);
    public static readonly SpireField<PlayerCombatState, int> AttackCounterField = new(() => 0);
    public static readonly SpireField<PlayerCombatState, bool> ToTheHorizonActiveField = new(() => false);

    public static int GetER(PlayerCombatState? state) => state != null ? ERField.Get(state) : DefaultER;
    
    public static void SetER(PlayerCombatState? state, int value)
    {
        if (state != null)
        {
            ERField.Set(state, Math.Clamp(value, 0, MaxER));
        }
    }

    public static void AddER(PlayerCombatState? state, int amount)
    {
        if (state != null)
        {
            SetER(state, GetER(state) + amount);
        }
    }

    public static void RemoveER(PlayerCombatState? state, int amount)
    {
        if (state != null)
        {
            SetER(state, GetER(state) - amount);
        }
    }

    public static int GetGauge(PlayerCombatState? state) => state != null ? GaugeField.Get(state) : 0;

    public static void SetGauge(PlayerCombatState? state, int value)
    {
        if (state != null)
        {
            GaugeField.Set(state, Math.Clamp(value, 0, MaxGauge));
        }
    }

    public static bool IsToTheHorizonActive(PlayerCombatState? state) => state != null && ToTheHorizonActiveField.Get(state);

    public static void SetToTheHorizonActive(PlayerCombatState? state, bool active)
    {
        if (state != null)
        {
            ToTheHorizonActiveField.Set(state, active);
        }
    }

    public static async Task AddGauge(Player player, int amount)
    {
        var state = player.PlayerCombatState;
        if (state == null) return;
        int currentGauge = GetGauge(state);
        for (int i = 0; i < amount; i++)
        {
            if (currentGauge >= MaxGauge)
            {
                break;
            }
            currentGauge++;
            SetGauge(state, currentGauge);

            // Every 2 gauges filled, gains 1 Energy and 5 ER
            if (currentGauge % 2 == 0)
            {
                await PlayerCmd.GainEnergy(1M, player);
                AddER(state, 5);
            }

            // Every 4 gauges filled, Ocean Odyssey card is created
            if (currentGauge >= MaxGauge)
            {
                var combatState = player.Creature.CombatState;
                if (combatState != null)
                {
                    var card = combatState.CreateCard<OceanOdyssey>(player);
                    await CardPileCmd.AddGeneratedCardsToCombat(new CardModel[] { card }, PileType.Hand, player);
                }
            }
        }
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == CombatSide.Player)
        {
            foreach (var player in combatState.Players)
            {
                if (player.Character is Character.BrantCharacterModel)
                {
                    // Resets To The Horizon turn active flag at the start of each player turn
                    SetToTheHorizonActive(player.PlayerCombatState, false);
                }
            }
        }
        await base.AfterSideTurnStart(side, participants, combatState);
    }
}
