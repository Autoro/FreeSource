using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FreeSource.Extensibility;

namespace FreeSource.VisualStudio
{
    public class Project : IProject
    {
        private readonly XElement reader;

        private string path;
        private List<SourceFile> sourceFiles;

        public string Name
        {
            get;
            private set;
        }

        public Project(string path)
        {
            this.path = path;
            this.reader = XElement.Load(path);
            this.Name = path.Split('\\').Last().Replace(".csproj", String.Empty);
            this.sourceFiles = new List<SourceFile>();
        }

        public IEnumerable<ISourceFile> GetSourceFiles()
        {
            if (this.sourceFiles.Count == 0)
            {
                var query = from item in this.reader.Descendants("Compile")
                            select item.Attribute("Include");

                foreach (var item in query)
                {
                    this.sourceFiles.Add(new SourceFile(this.path + "\\" + item.Value));
                }
            }

            return this.sourceFiles;
        }
    }
}
