using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
