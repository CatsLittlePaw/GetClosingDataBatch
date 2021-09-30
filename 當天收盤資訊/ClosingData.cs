using System;
using System.Collections.Generic;
using System.Text;

namespace DataTemplate
{
    public class ClosingData
    {
        public string STOCK_CODE { get; set; }
        public string TS { get; set; }
        public string VOLUME { get; set; }
        public string CLOSE_PRICE { get; set; }
        public string BID_PRICE { get; set; }
        public string BID_VOLUME { get; set; }
        public string ASK_PRICE { get; set; }
        public string ASK_VOLUME { get; set; }

        public void SetProperties(string code, string ts, string volume, string close, string bid_price, string bid_volume, string ask_price, string ask_volume)
        {
            this.STOCK_CODE = code;
            this.TS = ts;
            this.VOLUME = volume;
            this.CLOSE_PRICE = close;
            this.BID_PRICE = bid_price;
            this.BID_VOLUME = bid_volume;
            this.ASK_PRICE = ask_price;
            this.ASK_VOLUME = ask_volume;
        }
    }
}
