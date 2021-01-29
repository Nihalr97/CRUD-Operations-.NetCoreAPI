using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Demo.Core.Contracts.IUnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
