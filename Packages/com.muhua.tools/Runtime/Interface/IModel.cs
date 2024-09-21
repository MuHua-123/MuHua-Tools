using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class IModel<Modle> where Modle : new() {
        public static Modle I => Instantiate();

        private static Modle model;
        private static Modle Instantiate() {
            return model == null ? model = new Modle() : model;
        }
    }
}