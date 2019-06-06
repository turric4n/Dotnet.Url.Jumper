using System;
using System.Collections.Generic;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.Mocked
{
    public class MockerAdminRepository : IAdminRepository
    {
        private readonly IMapper _mapper;

        public MockerAdminRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Add(Admin entity)
        {
            throw new NotImplementedException();
        }

        public Admin FindByCreationDate(DateTime creationDate)
        {
            throw new NotImplementedException();
        }

        public Admin FindById(int id)
        {
            if (id == 945)
            {
                return null;
            }
            else return null;
        }

        public Admin FindByModificationDate(DateTime modificationDate)
        {
            throw new NotImplementedException();
        }

        public Admin FindByUsername(string userName)
        {
            return null;
        }

        public IEnumerable<Admin> List()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Admin entity)
        {
            throw new NotImplementedException();
        }

        Admin IReadRepository<Admin, int>.FindByModificationDate(DateTime modificationDate)
        {
            throw new NotImplementedException();
        }
    }
}
