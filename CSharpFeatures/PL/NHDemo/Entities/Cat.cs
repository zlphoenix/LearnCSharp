using System;
using Iesi.Collections;
using Iesi.Collections.Generic;

namespace NHDemo.Entities
{
    public class Cat
    {
        public Cat()
        {
        }

        private long id;
        public virtual long Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public virtual string Name { get; set; }

        public virtual char Sex { get; set; }

        public virtual float Weight { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual Cat Mate { get; set; }
        private ISet<Cat> _kittens;
        public virtual ISet<Cat> Kittens
        {
            get { return this._kittens ?? (this._kittens = new HashedSet<Cat>()); }
            set
            {
                this._kittens = value;
            }
        }
        public virtual string Color { get; set; }
        //public int GetNewId()
        //{
        //    return 1;
        //}
        //private Color color;
        ////private char sex;
        ////private float weight;

    }
}
