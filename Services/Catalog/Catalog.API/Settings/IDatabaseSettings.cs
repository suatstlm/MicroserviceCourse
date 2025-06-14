﻿namespace Catalog.API.Settings
{
    public interface IDatabaseSettings
    {
        string CourseCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

