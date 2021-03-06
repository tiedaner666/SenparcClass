using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;

namespace SenparcClass.Controllers
{
    public class WeixinController : Controller
    {
        public static readonly string Token = WebConfigurationManager.AppSettings["WeixinToken"];
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                               "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }  
        
            
        
    }
}