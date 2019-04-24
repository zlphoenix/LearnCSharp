using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHDemo.Entities
{
    public class Answer
    {
        public Answer()
        {
            this.ID = Guid.NewGuid();
        }
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }

        public virtual Question Question { get; set; }
    }
}
