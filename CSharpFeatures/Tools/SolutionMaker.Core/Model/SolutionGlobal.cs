using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolutionMaker.Core.Model
{
    public class SolutionGlobal : ICloneable
    {
        public class Section : ICloneable
        {
            public Section()
            {
                this.SectionItems = new List<string>();
            }

            public string SectionName { get; set; }
            public string PrePostSolution { get; set; }
            public IList<string> SectionItems { get; private set; }

            public override string ToString()
            {
                return string.Format("{0}", this.SectionName);
            }

            #region ICloneable Members

            public object Clone()
            {
                var section = new Section
                {
                    SectionName = this.SectionName, 
                    PrePostSolution = this.PrePostSolution
                };
                foreach (var item in this.SectionItems)
                {
                    section.SectionItems.Add(item);
                }
                return section;
            }

            #endregion
        }

        public SolutionGlobal()
        {
            this.Sections = new List<Section>();
        }

        public IList<Section> Sections { get; private set; }

        #region ICloneable Members

        public object Clone()
        {
            var global = new SolutionGlobal();
            foreach (var section in this.Sections)
            {
                global.Sections.Add(section.Clone() as SolutionGlobal.Section);
            }
            return global;
        }

        #endregion
    }
}
