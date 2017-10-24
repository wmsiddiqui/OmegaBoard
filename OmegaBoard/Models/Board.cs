using System.Collections.Generic;

namespace OmegaBoard.Models
{
    public class Board
    {
        private List<Lane> _lanes;

        public List<Lane> Lanes
        {
            get { return _lanes; }
        }

        public void AddLane(Lane lane)
        {
            _lanes.Add(lane);
        }

        public void AddLane(Lane lane, int index)
        {
            _lanes.Insert(index, lane);
        }

        public void AddLane(string laneName)
        {
            var lane = new Lane(laneName);
            _lanes.Add(lane);
        }

        public void AddLane(string laneName, int index)
        {
            var lane = new Lane(laneName);
            _lanes.Insert(index, lane);
        }
    }
}
