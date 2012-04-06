﻿namespace RecreateMe.Divisions
{
    public interface IDivisionRepository
    {
        void Save(Division division);
        Division GetById(string divisionId);
    }
}