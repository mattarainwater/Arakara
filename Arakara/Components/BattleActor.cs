﻿using Arakara.Battle;
using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public abstract class BattleActor : Component, IUpdatable, IComparable<BattleActor>
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public Faction Faction { get; set; }
        public BattleState State { get; set; }
        public bool Immune { get; set; }
        public int Size { get; set; }
        public bool Targetable { get; set; }
        public BattleController Controller { get; set; }
        public float DodgeChance { get; set; }
        public float CriticalHitChance { get; set; }
        public float Speed { get; set; }
        public BattleStatusCollection Statuses { get; set; }

        protected List<BattleActor> _selectedTargets;

        private TargetPolygon _targetablePolygon;
        private TurnMarkerPolygon _turnMarkerPolygon;

        public BattleActor(string name, int maxHp, Faction faction, float dodgeChance, float critChance, float speed, int size = 1)
        {
            Name = name;
            MaxHP = maxHp;
            CurrentHP = maxHp;
            Speed = speed;
            Faction = faction;
            State = BattleState.NotTurn;
            Size = size;
            DodgeChance = dodgeChance;
            CriticalHitChance = critChance;
            Statuses = new BattleStatusCollection();
        }

        public override void onAddedToEntity()
        {
            entity.addComponent(new Text(CommonResources.DefaultBitmapFont, Name, new Vector2(0f, -5), Color.Gray));

            var targetPolygon = new TargetPolygon();
            _targetablePolygon = entity.addComponent(targetPolygon);
            _targetablePolygon.enabled = false;

            var turnMarkerPolygon = new TurnMarkerPolygon();
            _turnMarkerPolygon = entity.addComponent(turnMarkerPolygon);
            _turnMarkerPolygon.enabled = false;
        }

        public void update()
        {
            if(Targetable)
            {
                _targetablePolygon.enabled = true;
            }
            else
            {
                _targetablePolygon.enabled = false;
            }

            if(State != BattleState.NotTurn)
            {
                _turnMarkerPolygon.enabled = true;
            }
            else
            {
                _turnMarkerPolygon.enabled = false;
            }
        }

        public void ProcessTurn()
        {
            switch (State)
            {
                case BattleState.StartOfTurn:
                    Statuses.ApplyStatuses(this, Controller);
                    OnStartOfTurn();
                    break;
                case BattleState.DuringTurn:
                    DuringTurn();
                    break;
                case BattleState.EndOfTurn:
                    OnEndOfTurn();
                    break;
            }
        }

        public void SelectTarget(BattleActor target)
        {
            SelectTargets(new List<BattleActor> { target });
        }

        public void SelectTargets(List<BattleActor> targets)
        {
            _selectedTargets = targets;
        }

        protected abstract void OnStartOfTurn();

        protected abstract void DuringTurn();

        protected abstract void OnEndOfTurn();

        public int CompareTo(BattleActor other)
        {
            return -Speed.CompareTo(other.Speed);
        }
    }
}
