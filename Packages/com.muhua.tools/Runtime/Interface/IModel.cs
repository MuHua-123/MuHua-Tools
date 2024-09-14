using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class IModel<Modle> where Modle : new() {
        private static Modle modle;

        public static Modle I {
            get { if (modle == null) { modle = new Modle(); } return modle; }
        }
    }
}