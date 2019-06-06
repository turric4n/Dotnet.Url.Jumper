using AutoMapper;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Repositories;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public Admin Authenticate(string username, string password)
        {
            var user = _mapper.Map<Admin>(_adminRepository.FindByUsername(username));
            if (user == null) { throw new InvalidAdminPasswordException("Invalid auth"); }            
            if (user.Password != password) { throw new InvalidAdminPasswordException("Invalid auth"); }
            if (user.Disabled) { throw new InvalidAdminDisabledException("User disabled"); }
            return user;
        }

        public Admin GetByUsername(string username)
        {
            var admin = _mapper.Map<Admin>(_adminRepository.FindByUsername(username));
            return admin;
        }
        public Admin GetById(int id)
        {
            var admin = _mapper.Map<Admin>(_adminRepository.FindById(id));
            return admin;
        }
    }
}
