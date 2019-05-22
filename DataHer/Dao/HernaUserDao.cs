using DataHer.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHer.Dao
{
    public class HernaUserDao : DaoBase<HernaUser>
    {
        public HernaUser GetByLoginAndPassword(string login, string password)
        {
          return  session.CreateCriteria<HernaUser>()
            .Add(Restrictions.Eq("Login", login))
            .Add(Restrictions.Eq("Password", password))
            .UniqueResult<HernaUser>();


        }
        public HernaUser GetByLogin(string login)
        {
            return session.CreateCriteria<HernaUser>()
              .Add(Restrictions.Eq("Login", login))
              .UniqueResult<HernaUser>();


        }
    }
}
