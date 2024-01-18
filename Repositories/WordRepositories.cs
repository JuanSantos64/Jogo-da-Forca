using AppJogoForca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJogoForca.Repositories
{
    public class WordRepositories
    {
        private List<Word> _worlds;
        public WordRepositories()
        {
            _worlds = new List<Word>();
            _worlds.Add(new Word("Nome", "Maria".ToUpper()));
            _worlds.Add(new Word("Vegetal", "Cenoura".ToUpper()));
            _worlds.Add(new Word("Fruta", "Abacate".ToUpper()));
            _worlds.Add(new Word("Tempero", "Baiano".ToUpper()));
            _worlds.Add(new Word("Tempero", "Nordestino".ToUpper()));
        
        }
        public Word GetRandomWord()
        {
            Random rand = new Random();
            var number = rand.Next(0, _worlds.Count);
            return _worlds[number];
        }
    }
}
