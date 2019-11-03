using Arakara.Battle;
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
        public bool Targetable { get; set; }
        public BattleController Controller { get; set; }
        public float DodgeChance { get; set; }
        public float CriticalHitChance { get; set; }
        public float Speed { get; set; }
        public BattleStatusCollection Statuses { get; set; }
        public SpriteAnimator Animator { get; set; }
        public BattleAction CurrentAction { get; set; }
        public List<BattleActor> SelectedTargets { get; set; }


        public BattleActor(int size = 1)
        {
            Statuses = new BattleStatusCollection();
        }

        public BattleActor(string name, int maxHp, Faction faction, float dodgeChance, float critChance, float speed)
        {
            Name = name;
            MaxHP = maxHp;
            CurrentHP = maxHp;
            Speed = speed;
            Faction = faction;
            IsActive = false;
            DodgeChance = dodgeChance;
            CriticalHitChance = critChance;
            Statuses = new BattleStatusCollection();
        }

        public override void OnAddedToEntity()
        {
            var nameText = Entity.AddComponent(new TextComponent(CommonResources.DefaultBitmapFont, Name, new Vector2(0f, -5), Color.Gray));
            nameText.RenderLayer = 70;

            Animator = Entity.GetComponent<SpriteAnimator>();
            Animator.Play(Animations.Idle);
        }

        public void Update()
        {
        }

        public void ProcessTurn()
        {
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
