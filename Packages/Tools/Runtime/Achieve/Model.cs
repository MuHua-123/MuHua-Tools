using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    [Obsolete("使用Module替换")]
    public class Model<ModuleCore> where ModuleCore : new() {
        public static ModuleCore I => Instantiate();

        private static ModuleCore core;
        private static ModuleCore Instantiate() => core == null ? core = new ModuleCore() : core;
    }
}