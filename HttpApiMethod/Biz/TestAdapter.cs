using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uitl;

namespace HttpApiMethod
{
    public class TestAdapter : IAdapter
    {
    

        public List<User> GetList(List<User> user)
        {
            List<User> list = new List<User>();
            list.Add(new User() { Phone = "1380013800", Name = "xialei1" });
            list.Add(new User() { Phone = "1380013800", Name = "xialei2" });
            return list;

        }

        public List<User> GetList()
        {
            List<User> list = new List<User>();
            list.Add(new User() { Phone = "1380013800", Name = "xialei1" });
            list.Add(new User() { Phone = "1380013800", Name = "xialei2" });

            return list;
        }

        public List<string> GetString()
        {
            List<string> result = new List<string>() { "1", "fdsafd", "fdsafdafdsa", "656565", "fdsafdsaqeqeqeq" };
            return result;
        }

        public List<User> GetUserList()
        {

            return new List<User>();

        }
    }
}
