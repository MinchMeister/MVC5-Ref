using System;

namespace MVC5_Ref.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonInfoRepository PersonInfoRepository { get; }

        void Save();
    }
}
