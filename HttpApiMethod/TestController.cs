using HttpMvc;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// 备注:要求 自定义对象或者集合放参数最后面
/// </summary>
namespace HttpApiMethod
{

    public class TestController : BaseController
    {
   

        public ActionResult Index()
        {
            // return View("Index.html");
            return View();

        }

        public ActionResult GetListJson()
        {
            List<User> list = new List<User>();
         
            IAdapter adapter = new TestAdapter();
            var result = adapter.GetList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region 重载
        public ActionResult GetListText(List<User> users)
        {
            string msg = string.Empty;
            string text = string.Empty;
            if (users == null)
            {
                //验证
                //foreach (var user in users)
                //{
                //    foreach (var item in user.GetRuleViolations())
                //    {
                //        if (string.IsNullOrEmpty(item.ToString()))
                //        {
                //            msg = item.ToString();
                //            goto lbl;
                //        }
                //    }
                //}
                text = JsonUitl.ToJson(new  { Code = 500, Msg = msg });
            }
            else
            {
                users = new List<User>()
                   {
                    new User(){ Phone="13800138000",Name="xialei" },
                    new User(){ Phone="13800138000",Name="zhangsanfeng" },
                   };
                text = JsonUitl.ToJson(users);
            }
            // lbl:
            return Content(text, "text/plain", System.Text.Encoding.Default);
        }

        //参数对象放最后面
        public ActionResult GetListText1(int parma1, int parma2, List<User> users)
        {
            if (users == null)
            {
                users = new List<User>();
            }
            users = new List<User>()
                   {
                    new User(){ Phone="13800138000",Name="xialei" },
                    new User(){ Phone="13800138000",Name="zhangsanfeng" },
                   };

            return Json(users, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetHasEnum(int parma1, int parma2, List<User> users)
        {
            string text = "fdsafdsafdsafdsafdsa";
            return Content(text, "text/plain", System.Text.Encoding.Default);



        }

        #endregion


        public ActionResult GetZip()
        {

            string imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "敏科手机点菜系统2016.zip");
            return DownFile(imageFilePath);

        }

        public ActionResult GetImage1()
        {
            string imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "王宝强.jpg");

            return DownFile(imageFilePath);

        }



        

        public List<string> GetString()
        {
            throw new NotImplementedException();
        }

       
    }
}
