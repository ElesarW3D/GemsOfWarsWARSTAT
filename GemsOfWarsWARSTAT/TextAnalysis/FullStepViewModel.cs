using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GemsOfWarsWARSTAT.TextAnalysis
{
    public abstract class FullStepViewModel<T> : ViewModelBase, IFullIEnumerator<T>  where T : IDisposable
    {
        private List<T> _items;
        private int _index = 0;

        public T Current => _items[_index];

        object IEnumerator.Current => _items[_index];

        public void Dispose()
        {
            _items.ForEach(x => x.Dispose());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            //if (Current is FullStepViewModel<U> fullStep)
            //{

            //}
            if (_index < _items.Count)
            {
                _index++;
                RaisePropertyChanged(nameof(Current));
                return true;
            }
            return false;
        }
        public bool MovePrevious()
        {
            if (_index > 0)
            {
                _index--;
                RaisePropertyChanged(nameof(Current));
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _index = 0;
            RaisePropertyChanged(nameof(Current));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void InitItems(List<T> items)
        {
            this._items = items;
        }
    }
}
