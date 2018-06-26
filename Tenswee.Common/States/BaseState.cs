using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Containers;

namespace Tenswee.Common.States
{
    public interface IState : IAspect
    {
        void Enter();
        bool CanTransition(IState other);
        void Exit();
    }

    public abstract class BaseState : Aspect, IState
    {
        public virtual void Enter() { }
        public virtual bool CanTransition(IState other) { return true; }
        public virtual void Exit() { }
    }
}
