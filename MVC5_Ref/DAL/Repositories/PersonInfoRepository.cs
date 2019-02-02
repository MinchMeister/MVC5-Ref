using log4net;
using MVC5_Ref.DAL.Models;
using MVC5_Ref.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC5_Ref.DAL.Repositories
{
    public class PersonInfoRepository : Repository<PersonInfo>, IPersonInfoRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PersonInfoRepository));

        #region Init
        public PersonInfoRepository(SiteContext context) : base(context) { }

        public SiteContext SiteContext => Context as SiteContext;
        #endregion
    }
}
