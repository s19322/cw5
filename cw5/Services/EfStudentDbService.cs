using cw5.Controllers;
using cw5.DTOs;
using cw5.DTOs.Request;
using cw5.DTOs.Response;
using cw5.ModelsFrameWorkCore;
using Microsoft.EntityFrameworkCore;
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

            var response = new EnrollStudResponse();

            if (!_dbContext.Studies.Any(s => s.Name == request.Studies))
            {
                throw new Exception($"Brak takiego kursu {request.Studies}");
            }
            var idEnroll = 0;
            var idStudy = _dbContext.Studies.Where(e => e.Name == request.Studies).Select(e => e.IdStudy).Single();
            var maxStartDate = _dbContext.Enrollment.Where(e => e.Semester == 1 && e.IdStudy == idStudy).Select(e => e.StartDate).Single();
            
            if (!_dbContext.Enrollment.Any())
            {
                idEnroll = _dbContext.Enrollment.Where(e => e.Semester == 1 && e.IdStudy == idStudy && e.StartDate == maxStartDate).Select(e => e.IdEnrollment).Single();
            }
            else
            {
                Enrollment enroll = new Enrollment
                {
                    IdEnrollment = _dbContext.Enrollment.OrderByDescending(e => e.IdEnrollment)
                    .FirstOrDefault()
                    .IdEnrollment,
                    Semester = 1,
                    IdStudy = idStudy,
                    StartDate = DateTime.Now
                };
                _dbContext.Enrollment.Add(enroll);
                idEnroll = enroll.IdEnrollment;
            }
            if (!_dbContext.Student.Any(e => e.IndexNumber == request.IndexNumber))
            {
                Student student = new Student
                {
                    IndexNumber = request.IndexNumber,
                    IdEnrollment = idEnroll,
                    FirstName = request.FirstName,
                    LastName = request.Lastname,
                    BirthDate = request.BirthDate,

                };
                _dbContext.Student.Add(student);

            }
            _dbContext.SaveChanges();
            response = new EnrollStudResponse
            {
                Semester = 1,
                IndexNumber = request.IndexNumber,
                Studies = request.Studies
            };

            return response;
     }

        public EnrollStudResponsePr PromoteStudent(EnrollStudRequestPr request)
        {
            

            if (!_dbContext.Studies
                    .Join(
                        _dbContext.Enrollment,
                        st => st.IdStudy,
                        e => e.IdStudy,
                        (st, e) => new { Studies = st, Enrollment = e }
                    )
                    .Any(
                        x => x.Studies.Name == request.Studies && x.Enrollment.Semester == request.Semester
                    )
                )
            {

                throw new Exception($"Nie ma studenta na danych studiach i semestrze {request.Studies} {request.Semester}");
            }

            int idStudy = _dbContext.Studies.Where(e => e.Name == request.Studies).Select(e => e.IdStudy).Single();

            var oldIdEnroll = _dbContext.Enrollment.Where(e => e.Semester == request.Semester && e.IdStudy == idStudy).Select(e => e.IdEnrollment).Single();
            var newIdEnroll = 0;
            var studIndex=" ";
            if (_dbContext.Enrollment.Any(e => e.Semester == request.Semester + 1 && e.IdStudy == idStudy))
            {
                newIdEnroll = _dbContext.Enrollment.Where(e => e.Semester == request.Semester + 1 && e.IdStudy == idStudy).Select(e => e.IdEnrollment)
                .Single();
                studIndex = _dbContext.Student.Where(e => e.IdEnrollment == oldIdEnroll).Select(e => e.IndexNumber).Single();
            }

            else
            {
                Enrollment enrollID = new Enrollment
                {
                    IdEnrollment = _dbContext
                        .Enrollment
                        .OrderByDescending(x => x.IdEnrollment)
                        .FirstOrDefault()
                        .IdEnrollment,
                    Semester = request.Semester + 1,
                    IdStudy = idStudy,
                    StartDate = DateTime.Now
                };
                _dbContext.Enrollment.Add(enrollID);
                newIdEnroll = enrollID.IdEnrollment;

                _dbContext.Entry(enrollID).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            Student enrollStud = new Student
            {
                IndexNumber = studIndex,
                IdEnrollment = newIdEnroll,
            };

            _dbContext.Entry(enrollStud).Property("IdEnrollment").IsModified = true;

            _dbContext.SaveChanges();

            EnrollStudResponsePr response = new EnrollStudResponsePr()
            {
                Semester = request.Semester + 1,
                IdEnrollment = newIdEnroll
            };
            return response;
        }
        public List<Student> getAllStudentsData()
        {
            List<Student> studResp= _dbContext.Student.ToList();
            return studResp;

        }
        public void ModifyStudent(Student request)
        {
            try
            {
                var student = _dbContext.Student.Where(e=>e.IndexNumber == request.IndexNumber).Select(e=>e).Single();
                student.FirstName = request?.FirstName ?? student.FirstName;
                student.LastName = request?.LastName ?? student.LastName;
                student.BirthDate = request?.BirthDate ?? student.BirthDate;
                student.IdEnrollment = request?.IdEnrollment ?? student.IdEnrollment;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Nie mozna zmodyfikowac studenta {request.IndexNumber} {e.StackTrace} {e.Message}"
                    );
            }
        }
        public void DeleteStudent(DeleteStudReq request)
        {
            try
            {
                var student = _dbContext.Student.Where(e=>e.IndexNumber == request.IndexNumber).Select(e=>e).Single();
                _dbContext.Student.Remove(student);

                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Nie mozna usunac studenta{request.IndexNumber} {e.StackTrace} {e.Message}"
                );
            }
        }


    }
    }

