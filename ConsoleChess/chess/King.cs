using board;

namespace chess
{
    class King : Piece
    {
        public King(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
