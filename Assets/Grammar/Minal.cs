﻿using System.Collections.Generic;

namespace Xtaieer.Grammar
{
    public class Minal
    {
        public static readonly Minal EMPTY = new Minal("ε");

        public string Name
        {
            get;
            private set;
        }

        public Minal(string name)
        {
            Name = name;
        }
    }

    public class Terminal : Minal
    {
        public Terminal(string name) : base(name) { }
    }

    public class Nonterminal : Minal
    {
        public Nonterminal(string name) : base(name) { }

        private List<Minal[]> productions = new List<Minal[]>();
        public void AddProduction(Minal[] minal)
        {
            productions.Add(minal);
        }

        public IEnumerable<Minal[]> ProductionIterator()
        {
            return productions;
        }
    }
}
