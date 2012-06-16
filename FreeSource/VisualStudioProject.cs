using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FreeSource
{
    internal sealed class VisualStudioProject
    {
        private readonly XmlReader reader;

        private List<string> files;

        public VisualStudioSolution Parent
        {
            get;
            private set;
        }

        public string RelativePath
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Guid Identifier
        {
            get;
            private set;
        }

        public VisualStudioProject(VisualStudioSolution parent, string relativePath)
        {
            this.Parent = parent;
            this.RelativePath = relativePath;

            this.Parse();
        }

        private void Parse()
        {
            XElement project = XElement.Load(this.Parent.Path + this.RelativePath);
        }
    }
}
