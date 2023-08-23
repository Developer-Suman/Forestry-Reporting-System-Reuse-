using Reuse.Bll.Repository.Interface;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class BlacklistServices : IBlackListServices
    {
        private readonly IRepository<BlackListToken> _repository;

        public BlacklistServices(IRepository<BlackListToken> repository)
        {
            _repository = repository;

        }
        public void Blacklist(string jwtId)
        {
            BlackListToken blackListeToken = new()
            {
                Token = jwtId
            };
            _repository.AddToken(blackListeToken);
        }

        public bool IsBlacklisted(string jwtId)
        {
            return _repository.CheckTokenIfExist(x => x.Token == jwtId);
        }
    }
}
