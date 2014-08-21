using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Lambda
{
    [TestClass]
    public class CollectionTraversingTest
    {
        [TestMethod]
        public void ForEachTest()
        {
            var strList = new List<string>()
                {
                    "支队长","测试","网格化管理问题情况上报考评"
                    ,"副市长","广告媒介","站牌广告","测试城区"
                    ,"市领导","科员","拱门广告","指挥中心案件来源"
                    ,"主任","中队长","公路广告","材质二","队员上报问题"
                    ,"局直属科室","03262","大队长","占道经营流程","材质一"
                    ,"指挥中心流程","重大活动考评","执法局","fsda","上报信息、宣传报道考评"
                    ,"布幅广告"
                };
            var result = strList.Where(s => s.StartsWith("支"));
        }
    }
}
