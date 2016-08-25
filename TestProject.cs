using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace UnitTestBoilerplate
{
    public class TestProject
    {
        public string Name { get; set; }

        public Project Project { get; set; }

        public string ProjectDirectory => Path.GetDirectoryName(this.Project.FileName);
    }
}
