using log4net;
using MVC5_Ref.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace MVC5_Ref.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        private static readonly ILog log = LogManager.GetLogger(typeof(Repository<TEntity>));


        public Repository(DbContext context)
        {
            Context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public TEntity Get(object id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public void Insert(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                log.Error(sb.ToString());
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex); //Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public void InsertAll(IEnumerable<TEntity> entityList)
        {
            foreach(TEntity entity in entityList)
            {
                Insert(entity);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public void DeleteByID(object id)
        {
            try
            {
                TEntity entityToDelete = Context.Set<TEntity>().Find(id); //should call Get instead of redoing it. remove the try catch
                Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public void DeleteAll(IEnumerable<TEntity> entityList)  //need try catch?
        {
            try
            {
                foreach(TEntity entity in entityList)
                {
                    Delete(entity);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }
    }
}