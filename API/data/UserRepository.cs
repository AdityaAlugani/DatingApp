using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.data
{
    public class UserRepository : IUserRepository
    {
        private IMapper _mapper;
        DataContext _datacontext;
        public UserRepository(DataContext datacontext,IMapper mapper)
        {
            _mapper=mapper;
            _datacontext=datacontext;
        }
        public async Task<specialuserDto?> GetMemberByUsernameAsync(string username)
        {
            // return await _datacontext.Users.Include(user=>user.Photos).SingleOrDefaultAsync((user)=>user.UserName==username);
            return await  _datacontext.Users.Include(user=>user.Photos).Where(user=>user.UserName==username).ProjectTo<specialuserDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            
        }
        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            return await _datacontext.Users.Include(user=>user.Photos).SingleOrDefaultAsync((user)=>user.UserName==username);
            
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _datacontext.Users.Include(user=>user.Photos).ToListAsync();
        }
        public async Task<IEnumerable<specialuserDto>> GetMembersAsync()
        {
            return await  _datacontext.Users.Include(user=>user.Photos).ProjectTo<specialuserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser> GetUsersByIdAsync(int id)
        {
            return await _datacontext.Users.FindAsync(id);   
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _datacontext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _datacontext.Entry(user).State=EntityState.Modified;
        }
    }
}