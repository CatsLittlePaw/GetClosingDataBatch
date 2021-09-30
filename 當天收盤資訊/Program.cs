using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using SQLExtend;
using DataTemplate;
using log4net;
using Sinopac.Shioaji;


// 這裡的Config，因為不能透過ConfigManager抓，使用相對路徑
[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"當天收盤資訊.dll.config", Watch = true)]
namespace ShioajiConsole
{

    class Program
    {
        public static ILog log = LogManager.GetLogger("logger");

        static void Main(string[] args)
        {
            log.Debug("排程「當天收盤資訊」 開始執行");

            
            MyApi ma = new MyApi();
            ma.Login();
            log.Debug("Shioaji 登入成功");
            ma.getTodayClosingData();
            
            log.Debug("排程「當天收盤資訊」 執行完畢");
        }
    }

    public class MyApi
    {
        private static Shioaji _api = new Shioaji();
        private static SJList _accounts;
        public void Login()
        {
            string personal_id = ConfigurationManager.AppSettings["account"].ToString();
            string pwd = ConfigurationManager.AppSettings["password"].ToString();
            _accounts = _api.Login(personal_id, pwd);
        }
        public void getTodayClosingData(string type = "TSE")
        {
            string StockCodes = ConfigurationManager.AppSettings["StockCodes"].ToString();
            string[] stockCodeList = StockCodes.Split(",");
            
            for(var i = 0; i < stockCodeList.Length; ++i)
            {
                var contract = _api.Contracts.Stocks[type][stockCodeList[i]];                
                Ticks ticks = _api.Ticks(contract, DateTime.Now.ToString("yyyy-MM-dd"));
                
                
                ClosingData data = new ClosingData();
                data.SetProperties(stockCodeList[i] , ticks.ts[0].ToString(), ticks.volume[0].ToString(), ticks.close[0].ToString(), ticks.bid_price[0].ToString(), ticks.bid_volume[0].ToString(), ticks.ask_price[0].ToString(), ticks.ask_volume[0].ToString());
                ADODB.Insert<ClosingData>(data);                
            }
        }
    }
}

