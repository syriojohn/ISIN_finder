namespace ISIN_finder.Models
{
    public class TradeInfo
    {
        public string ISIN { get; set; }
        public string ACID { get; set; }
        public string TradeId { get; set; }
        public string PositionLadder { get; set; }

        public TradeInfo()
        {
            ISIN = string.Empty;
            ACID = string.Empty;
            TradeId = string.Empty;
            PositionLadder = string.Empty;
        }
    }
}
