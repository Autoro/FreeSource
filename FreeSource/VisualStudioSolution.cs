﻿using System;
using System.Collections.Generic;
using System.IO;

namespace FreeSource
{
    internal sealed class VisualStudioSolution
    {
        private readonly TextReader reader;

        public string Path
        {
            get;
            private set;
        }

        public string Version
        {
            get;
            private set;
        }

        public Guid Identifier
        {
            get;
            private set;
        }

        public VisualStudioSolution(string solutionPath)
        {
            this.reader = new StreamReader(solutionPath);

            this.Path = solutionPath;
            this.Version = "Unknown Version";

            this.Parse();
        }

        private void Parse()
        {
            string line;
            while ((line = this.reader.ReadLine()) != null)
            {
                if (this.ParseLineComment(line))
                {
                    continue;
                }

                if (this.ParseVersion(line))
                {
                    continue;
                }

                if (this.ParseProjectDeclaration(line))
                {
                    continue;
                }
            }
        }
        
        private bool ParseLineComment(string line)
        {
            return line.StartsWith("#");
        }

        private bool ParseVersion(string line)
        {
            if (line.StartsWith("Microsoft Visual Studio Solution File"))
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 8)
                {
                    this.Version = parts[7];
                    return true;
                }
            }

            return false;
        }

        private bool ParseProjectDeclaration(string line)
        {
            if (line.StartsWith("Project"))
            {
                string[] assignment = line.Split('=');
                if (assignment.Length == 2)
                {
                    int identifierStartIndex = assignment[0].IndexOf('{') + 1;
                    int identifierEndIndex = assignment[0].LastIndexOf('}');
                    int identifierLength = identifierEndIndex - identifierStartIndex;
                    string identifier = assignment[0].Substring(identifierStartIndex, identifierLength);

                    if (!this.ParseIdentifier(identifier))
                    {
                        throw new FormatException("The project declaration is not of a supported format.");
                    }
                }
            }

            return false;
        }

        private bool ParseIdentifier(string line)
        {
            Guid identifier;
            if (Guid.TryParse(line, out identifier))
            {
                this.Identifier = identifier;
                return true;
            }

            return false;
        }

        public IEnumerable<VisualStudioProject> GetProjects()
        {
            throw new NotImplementedException();
        }
    }
}
