// Copyright � SoftAlgoTrade, 2014 - 2017.
//
// Web: https://softalgotrade.com
// Support: https://support.softalgotrade.com
//
// Licensed under the http://www.apache.org/licenses/LICENSE-2.0
//
// Examples are distributed in the hope that they will be useful, but without any warranty.
// It is provided "AS IS" without warranty of any kind, either expressed or implied. 

using SAT.Indicators;
using SAT.Trading;

namespace Arbitrage
{
    public class AverageSpreadOnTicks : IIndicator<Trade>
    {
        private readonly SMA _stockSma;
        private readonly SMA _futuresSma;

        private decimal _futuresPrice;
        private decimal _stockPrice;

        public AverageSpreadOnTicks(int period)
        {
            _stockSma = new SMA(period);
            _futuresSma = new SMA(period);
        }

        protected override void OnReset()
        {
            _stockSma.Reset();
            _futuresSma.Reset();
        }

        protected override decimal OnAdd(Trade tick)
        {
            if (tick.Security.InitialMargin > 0)
                _futuresPrice = _futuresSma.Add(tick.Price);
            else
                _stockPrice = _stockSma.Add(tick.Price*tick.Security.LotSize);

            if (_stockSma.IsFormed && _futuresSma.IsFormed)
            {
                return _futuresPrice - _stockPrice;
            }

            return Default;
        }
    }
    public class AverageSpreadOnCandles : IIndicator<Candle>
    {
        private readonly SMA _stockSma;
        private readonly SMA _futuresSma;

        private decimal _futuresPrice;
        private decimal _stockPrice;

        public AverageSpreadOnCandles(int period)
        {
            _stockSma = new SMA(period);
            _futuresSma = new SMA(period);
        }

        protected override void OnReset()
        {
            _stockSma.Reset();
            _futuresSma.Reset();
        }

        protected override decimal OnAdd(Candle candle)
        {
            if (candle.Security.InitialMargin > 0)
                _futuresPrice = _futuresSma.Add(candle.ClosePrice);
            else
                _stockPrice = _stockSma.Add(candle.ClosePrice * candle.Security.LotSize);

            if (_stockSma.IsFormed && _futuresSma.IsFormed)
            {
                return _futuresPrice - _stockPrice;
            }

            return Default;
        }
    }
}