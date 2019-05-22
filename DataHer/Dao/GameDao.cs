using DataHer.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHer.Dao
{
    public class GameDao : DaoBase<Game>
    {
        public GameDao() : base()
        {

        }

        public IList<Game> GetGamesPaged(int count, int page, out int totalGames)
        {
            totalGames = session.CreateCriteria<Game>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Game>()
                .AddOrder(Order.Asc("Name"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Game>();
        }

        public IList<Game> SearchGames(string phrase)
        {
            return session.CreateCriteria<Game>()
                .Add(Restrictions.Like("Name", string.Format("%{0}%", phrase)))
                .List<Game>();
        }

        public IList<Game> GetGamesInCategoryId(int id)
        {
            return session.CreateCriteria<Game>()
                .CreateAlias("Category", "cat")
                .Add(Restrictions.Eq("cat.Id", id))
                .List<Game>();
        }

    }
}
