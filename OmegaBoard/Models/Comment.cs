using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaBoard.Models
{
    public class Comment
    {
        private string _text;
        private DateTime _created;
        private DateTime _edited;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _edited = DateTime.Now;
            }
        }

        public Comment(string text)
        {
            _text = text;
            _created = DateTime.Now;
        }
    }
}
