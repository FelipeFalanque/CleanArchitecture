using AutoMapper;
using CleanArchitecture.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Tests
{
    public abstract class BaseTest
    {
        public IMapper Mapper { get; set; }
        public BaseTest()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DomainToDTOMappingProfile());
                });

                return config.CreateMapper();
            }
            public void Dispose()
            {
            }
        }
    }
}
