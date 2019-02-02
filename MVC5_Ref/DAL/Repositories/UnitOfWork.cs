using log4net;
using MVC5_Ref.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace MVC5_Ref.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UnitOfWork));

        private readonly SiteContext _context;

        public UnitOfWork(SiteContext context)
        {
            _context = context;
            PersonInfoRepository = new PersonInfoRepository(_context);
        }

        public IPersonInfoRepository PersonInfoRepository { get; private set; }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                log.Error(exceptionMessage, ex);
                //Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        //private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null) _context.Dispose();
            }
        }
    }
}
