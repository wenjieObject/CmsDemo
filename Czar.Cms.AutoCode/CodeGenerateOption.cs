﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.Cms.AutoCode
{
    public class CodeGenerateOption
    {
        public string OutputPath { get; set; }
        public string ModelsNamespace { get; set; }
        public string ControllersNamespace { get; set; }
        public string IRepositoriesNamespace { get; set; }
        public string RepositoriesNamespace { get; set; }
    }
}
