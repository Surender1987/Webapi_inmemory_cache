# In memory cache in .net core

## Introduction
Caching is a significant way to improve performance of an application. Caching works best with data which are not changing very frequently.Caching creates a copy of data in memory of server and return from memory instead of actual source. On the basis of memory location cache is devided in to foolowing category 

	1. In memory cache
	2. Distributed cache

### 1. In memory cache
In memory cache creates a copy of data by using server memory. The simplest way to implement in memory cache is by using "Microsoft.Extensions.Caching.Memory" nuget package.  
