using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace NHDemo.Entities
{
    public class Question
    {
        public Question()
        {
            this.QuestionID = Guid.NewGuid();
        }
        public virtual Guid QuestionID { get; set; }

        public virtual string Name { get; set; }

        private ISet<Answer> _mAnswers;
        public virtual ISet<Answer> Answers
        {
            get { return this._mAnswers ?? (this._mAnswers = new HashedSet<Answer>()); }
            protected internal set
            {
                this._mAnswers = value;
            }
        }
    }
}
