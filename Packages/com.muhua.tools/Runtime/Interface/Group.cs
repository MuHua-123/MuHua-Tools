using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class Group<T> : MonoBehaviour where T : Group<T> {
        public static Action<T> OnSelect;
        public static T currentSelect;
        protected bool isCancel;
        protected virtual void Awake() { Initialization(); }
        protected virtual void OnDestroy() { Release(); }

        public virtual void Initialization() { OnSelect += Group_OnSelect; }
        public virtual void Release() { OnSelect -= Group_OnSelect; }
        public virtual void Group_OnSelect(T target) {
            if (target == this) {
                if (currentSelect == this && isCancel) {
                    currentSelect = null;
                    OnSelect?.Invoke(null);
                }
                else { currentSelect = (T)this; SelectState(); }
            }
            else { DefaultState(); }
        }
        public virtual void DefaultState() { }
        public virtual void SelectState() { }
        public virtual void TriggerSelect() { OnSelect?.Invoke((T)this); }
    }
}