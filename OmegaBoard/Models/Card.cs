using System;
using System.Collections.Generic;
using System.Linq;

namespace OmegaBoard.Models
{
    public class Card
    {
        private string _cardText;
        private Dictionary<string, string> _links;
        private List<Comment> _comments;
        private DateTime _created;
        private DateTime _edited;
        private bool _flagged;

        public string CardText
        {
            get { return _cardText; }
            set
            {
                _cardText = value;
                _edited = DateTime.Now;
            }
        }
        public List<Comment> Comments
        {
            get { return _comments; }
        }
        public IEnumerable<KeyValuePair<string, string>> Links
        {
            get
            {
                return _links.AsEnumerable();
            }
        }
        public DateTime Created
        {
            get { return _created; }
        }
        public DateTime Edited
        {
            get { return _edited; }
        }
        public bool Flagged
        {
            get { return _flagged; }
        }

        public Card(string cardText = null)
        {
            _links = new Dictionary<string, string>();
            _comments = new List<Comment>();
            _cardText = cardText;
            _created = DateTime.Now;
        }

        public Comment AddComment(string commentText)
        {
            var comment = new Comment(commentText);
            _comments.Add(comment);
            return comment;
        }

        public void AddLink(string href, string text)
        {
            if(!_links.ContainsKey(text))
            {
                _links.Add(text, href);
            }
            else
            {
                _links[text] = href;
            }
        }
    }
}
