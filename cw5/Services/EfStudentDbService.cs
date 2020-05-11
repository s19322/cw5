using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Response;
using cw5.ModelsFrameWorkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public class EfStudentDbService : IStudentDbService
    {
        private readonly s19322Context _dbContext;
        public EfStudentDbService(s19322Context context)
        {
            _dbContext = context;
        }

        public EnrollStudResponse EnrollStudent(EnrollStudRequest request)
        {
            throw new NotImplementedException();
        }

        public EnrollStudResponsePr PromoteStudent(EnrollStudRequestPr request)
        {
            throw new NotImplementedException();
        }
    }
}
