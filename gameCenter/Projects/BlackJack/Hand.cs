using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.BlackJack
{
    class Hand
    {
        public List<Card> cards=new List<Card>();
        private int _total;
        public Hand(Card card)
        {
            cards.Add(card);
        }
        public Hand(Card card1, Card card2)
        {
            cards.Add(card1);
            cards.Add(card2);
        }
        public int HandTotal()
        {
            _total = 0;
            int aceTotal = 0;
            foreach(Card card in cards)
            {
                if(card.Name=="Ace")
                    aceTotal++;
                _total += card.Value;
            }
            for (int i = 0; i < aceTotal; i++)
            {
                if (_total > 21)
                {
                    _total -= 10;
                }
            }
            return _total;

        }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void Clear()
        {
            cards.Clear();
        }
    }
}
