﻿namespace RecreateMe.Organizations
{
    public interface IOrganizationRepository
    {
        void Save(Organization organization);
        Organization GetById(string organizationId);
        void AddLeagueToOrganization(Organization organization, string leagueId);
    }
}