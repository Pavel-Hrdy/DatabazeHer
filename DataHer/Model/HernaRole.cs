using DataHer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHer.Model
{
    public class HernaRole : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Identificator { get; set; }

        public virtual string RoleDescription { get; set; }
    }
}
