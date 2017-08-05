using Arakara.Battle;
using Arakara.Battle.Phases;
using Arakara.Common;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
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
        public bool IsActive { get; set; }
        public bool Immune { get; set; }
        public int Size { get; set; }
        public bool Targetable { get; set; }
        public BattleController Controller { get; set; }
        public float DodgeChance { get; set; }
        public float CriticalHitChance { get; set; }
        public float Speed { get; set; }
        public BattleStatusCollection Statuses { get; set; }
        public Sprite<Animations> Animator { get; set; }
        public Animations IdleAnimation { get; set; }
        public BattleAction<Animations> CurrentAction { get; set; }

        public LinkedList<Phase> Phases { get; set; }
        private LinkedListNode<Phase> _currentPhase;

        public List<BattleActor> SelectedTargets { get; set; }

        private TargetPolygon _targetablePolygon;
        private TurnMarkerPolygon _turnMarkerPolygon;

        public BattleActor(string name, int maxHp, Faction faction, float dodgeChance, float critChance, float speed, int size = 1)
        {
            Name = name;
            MaxHP = maxHp;
            CurrentHP = maxHp;
            Speed = speed;
            Faction = faction;
            IsActive = false;
            Size = size;
            DodgeChance = dodgeChance;
            CriticalHitChance = critChance;
            Statuses = new BattleStatusCollection();
            Phases = new LinkedList<Phase>();
        }

        public void AddPhase(Type phaseType)
        {
            var phase = (Phase)Activator.CreateInstance(phaseType, this);
            Phases.AddLast(phase);
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

            Animator = entity.getComponent<Sprite<Animations>>();
            Animator.play(IdleAnimation);
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

            if(IsActive)
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
            if(_currentPhase == null)
            {
                IsActive = true;
                _currentPhase = Phases.First;
                foreach(var phase in Phases)
                {
                    phase.IsFinished = false;
                }
            }
            _currentPhase.Value.Perform();
            if(_currentPhase.Value.IsFinished && _currentPhase.Next != null)
            {
                _currentPhase = _currentPhase.Next;
            }
            else if(_currentPhase.Value.IsFinished && _currentPhase.Next == null)
            {
                IsActive = false;
                _currentPhase = null;
            }
        }

        public void SelectTarget(BattleActor target)
        {
            SelectTargets(new List<BattleActor> { target });
        }

        public void SelectTargets(List<BattleActor> targets)
        {
            SelectedTargets = targets;
        }

        public int CompareTo(BattleActor other)
        {
            return -Speed.CompareTo(other.Speed);
        }
    }
}
