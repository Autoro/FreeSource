using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FreeSource.Extensibility;

namespace FreeSource.VisualStudio
{
    public class Solution : ISolution
    {
        private readonly StreamReader reader;

        private List<IProject> projects;

        public string Name
        {
            get;
            private set;
        }

        public Solution(string path)
        {
            this.reader = new StreamReader(path);
            this.Name = path.Split('\\').Last().Replace(".sln", String.Empty);
            this.projects = new List<IProject>();
        }

        public IEnumerable<IProject> GetProjects()
        {
            if (this.projects.Count == 0)
            {
                string line;

                while (!this.reader.EndOfStream)
                {
                    line = this.reader.ReadLine();

                    if (line.Contains("Project"))
                    {
                        string projectPath = line.Split(',')[1].Replace("\"", String.Empty);
                        this.projects.Add(new Project(projectPath));
                    }
                }
            }

            return this.projects;
        }
    }
}
