﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Core.Engine;

public interface INameable
{
    string Name { get; set; }
}

public class Word<T> where T : INameable
{
    private readonly T[]? Characters;
    private readonly T? Value;

    public Word(T value, string name)
    {
        Value = value;
        Name = name;
    }

    public Word(IEnumerable<T> values, string name)
    {
        Characters = values.ToArray();
        Name = name;
    }

    public Word(T value)
    {
        Value = value;
        Name = value.Name;
    }

    public Word(IEnumerable<T> values)
    {
        Characters = values.ToArray();
        Name = string.Join("", values.Select(x => x.Name));
    }

    public IEnumerable<T> Objects => Characters ?? new[] { Value! };

    public string Name { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj is Word<T> word) return Name == word.Name;
        return base.Equals(obj);
    }

    public static implicit operator Word<T>(T s) => new(s);

    public override string ToString() => Name;

    public override int GetHashCode() => Name.GetHashCode();
}

public class NounAdjective<T> where T : INameable
{
    public Word<T> Value;
    public Word<T>? Modifier;

    public NounAdjective(Word<T> value)
    {
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is NounAdjective<T> na) 
            return Equals(Value, na.Value) && Equals(Modifier, na.Modifier);
        return base.Equals(obj);
    }

    public override string ToString() => $"{Modifier} {Value}".Trim();

    public override int GetHashCode() => ToString().GetHashCode();
}

public class NA_WithRelationship<T> : NounAdjective<T> where T : INameable
{
    public Word<T> Relation;
    public NounAdjective<T> RelatedTo;
    public Word<T>? RelationModifier;

    public NA_WithRelationship(Word<T> value, Word<T> relation, NounAdjective<T> relatedTo) : base(value)
    {
        Relation = relation;
        RelatedTo = relatedTo;
    }

    public override bool Equals(object? obj)
    {
        if (obj is NA_WithRelationship<T> na) 
            return Equals(Relation, na.Relation) && RelatedTo.Equals(na.RelatedTo) && Equals(RelationModifier, na.RelationModifier) && base.Equals(obj);
        return base.Equals(obj);
    }

    public override string ToString() => $"{base.ToString()} " + $"{RelationModifier} {Relation} {RelatedTo}".Trim();

    public override int GetHashCode() => ToString().GetHashCode();
}

public class Conjunction<T> where T : INameable
{
    public Word<T> Conj;
    public NounAdjective<T> Item;

    public Conjunction(Word<T> conj, NounAdjective<T> item)
    {
        Conj = conj;
        Item = item;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Conjunction<T> c) 
            return Conj?.Equals(c.Conj) == true && Item.Equals(c.Item);
        return base.Equals(obj);
    }

    public override string ToString() => $"{Conj} {Item}";

    public override int GetHashCode() => ToString().GetHashCode();
}

public class Clause<T> where T : INameable
{
    public NounAdjective<T> First;
    public List<Conjunction<T>> Items = new();

    public Clause(NounAdjective<T> first)
    {
        First = first;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Clause<T> c) 
            return c.First?.Equals(First) == true && Items.Zip(c.Items).All(x => x.First.Equals(x.Second));
        return base.Equals(obj);
    }

    public override string ToString() => ($"{First} " + string.Join(" ", Items)).Trim();

    public override int GetHashCode() => ToString().GetHashCode();
}

public class Sentence<T> where T : INameable
{
    public Clause<T> Object;
    public Word<T> Verb;
    public Clause<T> Subject;

    public Sentence(Clause<T> @object, Word<T> verb, Clause<T> subject)
    {
        Object = @object;
        Verb = verb;
        Subject = subject;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Sentence<T> s) 
            return s.Object.Equals(Object) && Equals(s.Verb, Verb) && s.Subject.Equals(Subject);
        return base.Equals(obj);
    }

    public override string ToString() => $"{Object} {Verb} {Subject}";

    public override int GetHashCode() => ToString().GetHashCode();
}




internal class VocabSet : HashSet<string?>
{
    public VocabSet(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static VocabSet operator &(VocabSet v, IEnumerable<string?> one)
    {
        var c = new VocabSet(v.Name);
        foreach (var item in v.Union(one))
        {
            c.Add(item);
        }
        return c;
    }
}

public class Vocabulary
{
    internal VocabSet Verbs = new("Verbs");
    internal VocabSet Adjectives = new("Adjectives");
    internal VocabSet Nouns = new("Nouns");
    internal VocabSet Modifiers = new("Modifiers");
    internal VocabSet Conjunctions = new("Conjunctions");
    internal VocabSet Relations = new("Relations");
    internal Dictionary<string, string> Characters = new();
    internal VocabSet Total = new("Total");

    public HashSet<string> verbs { set { Verbs &= value!; Total &= value!; } }
    public HashSet<string> adjectives { set { Adjectives &= value!; Total &= value!; } }
    public HashSet<string> nouns { set { Nouns &= value!; Total &= value!; } }
    public HashSet<string> modifiers { set { Modifiers &= value!; Total &= value!; } }
    public HashSet<string> conjunctions { set { Conjunctions &= value!; Total &= value!; } }
    public HashSet<string> relations { set { Relations &= value!; Total &= value!; } }
    public Dictionary<string, string> characters { set { Characters = value; Total &= Characters.Keys; } }
}

internal class ConsumeCharacters<T> where T : INameable
{
    private readonly T[] chain;
    private readonly Vocabulary vocabulary;
    private uint index = 0;

    public ConsumeCharacters(T[] chain, Vocabulary vocabulary)
    {
        this.chain = chain;
        this.vocabulary = vocabulary;
    }

    private IEnumerable<T> getNextWord()
    {
        if (index >= chain.Length) yield break;

        yield return chain[index++];
        while (index < chain.Length && vocabulary.Characters.ContainsKey(chain[index].Name))
        {
            yield return chain[index++];
        }
    }

    private Word<T>? next()
    {
        var word = getNextWord().ToArray();
        if (word.Length == 0) return null;
        if (word.Length == 1) return new(word[0]);
        return new(word, "text_" + string.Join("", word.Select(x => vocabulary.Characters[x.Name])));
    }

    public IEnumerable<Word<T>> ParseAll()
    {
        while (next() is Word<T> word) yield return word;
    }
}


public class ParseSentences
{

    private static IEnumerable<(int index, T item)> enumerate<T>(IEnumerable<T> collection)
    {
        foreach (var (index, item) in collection.Select((t, i) => (i, t)))
        {
            yield return (index, item);
        }
    }

    public static List<T[]> GetWordChains<T>(List<List<T?>> grid, HashSet<string?> words) where T : INameable
    {
        List<(int x, int y, string dir)> starts = new();
        foreach (var (x, col) in enumerate(grid))
        {
            foreach (var (y, word) in enumerate(col))
            {
                if (word == null || words.Contains(word.Name) == false) continue;

                if (x < grid.Count - 2 && words.Contains(grid[x + 1][y]?.Name))
                    starts.Add((x, y, "right"));

                if (y < grid[x].Count - 2 && words.Contains(grid[x][y + 1]?.Name))
                    starts.Add((x, y, "down"));
            }
        }

        HashSet<(int, int, string)> consumed = new();
        List<T[]> chains = new();

        foreach (var m0 in starts)
        {
            var (x, y, dir) = m0;
            var chain = new List<T>();

            var m = (x, y, dir);

            while (x < grid.Count && y < grid[x].Count && grid[x][y] is T t && words.Contains(t.Name))
            {
                if (consumed.Contains(m))
                    goto Done;

                consumed.Add(m);

                chain.Add(t);

                if (dir == "down")
                    y += 1;
                else if (dir == "right")
                    x += 1;

                m = (x, y, dir);
            }

            chains.Add(chain.ToArray());

            Done: { }
        }

        return chains;
    }

    public static List<Sentence<T>> GetSentences<T>(List<List<T?>> grid, Vocabulary vocabulary) where T : INameable
    {
        var chains = new Stack<T[]>(GetWordChains(grid, vocabulary.Total).ToList());
        var sentences = new List<Sentence<T>>();
        while (chains.Count > 0)
        {
            var chain = chains.Pop();
            var i = 0;
            var current = chain;
            while (current.Length >= 3)
            {
                var words = new ConsumeCharacters<T>(current, vocabulary).ParseAll().ToList();

                if (matchSentence(words, vocabulary) is Sentence<T> match)
                {
                    sentences.Add(match);
                    chains.Push(chain[(i + current.Length)..]);
                    break;
                }
                else if (current.Length > 3)
                {
                    current = current[..^1];
                }
                else
                {
                    i++;
                    current = chain[i..];
                }
            }
        }
        return sentences;
    }

    private static NounAdjective<T>? matchSpecifier<T>(Word<T>[] words, HashSet<string?> exclude, Vocabulary vocabulary) where T : INameable
    {
        var re = new List<char>();

        foreach (var word in words)
        {
            var name = word.Name;

            if (exclude.Contains(name)) return null;
            else if (vocabulary.Nouns.Contains(name)) re.Add('n');
            else if (vocabulary.Adjectives.Contains(name)) re.Add('n');
            else if (vocabulary.Modifiers.Contains(name)) re.Add('m');
            else if (vocabulary.Relations.Contains(name)) re.Add('r');
            else return null;
        }
        var str = string.Join("", re);

        return str switch
        {
            "n" => new NounAdjective<T>(words[0]),
            "mn" => new NounAdjective<T>(words[1]) { Modifier = words[0] },
            "nrn" => new NA_WithRelationship<T>(words[0], words[1], new(words[2])),
            "mnrn" => new NA_WithRelationship<T>(words[1], words[2], new(words[3])) { Modifier = words[0] },
            "nrmn" => new NA_WithRelationship<T>(words[0], words[1], new(words[3]) { Modifier = words[2] }),
            "mnrmn" => new NA_WithRelationship<T>(words[1], words[2], new(words[4]) { Modifier = words[3] }) { Modifier = words[0] },
            "nmrn" => new NA_WithRelationship<T>(words[0], words[2], new(words[3])) { RelationModifier = words[1] },
            "mnmrn" => new NA_WithRelationship<T>(words[1], words[2], new(words[3])) { Modifier = words[0], RelationModifier = words[2] },
            "nmrmn" => new NA_WithRelationship<T>(words[0], words[1], new(words[4]) { Modifier = words[3] }) { RelationModifier = words[2] },
            "mnmrmn" => new NA_WithRelationship<T>(words[1], words[3], new(words[5]) { Modifier = words[4] }) { Modifier = words[0], RelationModifier = words[2] },
            _ => null,
        };
    }

    private static Clause<T>? matchClause<T>(Word<T>[] words, HashSet<string?> exclude, Vocabulary vocabulary) where T : INameable
    {
        Clause<T>? clause = null;
        int lastI = 0;
        Word<T>? lastConjunction = default;

        void addSpec(NounAdjective<T> spec, Word<T>? conj)
        {
            if (clause == null)
            {
                clause = new(spec);
            }
            else
            {
                clause.Items.Add(new(lastConjunction!, spec));
            }
            lastConjunction = conj;
        }

        foreach (var (index, word) in words.Select((t, i) => (i, t)))
        {
            if (!vocabulary.Conjunctions.Contains(word.Name)) continue;

            var spec = matchSpecifier(words[lastI..index], exclude, vocabulary);
            if (spec == null) return null;
            addSpec(spec, word);
            lastI = index + 1;
        }

        var last = words[lastI..];
        if (last.Length == 0) 
            return null;
        else
        {
            var spec = matchSpecifier(last, exclude, vocabulary);
            if (spec == null) return null;
            addSpec(spec, default);
        }

        if (clause?.First == null) return null;

        return clause;
    }

    private static Sentence<T>? matchSentence<T>(List<Word<T>> alist, Vocabulary vocabulary) where T : INameable
    {
        if (alist.FindAll(x => vocabulary.Verbs.Contains(x.Name)).Count > 1) return null;

        var verbIndex = alist.FindIndex(0, x => vocabulary.Verbs.Contains(x.Name));
        if (verbIndex == -1) return null;

        var chain = alist.ToArray();
        var first = chain[..verbIndex];
        var second = chain[(verbIndex + 1)..];
        if (matchClause(first, new(), vocabulary) is Clause<T> m1 && matchClause(second, vocabulary.Relations, vocabulary) is Clause<T> m2)
        {
            return new(m1, chain[verbIndex], m2);
        }
        return null;
    }
}
