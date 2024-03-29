# In memory cache in .net core

## Introduction
Caching is a significant way to improve performance of an application. Caching works best with data which are not changing very frequently. Caching creates a copy of data in memory of server and return from memory instead of actual source. On the basis of memory location cache is devided in to foolowing category 

	1. In memory cache
	2. Distributed cache

### 1. In memory cache
In memory cache creates a copy of data by using server memory. The simplest way to implement in memory cache is by using IMemoryCache exists in "Microsoft.Extensions.Caching.Memory" nuget package.

To use in memory cache we have to follow following steps 

A. Add in memory services to service container by using AddMemoryCache() method exists in "Microsoft.Extensions.DependencyInjection" nuget package.

	using Microsoft.Extensions.DependencyInjection;

	namespace Webapi_inmemory_cache
	{
		public class Startup
		{
			// This method gets called by the runtime. Use this method to add services to the container.
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
			public void ConfigureServices(IServiceCollection services)
			{
				services.AddMvc();
				services.AddMemoryCache();
			}
		}
	}

B. Inject IMemoryCache instance to constructor as below

	using Microsoft.Extensions.Caching.Memory;
	
    /// <summary>
    /// Memory cache variable
    /// </summary>
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// Initialize instance for <see cref="StudentController"/>
    /// </summary>
    public StudentController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

C. Use IMemoryCache instance to get or set cache objects. IMemoryCache provides a number of instance methods to handle cache operations. Some of them are below

C1. IMemoryCache.TryGetValue(Key, out type object):	TryGetValue method takes key and one out variable for cached object. It returns true if object corresponding to provided key found in cache otherwise false. For example we cached and get all student details in student controller by code below
	
	    /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<List<StudentDTO>> Get()
        {
            if (!_memoryCache.TryGetValue(ALLSTUDENTCACHEKEY, out List<StudentDTO> studentList))
            {
                studentList = await _studentService.Get();
                var memoryCacheOption = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                };
                _memoryCache.Set(ALLSTUDENTCACHEKEY, studentList, memoryCacheOption);
                SetSource(ref studentList, "From Database");
            }
            else
            {
                SetSource(ref studentList, "From Cache");
            }
            return studentList;
        }

C2. IMemoryCache.Set(Key, Object, memoryCacheEntryOption): As name indicates this method is used to cached an object corresponding to provided key as 

	    var memoryCacheOption = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        };
        _memoryCache.Set(ALLSTUDENTCACHEKEY, studentList, memoryCacheOption);

Here memoryCacheEntryOption is to indicate behaviour of cached object. In above code we set sliding expiration of 10 minutes to cached object.

C3. IMemoryCache.GetOrCreate(key, Func): As name indicates this method can be used to get or cache a object if not exists. It takes two parameters first key correspnding to which we are looking for cached object and second is logic to get data or object if not exists in cache. For example in student controller's Get(id) action

	    /// <summary>
        /// Get student by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StudentDTO> Get(int id)
        {
            return _memoryCache.GetOrCreate(STUDENTBYID, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                var student = await _studentService.Get(id);

                return student;
            });
        }

C4. IMemoryCache.GetOrCreateAsync(Key, Func): This method is exact same as GetOrCreate method as describe above. Only difference, this provide same functionality asynchronously.

### MemoryCacheEntryOption

MemoryCacheentryOption object provide us a way to describe behaviour of cached object. By this object we can set priority, size and eviction call back method chahed object.

	 var cacheEntryOptions = new MemoryCacheEntryOptions()
        // Pin to cache.
        .SetPriority(CacheItemPriority.NeverRemove)
        // Add eviction callback
        .RegisterPostEvictionCallback(callback: EvictionCallback, state: this);

		private static void EvictionCallback(object key, object value,
			EvictionReason reason, object state)
		{
			var message = $"Entry was evicted. Reason: {reason}.";
			((ControllerNameToCast)state)._cache.Set(CacheKeys.CallbackMessage, message);
		}


#### Use SetSize, Size and SizeLimit to limit cache size

We can provide size limit to cache by using provided methods and properties. Memory cache does not have unit for size so it's completly depends on developers how they handle unit for cache size, unit can be count of objects or size in bytes or something else. When we limit cache with a value in AddMemoryCache() method inside startup class then we also need to provide size for each entry to cache with same units. For example i am using bytes as unit for my cache limit

	services.AddMemoryCache(option=> option.SizeLimit = 10640);

Above code indicate cache size is limited to 10 MB. Once we set SizeLimit then its mandatory to provide size for each cache entry in same unit. All entry to cahe will be ignored if sum of cached object size exceed provided size limit. Cache entry size can be defined with following code 

	var cacheEntryOptions = new MemoryCacheEntryOptions() 
            // Set cache entry size by extension method.
            .SetSize(1) 
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromSeconds(3));

        // Set cache entry size via property.
        // cacheEntryOptions.Size = 1;

        // Save data in cache.
        _cache.Set(MyKey, cacheEntry, cacheEntryOptions);