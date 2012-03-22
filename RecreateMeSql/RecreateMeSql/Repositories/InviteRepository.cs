using System.Collections.Generic;
using RecreateMe.Friends.Invites;

namespace RecreateMeSql.Repositories
{
    public class InviteRepository : IInviteRepository
    {
        public void Save(Invite invite)
        {
            throw new System.NotImplementedException();
        }

        public List<Invite> GetInvitesToProfile(string profileId)
        {
            return new List<Invite> {new Invite()};
        }

        public void Delete(Invite invite)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string inviteId)
        {
            throw new System.NotImplementedException();
        }
    }
}