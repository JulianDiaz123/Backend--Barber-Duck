using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaApi.Config;
using PeluqueriaApi.Models.Rol;
using PeluqueriaApi.Models.User;
using PeluqueriaApi.Models.User.Dto;
using PeluqueriaApi.Repositories;
using PeluqueriaApi.Utils.Exceptions;
using System.Net;

namespace PeluqueriaApi.Services
{
    public class UserServices
    {
        private readonly IMapper _mapper;
        private readonly IEncoderServices _encoderServices;
        private readonly IUserRepository _userRepository;

        public UserServices(IMapper mapper, IEncoderServices encoderServices, IUserRepository userRepository)
        {
            _mapper = mapper;
            _encoderServices = encoderServices; 
            _userRepository = userRepository;
        }

        private async Task<User> GetOneByIdOrException(int id)
        {
            var user = await _userRepository.GetOne(u => u.id == id);
            if (user == null)
            {
                throw new Exception($"No se encontro el usuario con Id = {id}");
            }
            return user;
        }

        public async Task<List<UsersDTO>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UsersDTO>>(users);
        }

        public async Task<UserDTO> GetOneById(int id)
        {
            var user = await GetOneByIdOrException(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<User> CreateOne(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);
            user.password = _encoderServices.Encode(user.password);
            await _userRepository.Add(user);
            return user;
        }

        public async Task<User> UpdateOneById(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await GetOneByIdOrException(id);
            var userMapped = _mapper.Map(updateUserDTO, user);
            await _userRepository.Update(userMapped);
            return userMapped;
        }

        public async Task DeleteOneById(int id)
        {
            var user = await GetOneByIdOrException(id);
            await _userRepository.Delete(user);
        }

        public async Task<User> GetOneByUsernameOrEmail(string? username, string? email)
        {
            User user;

            if (email == null && username == null)
            {
                throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
            }

            if (email != null)
            {
                user = await _userRepository.GetOne(u => u.email == email);
            }
            else if (username != null)
            {
                user = await _userRepository.GetOne(u => u.userName == username);
            }
            else
            {
                throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
            }
            return user;
        }

        public async Task<User> UpdateRolesById (int id, List<Rol> roles)
        {
            var user = await GetOneByIdOrException(id);

            user.Roles = roles;

            return await _userRepository.Update(user);
        }

        public async Task<List<Rol>> GetRolById(int id)
        {
            var user = await GetOneByIdOrException(id);

            return user.Roles.ToList();
        }
    }
}
