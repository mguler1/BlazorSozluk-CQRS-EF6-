using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.InfastructurePersistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.Repositories
{
    public class UserRepository:GenericRepository<Users>,IUserRepository
    {
        
        public UserRepository(BlazorSozlukContext dbcontext):base(dbcontext)
        {
            
        }
    }
}
