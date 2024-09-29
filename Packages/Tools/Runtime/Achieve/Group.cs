using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class Group<T> : MonoBehaviour {
        public static T currentSelect;
        public static event Action<T> OnSelect;
        public static void Select(T value) => OnSelect?.Invoke(value);

        public T value;
        protected bool isCancel;
        protected virtual void Awake() { Initialize(); }
        protected virtual void OnDestroy() { Release(); }

        public virtual void Initialize() { OnSelect += Group_OnSelect; }
        public virtual void Release() { OnSelect -= Group_OnSelect; }
        public virtual void Group_OnSelect(T target) {
            if (target.Equals(value)) {
                if (currentSelect.Equals(this) && isCancel) {
                    currentSelect = default;
                    OnSelect?.Invoke(default);
                }
                else { currentSelect = value; SelectState(); }
            }
            else { DefaultState(); }
        }
        public virtual void DefaultState() { }
        public virtual void SelectState() { }
        public virtual void TriggerSelect() { OnSelect?.Invoke(value); }
    }
}