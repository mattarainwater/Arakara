using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Components
{
    public class Selector : Component, IUpdatable
    {
        public List<Entity> SelectableEntities { get; private set; }
        public Entity FocusedEntity { get; private set; }

        private Action<Entity> _onSelect;
        private Action<Entity> _onFocus;
        private Action<Entity> _onBlur;
        private int _hoveredIndex;

        private VirtualButton _selectInput;
        private VirtualButton _leftInput;
        private VirtualButton _rightInput;

        public Selector(VirtualButton selectInput,
            VirtualButton leftInput,
            VirtualButton rightInput,
            Action<Entity> onSelect = null, 
            Action<Entity> onBlur = null, 
            Action<Entity> onFocus = null)
        {
            SelectableEntities = new List<Entity>();
            _hoveredIndex = 0;
            _onSelect = onSelect;
            _onBlur = onBlur;
            _onFocus = onFocus;
            _selectInput = selectInput;
            _leftInput = leftInput;
            _rightInput = rightInput;
        }

        public void update()
        {
            if (_rightInput.isPressed)
            {
                MoveNext();
            }
            else if (_leftInput.isPressed)
            {
                MoveBack();
            }
            else if (_selectInput.isPressed)
            {
                SelectHoveredEntity();
            }
        }

        public void AddEntity(Entity entity)
        {
            if(SelectableEntities.Count() == 0)
            {
                FocusedEntity = entity;
                _onFocus?.Invoke(FocusedEntity);
            }
            SelectableEntities.Add(entity);
        }

        public void Reset()
        {
            SelectableEntities.ForEach(x => _onBlur?.Invoke(x));
            SelectableEntities.Clear();
            _hoveredIndex = 0;
        }

        public void MoveNext()
        {
            if(SelectableEntities.Count() <= 1)
            {
                return;
            }
            _onBlur?.Invoke(FocusedEntity);
            if (_hoveredIndex == SelectableEntities.Count() - 1)
            {
                _hoveredIndex = 0;
            }
            else
            {
                _hoveredIndex++;
            }
            FocusedEntity = SelectableEntities[_hoveredIndex];
            _onFocus?.Invoke(FocusedEntity);
        }

        public void MoveBack()
        {
            if (SelectableEntities.Count() <= 1)
            {
                return;
            }
            _onBlur?.Invoke(FocusedEntity);
            if (_hoveredIndex == 0)
            {
                _hoveredIndex = SelectableEntities.Count() - 1;
            }
            else
            {
                _hoveredIndex--;
            }
            FocusedEntity = SelectableEntities[_hoveredIndex];
            _onFocus?.Invoke(FocusedEntity);
        }

        public void SelectHoveredEntity()
        {
            if (FocusedEntity == null)
            {
                return;
            }
            _onSelect?.Invoke(FocusedEntity);
        }
    }
}
