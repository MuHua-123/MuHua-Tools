using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
    public interface ITemplateUI<T> {
        public void SetValue(T value, UIQuery query);
    }
}