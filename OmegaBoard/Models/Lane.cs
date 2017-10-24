using System;
using System.Collections.Generic;

namespace OmegaBoard.Models
{
    public class Lane
    {
        private List<Card> _cards;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public List<Card> Cards
        {
            get { return _cards; }
        }

        public Card AddCard(string cardText)
        {
            var card = new Card(cardText);
            _cards.Add(card);
            return card;
        }

        public void AddCard(Card card, int? index = null)
        {
            if (index == null)
            {
                _cards.Add(card);
            }
            else
            {
                _cards.Insert(index.Value, card);
            }
        }

        public void MoveCard(Card card, int index)
        {            
            if(!_cards.Remove(card))
            {
                throw new Exception("unable to move card within lane");
            }
            _cards.Insert(index, card);
        }

        public void RemoveCard(Card card)
        {
            if(!_cards.Remove(card))
            {
                throw new Exception("Unable to remove card from the lane");
            }
        }

        public Lane(string name)
        {
            _name = name;
            _cards = new List<Card>();
        }
    }
}
