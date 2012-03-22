using System.Collections.Generic;

namespace RecreateMe.Friends.Invites
{
    public interface IInviteRepository
    {
        void Save(Invite invite);
        List<Invite> GetInvitesToProfile(string profileId);
        void Delete(Invite invite);
        void Delete(string inviteId);
    }
}