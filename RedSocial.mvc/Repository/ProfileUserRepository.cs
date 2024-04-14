﻿using Microsoft.EntityFrameworkCore;
using RedSocial.mvc.Data;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Models;

namespace RedSocial.mvc.Repository
{
    public class ProfileUserRepository : BaseRepository, IProfileUserRepository
    {
        public ProfileUserRepository(RedSocialContext context) : base(context)
        {
        }

        public async Task<bool> Add(ProfileUser entity)
        {
           await _context.ProfileUser.AddAsync(entity);
           return await Save();
        }

        public Task<bool> Delete(ProfileUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProfileUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProfileUser> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileUser> GetByIdentityUserId(string identityUserId)
        {
            var profileUser = await _context.ProfileUser.FirstAsync(p=>p.IdentityUserId == identityUserId);

            return profileUser;
        }

        public async Task<bool> Update(ProfileUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await Save();
        }
    }
}